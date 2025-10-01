using SuppManagerDB.DTO;
using Microsoft.Data.SqlClient;
using SuppManagerDB.DAL.Interfaces;

namespace SuppManagerDB.DAL.Concrete
{
    public class CategoryDal : ICategoryDal
    {
        public Category Create(Category category)
        {
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SuppManagerDB;Integrated Security=True;Encrypt=True");

            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Categories (CategoryName) OUTPUT INSERTED.CategoryID VALUES (@CategoryName)";

            command.Parameters.AddWithValue("@CategoryName", category.CategoryName);

            category.CategoryID = (int)command.ExecuteScalar();

            connection.Close();

            return category;
        }
        public List<Category> GetAll()
        {
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SuppManagerDB;Integrated Security=True;Encrypt=True");
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT CategoryID, CategoryName FROM Categories";
            SqlDataReader reader = command.ExecuteReader();
            List<Category> categories = new List<Category>();
            while (reader.Read())
            {
                Category category = new Category
                {
                    CategoryID = (int)reader["CategoryID"],
                    CategoryName = (string)reader["CategoryName"]
                };
                categories.Add(category);
            }
            reader.Close();
            connection.Close();
            return categories;
        }

        public bool Update(Category category)
        {
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SuppManagerDB;Integrated Security=True;Encrypt=True");
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE Categories SET CategoryName = @CategoryName WHERE CategoryID = @CategoryID";

            command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
            command.Parameters.AddWithValue("@CategoryID", category.CategoryID);

            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowsAffected > 0;
        }

        public bool Delete(int CategoryID)
        {
            SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SuppManagerDB;Integrated Security=True;Encrypt=True");
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Categories WHERE CategoryID = @CategoryID";

            command.Parameters.AddWithValue("@CategoryID", CategoryID);
            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowsAffected > 0;
        }

        public Category GetById(int CategoryID)
        {
            Category category = null;

            using var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SuppManagerDB;Integrated Security=True;Encrypt=True");
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT CategoryID, CategoryName FROM Categories WHERE CategoryID = @CategoryID";
            command.Parameters.AddWithValue("@CategoryID", CategoryID);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                category = new Category
                {
                   CategoryID = (int)reader["CategoryID"],
                   CategoryName = (string)reader["CategoryName"]
                };
            }

            return category;
        }
    }
}
