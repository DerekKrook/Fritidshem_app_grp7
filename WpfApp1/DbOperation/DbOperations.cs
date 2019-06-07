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
                WHERE child.firstname LIKE @A OR child.lastname LIKE @A", new {A ='%' + a + '%'}).ToList();

                return output;

            }

        }

        //Hämtar alla barn
        public static List<Child> GetAllChildren()
        {
            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Child>($@"SELECT child.id, child.firstname, child.lastname, child.age, child.leavealone, class.name AS Class 
                FROM(child INNER JOIN class ON class_id = class.id) 
                ORDER BY class.name DESC").ToList();

                return output;
            }
        }

        //Hämtar alla klasser
        public static List<Class> GetAllClasses()
        {
            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Class>($@"SELECT * FROM CLASS").ToList();

                return output;
            }
        }
        // hämtar årkurser
        public static List<Child> GetFirstGraders(int departmentid)
        {
            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Child>($@"SELECT child.id, child.firstname, child.lastname, child.leavealone, class.name AS Class, department.id AS avdelning 
                FROM((child INNER JOIN class ON class_id = class.id) 
                INNER JOIN department ON department_id = department.id) 
                WHERE department_id = @DepartmentID ORDER BY class_id DESC;", new {DepartmentID = departmentid}).ToList();

                return output;
            }
        }
        // ersatt med hämtar årkurs
        // hämtar årkurs 2
        //public static List<Child> GetSecondGraders()
        //{
        //    using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
        //    {
        //        var output = connection.Query<Child>($@"SELECT child.id, child.firstname, child.lastname, child.leavealone, class.name AS Class, department.id AS avdelning 
        //        FROM((child INNER JOIN class ON class_id = class.id) INNER JOIN department ON department_id = department.id) 
        //        WHERE department_id = 2 ORDER BY class_id DESC;").ToList();

        //        return output;
        //    }
        //}
        //// hämtar årkurs 3
        //public static List<Child> GetThirdGraders()
        //{
        //    using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
        //    {
        //        var output = connection.Query<Child>($@"SELECT child.id, child.firstname, child.lastname, child.leavealone, class.name AS Class, department.id AS avdelning 
        //        FROM((child INNER JOIN class ON class_id = class.id) 
        //        INNER JOIN department ON department_id = department.id)
        //        WHERE department_id = 4 ORDER BY class_id DESC;").ToList();

        //        return output;
        //    }
        //}

        //Hämtar alla anställda 
        public static List<Staff> GetAllStaff()
        {
            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Staff>($@"SELECT staff.id, firstname, lastname, email, department.name AS department
                                                        FROM ((staff 
	                                                    INNER JOIN department_staff on id = department_staff.staff_id)
	                                                    INNER JOIN department on department.id = department_staff.department_id)
	                                                    WHERE department.id = 1 OR department.id = 2 OR department.id = 4").ToList();

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
        public static List<Guardian> GetGuardian(string name)
        {
            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Guardian>($@"SELECT * FROM guardian 
                WHERE firstname LIKE @Firstname OR lastname LIKE @Lastname", new {Firstname = '%' + name + '%', Lastname = '%' + name + '%' }).ToList();

                return output;
            }
        }

        //Hämtar barn till vårdnadshavare 
        public static List<Child> GetChildrenOfGuardian()
        {
            var id = Activeguardian.Id;

           
            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Child>($@"SELECT guardian_id, child_id AS Id, child.firstname, child.lastname, child.age, child.leavealone, child.class_id, meals.id AS Mealsid
                                  FROM ((meals
                                  INNER JOIN child ON child_id = child.id)
                                  INNER JOIN guardian ON guardian_id = guardian.id)
                                  WHERE guardian_id = @Id", new { Id = id }).ToList();

                return output;
            }
        }

        // Hämtar vårdnadshavare till barn
        public static List<Guardian> GetGuardianOfChild(int id)
        {
                                
            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {               
                    var output = connection.Query<Guardian>($@"SELECT * FROM guardian_child 
                    INNER JOIN guardian ON guardian_id = guardian.id 
                    WHERE child_id = @Id", new {Id = id}).ToList();
                    return output;
                
            }
        }

        //Hämta scheman för barn   Vill ha med category attendance ID  för dag/barnet 
        public static List<Schedule> GetSchedule (string day)
        {
            var Id = Activechild.Id;
            Schedule s = new Schedule();
            List <Schedule> schedules = new List<Schedule>();

                       

            using (var conn = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {


                    cmd.Connection = conn; 
                    cmd.CommandText = $@"SELECT lecture.name AS Lecturename, dates.day AS Day, time.timestart AS Timestart, time.timefinish AS Timefinish 
                    FROM ((((((child INNER JOIN schedule ON child.id = child_id) 
                    INNER JOIN schedule_lecture ON schedule.id = schedule_id) 
                    INNER JOIN lecture ON lecture__id = lecture.id) 
                    INNER JOIN lecture_dates_time ON lecture.id = lecture_id) 
                    INNER JOIN dates ON dates_id = dates.id) INNER JOIN time ON time_id = time.id)
                    WHERE child.id = @Id AND dates.day=@Day ORDER BY time.timestart ASC";

                    cmd.Parameters.AddWithValue("Id", Id);
                    cmd.Parameters.AddWithValue("Day", day);



                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            s = new Schedule()

                            {
                                Lecturename = reader["Lecturename"].ToString(),
                                Day = reader["Day"].ToString(),
                                Timestart = Convert.ToDateTime((reader["Timestart"]).ToString()),
                                Timefinish = Convert.ToDateTime((reader["Timefinish"]).ToString())

                            };

                            schedules.Add(s);
                            s.changeDateTime();
                        }
                    }

                }
            }
            return schedules;
        }

        // hämtar sjuka och lediga barn
        public static List<Attendance> GetSickAndHolidayChildren()
        {

            Attendance a = new Attendance();
            List<Attendance> attendance = new List<Attendance>();

            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Attendance>($@"SELECT attendance_id AS id, child.firstname ||' '|| child.lastname AS Child, guardian.firstname ||' '|| guardian.lastname AS Guardian, category_attendance.name_type AS Category_attendance, dates.day AS Day, attendance.comment AS Comment, child.leavealone AS LeaveAlone, dates.week AS Week
            FROM (((((attendance INNER JOIN child ON child_id = child.id) INNER JOIN guardian ON guardian_id = guardian.id) 
            INNER JOIN category_attendance ON category_attendance_id = category_attendance.id)  
            INNER JOIN attendance_dates ON attendance_dates.dates_id = attendance_dates.dates_id AND attendance.id = attendance_dates.attendance_id) 
            INNER JOIN dates ON attendance_dates.dates_id = dates.id AND dates.day = dates.day) 
            WHERE (category_attendance_id = 1 OR category_attendance_id = 2) 
            ORDER BY dates.week;").ToList();

                return output;
            }


        }
        //Hämtar barn registrerade på fritids
        public static List<Attendance> GetChildrenAtFritids()
        {

            Attendance a = new Attendance();
            List<Attendance> attendance = new List<Attendance>();

            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Attendance>($@"SELECT attendance_id AS id, child.firstname ||' '|| child.lastname AS Child, guardian.firstname ||' '|| guardian.lastname AS Guardian, category_attendance.name_type AS Category_attendance, dates.day AS Day, attendance.comment AS Comment, child.leavealone AS LeaveAlone, dates.week AS Week
            FROM (((((attendance INNER JOIN child ON child_id = child.id) INNER JOIN guardian ON guardian_id = guardian.id) 
            INNER JOIN category_attendance ON category_attendance_id = category_attendance.id)  
            INNER JOIN attendance_dates ON attendance_dates.dates_id = attendance_dates.dates_id AND attendance.id = attendance_dates.attendance_id) 
            INNER JOIN dates ON attendance_dates.dates_id = dates.id AND dates.day = dates.day) 
            WHERE (category_attendance_id = 3 OR category_attendance_id = 7) 
            ORDER BY attendance_dates.dates_id;").ToList();
                
                return output;
            }
                

        }

        //Hämtar barn som gått hem
        public static List<Attendance> GetChildrenGoneHome()
        {
            Attendance a = new Attendance();
            List<Attendance> attendance = new List<Attendance>();

            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Attendance>($@"SELECT attendance_id AS id, child.firstname ||' '|| child.lastname AS Child, guardian.firstname ||' '|| guardian.lastname AS Guardian, category_attendance.name_type AS Category_attendance, dates.day AS Day, attendance.comment AS Comment, child.leavealone AS LeaveAlone, dates.week AS Week
            FROM (((((attendance INNER JOIN child ON child_id = child.id) INNER JOIN guardian ON guardian_id = guardian.id) 
            INNER JOIN category_attendance ON category_attendance_id = category_attendance.id)  
            INNER JOIN attendance_dates ON attendance_dates.dates_id = attendance_dates.dates_id AND attendance.id = attendance_dates.attendance_id) 
            INNER JOIN dates ON attendance_dates.dates_id = dates.id AND dates.day = dates.day) 
            WHERE category_attendance_id = 4
 
            ORDER BY dates.day;").ToList();

                return output;
            }
        }

       // Markera att barn gått hem
        public static void SetChildGoneHome()
        {
            
            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                connection.Execute($@"UPDATE attendance SET category_attendance_id = 4, staff_id = @StaffID WHERE id = @AttendanceID", new {StaffID = Activestaff.Id, AttendanceID = ActiveAttendance.Id });
          
            }

        }

        //uppdatera mail och/eller telefon på förälder 
        public static void UpdateGuardianProperties(int phone, string email, string firstname, string lastname)
        {
            var id = Activeguardian.Id;
            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                connection.Execute($@"UPDATE guardian SET firstname = @Firstname, lastname = @Lastname, email = @Email, phone = @Phone WHERE id = @Id", new {Firstname = firstname, Lastname = lastname, Email = email, Phone = phone, Id = id});
               
            }

        }

        // lägg till ny vårdnadshavare
        public static void AddNewGuardian(int phone, string firstname, string lastname, string email)
        {
            InputHandler inputhandler = new InputHandler();

            var a = inputhandler.Uppercase(firstname);
            var b = inputhandler.Uppercase(lastname);

            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                connection.Execute($@"INSERT INTO guardian (firstname, lastname, phone, email) VALUES (@Firstname, @Lastname, @Phone, @Email)", new {Firstname = firstname, Lastname = lastname, Phone = phone, Email = email });
               
            }
        }

       // Uppdatera barnuppgifter
        public static void UpdateChildProperties(string firstname, string lastname, string age, int classid)
        {
            var id = Activechild.Id;
            InputHandler inputhandler = new InputHandler();
   
            var a = inputhandler.Uppercase(firstname);
            var b = inputhandler.Uppercase(lastname);


            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                connection.Execute($@"UPDATE child SET firstname = @Firstname, lastname = @Lastname, age = @Age, class_id = @Classid WHERE id = @Id", new {Firstname = firstname, Lastname = lastname, Age = age, Classid = classid, Id = id});               
            }

        }
  
        // lägg till nytt barn
        public static void AddNewChild(string firstname, string lastname, int age, int classid)
        {

            InputHandler inputhandler = new InputHandler();
            var a = inputhandler.Uppercase(firstname);
            var b = inputhandler.Uppercase(lastname);

            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                connection.Execute($@"INSERT INTO child (firstname, lastname, age, class_id) VALUES (@Firstname, @Lastname, @Age, @Classid)", new {Firstname = firstname, Lastname = lastname, Age = age, Classid = classid });
                             
            }

        }
        // tar bort barn
        public static void DeleteChild()
        {
            var id = Activechild.Id;

            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {

                connection.Execute($@"DELETE FROM child WHERE id = @Id;", new {Id = id});
                
            }

        }

       // Hämtar tabellen guardian_child med namn för att se kopplingar mellan barn och vårdnadshavare
        public static List<Connections> GetChildGuardian()
        {

            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                //LÄGG TILL I TABELL GUARDIAN_CHILD
                var output = connection.Query<Connections>
                    ($@"SELECT guardian_id,  guardian.firstname ||' '|| guardian.lastname AS Guardian, child_id, child.firstname ||' '|| child.lastname AS Child
                    FROM((guardian_child INNER JOIN child ON child_id = child.id AND child.firstname = child.firstname AND child.lastname = child.lastname) 
                    INNER JOIN guardian ON guardian_id = guardian.id AND guardian.firstname = guardian.firstname AND guardian.lastname = guardian.lastname);").ToList();

                return output;
            }

}

        // Koppla ihop vårdnadshavare och barn
        public static void ConnectChildAndGuardian()
        {

            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
               
                connection.Execute($@"INSERT INTO guardian_child(guardian_id, child_id) VALUES(@GuardianID, @ChildID); INSERT INTO meals (name, guardian_id, child_id) VALUES('frukost', @GuardianID, @ChildID)", new { GuardianID = Activeguardian.Id, ChildID = Activechild.Id});
                  
            }

        }

        // ta bort koppling mellan vårdnadshavare och barn
        public static void DeleteConnection()
        {

            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {

                connection.Execute($@"DELETE FROM guardian_child 
                WHERE guardian_id = @GuardianID AND child_id = @ChildID; 
                DELETE FROM meals WHERE meals.id = @ChildMealsID", new {GuardianID = Activeguardian.Id, ChildID = Activechild.Id, ChildMealsID = Activechild.Mealsid});
                
            }
        }

        // Tar bort vårdnadshavare
        public static void DeleteGuardian()
        {
            var id = Activeguardian.Id;

            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {

                connection.Execute($@"DELETE FROM guardian WHERE id = @Id", new {Id = id});
               
            }

        }

        //Hämtar Avdelning och Telefonnummer 
        public static List<Department> ContactDepartment()
        {
            List<Department> departments = new List<Department>();

            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {

                var output = connection.Query<Department>($@"SELECT * FROM Department").ToList();

                return output;
            }
        }

        //Lägg till frånvaro som Guardian
        public static void GuardianReportAttendance(string comment)
        {

            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                connection.Execute($@"INSERT INTO attendance (child_id, guardian_id, category_attendance_id, comment)
                                                                    VALUES (@ChildID, @GuardianID, @AttendanceCategoryID, @Comment);
                                                             INSERT INTO attendance_dates (attendance_id, dates_id) 
                                                                    SELECT attendance.id, dates.id 
                                                                    FROM attendance, dates 
                                                                    WHERE attendance.id = (SELECT MAX(attendance.id) 
                                                                    FROM attendance) AND dates.id = @DateID", new {ChildID = Activechild.Id, GuardianID = Activeguardian.Id, AttendanceCategoryID = ActiveAttendancecategory.Id, Comment = comment, DateID = ActiveDate.Id});
              
            }

        }

        // rapportera frånvaro som staff
        public static void StaffReportAttendance(string comment)
        {

            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                connection.Execute($@"INSERT INTO attendance(child_id, staff_id, category_attendance_id, comment)
                VALUES(@ChildID, @StaffID, @AttendanceCategoryID, @Comment);
                INSERT INTO attendance_dates(attendance_id, dates_id)
                SELECT attendance.id, dates.id
                FROM attendance, dates
                WHERE attendance.id = (SELECT MAX(attendance.id)
                FROM attendance) AND dates.id = @DateID", new {ChildID = Activechild.Id, StaffID = Activestaff.Id, AttendanceCategoryID = ActiveAttendancecategory.Id, Comment = comment, DateID = ActiveDate.Id});

                
            }

        }

        //Hämta category attendances
        public static List<Attendancecategory> GetAttendances()
        {

            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Attendancecategory>($@"SELECT * FROM category_attendance WHERE present = false").ToList();
                return output;
            }

        }

        //Hämtar alla datum
        public static List<Date> GetDays(Weeks week)
        {
            var weeks = week.Week;

            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Date>($@"SELECT dates.id, week, dates.day 
                            FROM dates 
                            WHERE week=@Week
                            GROUP BY dates.id, week, dates.day
                            ORDER BY dates.id ASC", new {Week = weeks}).ToList();
                return output;
            }

        }

        //Hämtar alla Veckor
        public static List<Weeks> GetWeek()
        {

            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Weeks>($@"SELECT week FROM dates GROUP BY week ORDER BY week ASC").ToList();


                return output;
            }

        }

        //Hämtar alla Frånvaro till Förälder
        public static List<Attendance> Getabscenceasguardian()
        {

            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Attendance>($@"SELECT child.id, attendance.id AS Id, category_attendance.name_type AS Category_attendance, dates.day AS Day, dates.week AS Week, attendance.comment AS Comment
                                                             FROM ((((attendance 
                                                             INNER JOIN child ON child_id=child.id)   
                                                             INNER JOIN attendance_dates ON attendance_id=attendance.id)
                                                             INNER JOIN dates ON dates_id=dates.id)
                                                             INNER JOIN category_attendance ON category_attendance_id = category_attendance.id) 
                                                             where child_id = {Activechild.Id} AND (category_attendance_id = 1 OR category_attendance_id = 2)").ToList();


                return output;
            }

        }

        //uppdatera mail på Staff 
        public static void UpdateStaffProperties(string email)
        {
            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                connection.Execute($@"UPDATE staff SET email = @Email WHERE id = @StaffID", new {Email = email, StaffID = Activestaff.Id});
               
            }

        }

        //Lägg till fritids som Guardian utan frukost
        public static void GuardianReportFritids(string comment, int attendanceid)
        {

            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                connection.Execute($@"INSERT INTO attendance (child_id, guardian_id, category_attendance_id, comment)
                VALUES (@ChildID, @GuardianID, @AttendanceID, @Comment);
                INSERT INTO attendance_dates (attendance_id, dates_id) 
                SELECT attendance.id, dates.id 
                FROM attendance, dates 
                WHERE attendance.id = (SELECT MAX(attendance.id) 
                FROM attendance) AND dates.id = @DateID", new {ChildID = Activechild.Id, GuardianID = Activeguardian.Id, AttendanceID = attendanceid, Comment = comment, DateID = ActiveDate.Id});
                
            }

        }

        //Lägg till fritids som Guardian med frukost
        public static void GuardianReportFritidsBreakfast(string comment, int attendanceid)
        {

            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                connection.Execute($@"INSERT INTO attendance (child_id, guardian_id, category_attendance_id, comment)
                VALUES (@ChildID, @GuardianID, @AttendanceID, @Comment);
                INSERT INTO attendance_dates (attendance_id, dates_id) 
                SELECT attendance.id, dates.id 
                FROM attendance, dates 
                WHERE attendance.id = (SELECT MAX(attendance.id) 
                FROM attendance) AND dates.id = @DateID;
                INSERT INTO meals_dates (meals_id, dates_id) 
			    VALUES (@ChildMealsID, @DateID);", new {ChildID = Activechild.Id, GuardianID = Activeguardian.Id, AttendanceID = attendanceid, Comment = comment, DateID = ActiveDate.Id, ChildMealsID = Activechild.Mealsid});
                
            }

        }


        //Hämtar alla anmälda fritidsdagar till förälder 
        public static List<Attendance> Getfritidsguardian()
        {

            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Attendance>($@"SELECT child_id, attendance.id AS Id, category_attendance_id, category_attendance.name_type AS Category_attendance, dates.day AS Day, dates.week AS Week, comment AS Comment 
                    FROM ((((attendance
                    INNER JOIN child on child_id = child.id)
                    INNER JOIN category_attendance on category_attendance_id = category_attendance.id)
                    INNER JOIN attendance_dates on attendance.id = attendance_id)
                    INNER JOIN dates on dates_id = dates.id)
                    WHERE child_id = @ChildID AND (category_attendance_id = 3 OR category_attendance_id = 7)", new {ChildID = Activechild.Id}).ToList(); 

                return output;
            }

        }

        //Hämtar anmäld frukost 
        public static List<Meal> GetMeals()
        {
            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Meal>($@"SELECT dates.id AS DatesId, dates.day AS Day, dates.week AS Week, meals.name AS Name, meals.id AS Id
                    FROM (((child                   
                    INNER JOIN meals on child.id=meals.child_id)
                    INNER JOIN meals_dates ON meals.id=meals_id)
                    INNER JOIN dates on meals_dates.dates_id=dates.id)
                    WHERE child.id=@ChildID
                    GROUP BY dates.id, dates.day, dates.week, meals.name, meals.id
                    ORDER BY dates.id ASC", new {ChildID = Activechild.Id}).ToList();

                return output;
            }
        }

        //Hämtar Fritids morgon/kväll
        public static List<Attendancecategory> GetFritidsMorningEvening()
        {
            using (IDbConnection connection = new NpgsqlConnection(ConnString.ConnVal("dbConn")))
            {
                var output = connection.Query<Attendancecategory>($@"SELECT * FROM category_attendance WHERE (id = 3 OR id = 7 OR id = 8)
                                                                     ORDER BY category_attendance.id DESC").ToList();


                return output;
            }
        }
    }
}

