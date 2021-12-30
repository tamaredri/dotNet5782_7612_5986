using System;
using DalApi;
namespace Console_Dal
{
    class Program
    {
        static void Main(string[] args)
        {
            IDal dalAccess = DalFactory.GetDal();
            Console.WriteLine("Hello World!");
        }
    }
}
