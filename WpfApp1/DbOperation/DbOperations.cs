using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Npgsql;
using WpfApp1.DbOperation;
using WpfApp1.Models;
using Dapper;
using System.Data;

namespace WpfApp1
{
    class DbOperations
    {
        //Hämtar barn
        public List<Child> GetAllChildren()
        {
            Child child;
            List<Child> children = new List<Child>();

            string stmt = "SELECT * FROM child ORDER BY id ASC";

            using (var conn = new
            NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            child = new Child()
                            {
                                Id = reader.GetInt32(0),
                                Firstname = reader.GetString(1),
                                Lastname = reader.GetString(4)

                            };
                            children.Add(child);
                        }
                    }
                }
            }

            return children;


        }

        //Hämtar föräldrar beroende på sökning av efternamn
        public List<Guardian> GetGuardian(string lastName)
        {

            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Guardian>($"SELECT * FROM guardian WHERE lastname = '{lastName}'").ToList();

                return output;
            }
        }

        public void InsertGuardian()
        {

        }
    }
}


