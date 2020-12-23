using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Task2Web
{
    class Program
    {
        static void Main(string[] args)
        {
            WebClient client = new WebClient();
            Stream stream = client.OpenRead("http://localhost:8000/");
            StreamReader sr = new StreamReader(stream);
            string newLine;
            while ((newLine = sr.ReadLine()) != null)
                Console.WriteLine(newLine);

            Console.WriteLine("Запрос выполнен");
            Console.Read();
        }
    }
}
