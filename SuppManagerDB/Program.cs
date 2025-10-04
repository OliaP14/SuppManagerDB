using SuppManagerDB.DAL.Concrete;
using SuppManagerDB.DTO;

namespace SuppManagerApp
{
    internal class Program
    {
        // Connection to the Database
        static readonly string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SuppManagerDB;Integrated Security=True;Encrypt=True";  

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
                    "14. Show all users.\n" +
                    "15. Add User.\n" +
                    "16. Update User.\n" +
                    "17. Delete User.\n" +
                    "18.Show all categories.\n" +
                    "19. Add Category.\n" +
                    "20. Delete Category.\n" +
                    "21. Update Category.\n" +
                    "22. Show all contracts.\n" +
                    "23. Add Contract.\n" +
                    "24. Delete Contract.\n" +
                    "25. Update Contract.\n" +
                    "26. Update Product.\n" +
                    "27. Update Caracteristic.\n" +
                    "28. Update Manufacturer.\n" +
                    "0. Exit");

                var userInput = Console.ReadLine();

                if (userInput == "1")
                {
                    ShowAllSuppliers();
                }
                else if (userInput == "2")
                {
                    ShowAllProducts();
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
                    ShowAllCharacteristics();
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
                    ShowAllManufacturers();
                }
                else if (userInput == "13")
                {
                    DeleteManufacturer();
                }
                else if (userInput == "14")
                {
                    ShowAllUsers();
                }
                else if (userInput == "15")
                {
                    AddUser();
                }
                else if (userInput == "16")
                {
                    UpdateUser(); 
                }
                else if (userInput == "17")
                {
                    DeleteUser();    
                }
                else if (userInput == "18")
                {
                    ShowAllCategories();                   
                }
                else if (userInput == "19")
                {
                    AddCategory();                    
                }
                else if (userInput == "20")
                {
                    DeleteCategory();                    
                }
                else if (userInput == "21")
                {
                    UpdateCategory();                    
                }
                else if (userInput == "22")
                {
                    ShowAllContracts();                    
                }
                else if (userInput == "23")
                {
                    AddContract();                    
                }
                else if (userInput == "24")
                {
                    DeleteContract();                    
                }
                else if (userInput == "25")
                {
                    UpdateContract();                    
                }
                else if (userInput == "26")
                {
                    UpdateProduct();
                }
                else if (userInput == "27")
                {
                    UpdateCaracteristic();
                }
                else if (userInput== "28")
                {
                    UpdateManufacturer();
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

        private static void UpdateManufacturer()
        {
            var dal = new ManufacturerDal();

            Console.Write("Enter Manufacturer ID to update: ");
            int manufacturerId = int.Parse(Console.ReadLine() ?? "0");

            var manufacturer = dal.GetById(manufacturerId);
            if (manufacturer == null)
            {
                Console.WriteLine("Manufacturer not found!");
                return;
            }

            Console.Write($"Enter new Name (current: {manufacturer.Name}): ");
            manufacturer.Name = Console.ReadLine() ?? manufacturer.Name;

            Console.Write($"Enter new Country (current: {manufacturer.Country}): ");
            manufacturer.Country = Console.ReadLine() ?? manufacturer.Country;

            Console.Write($"Enter new Website (current: {manufacturer.Website}): ");
            manufacturer.Website = Console.ReadLine() ?? manufacturer.Website;


            bool updated = dal.Update(manufacturer);
            Console.WriteLine(updated ? "Manufacturer updated successfully." : "Update failed.");
        }

        private static void UpdateCaracteristic()
        {
            var dal = new CharacteristicDal();

            Console.Write("Enter Characteristic ID to update: ");
            int characteristicId = int.Parse(Console.ReadLine() ?? "0");

            var characteristic = dal.GetById(characteristicId);
            if (characteristic == null)
            {
                Console.WriteLine("Characteristic not found!");
                return;
            }

            Console.Write($"Enter new Power (current: {characteristic.Power}): ");
            string powerInput = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(powerInput))
            {
                characteristic.Power = float.Parse(powerInput);
            }

            Console.Write($"Enter new Material (current: {characteristic.Material}): ");
            characteristic.Material = Console.ReadLine() ?? characteristic.Material;

            Console.Write($"Enter new Warranty (current: {characteristic.Warranty}): ");
            characteristic.Warranty = Console.ReadLine() ?? characteristic.Warranty;


            Console.Write($"Enter new Release Year (current: {characteristic.ReleaseYear}): ");
            characteristic.ReleaseYear = int.Parse(Console.ReadLine() ?? characteristic.ReleaseYear.ToString());

            Console.Write($"Enter new Product ID (current: {characteristic.ProductID}): ");
            characteristic.ProductID = int.Parse(Console.ReadLine() ?? characteristic.ProductID.ToString());

            bool updated = dal.Update(characteristic);
            Console.WriteLine(updated ? "Characteristic updated successfully." : "Update failed.");
        }

        private static void UpdateProduct()
        {
            var dal = new ProductDal();

            Console.Write("Enter Product ID to update: ");
            int productId = int.Parse(Console.ReadLine() ?? "0");

            var product = dal.GetById(productId);
            if (product == null)
            {
                Console.WriteLine("Product not found!");
                return;
            }

            Console.Write($"Enter new Name (current: {product.Name}): ");
            string name = Console.ReadLine() ?? product.Name;

            Console.Write($"Enter new Model (current: {product.Model}): ");
            string model = Console.ReadLine() ?? product.Model;

            Console.Write($"Enter new Number (current: {product.Number}): ");
            string number = Console.ReadLine() ?? product.Number;

            Console.Write($"Enter new Price (current: {product.Price}): ");
            decimal price = decimal.Parse(Console.ReadLine() ?? product.Price.ToString());

            Console.Write($"Enter new SupplierID (current: {product.SupplierID}): ");
            int supplierId = int.Parse(Console.ReadLine() ?? product.SupplierID.ToString());

            Console.Write($"Enter new CategoryID (current: {product.CategoryID}): ");
            int categoryId = int.Parse(Console.ReadLine() ?? product.CategoryID.ToString());

            Console.Write($"Enter new ManufacturerID (current: {product.ManufacturerID}): ");
            int manufacturerId = int.Parse(Console.ReadLine() ?? product.ManufacturerID.ToString());

            product.Name = name;
            product.Model = model;
            product.Number = number;
            product.Price = price;
            product.SupplierID = supplierId;
            product.CategoryID = categoryId;
            product.ManufacturerID = manufacturerId;

            bool updated = dal.Update(product);
            Console.WriteLine(updated ? "Product updated successfully." : "Update failed.");
        }

        private static void UpdateContract()
        {
            var dal = new ContractDal();

            Console.Write("Enter Contract ID to update: ");
            int contractId = int.Parse(Console.ReadLine() ?? "0");

            var contract = dal.GetById(contractId);
            if (contract == null)
            {
                Console.WriteLine("Contract not found!");
                return;
            }

            Console.Write($"Enter new Supplier ID (current: {contract.SupplierID}): ");
            contract.SupplierID = int.Parse(Console.ReadLine() ?? contract.SupplierID.ToString());

            Console.Write($"Enter new Contract Number (current: {contract.ContractNumber}): ");
            contract.ContractNumber = Console.ReadLine() ?? contract.ContractNumber;

            Console.Write($"Enter new Start Date (yyyy-mm-dd) (current: {contract.StartDate:d}): ");
            contract.StartDate = DateTime.Parse(Console.ReadLine() ?? contract.StartDate.ToString());

            Console.Write($"Enter new End Date (yyyy-mm-dd) (current: {contract.EndDate:d}): ");
            contract.EndDate = DateTime.Parse(Console.ReadLine() ?? contract.EndDate.ToString());

            Console.Write($"Enter new User ID (current: {contract.UserID}): ");
            contract.UserID = int.Parse(Console.ReadLine() ?? contract.UserID.ToString());

            bool updated = dal.Update(contract);
            Console.WriteLine(updated ? "Contract updated successfully." : "Update failed.");
        }

        private static void DeleteContract()
        {
            var dal = new ContractDal();

            Console.Write("Enter Contract ID to delete: ");
            int contractId = int.Parse(Console.ReadLine() ?? "0");

            bool deleted = dal.Delete(contractId);
            Console.WriteLine(deleted ? "Contract deleted successfully." : "Delete failed.");
        }

        private static void AddContract()
        {
            var dal = new ContractDal();

            Console.Write("Enter Supplier ID: ");
            int supplierId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Enter Contract Number: ");
            string contractNumber = Console.ReadLine() ?? "";

            Console.Write("Enter Start Date (yyyy-mm-dd): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine() ?? DateTime.Now.ToString());

            Console.Write("Enter End Date (yyyy-mm-dd): ");
            DateTime endDate = DateTime.Parse(Console.ReadLine() ?? DateTime.Now.ToString());

            Console.Write("Enter User ID: ");
            int userId = int.Parse(Console.ReadLine() ?? "0");

            var contract = new Contract
            {
                SupplierID = supplierId,
                ContractNumber = contractNumber,
                StartDate = startDate,
                EndDate = endDate,
                UserID = userId
            };

            var newContract = dal.Create(contract);
            Console.WriteLine($"Contract added: ID {newContract.ContractID}, ContractNumber {newContract.ContractNumber}");

        }

        private static void ShowAllContracts()
        {
            var dal = new ContractDal();
            var contracts = dal.GetAll();

            if (contracts.Count == 0)
            {
                Console.WriteLine("No contracts found.");
                return;
            }

            foreach (var contract in contracts)
            {
                Console.WriteLine($"ID: {contract.ContractID}, SupplierID: {contract.SupplierID}, ContractNumber: {contract.ContractNumber}, StartDate: {contract.StartDate:d}, EndDate: {contract.EndDate:d}, UserID: {contract.UserID}");
            }
        }

        private static void UpdateCategory()
        {
            var dal = new CategoryDal();

            Console.Write("Enter Category ID to update: ");
            int categoryId = int.Parse(Console.ReadLine() ?? "0");

            var category = dal.GetById(categoryId);
            if (category == null)
            {
                Console.WriteLine("Category not found!");
                return;
            }

            Console.Write($"Enter new Category Name (current: {category.CategoryName}): ");
            string newName = Console.ReadLine() ?? category.CategoryName;

            category.CategoryName = newName;

            bool updated = dal.Update(category);
            Console.WriteLine(updated ? "Category updated successfully." : "Update failed.");
        }

        private static void DeleteCategory()
        {
            var dal = new CategoryDal();

            Console.Write("Enter Category ID to delete: ");
            int categoryId = int.Parse(Console.ReadLine() ?? "0");

            bool deleted = dal.Delete(categoryId);
            Console.WriteLine(deleted ? "Category deleted successfully." : "Delete failed.");
        }

        private static void AddCategory()
        {
            var dal = new CategoryDal();

            Console.Write("Enter Category Name: ");
            string name = Console.ReadLine() ?? "";

            var category = new Category
            {
                CategoryName = name
            };

            var newCategory = dal.Create(category);
            Console.WriteLine($"Category added: ID {newCategory.CategoryID}, Name {newCategory.CategoryName}");
        }

        private static void ShowAllCategories()
        {
            var dal = new CategoryDal();
            var categories = dal.GetAll();

            if (categories.Count == 0)
            {
                Console.WriteLine("No categories found.");
                return;
            }

            foreach (var category in categories)
            {
                Console.WriteLine($"ID: {category.CategoryID}, Name: {category.CategoryName}");
            }
        }

        private static void DeleteUser()
        {
            var dal = new UserDal();

            Console.Write("Enter User ID to delete: ");
            int userId = int.Parse(Console.ReadLine() ?? "0");

            bool deleted = dal.Delete(userId);
            Console.WriteLine(deleted ? "User deleted successfully." : "Delete failed.");
        }

        private static void UpdateUser()
        {
            var dal = new UserDal();

            Console.Write("Enter User ID to update: ");
            int userId = int.Parse(Console.ReadLine() ?? "0");

            var user = dal.GetById(userId);
            if (user == null)
            {
                Console.WriteLine("User not found!");
                return;
            }
            Console.Write($"Enter new UserName (current: {user.UserName}): ");
            string newUserName = Console.ReadLine() ?? user.UserName;

            Console.Write($"Enter new PasswordHash (current: {user.PasswordHash}): ");
            string newPasswordHash = Console.ReadLine() ?? user.PasswordHash;

            Console.Write($"Enter new Role (current: {user.Role}): ");
            string newRole = Console.ReadLine() ?? user.Role;

            user.UserName = newUserName;
            user.PasswordHash = newPasswordHash;
            user.Role = newRole;

            bool updated = dal.Update(user);
            Console.WriteLine(updated ? "User updated successfully." : "Update failed.");
        }

        private static void AddUser()
        {
            var dal = new UserDal();

            Console.Write("Enter UserName: ");
            string userName = Console.ReadLine() ?? "";

            Console.Write("Enter PasswordHash: ");
            string passwordHash = Console.ReadLine() ?? "";

            Console.Write("Enter Role: ");
            string role = Console.ReadLine() ?? "";

            var user = new User
            {
                UserName = "Anna",
                PasswordHash = "123an",
                Role = "Admin"
            };

            var newUser = dal.Create(user);
            Console.WriteLine($"User added: ID {newUser.UserID}, UserName {newUser.UserName}, Role {newUser.Role}");
        }

        private static void ShowAllUsers()
        {
            var dal = new UserDal();
            var users = dal.GetAll();

            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.UserID}, UserName: {user.UserName}, Role: {user.Role}");
            }
        }

        private static void DeleteManufacturer()
        {
            var dal = new ManufacturerDal();

            Console.Write("Enter Manufacturer ID to delete: ");
            int manufacturerId = int.Parse(Console.ReadLine() ?? "0");

            bool deleted = dal.Delete(manufacturerId);
            Console.WriteLine(deleted ? "Manufacturer deleted successfully." : "Delete failed.");
        }

        private static void ShowAllManufacturers()
        {
            var dal = new ManufacturerDal();
            var manufacturers = dal.GetAll();

            if (manufacturers.Count == 0)
            {
                Console.WriteLine("No manufacturers found.");
                return;
            }

            foreach (var m in manufacturers)
            {
                Console.WriteLine($"ID: {m.ManufacturerID}, Name: {m.Name}, Country: {m.Country}, Website: {m.Website}");
            }
        }

        private static void AddManufacturer()
        {
            var dal = new ManufacturerDal();

            Console.Write("Enter Name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Enter Country: ");
            string country = Console.ReadLine() ?? "";

            Console.Write("Enter Website: ");
            string website = Console.ReadLine() ?? "";

            Console.Write("Enter Product ID: ");
            int productId = int.Parse(Console.ReadLine() ?? "0");

            var manufacturer = new Manufacturer
            {
                Name = name,
                Country = country,
                Website = website
            };

            var newManufacturer = dal.Create(manufacturer);
            Console.WriteLine($"Manufacturer added: ID {newManufacturer.ManufacturerID}, Name: {newManufacturer.Name}, Country: {newManufacturer.Country}, Website: {newManufacturer.Website}");
        }

        private static void DeleteCharacteristic()
        {
            var dal = new CharacteristicDal();

            Console.Write("Enter Characteristic ID to delete: ");
            int characteristicId = int.Parse(Console.ReadLine() ?? "0");

            bool deleted = dal.Delete(characteristicId);
            Console.WriteLine(deleted ? "Characteristic deleted successfully." : "Delete failed.");
        }

        private static void ShowAllCharacteristics()
        {
            var dal = new CharacteristicDal();

            Console.Write("Enter Product ID to show characteristics: ");
            int productId = int.Parse(Console.ReadLine() ?? "0");

            var characteristics = dal.GetByProductId(productId);

            if (characteristics.Count == 0)
            {
                Console.WriteLine("No characteristics found for this product.");
                return;
            }

            foreach (var c in characteristics)
            {
                Console.WriteLine($"ID: {c.CharacteristicID}, Power: {c.Power}, Material: {c.Material}, Warranty: {c.Warranty}, ReleaseYear: {c.ReleaseYear}");
            }
        }

        private static void AddCharacteristic()
        {
            var dal = new CharacteristicDal();

            Console.Write("Enter Product ID: ");
            int productId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Enter Power: ");
            float power = float.Parse(Console.ReadLine() ?? "0");

            Console.Write("Enter Material: ");
            string material = Console.ReadLine() ?? "";

            Console.Write("Enter Warranty (years): ");
            string warranty = Console.ReadLine() ?? "";


            Console.Write("Enter Release Year: ");
            int releaseYear = int.Parse(Console.ReadLine() ?? "0");

            var characteristic = new Characteristic
            {
                ProductID = productId,
                Power = power,
                Material = material,
                Warranty = warranty,
                ReleaseYear = releaseYear
            };

            var newCharacteristic = dal.Create(characteristic);
            Console.WriteLine($"Characteristic added: ID {newCharacteristic.CharacteristicID}, ProductID {newCharacteristic.ProductID}");
        }

        private static void DeleteProduct()
        {
            var dal = new ProductDal();

            Console.Write("Enter Product ID to delete: ");
            int productId = int.Parse(Console.ReadLine() ?? "0");

            bool deleted = dal.Delete(productId);
            Console.WriteLine(deleted ? "Product deleted successfully." : "Delete failed.");
        }

        private static void DeleteSupplier()
        {
            throw new NotImplementedException();
        }

        private static void UpdateSupplierStatus()
        {
            var dal = new SupplierDal();

            Console.Write("Enter Supplier ID: ");
            int supplierId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Enter new status (true/false): ");
            bool newStatus = bool.Parse(Console.ReadLine() ?? "false");

            var supplier = new Supplier
            {
                SupplierID = supplierId,
                Status = newStatus
            };

            dal.UpdateSupplierStatus(supplier);

            Console.WriteLine($"Supplier {supplierId} status updated to {(newStatus ? "Active" : "Inactive")}");
        }

        private static void AddProduct()
        {
            var dal = new ProductDal();

            Console.Write("Enter Name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Enter Model: ");
            string model = Console.ReadLine() ?? "";

            Console.Write("Enter Number: ");
            string number = Console.ReadLine() ?? "";

            Console.Write("Enter Price: ");
            decimal price = decimal.Parse(Console.ReadLine() ?? "0");

            Console.Write("Enter SupplierID: ");
            int supplierId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Enter CategoryID: ");
            int categoryId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Enter ManufacturerID: ");
            int manufacturerId = int.Parse(Console.ReadLine() ?? "0");

            var product = new Product
            {
                Name = name,
                Model = model,
                Number = number,
                Price = price,
                SupplierID = supplierId,
                CategoryID = categoryId,
                ManufacturerID = manufacturerId
            };

            var newProduct = dal.Create(product);
            Console.WriteLine($"Product added: ID {newProduct.ProductID}, Name {newProduct.Name}");
        }

        private static void AddSupplier()
        {
            var dal = new SupplierDal();

            var oldSupplier = new Supplier
            {
                Name = "ASH mobile",
                Info = "+380931267001",
                Location = "Kyiv",
                Status = true
            };
            var newSupplier = dal.Create(oldSupplier);

            Console.WriteLine($"Inserted Supplier: {newSupplier.SupplierID}: {newSupplier.Name} - {newSupplier.Info} - {newSupplier.Location} - {(newSupplier.Status ? "Active" : "Inactive")}");
        }

        private static void ShowAllProducts()
        {
            var dal = new ProductDal();
            var products = dal.GetAll();

            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.ProductID}, Name: {product.Name}, Model: {product.Model}, Number: {product.Number}, Price: {product.Price}, SupplierID: {product.SupplierID}, CategoryID: {product.CategoryID}, ManufacturerID: { product.ManufacturerID}");
            }
        }

        private static void ShowAllSuppliers()
        {
            var dal = new SupplierDal();
            var suppliers = dal.GetAll();

            foreach (var supplier in suppliers)
            {
                Console.WriteLine($"ID: {supplier.SupplierID}, Name: {supplier.Name}, Info: {supplier.Info}, Location: {supplier.Location}, Status: {supplier.Status}");
            }

        }

    }
}
    
      

 
