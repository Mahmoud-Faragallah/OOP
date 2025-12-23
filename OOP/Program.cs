/*----------- >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Description <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< ------------------------------------------
-- Developer Name  : Mahmoud Faragallah
-- Creation Date   : 23 - 12 - 2025
-- Modify Date     : 23 - 12 - 2025
-- Description     : Lec7 Task (Student Management System)
---------- - >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Core of Procedure <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<-------------------------------------------*/

using System;
using System.Collections.Generic;

namespace StudentManagementSystem
{
    // ===================== Student =====================
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<Course> Courses { get; set; }

        public Student(int studentId, string name, int age)
        {
            StudentId = studentId;
            Name = name;
            Age = age;
            Courses = new List<Course>();
        }

        public bool Enroll(Course course)
        {
            if (Courses.Contains(course))
                return false;

            Courses.Add(course);
            return true;
        }

        public string PrintDetails()
        {
            string courseList = Courses.Count == 0
                ? "None"
                : string.Join(", ", Courses.ConvertAll(c => c.Title));

            return $"ID: {StudentId}, Name: {Name}, Age: {Age}, Courses: {courseList}";
        }
    }

    // ===================== Instructor =====================
    public class Instructor
    {
        public int InstructorId { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }

        public Instructor(int instructorId, string name, string specialization)
        {
            InstructorId = instructorId;
            Name = name;
            Specialization = specialization;
        }

        public string PrintDetails()
        {
            return $"ID: {InstructorId}, Name: {Name}, Specialization: {Specialization}";
        }
    }

    // ===================== Course =====================
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public Instructor Instructor { get; set; }

        public Course(int courseId, string title, Instructor instructor)
        {
            CourseId = courseId;
            Title = title;
            Instructor = instructor;
        }

        public string PrintDetails()
        {
            return $"ID: {CourseId}, Title: {Title}, Instructor: {Instructor.Name}";
        }
    }

    // ===================== SchoolStudentManager (IMPORTANT NAME) =====================
    public class SchoolStudentManager
    {
        public List<Student> Students { get; set; }
        public List<Course> Courses { get; set; }
        public List<Instructor> Instructors { get; set; }

        public SchoolStudentManager()
        {
            Students = new List<Student>();
            Courses = new List<Course>();
            Instructors = new List<Instructor>();
        }

        public bool AddStudent(Student student)
        {
            if (FindStudent(student.StudentId) != null)
                return false;

            Students.Add(student);
            return true;
        }

        public bool AddInstructor(Instructor instructor)
        {
            if (FindInstructor(instructor.InstructorId) != null)
                return false;

            Instructors.Add(instructor);
            return true;
        }

        public bool AddCourse(Course course)
        {
            if (FindCourse(course.CourseId) != null)
                return false;

            Courses.Add(course);
            return true;
        }

        public Student FindStudent(int studentId)
        {
            return Students.Find(s => s.StudentId == studentId);
        }

        public Course FindCourse(int courseId)
        {
            return Courses.Find(c => c.CourseId == courseId);
        }

        public Instructor FindInstructor(int instructorId)
        {
            return Instructors.Find(i => i.InstructorId == instructorId);
        }

        public bool EnrollStudentInCourse(int studentId, int courseId)
        {
            Student student = FindStudent(studentId);
            Course course = FindCourse(courseId);

            if (student == null || course == null)
                return false;

            return student.Enroll(course);
        }

        // BONUS
        public bool IsStudentEnrolledInCourse(int studentId, string courseName)
        {
            Student student = FindStudent(studentId);
            if (student == null)
                return false;

            return student.Courses.Exists(c => c.Title == courseName);
        }

        // BONUS
        public string GetInstructorNameByCourseName(string courseName)
        {
            Course course = Courses.Find(c => c.Title == courseName);
            return course == null ? "Course not found" : course.Instructor.Name;
        }
    }

    // ===================== Program =====================
    class Program
    {
        static void Main()
        {
            SchoolStudentManager manager = new SchoolStudentManager();
            int choice;

            do
            {
                Console.WriteLine("\n===== Student Management System =====");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Add Instructor");
                Console.WriteLine("3. Add Course");
                Console.WriteLine("4. Enroll Student in Course");
                Console.WriteLine("5. Show All Students");
                Console.WriteLine("6. Show All Courses");
                Console.WriteLine("7. Show All Instructors");
                Console.WriteLine("8. Find Student by ID");
                Console.WriteLine("9. Find Course by ID");
                Console.WriteLine("10. Exit");
                Console.WriteLine("11. Check Student Enrollment (Bonus)");
                Console.WriteLine("12. Get Instructor by Course Name (Bonus)");
                Console.Write("Enter choice: ");

                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Write("Student ID: ");
                        int sid = int.Parse(Console.ReadLine());
                        Console.Write("Name: ");
                        string sname = Console.ReadLine();
                        Console.Write("Age: ");
                        int age = int.Parse(Console.ReadLine());
                        manager.AddStudent(new Student(sid, sname, age));
                        break;

                    case 2:
                        Console.Write("Instructor ID: ");
                        int iid = int.Parse(Console.ReadLine());
                        Console.Write("Name: ");
                        string iname = Console.ReadLine();
                        Console.Write("Specialization: ");
                        string spec = Console.ReadLine();
                        manager.AddInstructor(new Instructor(iid, iname, spec));
                        break;

                    case 3:
                        Console.Write("Course ID: ");
                        int cid = int.Parse(Console.ReadLine());
                        Console.Write("Title: ");
                        string title = Console.ReadLine();
                        Console.Write("Instructor ID: ");
                        int insId = int.Parse(Console.ReadLine());

                        Instructor instructor = manager.FindInstructor(insId);
                        if (instructor != null)
                            manager.AddCourse(new Course(cid, title, instructor));
                        else
                            Console.WriteLine("Instructor not found");
                        break;

                    case 4:
                        Console.Write("Student ID: ");
                        sid = int.Parse(Console.ReadLine());
                        Console.Write("Course ID: ");
                        cid = int.Parse(Console.ReadLine());
                        Console.WriteLine(manager.EnrollStudentInCourse(sid, cid)
                            ? "Enrolled successfully"
                            : "Enrollment failed");
                        break;

                    case 5:
                        manager.Students.ForEach(s => Console.WriteLine(s.PrintDetails()));
                        break;

                    case 6:
                        manager.Courses.ForEach(c => Console.WriteLine(c.PrintDetails()));
                        break;

                    case 7:
                        manager.Instructors.ForEach(i => Console.WriteLine(i.PrintDetails()));
                        break;

                    case 8:
                        Console.Write("Student ID: ");
                        sid = int.Parse(Console.ReadLine());
                        Student st = manager.FindStudent(sid);
                        Console.WriteLine(st == null ? "Student not found" : st.PrintDetails());
                        break;

                    case 9:
                        Console.Write("Course ID: ");
                        cid = int.Parse(Console.ReadLine());
                        Course cr = manager.FindCourse(cid);
                        Console.WriteLine(cr == null ? "Course not found" : cr.PrintDetails());
                        break;

                    case 11:
                        Console.Write("Student ID: ");
                        sid = int.Parse(Console.ReadLine());
                        Console.Write("Course Name: ");
                        string cname = Console.ReadLine();
                        Console.WriteLine(manager.IsStudentEnrolledInCourse(sid, cname));
                        break;

                    case 12:
                        Console.Write("Course Name: ");
                        cname = Console.ReadLine();
                        Console.WriteLine(manager.GetInstructorNameByCourseName(cname));
                        break;
                }

            } while (choice != 10);
        }
    }
}
