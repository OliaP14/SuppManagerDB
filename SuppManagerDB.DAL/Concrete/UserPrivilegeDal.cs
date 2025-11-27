using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using SuppManagerDB.DAL.Interfaces;
using SuppManagerDB.DTO;

namespace SuppManagerDB.DAL.Concrete
{
    public class UserPrivilegeDal : IUserPrivilegeDal
    {
        public List<Privilege> GetPrivilegesForUser(int userId)
        {
            var result = new List<Privilege>();

            using var connection = new SqlConnection(Constants.DB_CONNECTION);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT p.PrivilegeID, p.Name
                FROM UserPrivileges up
                JOIN Privileges p ON up.PrivilegeID = p.PrivilegeID
                WHERE up.UserID = @UserID;
            ";

            command.Parameters.AddWithValue("@UserID", userId);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Privilege
                {
                    PrivilegeID = reader.GetInt32(0),
                    Name = reader.GetString(1)
                });
            }

            return result;
        }
    }
}
