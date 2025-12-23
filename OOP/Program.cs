/*----------- >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Description <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< ------------------------------------------
-- Developer Name  : Mahmoud Faragallah
-- Creation Date   : 23 - 12 - 2025
-- Modify Date     : 23 - 12 - 2025
-- Description     : Lec7 Task (Student Management System)
---------- - >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Core of Procedure <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<-------------------------------------------*/

using System;

namespace StudentManagementSystem
{
    // ===================== Student =====================
    public class Student
    {
        public int StudentId;
        public string Name;
        public int Age;

        public Course[] Courses;
        public int CourseCount;

        public Student(int id, string name, int age)
        {
            StudentId = id;
            Name = name;
            Age = age;
            Courses = new Course[10]; // max courses per student
            CourseCount = 0;
        }

        public bool Enroll(Course course)
        {
            for (int i = 0; i < CourseCount; i++)
            {
                if (Courses[i] == course)
                    return false;
            }

            Courses[CourseCount++] = course;
            return true;
        }

        public string PrintDetails()
        {
            string courseNames = CourseCount == 0 ? "None" : "";

            for (int i = 0; i < CourseCount; i++)
            {
                courseNames += Courses[i].Title;
                if (i < CourseCount - 1)
                    courseNames += ", ";
            }

            return $"ID: {StudentId}, Name: {Name}, Age: {Age}, Courses: {courseNames}";
        }
    }

    // ===================== Instructor =====================
    public class Instructor
    {
        public int InstructorId;
        public string Name;
        public string Specialization;

        public Instructor(int id, string name, string specialization)
        {
            InstructorId = id;
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
        public int CourseId;
        public string Title;
        public Instructor Instructor;

        public Course(int id, string title, Instructor instructor)
        {
            CourseId = id;
            Title = title;
            Instructor = instructor;
        }

        public string PrintDetails()
        {
            return $"ID: {CourseId}, Title: {Title}, Instructor: {Instructor.Name}";
        }
    }

    // ===================== SchoolStudentManager =====================
    public class SchoolStudentManager
    {
        public Student[] Students = new Student[50];
        public Course[] Courses = new Course[50];
        public Instructor[] Instructors = new Instructor[50];

        public int StudentCount = 0;
        public int CourseCount = 0;
        public int InstructorCount = 0;

        public bool AddStudent(Student student)
        {
            if (FindStudent(student.StudentId) != null)
                return false;

            Students[StudentCount++] = student;
            return true;
        }

        public bool AddInstructor(Instructor instructor)
        {
            if (FindInstructor(instructor.InstructorId) != null)
                return false;

            Instructors[InstructorCount++] = instructor;
            return true;
        }

        public bool AddCourse(Course course)
        {
            if (FindCourse(course.CourseId) != null)
                return false;

            Courses[CourseCount++] = course;
            return true;
        }

        public Student FindStudent(int id)
        {
            for (int i = 0; i < StudentCount; i++)
                if (Students[i].StudentId == id)
                    return Students[i];

            return null;
        }

        public Instructor FindInstructor(int id)
        {
            for (int i = 0; i < InstructorCount; i++)
                if (Instructors[i].InstructorId == id)
                    return Instructors[i];

            return null;
        }

        public Course FindCourse(int id)
        {
            for (int i = 0; i < CourseCount; i++)
                if (Courses[i].CourseId == id)
                    return Courses[i];

            return null;
        }

        public bool EnrollStudentInCourse(int studentId, int courseId)
        {
            Student student = FindStudent(studentId);
            Course course = FindCourse(courseId);

            if (student == null || course == null)
                return false;

            return student.Enroll(course);
        }

        // ========= BONUS 11 =========
        public bool IsStudentEnrolledInCourse(int studentId, string courseTitle)
        {
            Student student = FindStudent(studentId);
            if (student == null)
                return false;

            for (int i = 0; i < student.CourseCount; i++)
            {
                if (student.Courses[i].Title == courseTitle)
                    return true;
            }

            return false;
        }

        // ========= BONUS 12 =========
        public string GetInstructorNameByCourseName(string courseTitle)
        {
            for (int i = 0; i < CourseCount; i++)
            {
                if (Courses[i].Title == courseTitle)
                    return Courses[i].Instructor.Name;
            }

            return "Course not found";
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
                Console.WriteLine("12. Get Instructor by Course

