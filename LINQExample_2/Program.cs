using LINQExample_Northewind.Models;
using LINQExample_Northwind.DAL;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace LINQExample_2
{
    class Program
    {
        private static IConfiguration _iconfiguration;
        static void Main(string[] args)
        {
            GetAppSettingsFile();
            ShowAdventureDepartments();
        }
        static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _iconfiguration = builder.Build();
        }
        static void ShowAdventureDepartments()
        {
            var custDAL = new CustomerDAL(_iconfiguration);
            var lstCustomer = custDAL.GetAllCustomer();
            int count = 1;
            lstCustomer.ForEach(item =>
            {
                Console.WriteLine(
                    $"- - - - - - - - - - - - -   {count}    - - - - - - - - - - - - - -\n" +
                    $"ID: {item.CustomerID,-7}" +
                    $" Name: {item.ContactName,-20}" +
                    $" Title: {item.ContactTitle,-20}" +
                    $" Company: {item.CompanyName}\n" +
                    $" Address: {item.Address}\n" +
                    $" City: {item.City,-20}" +
                    $" Country: {item.Country,-15}" +
                    $" Postal code: {item.PostalCode,-15}" +
                    $" Phone: {item.Phone,-15}" +
                    $" Fax: {item.Fax} \n" 
                    );
                count++;
            });
            var orderDAL = new OrderDAL(_iconfiguration);
            var lstOrder = orderDAL.GetAllOrder(10);
            count = 1;
            lstOrder.ForEach(item =>
            {
                Console.WriteLine(
                    $"- - - - - - - - - - - - -   {count}    - - - - - - - - - - - - - -\n" +
                    $"OrderID: {item.OrderID}\t"+
                    $"CustomerID: {item.CustomerID}\t"+
                    $"EmployeeID: {item.EmployeeID}\n"+
                    $"OrderDate: {item.OrderDate}\t"+
                    $"RequiredDate: {item.RequiredDate}\t"+
                    $"ShippedDate: {item.ShippedDate}\n"+
                    $"ShipVia: {item.ShipVia}\t"+
                    $"Freight: {item.Freight}\t"+
                    $"ShipName: {item.ShipName}\n"+
                    $"ShipAddress: {item.ShipAddress}\n"+
                    $"ShipCity: {item.ShipCity}\t"+
                    $"ShipRegion: {item.ShipRegion}\t"+
                    $"ShipPostalCode: {item.ShipPostalCode}\t"+
                    $"ShipCountry: {item.ShipCountry}\n"
                    );
                count++;
            });
        }
    }
}