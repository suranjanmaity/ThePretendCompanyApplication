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
            // using Select Linq operator
            var result = employeeList.Select(e => new
            {
                FullName = e.FirstName + " " + e.LastName,
                AnnualSalary = e.AnualSalary
            });
            foreach (var res in result)
            {
                Console.WriteLine($"Full Name : {res.FullName}");
                Console.WriteLine($"Annual Salary : {res.AnnualSalary}");
            }
        }
    }
}