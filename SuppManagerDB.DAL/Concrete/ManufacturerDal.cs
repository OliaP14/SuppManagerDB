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
            command.CommandText = "INSERT INTO Manufacturers (Name, Country, Website) OUTPUT INSERTED.ManufacturerID VALUES (@Name, @Country, @Website)";

            command.Parameters.AddWithValue("@Name", manufacturer.Name);
            command.Parameters.AddWithValue("@Country", manufacturer.Country);
            command.Parameters.AddWithValue("@Website", manufacturer.Website);

            manufacturer.ManufacturerID = (int)command.ExecuteScalar();
            connection.Close();
            return manufacturer;
        }
        public List<Manufacturer> GetAll()
        {
            SqlConnection connection = new SqlConnection(Constants.DB_CONNECTION);

            connection.Open();

            SqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT ManufacturerID, Name, Country, Website FROM Manufacturers";

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

            command.CommandText = "UPDATE Manufacturers SET Name = @Name, Country = @Country, Website = @Website WHERE ManufacturerID = @ManufacturerID";


            command.Parameters.AddWithValue("@Name", manufacturer.Name);
            command.Parameters.AddWithValue("@Country", manufacturer.Country);
            command.Parameters.AddWithValue("@Website", manufacturer.Website);            
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
            command.CommandText = "SELECT ManufacturerID, Name, Country, Website FROM Manufacturers WHERE ManufacturerID = @ManufacturerID";
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
                };
            }
            reader.Close();
            connection.Close();
            return manufacturer;

        }

    }
}
