using LINQExample_Northewind.Models;
using LINQExample_Northwind.DAL;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace LINQExample_2
{
    class Program
    {
        private static IConfiguration _iconfiguration;
        private static OrderDAL _orderDAL;
        private static CustomerDAL _customerDAL;
        static void Main(string[] args)
        {
            GetAppSettingsFile();
            //ShowOrderAndCustomer();
            ShowOrderAndCustomerOrderBy();
        }

        private static void ShowOrderAndCustomerOrderBy()
        {
            //// Sorting Operators
            //// OrderBy OrderByDescending ThenBy ThenByDescending
            //// Method Syntax
            //var result = _orderDAL.GetAllOrder().Join(_customerDAL.GetAllCustomer(), o => o.CustomerID, c => c.CustomerID,
            //    (order, customer) => new
            //    {
            //        OrderId = order.OrderID,
            //        CustomerId = order.CustomerID,
            //        CustomerName = customer.ContactName,
            //        ContactTitle = customer.ContactTitle,
            //        CompanyName = customer.CompanyName,
            //        Price = order.Freight,
            //        Address = order.ShipAddress + " " + order.ShipCity + " " + order.ShipPostalCode + " " + order.ShipCountry,
            //        Phone = customer.Phone
            //    }).OrderBy(o=>o.CustomerId).ThenBy(o=>o.Price);

            //// Query Syntax
            //var result = from order in _orderDAL.GetAllOrder()
            //             join customer in _customerDAL.GetAllCustomer()
            //             on order.CustomerID equals customer.CustomerID
            //             orderby customer.CustomerID, order.Freight descending
            //             select new
            //             {
            //                 OrderId = order.OrderID,
            //                 CustomerId = order.CustomerID,
            //                 CustomerName = customer.ContactName,
            //                 ContactTitle = customer.ContactTitle,
            //                 CompanyName = customer.CompanyName,
            //                 Price = order.Freight,
            //                 Address = order.ShipAddress + " " + order.ShipCity + " " + order.ShipPostalCode + " " + order.ShipCountry,
            //                 Phone = customer.Phone
            //             };

            //int count = 0;
            //foreach (var item in result)
            //{
            //    Console.WriteLine($"{count}\nOrder Id: {item.OrderId,-7} Customer Id: {item.CustomerId,-7} Customer: {item.CustomerName}\t{item.ContactTitle} \nCompany Name: {item.CompanyName}\n Address: {item.Address}\n Price: {item.Price,5} Phone: {item.Phone}\n");
            //    count++;
            //}

            //// Grouping Operators
            //// GroupBY query syntax
            //var groupResult = from order in _orderDAL.GetAllOrder()
            //                  group order by order.CustomerID;
            
            ////ToLookup Operator method syntax
            var groupResult = _orderDAL.GetAllOrder().ToLookup(o => o.CustomerID);
            foreach (var ordGroup in groupResult)
            {
                int count = 0;
                Console.WriteLine($"Customer Id: {ordGroup.Key}");
                foreach (Order o in ordGroup)
                {
                    Console.WriteLine($"\t-{count}. Order Id: {o.OrderID,-7}\n\tOrder Date: {o.OrderDate} Required Date: {o.RequiredDate} Shiped Date: {o.ShippedDate}\n\t Address: {o.ShipAddress + " " + o.ShipRegion + " " + o.ShipCity + " " + o.ShipCountry + " " + o.ShipPostalCode}\n\t Price: {o.Freight,5}\n");
                    count++;
                }
            }

        }

        static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _iconfiguration = builder.Build();
            _orderDAL = new OrderDAL(_iconfiguration);
            _customerDAL = new CustomerDAL(_iconfiguration);
        }
        static void ShowOrderAndCustomer()
        {
            var customerDAL = new CustomerDAL(_iconfiguration);
            var lstCustomer = customerDAL.GetAllCustomer();
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