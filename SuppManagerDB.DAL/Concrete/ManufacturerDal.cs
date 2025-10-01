using Microsoft.Data.SqlClient;
using SuppManagerDB.DTO;
using SuppManagerDB.DAL.Interfaces;

namespace SuppManagerDB.DAL.Concrete
{
    public class ManufacturerDal : IManufacturerDal
    {
        public Manufacturer Create(Manufacturer manufacturer)
        {
            SqlConnection connection = new SqlConnection(Constants.DB_CONNECTION);

            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Manufacturers (Name, Country, Website, ProductID) OUTPUT INSERTED.ManufacturerID VALUES (@Name, @Country, @Website, @ProductID)";

            command.Parameters.AddWithValue("@Name", manufacturer.Name);
            command.Parameters.AddWithValue("@Country", manufacturer.Country);
            command.Parameters.AddWithValue("@Website", manufacturer.Website);
            command.Parameters.AddWithValue("@ProductID", manufacturer.ProductID);

            manufacturer.ManufacturerID = (int)command.ExecuteScalar();
            connection.Close();
            return manufacturer;
        }
        public List<Manufacturer> GetAll()
        {
            SqlConnection connection = new SqlConnection(Constants.DB_CONNECTION);

            connection.Open();

            SqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT ManufacturerID, Name, Country, Website, ProductID FROM Manufacturers";

            SqlDataReader reader = command.ExecuteReader();

            List<Manufacturer> manufacturers = new List<Manufacturer>();

            while (reader.Read())
            {
                Manufacturer manufacturer = new Manufacturer
                {
                    ManufacturerID = (int)reader["ManufacturerID"],
                    Name = (string)reader["Name"],
                    Country = (string)reader["Country"],
                    Website = (string)reader["Website"],
                    ProductID = (int)reader["ProductID"]
                };
                manufacturers.Add(manufacturer);
            }
            reader.Close();
            connection.Close();
            return manufacturers;
        }

        public bool Update(Manufacturer manufacturer)
        {
            SqlConnection connection = new SqlConnection(Constants.DB_CONNECTION);

            connection.Open();

            SqlCommand command = connection.CreateCommand();

            command.CommandText = "UPDATE Manufacturers SET Name = @Name, Country = @Country, Website = @Website, ProductID = @ProductID WHERE ManufacturerID = @ManufacturerID";

            command.Parameters.AddWithValue("@Name", manufacturer.Name);
            command.Parameters.AddWithValue("@Country", manufacturer.Country);
            command.Parameters.AddWithValue("@Website", manufacturer.Website);
            command.Parameters.AddWithValue("@ProductID", manufacturer.ProductID);
            command.Parameters.AddWithValue("@ManufacturerID", manufacturer.ManufacturerID);

            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowsAffected > 0;
        }

        public bool Delete(int ManufacturerID)
        {
            SqlConnection connection = new SqlConnection(Constants.DB_CONNECTION);

            connection.Open();

            SqlCommand command = connection.CreateCommand();

            command.CommandText = "DELETE FROM Manufacturers WHERE ManufacturerID = @ManufacturerID";
            command.Parameters.AddWithValue("@ManufacturerID", ManufacturerID);
            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowsAffected > 0;
        }

        public Manufacturer GetById(int ManufacturerID)
        {
            SqlConnection connection = new SqlConnection(Constants.DB_CONNECTION);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT ManufacturerID, Name, Country, Website, ProductID FROM Manufacturers WHERE ManufacturerID = @ManufacturerID";
            command.Parameters.AddWithValue("@ManufacturerID", ManufacturerID);
            SqlDataReader reader = command.ExecuteReader();
            Manufacturer manufacturer = null;
            if (reader.Read())
            {
                manufacturer = new Manufacturer
                {
                    ManufacturerID = (int)reader["ManufacturerID"],
                    Name = (string)reader["Name"],
                    Country = (string)reader["Country"],
                    Website = (string)reader["Website"],
                    ProductID = (int)reader["ProductID"]
                };
            }
            reader.Close();
            connection.Close();
            return manufacturer;

        }

        public List<Manufacturer> GetByProduct(int productId)
        {
            var manufacturers = new List<Manufacturer>();

            using (SqlConnection connection = new SqlConnection(Constants.DB_CONNECTION))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT ManufacturerID, Name, Country, Website, ProductID FROM Manufacturers WHERE ProductID = @ProductID";
                command.Parameters.AddWithValue("@ProductID", productId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Manufacturer manufacturer = new Manufacturer
                        {
                            ManufacturerID = (int)reader["ManufacturerID"],
                            Name = (string)reader["Name"],
                            Country = (string)reader["Country"],
                            Website = (string)reader["Website"],
                            ProductID = (int)reader["ProductID"]
                        };
                        manufacturers.Add(manufacturer);
                    }
                }
                connection.Close();
                return manufacturers;
            }
        }
    }
}
