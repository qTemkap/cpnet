using System;
using CNnet_Lab1.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.Office.Interop.Excel;

namespace OrganizationChecker
{
    public class Program
    {
        static void Main(string[] args)
        {
            var organization = new Organization(1,"Rost");

            organization.AddCustomer(0, "Customer Artem", "+34966899997", "product1", "10");
            organization.AddCustomer(2, "Customer Alex", "+34966878945", "product2", "12");
            organization.AddCustomer(-1, "Customer Yevgeny", "+34966872445", "product3", "14");
            

            foreach(var employee in organization.Customers)
            {
                Console.WriteLine(employee.ToString());
            }

            foreach (var id in organization.Customers)
            {
                bool validid = ValidationId(id);
                Console.WriteLine($"Id hass been: {validid}");
            }

            Console.WriteLine("*** Информация о домене приложения ***\n");
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var a in assemblies)
            {
                Console.WriteLine($"{a.FullName}\n");
            }

            //загружаем сбоку мтодом LoadFrom()
            Assembly asm = Assembly.LoadFrom("CNnet_Lab1.dll");

            Console.WriteLine(asm.FullName);
            // получаем все типы из сборки MyApp.dll
            Type[] types = asm.GetTypes();
            foreach (Type t in types)
            {
                Console.WriteLine($"{t.FullName}\n");
            }

            //  Получение информации о конструкторах
            var customerType = types.Single(t => t.Name.EndsWith("Customer"));

            Console.WriteLine("Конструкторы:");
            foreach (ConstructorInfo ctor in customerType.GetConstructors())
            {
                Console.Write(customerType.Name + " (");
                // получаем параметры конструктора
                ParameterInfo[] parameters = ctor.GetParameters();
                for (int i = 0; i < parameters.Length; i++)
                {
                    Console.Write(parameters[i].ParameterType.Name + " " + parameters[i].Name);
                    if (i + 1 < parameters.Length) Console.Write(", ");
                }
                Console.WriteLine(")");
            }

            
            // Используем позднее связывание
            dynamic obj = Activator.CreateInstance(customerType, new object[] { 1, "Max", "111111", "Product", "31", "otherInfo"});

            Console.WriteLine(obj.FullName);

            Console.WriteLine("Объект создан!");
            //вызов метода
            obj = "CNnet_Lab1.dll";
            obj = new Customer(2, "Denis", "+3412125797", "prod4", "2", "asdasd");
            obj.SetName("Denis Second");
            Console.WriteLine(obj.FullName);

            // Start Excel and get Application object.  
            var excel = new Microsoft.Office.Interop.Excel.Application();
            // for making Excel visible  
            excel.Visible = false;
            excel.DisplayAlerts = false;
            // Creation a new Workbook  
            var excelworkBook = excel.Workbooks.Add(Type.Missing);
            // Workk sheet  
            var excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;
            excelSheet.Name = "Test work sheet";

            excelSheet.Cells[1, 1] = obj.FullName;
            excelSheet.Cells[1, 2] = obj.Telephone;

            excelworkBook.SaveAs(@"D:\project\Lab.xlsx");

            Console.ReadLine();
        }
        public static bool ValidationId(Customer id)
        {
            Type t = typeof(Customer);
            object[] attrs = t.GetCustomAttributes(false);
            foreach (IdValidationsAttribute attr in attrs)
            {
                if (id.Id >= attr.Id) return true;
                else return false;
            }
            return true;
        }



    }
}
