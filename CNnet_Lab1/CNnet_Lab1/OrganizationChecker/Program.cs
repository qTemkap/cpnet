using System;
using CNnet_Lab1.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace OrganizationChecker
{
    public class Program
    {
        static void Main(string[] args)
        {
            var organization = new Organization(1,"Sevaastol");

            organization.AddCustomer(1,"Customer Artem", "+34966899997", "product1", "10");
            organization.AddCustomer(2,"Customer Alex", "+34966878945", "product2", "12");

            foreach(var employee in organization.Customers)
            {
                Console.WriteLine(employee.ToString());
            }

            Console.WriteLine("*** Информация о домене приложения ***\n");
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
          
            Console.ReadLine();
        }
 
    }
}
