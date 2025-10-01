using SuppManagerDB.DTO;
using Microsoft.Data.SqlClient;
using SuppManagerDB.DAL.Interfaces;

namespace SuppManagerDB.DAL.Concrete
{
    public class ProductDal : IProductDal
    {
        public Product Create(Product product)
        {
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SuppManagerDB;Integrated Security=True;Encrypt=True");

            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Products (Name, Model, Number, Price, SupplierID, CategoryID) OUTPUT INSERTED.ProductID VALUES (@Name, @Model, @Number, @Price, @SupplierID, @CategoryID)";

            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Model", product.Model);
            command.Parameters.AddWithValue("@Number", product.Number);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@SupplierID", product.SupplierID);
            command.Parameters.AddWithValue("@CategoryID", product.CategoryID);

            product.ProductID = (int)command.ExecuteScalar();

            connection.Close();

            return product;
        }

        public bool Update(Product product)
        {
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SuppManagerDB;Integrated Security=True;Encrypt=True");
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE Products SET Name = @Name, Model = @Model, Number = @Number, Price = @Price, SupplierID = @SupplierID, CategoryID = @CategoryID WHERE ProductID = @ProductID";
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Model", product.Model);
            command.Parameters.AddWithValue("@Number", product.Number);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@SupplierID", product.SupplierID);
            command.Parameters.AddWithValue("@CategoryID", product.CategoryID);

            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowsAffected > 0;
        }

        public bool Delete(int ProductID)
        {
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SuppManagerDB;Integrated Security=True;Encrypt=True");
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Products WHERE ProductID = @ProductID";
            command.Parameters.AddWithValue("@ProductID", ProductID);
            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowsAffected > 0;
        }

        public List<Product> GetAll()
        {

            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SuppManagerDB;Integrated Security=True;Encrypt=True");

            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT ProductID, Name, Model, Number, Price, SupplierID, CategoryID FROM Products";

            SqlDataReader reader = command.ExecuteReader();

            List<Product> products = new List<Product>();

            while (reader.Read())
            {
                Product product = new Product
                {
                    ProductID = reader["ProductID"] != DBNull.Value ? Convert.ToInt32(reader["ProductID"]) : 0,
                    Name = reader["Name"] != DBNull.Value ? reader["Name"].ToString()! : string.Empty,
                    Model = reader["Model"] != DBNull.Value ? reader["Model"].ToString()! : string.Empty,
                    Number = reader["Number"] != DBNull.Value ? reader["Number"].ToString()! : string.Empty,
                    Price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : 0,
                    SupplierID = reader["SupplierID"] != DBNull.Value ? Convert.ToInt32(reader["SupplierID"]) : 0,
                    CategoryID = reader["CategoryID"] != DBNull.Value ? Convert.ToInt32(reader["CategoryID"]) : 0
                };
                products.Add(product);
            }
            reader.Close();
            connection.Close();
            return products;
        }

        public Product GetById(int ProductID)
        {

            Product product = null;

            using var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SuppManagerDB;Integrated Security=True;Encrypt=True");
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT ProductID, Name, Model, Number, Price, SupplierID, CategoryID FROM Products WHERE ProductID = @ProductID";
            command.Parameters.AddWithValue("@ProductID", ProductID);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                product = new Product
                {
                    ProductID = (int)reader["ProductID"],
                    Name = (string)reader["Name"],
                    Model = (string)reader["Model"],
                    Number = (string)reader["Number"],
                    Price = (decimal)reader["Price"],
                    SupplierID = (int)reader["SupplierID"],
                    CategoryID = (int)reader["CategoryID"]
                };
            }

            return product;
        }

        public List<Product> GetBySupplier(int supplierId)
        {
            var products = new List<Product>();

            using var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SuppManagerDB;Integrated Security=True;Encrypt=True");
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT ProductID, Name, Model, Number, Price, SupplierID, CategoryID FROM Products WHERE SupplierID = @SupplierID";
            command.Parameters.AddWithValue("@SupplierID", supplierId);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                products.Add(new Product
                {
                    ProductID = (int)reader["ProductID"],
                    Name = (string)reader["Name"],
                    Model = (string)reader["Model"],
                    Number = (string)reader["Number"],
                    Price = (decimal)reader["Price"],
                    SupplierID = (int)reader["SupplierID"],
                    CategoryID = (int)reader["CategoryID"]
                });
            }

            return products;
        }

        public List<Product> GetByCategory(int categoryId)
        {
            var products = new List<Product>();

            using var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SuppManagerDB;Integrated Security=True;Encrypt=True");
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT ProductID, Name, Model, Number, Price, SupplierID, CategoryID FROM Products WHERE CategoryID = @CategoryID";
            command.Parameters.AddWithValue("@CategoryID", categoryId);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                products.Add(new Product
                {
                    ProductID = (int)reader["ProductID"],
                    Name = (string)reader["Name"],
                    Model = (string)reader["Model"],
                    Number = (string)reader["Number"],
                    Price = (decimal)reader["Price"],
                    SupplierID = (int)reader["SupplierID"],
                    CategoryID = (int)reader["CategoryID"]
                });
            }

            return products;
        }
    }
}
