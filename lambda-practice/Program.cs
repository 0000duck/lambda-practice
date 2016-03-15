using lamda_practice.Data;
using System;
using System.Globalization;
using System.Linq;

namespace lambda_practice
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var ctx = new DatabaseContext())
            {
                //1. Listar todos los empleados cuyo departamento tenga una sede en Chihuahua
                Console.WriteLine("==========================================================================");
                Console.WriteLine("");
                Console.WriteLine("Query 1");
                var employees = ctx.Employees
                        .Where(employ => employ.City.Name == "Chihuahua");

                foreach (var empleado in employees)
                {
                    Console.WriteLine(empleado.FirstName + " " + empleado.LastName);
                }
                Console.WriteLine("");
                //2. Listar todos los departamentos y el numero de empleados que pertenezcan a cada departamento.
                Console.WriteLine("==========================================================================");
                Console.WriteLine("");
                Console.WriteLine("Query 2");
                var deparments = ctx.Employees
                    .GroupBy(department => department.Department.Name)
                    .Select(number => new
                    {
                        employee = number.Key, employee2 = number.Count()
                    });

                foreach (var departamento in deparments)
                {
                    Console.WriteLine("Departamento: {0}, Empleados: {1}", departamento.employee, departamento.employee2);
                }
                Console.WriteLine("");
                //3. Listar todos los empleados remotos. Estos son los empleados cuya ciudad no se encuentre entre las sedes de su departamento.
                Console.WriteLine("==========================================================================");
                Console.WriteLine("");
                Console.WriteLine("Query 3");
               
                var remote = ctx.Employees
                    .Where(employee => employee.Department.Cities.Any(city => city.Name == employee.City.Name));

                foreach (var remoto in remote)
                {
                    Console.WriteLine(remoto.FirstName + " " + remoto.LastName);
                }
                Console.WriteLine("");
                //4. Listar todos los empleados cuyo aniversario de contratación sea el próximo mes.
                Console.WriteLine("==========================================================================");
                Console.WriteLine("");
                Console.WriteLine("Query 4");
                
                var anniversary = ctx.Employees
                    .Where(employee => employee.HireDate.Month == System.DateTime.Now.Month + 1);

                foreach (var aniversario in anniversary)
                {
                    Console.WriteLine(aniversario.FirstName + " " + aniversario.LastName);
                }

                //Listar los 12 meses del año y el numero de empleados contratados por cada mes.


            }


            Console.Read();
        }
    }
}
