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
            //var result = from emp in employeeList
            //             where emp.AnualSalary >= 50000
            //             select new
            //             {
            //                 FullName = emp.FirstName + " " + emp.LastName,
            //                 AnnualSalary = emp.AnualSalary
            //             };
            //foreach (var res in result)
            //{
            //    Console.WriteLine($"{res.FullName,-20} {res.AnnualSalary,10}");
            //}

            // Differed Execution Example
            /*
             * Differed execution revaluates on each execution which is know as  Lazy Evaluation 
             */
            //var result = from emp in employeeList.GetHighSalariedEmployees()
            //             select new
            //             {
            //                 FullName = emp.FirstName + " " + emp.LastName,
            //                 AnnualSalary = emp.AnualSalary
            //             };
            //employeeList.Add(new Employee
            //{
            //    Id = 5,
            //    FirstName = "Surj",
            //    LastName = "Maity",
            //    AnualSalary = 100000.20m,
            //    IsManager = true,
            //    DepartmentId = 2,
            //});
            //foreach (var res in result)
            //{
            //    Console.WriteLine($"{res.FullName,-20} {res.AnnualSalary,10}");
            //}

            // Immediate Execution Example
            /*
             * Immediate execution evaluates on single execution while beign declared which is you can say Early Evaluation 
             */
            //var result = (from emp in employeeList.GetHighSalariedEmployees()
            //             select new
            //             {
            //                 FullName = emp.FirstName + " " + emp.LastName,
            //                 AnnualSalary = emp.AnualSalary
            //             }).ToList();
            /* immediate execution means the new employee will not be added to employeelist before the execution of result */
            //employeeList.Add(new Employee
            //{
            //    Id = 5,
            //    FirstName = "Surj",
            //    LastName = "Maity",
            //    AnualSalary = 100000.20m,
            //    IsManager = true,
            //    DepartmentId = 2,
            //});
            //foreach (var res in result)
            //{
            //    Console.WriteLine($"{res.FullName,-20} {res.AnnualSalary,10}");
            //}

            //// Join Operation Example - Method Syntax
            //var result = departmentList.Join(employeeList,
            //        department => department.Id,
            //        employee => employee.DepartmentId,
            //        (department, employee) => new {
            //            FullName = employee.FirstName + " " + employee.LastName,
            //            AnnualSalary = employee.AnualSalary,
            //            DeparmentName = department.LongName
            //        }
            //    );
            //foreach (var res in result)
            //{
            //    Console.WriteLine($"{res.FullName,-20} {res.AnnualSalary,10}\t{res.DeparmentName}");
            //}

            //// Join Operation Example - Query Syntax
            var result = from employee in employeeList
                         join department in departmentList
                         on employee.DepartmentId equals department.Id
                         select new
                         {
                             FullName = employee.FirstName + " " + employee.LastName,
                             AnnualSalary = employee.AnualSalary,
                             DeparmentName = department.LongName
                         };
            foreach (var res in result)
            {
                Console.WriteLine($"{res.FullName,-20} {res.AnnualSalary,10}\t{res.DeparmentName}");
            }
        }
    }
    public static class EnumrableCollecctionExtensionMethods
    {
        // Extension method
        public static IEnumerable<Employee> GetHighSalariedEmployees(this IEnumerable<Employee> employees)
        {
            foreach (Employee employee in employees)
            {
                Console.WriteLine($"Accessing employee : {employee.FirstName + " " + employee.LastName}");
                if (employee.AnualSalary>=50000)
                {
                    yield return employee;
                    /* you can use the yield return keyword to return each element one at a time,
                     * the sequence returned from the iterator method can be consumed by using foreach statement or linq query
                     * each iteration of the for each loop calls the iterator mehtod when a yield return statement is reached in the iterator method the relevent value in the IEnumerable collection is returned and the current location in code is retained,
                     * execution is restarted from that location when the iterator function is called.
                    */
                }
            }
        }
    }
}