using System;
using System.Data.SqlClient;

namespace SuppManagerApp
{
    public class Menu
    {
        public void ShowMenu()
        {
            Console.WriteLine("Greetings from SuppManager!");
            while (true)
            {
                Console.WriteLine("Please select an option:\n" +
                    "1. Show all suppliers.\n" +
                    "2. Show all products.\n" +
                    "3. Add Supplier.\n" +
                    "4. Add Product.\n" +
                    "5. Update Supplier Status.\n" +
                    "6. Delete Supplier.\n" +
                    "7. Delete Product.\n" +
                    "0. Exit");

                var userInput = Console.ReadLine();

                if (userInput == "0")
                {
                    break;
                }
            }
        }
    }
}
