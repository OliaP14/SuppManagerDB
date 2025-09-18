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

              
                 if (userInput == "1")
                {
                    ShowSuppliers();
                }
                else if (userInput == "2")
                {
                    ShowProducts();
                }
                else if (userInput == "3")
                {
                    AddSupplier();
                }
                else if (userInput == "4")
                {
                    AddProduct();
                }
                else if (userInput == "5")
                {
                    UpdateSupplierStatus();
                }
                else if (userInput == "6")
                {
                    DeleteSupplier();
                }
                else if (userInput == "7")
                {
                    DeleteProduct();
                }
                else if (userInput == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine(" Невірний вибір!");
                }

                
                Console.ReadKey();
            }
        }

        private void DeleteProduct()
        {
            throw new NotImplementedException();
        }

        private void DeleteSupplier()
        {
            throw new NotImplementedException();
        }

        private void UpdateSupplierStatus()
        {
            throw new NotImplementedException();
        }

        private void AddProduct()
        {
            throw new NotImplementedException();
        }

        private void AddSupplier()
        {
            throw new NotImplementedException();
        }

        private void ShowProducts()
        {
            throw new NotImplementedException();
        }

        private void ShowSuppliers()
        {
            throw new NotImplementedException();
        }
    }
}