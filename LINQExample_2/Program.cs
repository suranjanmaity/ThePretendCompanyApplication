using LINQExample_Northewind.Models;
using LINQExample_Northwind.Comparer;
using LINQExample_Northwind.DAL;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.ComponentModel.DataAnnotations;
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
            //SortingOperator();
            //GroupingOperator();
            //QuantifierOperator();
            FilterOpertor();
        }

        private static void FilterOpertor()
        {
            ArrayList arrayList = new ArrayList(); // using array list just for storing different type of data
            arrayList.Add(_orderDAL.GetAllOrder());
            arrayList.Add(_customerDAL.GetAllCustomer());

            var customerResult = from s in arrayList.OfType<List<Customer>>()
                                 select s;
            int count = 1;
            foreach(var item in customerResult.First()) // to get first element of enumerable of list of customer
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
            }
        }

        private static void SortingOperator()
        {
            // Sorting Operators
            // OrderBy OrderByDescending ThenBy ThenByDescending
            // Method Syntax
            var mresult = _orderDAL.GetAllOrder().Join(_customerDAL.GetAllCustomer(), o => o.CustomerID, c => c.CustomerID,
                (order, customer) => new
                {
                    OrderId = order.OrderID,
                    CustomerId = order.CustomerID,
                    CustomerName = customer.ContactName,
                    ContactTitle = customer.ContactTitle,
                    CompanyName = customer.CompanyName,
                    Price = order.Freight,
                    Address = order.ShipAddress + " " + order.ShipCity + " " + order.ShipPostalCode + " " + order.ShipCountry,
                    Phone = customer.Phone
                }).OrderBy(o => o.CustomerId).ThenBy(o => o.Price);
            int count = 0;
            foreach (var item in mresult)
            {
                Console.WriteLine($"{count}\nOrder Id: {item.OrderId,-7} Customer Id: {item.CustomerId,-7} Customer: {item.CustomerName}\t{item.ContactTitle} \nCompany Name: {item.CompanyName}\n Address: {item.Address}\n Price: {item.Price,5} Phone: {item.Phone}\n");
                count++;
            }

            // Query Syntax
            var qresult = from order in _orderDAL.GetAllOrder()
                         join customer in _customerDAL.GetAllCustomer()
                         on order.CustomerID equals customer.CustomerID
                         orderby customer.CustomerID, order.Freight descending
                         select new
                         {
                             OrderId = order.OrderID,
                             CustomerId = order.CustomerID,
                             CustomerName = customer.ContactName,
                             ContactTitle = customer.ContactTitle,
                             CompanyName = customer.CompanyName,
                             Price = order.Freight,
                             Address = order.ShipAddress + " " + order.ShipCity + " " + order.ShipPostalCode + " " + order.ShipCountry,
                             Phone = customer.Phone
                         };

            count = 0;
            foreach (var item in qresult)
            {
                Console.WriteLine($"{count}\nOrder Id: {item.OrderId,-7} Customer Id: {item.CustomerId,-7} Customer: {item.CustomerName}\t{item.ContactTitle} \nCompany Name: {item.CompanyName}\n Address: {item.Address}\n Price: {item.Price,5} Phone: {item.Phone}\n");
                count++;
            }
        }
        private static void GroupingOperator()
        {
            // Grouping Operators
            // GroupBY query syntax
            var qgroupResult = from order in _orderDAL.GetAllOrder()
                              group order by order.CustomerID;
            foreach (var ordGroup in qgroupResult)
            {
                int count = 0;
                Console.WriteLine($"Customer Id: {ordGroup.Key}");
                foreach (Order o in ordGroup)
                {
                    Console.WriteLine($"\t-{count}. Order Id: {o.OrderID,-7}\n\tOrder Date: {o.OrderDate} Required Date: {o.RequiredDate} Shiped Date: {o.ShippedDate}\n\t Address: {o.ShipAddress + " " + o.ShipRegion + " " + o.ShipCity + " " + o.ShipCountry + " " + o.ShipPostalCode}\n\t Price: {o.Freight,5}\n");
                    count++;
                }
            }

            //ToLookup Operator method syntax
            var mgroupResult = _orderDAL.GetAllOrder().ToLookup(o => o.CustomerID);
            foreach (var ordGroup in mgroupResult)
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
        private static void QuantifierOperator()
        {
            //// Quantifier Operators
            // All and Any Operators
            decimal freightCompare = 20M;
            bool isTrueAll = _orderDAL.GetAllOrder().All(o => o.Freight > freightCompare);
            if(isTrueAll)
            {
                Console.WriteLine($"All orders have freight above {freightCompare:C}");
            }
            else
            {
                Console.WriteLine($"Not all orders have freight above {freightCompare:C}");
            }

            bool isTrueAny = _orderDAL.GetAllOrder().Any(o => o.Freight > freightCompare);
            if (isTrueAny)
            {
                Console.WriteLine($"At least one order has freight above {freightCompare:C}");
            }
            else
            {
                Console.WriteLine($"Not a single order has freight above {freightCompare:C}");
            }
            // Contains Operator
            var searchOrder = from order in _orderDAL.GetAllOrder()
                              where order.Freight >= freightCompare // it will return a list
                              select order;
            if (searchOrder!=null)
            {
                bool containsOrder = _orderDAL.GetAllOrder()
                                              .Contains(searchOrder.First(), new OrderComparer()); // that's why we are using first one
                if (containsOrder)
                {
                    Console.WriteLine($"Order that has freight above than {freightCompare:C}");
                    int count = 0;
                    foreach(var o in searchOrder)
                    {
                        Console.WriteLine($"{count++}\nOrder Id: {o.OrderID}\tCustomer Id: {o.CustomerID}\tPrice: {o.Freight:C}");
                    }
                }
                else
                {
                    Console.WriteLine($"Not a single order has freight above {freightCompare:C}");
                }
            }
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
        static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _iconfiguration = builder.Build();
            _orderDAL = new OrderDAL(_iconfiguration);
            _customerDAL = new CustomerDAL(_iconfiguration);
        }
    }
}