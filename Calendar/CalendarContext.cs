using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace Calendar
{
    public class CalendarContext : DbContext
    {
        // Your context has been configured to use a 'CalendarContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'CalendarClass.CalendarContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'CalendarContext' 
        // connection string in the application configuration file.
        public CalendarContext()
            : base("name=CalendarContext")
        {
        }

        public DbSet<Day> Dates { get; set; }
        public DbSet<Event> Events { get; set; }
    }

    //public class CalendarDbInitializer : DropCreateDatabaseAlways<CalendarContext>
    //{

    //    protected override void Seed(CalendarContext context)
    //    {
    //        var events = new List<Event>
    //            {
    //                new Event() { Id=1, Name="Doctor", Description="Anything"},
    //                new Event() { Id=2, Name="Shopping", Description="Anything" },
    //                new Event() { Id=3, Name="Hairdresser", Description = "Anything"}
    //            };
    //        events.ForEach(c => context.Events.Add(c));
    //        context.SaveChanges();

    //        List<Course> courses_db = context.Courses.ToList();

    //        var students = new List<Student>
    //            {
    //                new Student()
    //                {
    //                    ID=1001,
    //                    StudentName="Andrew Peters",
    //                    Enrollments = new Collection<Enrollment> {
    //                        new Enrollment()
    //                        {
    //                            ID = 3001,
    //                            CourseID = courses_db[0].ID,
    //                            StudentID = 1001,
    //                        },
    //                        new Enrollment()
    //                        {
    //                            ID = 3002,
    //                            CourseID = courses_db[1].ID,
    //                            StudentID = 1001,
    //                        }
    //                    }
    //                },
    //                new Student()
    //                {
    //                    ID=1002,
    //                    StudentName="Brice Lambson",
    //                    Enrollments = new Collection<Enrollment> {
    //                        new Enrollment()
    //                        {
    //                            ID = 3003,
    //                            CourseID = courses_db[0].ID,
    //                            StudentID = 1002,
    //                        },
    //                        new Enrollment()
    //                        {
    //                            ID = 3004,
    //                            CourseID = courses_db[2].ID,
    //                            StudentID = 1002,
    //                        }
    //                    }
    //                },
    //                new Student()
    //                {
    //                    ID=1003,
    //                    StudentName="Rowan Miller",
    //                    Enrollments = new Collection<Enrollment> {
    //                        new Enrollment()
    //                        {
    //                            ID = 3005,
    //                            CourseID = courses_db[1].ID,
    //                            StudentID = 1003,
    //                        },
    //                        new Enrollment()
    //                        {
    //                            ID = 3006,
    //                            CourseID = courses_db[2].ID,
    //                            StudentID = 1003,
    //                        }
    //                    }
    //                }
    //            };
    //        students.ForEach(s => context.Students.Add(s));
    //        context.SaveChanges();
    //    }
    //}
}