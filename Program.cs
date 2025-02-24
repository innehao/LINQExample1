using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static LinqExample_1.Program;

namespace LinqExample_1
{
    public static class EnumerableCollectionExtensionMethods
    {
        public static IEnumerable<Employee> GethighestSalariedEmployees(this IEnumerable<Employee> employees)
        {
            foreach (Employee emp in employees)
            {
                Console.WriteLine($"Accessing employee: {emp.Fname + " " + emp.Lname}");
                if (emp.AnnualSalary > 50000)
                {
                    yield return emp;
                }
            }
        }
    }
    public class Program
    {

        static void Main(string[] args)
        {
            List<Employee> employeeList = EmpData.GetEmployee();
            List<Department> departmentList = EmpData.GetDepartments();

            //Select and Where Operator - Method-syntax
            //var result = employeeList.Select(emp => new
            //{
            //    Fullname = emp.Fname + " " + emp.Lname,
            //    AnnualSalary = emp.AnnualSalary
            //}).Where(emp => emp.AnnualSalary > 50000);

            //foreach (var items in result)
            //{
            //    Console.WriteLine($"{items.Fullname,-10} ${items.AnnualSalary}");
            //}

            //Select and Where Operator - Query-syntax
            //var result = from emp in employeeList
            //             where emp.AnnualSalary > 50000
            //             select new
            //             {
            //                 Fullname = emp.Fname + " " + emp.Lname,
            //                 AnnualSalary = emp.AnnualSalary
            //             };
            //employeeList.Add(new Employee
            //{
            //    Id = 5,
            //    Fname = "Yin",
            //    Lname = "Chaychinh",
            //    isManager = true,
            //    AnnualSalary = 65000,
            //    DepartmenId = 2  
            //});

            //foreach (var items in result)
            //{
            //    Console.WriteLine($"{items.Fullname,-10} ${items.AnnualSalary}");
            //}

            //Deferred Executed Example 
            //var result = from emp in employeeList.GethighestSalariedEmployees()
            //             select new
            //             {
            //                 Fullname = emp.Fname + " " + emp.Lname,
            //                 AnnualSalary = emp.AnnualSalary
            //             };

            //employeeList.Add(new Employee
            //{
            //    Id = 5,
            //    Fname = "Yin",
            //    Lname = "Chaychinh",
            //    isManager = true,
            //    AnnualSalary = 65000,
            //    DepartmenId = 2
            //});

            //foreach (var items in result)
            //{
            //    Console.WriteLine($"{items.Fullname,-10} ${items.AnnualSalary}");
            //}

            //Immediate Executed Example
            //var result = (from emp in employeeList.GethighestSalariedEmployees()
            //              select new
            //              {
            //                  Fullname = emp.Fname + " " + emp.Lname,
            //                  AnnualSalary = emp.AnnualSalary
            //              }).ToList();

            //employeeList.Add(new Employee
            //{
            //    Id = 5,
            //    Fname = "Yin",
            //    Lname = "Chaychinh",
            //    isManager = true,
            //    AnnualSalary = 65000,
            //    DepartmenId = 2
            //});

            //foreach (var items in result)
            //{
            //    Console.WriteLine($"{items.Fullname,-10} ${items.AnnualSalary}");
            //}

            ////Jion operator Example - Method-Syntax
            //var result = departmentList.Join(employeeList,
            //             department => department.DptID,
            //             employee => employee.DepartmenId,
            //             (department,employee) => new
            //             {
            //                 FullName = employee.Fname + " " + employee.Lname,
            //                 AnnualSalary = employee.AnnualSalary,
            //                 departmentName = department.LongName
            //             }
            //    );

            //foreach (var items in result)
            //{
            //    Console.WriteLine($"{items.FullName,-10} ${items.AnnualSalary,-5} {items.departmentName}");
            //}

            ////Jion operator Example - Query-Syntax
            //var result = from dept in departmentList
            //             join emp in employeeList
            //             on dept.DptID equals emp.DepartmenId
            //             select new
            //             {
            //                 FullName = emp.Fname + " " + emp.Lname,
            //                 AnnualSalary = emp.AnnualSalary,
            //                 departmentName = dept.LongName
            //             };

            //foreach (var items in result)
            //{
            //    Console.WriteLine($"{items.FullName,-10} ${items.AnnualSalary,-5} {items.departmentName}");
            //}

            ////GroupJoin operator Example - Method-Syntax
            //var result = departmentList.GroupJoin(employeeList,
            //             dept => dept.DptID,
            //             emp => emp.DepartmenId,
            //             (department, employeesGroup) => new
            //             {
            //                 Employees = employeesGroup,
            //                 DepartmentName = department.LongName
            //             }
            //    );

            //foreach (var items in result)
            //{
            //    Console.WriteLine($"Department Name:{items.DepartmentName}");
            //    foreach (var emp in items.Employees)
            //    {
            //        Console.WriteLine($"{emp.Fname} {emp.Lname}");
            //    }
            //}

            ////GroupJoin operator Example - Query-Syntax
            var result = from dept in departmentList
                         join emp in employeeList
                         on dept.DptID equals emp.DepartmenId
                         into EmployeesGroup
                         select new
                         {
                             Employees = EmployeesGroup,
                             DepartmentName = dept.LongName
                         };

            foreach (var items in result)
            {
                Console.WriteLine($"Department Name:{items.DepartmentName}");
                foreach (var emp in items.Employees)
                {
                    Console.WriteLine($"{emp.Fname} {emp.Lname}");
                }
            }
            Console.ReadKey();
        }

        public class Employee
        {
            public int Id { get; set; }
            public string Fname { get; set; }
            public string Lname { get; set; }
            public decimal AnnualSalary { get; set; }
            public bool isManager { get; set; }
            public int DepartmenId { get; set; }
        }
        public class Department
        {
            public int DptID { get; set; }
            public string LongName { get; set; }
            public string ShortName { get; set; }
        }
        public static class EmpData
        {
            public static List<Employee> GetEmployee()
            {
                List<Employee> employees = new List<Employee>();
                Employee employee = new Employee
                {
                    Id = 1,
                    Fname = "Inn",
                    Lname = "Ehao",
                    AnnualSalary = 60000,
                    isManager = true,
                    DepartmenId = 1,
                };
                employees.Add(employee);
                employee = new Employee
                {
                    Id = 2,
                    Fname = "Ngek",
                    Lname = "Vichai",
                    AnnualSalary = 55000,
                    isManager = true,
                    DepartmenId = 2,
                };
                employees.Add(employee);
                employee = new Employee
                {
                    Id = 3,
                    Fname = "Chork",
                    Lname = "Narin",
                    AnnualSalary = 50000,
                    isManager = false,
                    DepartmenId = 1,
                };
                employees.Add(employee);
                employee = new Employee
                {
                    Id = 4,
                    Fname = "Koeus",
                    Lname = "Soknet",
                    AnnualSalary = 56000,
                    isManager = false,
                    DepartmenId = 3,
                };
                employees.Add(employee);
                return employees;
            }

            public static List<Department> GetDepartments()
            {
                List<Department> departments = new List<Department>();
                Department department = new Department
                {
                    DptID = 1,
                    ShortName = "CS",
                    LongName = "Computer Siences",
                };
                departments.Add(department);
                department = new Department
                {
                    DptID = 2,
                    ShortName = "IT",
                    LongName = "Information Technology"
                };
                departments.Add(department);
                department = new Department
                {
                    DptID = 3,
                    ShortName = "HR",
                    LongName = "Human Resources"
                };
                departments.Add(department);
                return departments;
            }
        }
    }
}
