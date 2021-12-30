using System;
using BlApi;

namespace ConsoleBl
{
    class Program
    {
        static void Main(string[] args)
        {
            IBL blaccess = BlFactory.GetBl();

            blaccess.GetCustomerList();

            Console.WriteLine("Hello World!");
        }
    }
}
