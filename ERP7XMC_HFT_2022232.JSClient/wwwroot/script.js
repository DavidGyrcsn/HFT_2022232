let cars = [];
let brands = [];
let services = [];

let connection = null;


getcardata();
getbranddata();
getservicedata();
setupSignalR();

let CarToUpdate = -1;
let BrandToUpdate = -1;
let ServiceToUpdate = -1;

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
    connection.on("BrandCreated", (user, message) => {
        getcardata();
    });

    connection.on("BrandDeleted", (user, message) => {
        getcardata();
    });

    connection.on("BrandUpdated", (user, message) => {
        getcardata();
    });
    connection.on("ServiceCreated", (user, message) => {
        getcardata();
    });

    connection.on("ServiceDeleted", (user, message) => {
        getcardata();
    });

    connection.on("ServiceUpdated", (user, message) => {
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
async function getbranddata() {
    await fetch('http://localhost:2810/Brand')
        .then(x => x.json())
        .then(y => {
            brands = y;
            //console.log(brands);
            displayBrand();
        });
}
async function getservicedata() {
    await fetch('http://localhost:2810/Service')
        .then(x => x.json())
        .then(y => {
            services = y;
            //console.log(Services);
            displayService();
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

function displayBrand() {
    document.getElementById('resultarea2').innerHTML = "";
    brands.forEach(t => {
        document.getElementById('resultarea2').innerHTML +=
            "<tr><td>" + t. brandId + "</td><td>"
            + t.brandName + "</td><td>" +
        `<button type="button" onclick="removeBrand(${t. brandId})">Delete</button>` +
        `<button type="button" onclick="ShowBrandUpdate(${t. brandId})">Update</button>`
            + "</td></tr>";
    });
}
function displayService() {
    document.getElementById('resultarea3').innerHTML = "";
    services.forEach(t => {
        document.getElementById('resultarea3').innerHTML +=
            "<tr><td>" + t.serviceId + "</td><td>"
        + t.serviceName + "</td><td>" +
        `<button type="button" onclick="removeService(${t.serviceId})">Delete</button>` +
        `<button type="button" onclick="ShowServiceUpdate(${t.serviceId})">Update</button>`
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
function removeBrand(id) {
    fetch('http://localhost:2810/Brand/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getbranddata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function removeService(id) {
    fetch('http://localhost:2810/Service/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getservicedata();
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
function createBrand() {
    let name = document.getElementById('brandName').value;
    fetch('http://localhost:2810/Brand', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { brandName: name })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getbranddata();
        })
        .catch((error) => { console.error('Error:', error); });

}
function createService() {
    let name = document.getElementById('serviceName').value;
    fetch('http://localhost:2810/Service', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { serviceName: name })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getservicedata();
        })
        .catch((error) => { console.error('Error:', error); });

}
function ShowCarUpdate(id) {
    document.getElementById('CarToUpdate').value = cars.find(t => t['carId'] == id)['carName'];
    document.getElementById('updatecarformdiv').style.display = 'flex';
    CarIdToUpdate = id;
}
function ShowBrandUpdate(id) {
    document.getElementById('BrandToUpdate').value = brands.find(t => t['brandId'] == id)['brandName'];
    document.getElementById('updatebrandformdiv').style.display = 'flex';
    BrandIdToUpdate = id;
}
function ShowServiceUpdate(id) {
    document.getElementById('ServiceToUpdate').value = services.find(t => t['serviceId'] == id)['serviceName'];
    document.getElementById('updateserviceformdiv').style.display = 'flex';
    ServiceIdToUpdate = id;
}

function UpdateCar() {
    document.getElementById('updatecarformdiv').style.display = 'none';
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
function UpdateBrand() {
    document.getElementById('updatebrandformdiv').style.display = 'none';
    let name = document.getElementById('BrandToUpdate').value;
    fetch('http://localhost:2810/Brand', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { brandName: name, brandId: BrandIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getbranddata();
        })
        .catch((error) => { console.error('Error:', error); });
}
function UpdateService() {
    document.getElementById('updateserviceformdiv').style.display = 'none';
    let name = document.getElementById('ServiceToUpdate').value;
    fetch('http://localhost:2810/Service', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { serviceName: name, serviceId: ServiceIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getservicedata();
        })
        .catch((error) => { console.error('Error:', error); });
}
async function HighestCost() {
    let cost;
    await fetch('http://localhost:2810/Values/HighestCost')
        .then(x => x.json())
        .then(y => cost = y);
    alert("HighestCost: " + cost);
}
async function LowestCost() {
    let cost;
    await fetch('http://localhost:2810/Values/LowestCost')
        .then(x => x.json())
        .then(y => cost = y);
    alert("LowestCost: " + cost);
}
async function AverageCostForAllBrands() {
    let cost;
    await fetch('http://localhost:2810/Values/AverageCostForAllBrands')
        .then(x => x.json())
        .then(y => cost = y);
    alert("AverageCostForAllBrands: " + cost);
}

async function AlphabeticOrder() {
    let cost;
    await fetch('http://localhost:2810/Values/AlphabeticOrder')
        .then(x => x.json())
        .then(y => cost = y);
    alert("AlphabeticOrder: " + cost);
}

async function MaintenanceCostUnder() {
    let cost = prompt("MaintenanceCostUnder type in a number");
    let cost2;
    await fetch('http://localhost:2810/Values/MaintenanceCostUnder/'+cost)
        .then(x => x.json())
        .then(y => cost2 = y);
    alert("MaintenanceCostUnder"+ cost + ": " + cost2);
}