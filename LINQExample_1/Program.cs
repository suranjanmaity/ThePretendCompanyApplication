using System;
using TCPData;

namespace LINQExample_1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employeeList = Data.GetEmployees();
            List<Department> departmentList = Data.GetDepartments();
            //Select and Where operators - Method Syntax
            // using Select Linq operator
            //var result = employeeList.Select(e => new
            //{
            //    FullName = e.FirstName + " " + e.LastName,
            //    AnnualSalary = e.AnualSalary
            //}).Where(e=>e.AnnualSalary >= 50000) ;
            //foreach (var res in result)
            //{
            //    Console.WriteLine($"Full Name : {res.FullName}");
            //    Console.WriteLine($"Annual Salary : {res.AnnualSalary}");
            //}

            //Select and Where operators - Querry Syntax
            var result = from emp in employeeList
                         select new
                         {
                             FullName = emp.FirstName + " " + emp.LastName,
                             AnnualSalary = emp.AnualSalary
                         };
            foreach (var res in result)
            {
                Console.WriteLine($"{res.FullName,-20} {res.AnnualSalary,10}");
            }
        }
    }
}