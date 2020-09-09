﻿using Microsoft.Data.SqlClient;
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
        public Roommate GetById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT FirstName FROM Roommate WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    Roommate roommate = null;

                    if (reader.Read())
                    {
                        roommate = new Roommate
                        {
                            Id = id,
                            Firstname = reader.GetString(reader.GetOrdinal("FirstName"))
                        };
                    }
                    reader.Close();

                    return roommate;

                }
            }
        }
    }
}


 