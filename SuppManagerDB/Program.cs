using System;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using SuppManagerDB.DAL;           
using SuppManagerDB.DTO;

namespace SuppManagerApp
{
    internal class Program
    {
        // Connection to the Database
        static readonly string ConnectionString = "Data Source = DESKTOP-4H7JQK1\\SQLEXPRESS; Initial Catalog = SuppManagerDB;Integrated Security = True; TrustServerCertificate = True";  

        static void Main(string[] args)
        {
            Console.WriteLine("Greetings from SuppManager!");

            while (true)
            {
                Console.WriteLine("\nPlease select an option:\n" +
                    "1. Show all suppliers.\n" +
                    "2. Show all products.\n" +
                    "3. Add Supplier.\n" +
                    "4. Add Product.\n" +
                    "5. Update Supplier Status.\n" +
                    "6. Delete Supplier.\n" +
                    "7. Delete Product.\n" +
                    "8. Add Characteristic to Product.\n" +
                    "9. Show all characteristics of a Product.\n" +
                    "10. Delete Characteristic from Product.\n" +
                    "11. Add Manufacturer.\n" +
                    "12. Show all Manufacturers.\n" +
                    "13. Delete Manufacturer.\n" +
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
                else if (userInput == "8")
                {
                    AddCharacteristic();
                }
                else if (userInput == "9")
                {
                    ShowCharacteristics();
                }
                else if (userInput == "10")
                {
                    DeleteCharacteristic();
                }
                else if (userInput == "11")
                {
                    AddManufacturer();
                }
                else if (userInput == "12")
                {
                    ShowManufacturers();
                }
                else if (userInput == "13")
                {
                    DeleteManufacturer();
                }
                else if (userInput == "0")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Incorrect choice!");
                }


            }
            
        }

        private static void DeleteManufacturer()
        {
            throw new NotImplementedException();
        }

        private static void ShowManufacturers()
        {
            throw new NotImplementedException();
        }

        private static void AddManufacturer()
        {
            throw new NotImplementedException();
        }

        private static void DeleteCharacteristic()
        {
            throw new NotImplementedException();
        }

        private static void ShowCharacteristics()
        {
            throw new NotImplementedException();
        }

        private static void AddCharacteristic()
        {
            throw new NotImplementedException();
        }

        private static void DeleteProduct()
        {
            throw new NotImplementedException();
        }

        private static void DeleteSupplier()
        {
            throw new NotImplementedException();
        }

        private static void UpdateSupplierStatus()
        {
            throw new NotImplementedException();
        }

        private static void AddProduct()
        {
            throw new NotImplementedException();
        }

        private static void AddSupplier()
        {
            throw new NotImplementedException();
        }

        private static void ShowProducts()
        {
            throw new NotImplementedException();
        }

        private static void ShowSuppliers()
        {
            SupplierDal dal = new SupplierDal();
            List<Supplier> suppliers = dal.GetAll();

            if (suppliers.Count == 0)
            {
                Console.WriteLine("No suppliers found.");
                return;
            }

            foreach (var s in suppliers)
            {
                Console.WriteLine($"ID: {s.SupplierID}, Name: {s.Name}, Info: {s.Info}, Location: {s.Location}, Status: {s.Status}");
            }
        }

    }
}
    
      

 
