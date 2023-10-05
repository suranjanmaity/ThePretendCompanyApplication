using System;
using TCPData;
using TCPExtensions;
using System.Linq;

namespace ThePretendCompanyApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Employees who are manager...");
            List<Employee> employeeList = Data.GetEmployees();
            var filteredEmployees = employeeList.Filter(emp => emp.IsManager == true);
            foreach (var employee in filteredEmployees)
            {
                Console.WriteLine($"First Name : {employee.FirstName}");
                Console.WriteLine($"Last Name : {employee.LastName}");
                Console.WriteLine($"Annual Salary : {employee.AnualSalary}");
                Console.WriteLine($"Manager : {employee.IsManager}");
                Console.WriteLine();
            }
            
            Console.WriteLine("Employees who are earning more than 50000...");
            filteredEmployees = employeeList.Filter(emp => emp.AnualSalary > 50000);
            foreach (var employee in filteredEmployees)
            {
                Console.WriteLine($"First Name : {employee.FirstName}");
                Console.WriteLine($"Last Name : {employee.LastName}");
                Console.WriteLine($"Annual Salary : {employee.AnualSalary}");
                Console.WriteLine($"Manager : {employee.IsManager}");
                Console.WriteLine();
            }

            Console.WriteLine("Finance and Human Resource Department...");
            List<Department> departmentList = Data.GetDepartments();
            var filteredDepartment = departmentList.Filter(dept => dept.ShortName=="HR" || dept.ShortName == "FN");
            foreach (var department in filteredDepartment)
            {
                Console.WriteLine($"Id : {department.Id}");
                Console.WriteLine($"Short Name : {department.ShortName}");
                Console.WriteLine($"Long Name : {department.LongName}");
                Console.WriteLine();
            }
            // filtering the data using where method of System.Linq namespace
            Console.WriteLine("Finance and Human Resource Department...");
            var whereDepartment = departmentList.Where(dept=> dept.ShortName=="HR" || dept.ShortName == "FN" );
            foreach (var department in filteredDepartment)
            {
                Console.WriteLine($"Id : {department.Id}");
                Console.WriteLine($"Short Name : {department.ShortName}");
                Console.WriteLine($"Long Name : {department.LongName}");
                Console.WriteLine();
            }
        }
    }
}