using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Npgsql;


namespace WpfApp1
{
    class DbOperations
    {

        public List<Child> GetAllChildren()
        {
            Child c;
            List<Child> children = new List<Child>();

            string stmt = "SELECT * FROM child";

            var conn = new

               NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString);
            conn.Open();

            var cmd = new NpgsqlCommand(stmt, conn);

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                c = new Child()
                {
                    id = reader.GetInt32(0),
                    firstname = reader.GetString(1),
                    lastname = reader.GetString(4)
                };
                children.Add(c);
            }
            conn.Close();

            return children;
        }

    }
}
