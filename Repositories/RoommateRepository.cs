using Microsoft.Data.SqlClient;
using Roommates.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Roommates.Repositories
{
    /// <summary>
    ///  This class is responsible for interacting with Room data.
    ///  It inherits from the BaseRepository class so that it can use the BaseRepository's Connection property
    /// </summary>
    public class RoommateRepository : BaseRepository
    {
        /// <summary>
        ///  When new RoomRespository is instantiated, pass the connection string along to the BaseRepository
        /// </summary>
        public RoommateRepository(string connectionString) : base(connectionString) { }

        // ...We'll add some methods shortly...

        public List<Roommate> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, FirstName, RoomId FROM Roommate";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Roommate> roommates = new List<Roommate>();

                    while (reader.Read())
                    {
                        int IdColumnPosition = reader.GetOrdinal("Id");
                        int IdValue = reader.GetInt32(IdColumnPosition);
                        int FirstNameColumnPosition = reader.GetOrdinal("FirstName");
                        string FirstNameValue = reader.GetString(FirstNameColumnPosition);
                       // int RoomIdColumnPosition = reader.GetOrdinal("Room");
                       // string RoomValue = reader.GetString(RoomIdColumnPosition);
                        Roommate roommate = new Roommate
                        {
                            Id = IdValue,
                            Firstname = FirstNameValue,
                           // Room = RoomValue
                        };

                        roommates.Add(roommate);
                    }
                    reader.Close();
                    return roommates;
                }
            }
        }
        public void Insert(Roommate roommate)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Roommate (Firstname)
                            OUTPUT INSERTED.Id
VALUES (@firstname)";
                    cmd.Parameters.AddWithValue("@FirstName", roommate.Firstname);
                    int id = (int)cmd.ExecuteScalar();

                    roommate.Id = id;
                }
            }
        }
    }
}


 