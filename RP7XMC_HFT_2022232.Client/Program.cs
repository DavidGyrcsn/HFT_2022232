using ConsoleTools;
using RP7XMC_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Channels;

namespace RP7XMC_HFT_2022232.Client
{
    internal class Program
    {
        static RestService rest;
        //static RestService brandrest;
        //static RestService carrest;
        //static RestService servicerest;
        static void Create(string entity)
        {
            if (entity == "Brand")
            {
                Console.Write("Enter Brand name: ");
                string name = Console.ReadLine();
                rest.Post(new Brand() { BrandName = name }, "brand");
                //brandrest.Post(new Brand() { BrandName = name }, "brand");
            }
            if (entity == "Car")
            {
                Console.Write("Enter Car name: ");
                string name = Console.ReadLine();
                rest.Post(new Car() { CarName = name }, "car");
            }
            if (entity == "Service")
            {
                Console.Write("Enter Service name: ");
                string name = Console.ReadLine();
                rest.Post(new Service() { ServiceName = name }, "service");
            }
            Console.ReadLine();
        }
        static void List(string entity)
        {
            if (entity == "Brand")
            {
                List<Brand> brands = rest.Get<Brand>("brand");
                foreach (var item in brands)
                {
                    Console.WriteLine(item.BrandName);
                }
            }
            if (entity == "Car")
            {
                List<Car> cars = rest.Get<Car>("car");
                foreach (var item in cars)
                {
                    Console.WriteLine(item.CarName);
                }
            }
            if (entity == "Service")
            {
                List<Service> service = rest.Get<Service>("service");
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
                Brand one = rest.Get<Brand>(id, "brand");
                Console.Write($"New name [old: {one.BrandName}]: ");
                string name = Console.ReadLine();
                one.BrandName = name;
                rest.Put(one, "brand");
            }
            if (entity == "Car")
            {
                Console.Write("Enter Car id to update: ");
                int id = int.Parse(Console.ReadLine());
                Car one = rest.Get<Car>(id, "car");
                Console.Write($"New name [old: {one.CarName}]: ");
                string name = Console.ReadLine();
                one.CarName = name;
                rest.Put(one, "car");
            }
            if (entity == "Service")
            {
                Console.Write("Enter Service id to update: ");
                int id = int.Parse(Console.ReadLine());
                Service one = rest.Get<Service>(id, "service");
                Console.Write($"New name [old: {one.ServiceName}]: ");
                string name = Console.ReadLine();
                one.ServiceName = name;
                rest.Put(one, "service");
            }
        }
        static void Delete(string entity)
        {
            if (entity == "Brand")
            {
                Console.Write("Enter Brand id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "brand");
            }
            if (entity == "Car")
            {
                Console.Write("Enter Car id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "car");
            }
            if (entity == "Service")
            {
                Console.Write("Enter Service id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "service");
            }
        }
        static void NonCrud(string entity)
        {
            if (entity == "HighestCost")
            {
                List<string> service = rest.Get<string>("/Values/HighestCost");
                foreach (var item in service)
                {
                    Console.WriteLine(item);
                }
            }
            if (entity == "LowestCost")
            {
                List<string> service = rest.Get<string>("/Values/LowestCost");
                foreach (var item in service)
                {
                    Console.WriteLine(item);
                }
            }
            if (entity == "AverageCostForAllBrands")
            {
                List<string> service = rest.Get<string>("/Values/AverageCostForAllBrands");
                foreach (var item in service)
                {
                    Console.WriteLine(item);
                }
            }
            if (entity == "MaintenanceCostUnder")
            {
                int cost = int.Parse(Console.ReadLine());
                List<string> service = rest.Get<string>("/Values/MaintenanceCostUnder/"+ cost);
                foreach (var item in service)
                {
                    Console.WriteLine(item);
                }
            }
            if (entity == "MaintenanceCostAbowe")
            {
                int cost = int.Parse(Console.ReadLine());
                List<string> service = rest.Get<string>("/Values/MaintenanceCostAbowe/" + cost);
                foreach (var item in service)
                {
                    Console.WriteLine(item);
                }
            }

            Console.ReadLine();
        }
        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:2810/", "swagger");

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

            var valueSubMenu = new ConsoleMenu(args, level: 1)
            .Add("HighestCost", () => NonCrud("HighestCost"))
            .Add("LowestCost", () => NonCrud("LowestCost"))
            .Add("AverageCostForAllBrands", () => NonCrud("AverageCostForAllBrands"))
            .Add("MaintenanceCostUnder", () => NonCrud("MaintenanceCostUnder"))
            .Add("MaintenanceCostAbowe", () => NonCrud("MaintenanceCostAbowe"))
            .Add("Exit", ConsoleMenu.Close);

            var menu = new ConsoleMenu(args, level: 0)
            .Add("Brands", () => brandSubMenu.Show())
            .Add("Cars", () => carSubMenu.Show())
            .Add("Services", () => serviceSubMenu.Show())
            .Add("Value",() => valueSubMenu.Show())
            .Add("Exit", ConsoleMenu.Close);
           

            menu.Show();          
        }
    }
}
