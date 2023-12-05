let cars = [];
let connection = null;


getcardata();
setupSignalR();

let CarToUpdate = -1;

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:2810/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("CarCreated", (user, message) => {
        getcardata();
    });

    connection.on("CarDeleted", (user, message) => {
        getcardata();
    });

    connection.on("CarUpdated", (user, message) => {
        getcardata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();


}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getcardata() {
    await fetch('http://localhost:2810/Car')
        .then(x => x.json())
        .then(y => {
            cars = y;
            //console.log(Cars);
            displayCar();
        });
}

function displayCar() {
    document.getElementById('resultarea').innerHTML = "";
    cars.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.carId + "</td><td>"
            + t.carName + "</td><td>" +
            `<button type="button" onclick="removeCar(${t.carId})">Delete</button>` +
            `<button type="button" onclick="ShowCarUpdate(${t.carId})">Update</button>`
            + "</td></tr>";
    });
}

function removeCar(id) {
    fetch('http://localhost:2810/Car/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getcardata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function createCar() {
    let name = document.getElementById('carName').value;
    fetch('http://localhost:2810/Car', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { carName: name })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getcardata();
        })
        .catch((error) => { console.error('Error:', error); });

}
function ShowCarUpdate(id) {
    document.getElementById('CarToUpdate').value = cars.find(t => t['carId'] == id)['carName'];
    document.getElementById('updateformdiv');//.style.display = 'flex';
    CarIdToUpdate = id;
}

function UpdateCar() {
    document.getElementById('updateformdiv');//.style.display = 'none';
        let name = document.getElementById('CarToUpdate').value;
        fetch('http://localhost:2810/Car', {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json', },
            body: JSON.stringify(
                { carName: name, carId: CarIdToUpdate })
        })
            .then(response => response)
            .then(data => {
                console.log('Success:', data);
                getcardata();
            })
            .catch((error) => { console.error('Error:', error); });
}