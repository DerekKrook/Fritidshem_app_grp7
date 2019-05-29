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
                var output = connection.Query<Child>($@"SELECT child.id, child.firstname, child.lastname, child.leavealone, class_id, class.name AS Class 
                FROM(child INNER JOIN class on class_id = class.id) 
                WHERE child.firstname LIKE '%{a}%' OR child.lastname LIKE '%{a}%';").ToList();

                return output;

            }

        }

        //Hämtar alla barn
        public static List<Child> GetAllChildren()
        {
            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Child>($@"SELECT child.id, child.firstname, child.lastname, child.leavealone, department.name AS Class 
                FROM((child INNER JOIN class ON class_id = class.id) 
                INNER JOIN department ON department_id = department.id) 
                ORDER BY department.name DESC").ToList();

                return output;
            }
        }
        // hämtar årkurs 1
        public static List<Child> GetFirstGraders()
        {
            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Child>($@"SELECT child.id, child.firstname, child.lastname, child.leavealone, class.name AS Class, department.id AS avdelning 
                FROM((child INNER JOIN class ON class_id = class.id) 
                INNER JOIN department ON department_id = department.id) 
                WHERE department_id = 1 ORDER BY class_id DESC;").ToList();


                return output;
            }
        }
        // hämtar årkurs 2
        public static List<Child> GetSecondGraders()
        {
            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Child>($@"SELECT child.id, child.firstname, child.lastname, child.leavealone, class.name AS Class, department.id AS avdelning 
                FROM((child INNER JOIN class ON class_id = class.id) INNER JOIN department ON department_id = department.id) 
                WHERE department_id = 2 ORDER BY class_id DESC;").ToList();

                return output;
            }
        }
        // hämtar årkurs 3
        public static List<Child> GetThirdGraders()
        {
            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Child>($@"SELECT child.id, child.firstname, child.lastname, child.leavealone, class.name AS Class, department.id AS avdelning 
                FROM((child INNER JOIN class ON class_id = class.id) 
                INNER JOIN department ON department_id = department.id)
                WHERE department_id = 4 ORDER BY class_id DESC;").ToList();

                return output;
            }
        }

        //Hämtar alla anställda
        public static List<Staff> GetAllStaff()
        {
            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Staff>($@"SELECT * FROM staff").ToList();

                return output;
            }
        }

        //Hämtar alla föräldrar
        public static List<Guardian> GetAllGuardians()
         {

            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {              
                var output = connection.Query<Guardian>($@"SELECT * FROM guardian").ToList();

                return output;
            }
        }

        //Hämtar föräldrar efter sökning av för - och efternamn
        public static List<Guardian> GetGuardian(string firstName, string lastName)
        {
            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Guardian>($@"SELECT * FROM child 
                WHERE firstname LIKE '%{firstName}%' OR lastname LIKE '%{lastName}%'").ToList();

                return output;
            }
        }

        //Hämtar barn till vårdnadshavare 
        public static List<Child> GetChildrenOfGuardian(Guardian guardian)
        {

            var Id = guardian.Id;

            var Query = $@"SELECT * FROM guardian_child INNER JOIN child ON child_id = child.id WHERE guardian_id='{Id}'"; 

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
                      
            var Query = $@"SELECT * FROM guardian_child 
            INNER JOIN guardian ON guardian_id = guardian.id 
            WHERE child_id='{Id}'";

            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Guardian>(Query).ToList();

                return output;
            }
        }

        //Hämta scheman för barn
        public static List<Schedule> GetSchedule (Child child)
        {

            Schedule s = new Schedule();
            List <Schedule> schedules = new List<Schedule>();

            var Id = child.Id;

            var Query = $@"SELECT lecture.name AS Lecturename, dates.day AS Day, time.timestart AS Timestart 
            FROM ((((((child INNER JOIN schedule ON child.id = child_id) 
            INNER JOIN schedule_lecture ON schedule.id = schedule_id) 
            INNER JOIN lecture ON lecture__id = lecture.id) 
            INNER JOIN lecture_dates_time ON lecture.id = lecture_id) 
            INNER JOIN dates ON dates_id = dates.id) INNER JOIN time ON time_id = time.id)
            WHERE child.id='{Id}' ORDER BY time.timestart ASC";           

            using (var conn = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand(Query, conn))
                    using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        s = new Schedule()

                        {
                            Lecturename = reader["Lecturename"].ToString(),
                            Day = reader["Day"].ToString(),
                            Timestart = Convert.ToDateTime((reader["Timestart"]).ToString())

                        };

                    schedules.Add(s);
                    }
                }
            }
            return schedules;
        }

        public static List<Attendance> GetChildrenAtFritids()
        {

            Attendance a = new Attendance();
            List<Attendance> attendance = new List<Attendance>();

            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Attendance>($@"SELECT attendance_id AS id, child.firstname ||' '|| child.lastname AS Child, guardian.firstname ||' '|| guardian.lastname AS Guardian, category_attendance.name_type AS Category_attendance, dates.day AS Day, attendance.comment AS Comment 
            FROM (((((attendance INNER JOIN child ON child_id = child.id) INNER JOIN guardian ON guardian_id = guardian.id) 
            INNER JOIN category_attendance ON category_attendance_id = category_attendance.id)  
            INNER JOIN attendance_dates ON attendance_dates.dates_id = attendance_dates.dates_id AND attendance.id = attendance_dates.attendance_id) 
            INNER JOIN dates ON attendance_dates.dates_id = dates.id AND dates.day = dates.day) 
            WHERE category_attendance_id = 3 
            ORDER BY dates.day;").ToList();

                return output;
            }

        }
    }
}


