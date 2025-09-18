using SuppManagerApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuppManagerDB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var menu = new Menu();
            menu.ShowMenu();    

            Console.ReadLine();
        }
        
    }
}
