using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assignment01.Model;

namespace Assignment01.Controller
{
    class ReportController
    {
        private CourseController courseController;
        private ClassController classController;

        public ReportController(CourseController courseController, ClassController classController)
        {
            // TODO: Complete member initialization
            this.courseController = courseController;
            this.classController = classController;
        }

        internal void PrintEnrolReport()
        {
            Dictionary<string, Class> classList = classController.RetrieveClassList();
            Dictionary<string, Course> courseList = courseController.RetrieveCourseList();
            
            if (courseList.Count == 0)
            {
                Console.WriteLine("Please add class!");
            }
            else
            {
                foreach (KeyValuePair<string, Course> KeyValOfCourse in courseList)
                {
                    Console.WriteLine("Course " + KeyValOfCourse.Value.Name);
                    foreach (KeyValuePair<string, Class> KeyValOfClass in classList)
                    {
                        if (KeyValOfClass.Value.Course.Id.Equals(KeyValOfCourse.Value.Id))
                        {
                            Console.WriteLine("+ Class " + KeyValOfClass.Value.ToString() + " (Lecturer " + KeyValOfClass.Value.Lecturer.ToString() + ")");

                            foreach (Student student in KeyValOfClass.Value.StudentList)
                            {
                                Console.WriteLine("\t+ " + student.ToString());
                            }
                        }
                    }
                }
            }
            Console.Write("[Enter] to continue...");
            Console.ReadLine();
        }
    }
}
