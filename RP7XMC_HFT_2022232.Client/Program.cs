using ConsoleTools;
using RP7XMC_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace RP7XMC_HFT_2022232.Client
{
    internal class Program
    {
        static RestService brandrest;
        static RestService carrest;
        static RestService servicerest;
        static void Create(string entity)
        {
            if (entity == "Brand")
            {
                Console.Write("Enter Brand name: ");
                string name = Console.ReadLine();
                brandrest.Post(new Brand() { BrandName = name }, "brand");
            }
            if (entity == "Car")
            {
                Console.Write("Enter Car name: ");
                string name = Console.ReadLine();
                carrest.Post(new Car() { CarName = name }, "car");
            }
            if (entity == "Service")
            {
                Console.Write("Enter Service name: ");
                string name = Console.ReadLine();
                servicerest.Post(new Service() { ServiceName = name }, "service");
            }
            Console.ReadLine();
        }
        static void List(string entity)
        {
            if (entity == "Brand")
            {
                List<Brand> brands = brandrest.Get<Brand>("brand");
                foreach (var item in brands)
                {
                    Console.WriteLine(item.BrandName);
                }
            }
            if (entity == "Car")
            {
                List<Car> cars = carrest.Get<Car>("car");
                foreach (var item in cars)
                {
                    Console.WriteLine(item.CarName);
                }
            }
            if (entity == "Service")
            {
                List<Service> service = servicerest.Get<Service>("service");
                foreach (var item in service)
                {
                    Console.WriteLine(item.ServiceName);
                }
            }
            Console.ReadLine();
        }
        static void Update(string entity)
        {
            if (entity == "Brand")
            {
                Console.Write("Enter Brand id to update: ");
                int id = int.Parse(Console.ReadLine());
                Brand one = brandrest.Get<Brand>(id, "brand");
                Console.Write($"New name [old: {one.BrandName}]: ");
                string name = Console.ReadLine();
                one.BrandName = name;
                brandrest.Put(one, "brand");
            }
            if (entity == "Car")
            {
                Console.Write("Enter Car id to update: ");
                int id = int.Parse(Console.ReadLine());
                Car one = carrest.Get<Car>(id, "car");
                Console.Write($"New name [old: {one.CarName}]: ");
                string name = Console.ReadLine();
                one.CarName = name;
                carrest.Put(one, "car");
            }
            if (entity == "Service")
            {
                Console.Write("Enter Service id to update: ");
                int id = int.Parse(Console.ReadLine());
                Service one = servicerest.Get<Service>(id, "service");
                Console.Write($"New name [old: {one.ServiceName}]: ");
                string name = Console.ReadLine();
                one.ServiceName = name;
                servicerest.Put(one, "service");
            }
        }
        static void Delete(string entity)
        {
            if (entity == "Brand")
            {
                Console.Write("Enter Brand id to delete: ");
                int id = int.Parse(Console.ReadLine());
                brandrest.Delete(id, "brand");
            }
            if (entity == "Car")
            {
                Console.Write("Enter Car id to delete: ");
                int id = int.Parse(Console.ReadLine());
                carrest.Delete(id, "car");
            }
            if (entity == "Service")
            {
                Console.Write("Enter Service id to delete: ");
                int id = int.Parse(Console.ReadLine());
                servicerest.Delete(id, "service");
            }
        }
        static void Main(string[] args)
        {
            brandrest = new RestService("http://localhost:2810/", "brand");

            var brandSubMenu = new ConsoleMenu(args, level: 1)
            .Add("List", () => List("Brand"))
            .Add("Create", () => Create("Brand"))
            .Add("Delete", () => Delete("Brand"))
            .Add("Update", () => Update("Brand"))
            .Add("Exit", ConsoleMenu.Close);

            var carSubMenu = new ConsoleMenu(args, level: 1)
            .Add("List", () => List("Car"))
            .Add("Create", () => Create("Car"))
            .Add("Delete", () => Delete("Car"))
            .Add("Update", () => Update("Car"))
            .Add("Exit", ConsoleMenu.Close);

            var serviceSubMenu = new ConsoleMenu(args, level: 1)
            .Add("List", () => List("Service"))
            .Add("Create", () => Create("Service"))
            .Add("Delete", () => Delete("Service"))
            .Add("Update", () => Update("Service"))
            .Add("Exit", ConsoleMenu.Close);

            var menu = new ConsoleMenu(args, level: 0)
            .Add("Brands", () => brandSubMenu.Show())
            .Add("Cars", () => carSubMenu.Show())
            .Add("Services", () => serviceSubMenu.Show())
            .Add("Exit", ConsoleMenu.Close);

            menu.Show();
        }
    }
}
