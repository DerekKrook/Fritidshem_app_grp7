﻿using System;
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
using WpfApp1.ErrorHandler;

namespace WpfApp1
{
    static class DbOperations
    {
        //Hämtar specifikt barn SÖK för- och efternamn.
        public static List<Child> GetChildren(string input)
        {
            InputHandler inputhandler = new InputHandler();
            var a = inputhandler.Uppercase(input);

            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Child>($"SELECT * FROM child WHERE firstname LIKE '%{a}%' OR lastname LIKE '%{a}%'").ToList();

                return output;

            }

        }

        //Hämtar alla barn
        public static List<Child> GetAllChildren()
        {
            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Child>($"SELECT child.id, child.firstname, child.lastname, child.leavealone, department.name AS Class FROM((child INNER JOIN class ON class_id = class.id) INNER JOIN department ON department_id = department.id) ORDER BY department_name DESC").ToList();

                return output;

            }

        }

        //Hämtar alla anställda
        public static List<Staff> GetAllStaff()
        {
            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Staff>($"SELECT * FROM staff").ToList();

                return output;

            }

        }

        //Hämtar alla föräldrar
        public static List<Guardian> GetAllGuardians()
         {

            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {              
                var output = connection.Query<Guardian>($"SELECT * FROM guardian").ToList();

                return output;
            }
        }

        //Hämtar föräldrar efter sökning av för - och efternamn
        public static List<Guardian> GetGuardian(string firstName, string lastName)
        {
            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Guardian>($"SELECT * FROM child WHERE firstname LIKE '%{firstName}%' OR lastname LIKE '%{lastName}%'").ToList();

                return output;
            }
        }

        //Hämtar barn till vårdnadshavare 
        public static List<Child> GetChildrenOfGuardian(Guardian guardian)
        {

            var Id = guardian.Id;

            var Query = $"SELECT child.firstname, child.lastname FROM guardian_child INNER JOIN child ON child_id = child.id WHERE guardian_id='{Id}'"; 

            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Child>(Query).ToList();

                return output;
            }
        }

        // Hämtar vårdnadshavare till barn
        public static List<Guardian> GetGuardianOfChild(Child child)
        {

            var Id = child.Id;
                      
            var Query = $"SELECT * FROM guardian_child INNER JOIN guardian ON guardian_id = guardian.id WHERE child_id='{Id}'";

            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Guardian>(Query).ToList();

                return output;
            }
        }

        //Hämta scheman för barn
        public static List<Schedule> GetSchedule (Child child)
        {

            var Id = child.Id;

            var Query = $"SELECT  INNER JOIN guardian ON guardian_id = guardian.id WHERE child_id='{Id}'";

            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Schedule>(Query).ToList();

                return output;
            }
        }
    }
}


