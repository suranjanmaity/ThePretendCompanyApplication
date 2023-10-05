using System;
using System.Linq;
using TCPData;

namespace LINQExample_2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employeeList = Data.GetEmployees();
            List<Department> departmentList = Data.GetDepartments();

            var result = employeeList.Join(departmentList,e=>e.DepartmentId,d=>d.Id,
                (emp,dept) => new
                {
                    Id = emp.Id,
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    AnnualSalary = emp.AnualSalary,
                    DepartmentId = emp.DepartmentId,
                    DepartmentName = dept.LongName
                }).OrderBy(e=>e.DepartmentId);
            foreach(var item in result)
            {
                Console.WriteLine($"Id: {item.Id,-5} First Name: {item.FirstName,-10} Last Name: {item.LastName,-10} Annual Salary: {item.AnnualSalary,8}\tDepartment Name: {item.DepartmentName}");
            }
        }
    }
}