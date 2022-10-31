using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HOST
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("введите логин");
            string login = Console.ReadLine();
            if (login == "HOST")
            {
                using (var host = new ServiceHost(typeof(Test.TestService)))
                {
                    host.Open();
                    Console.WriteLine("Хост стартовал");
                    Console.ReadKey();
                };
            }
        }
    }
}
