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
        public List<Child> GetAllChildren(string firstName)
        {
            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Child>($"SELECT * FROM child WHERE firstname = '{firstName}'").ToList();
            
               return output;

            }
            
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


