using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assignment01.Model;
using Assignment01.Utility;

namespace Assignment01.Controller
{
    class CourseController
    {
        private Dictionary<String, Course> coursesList = new Dictionary<string, Course>();

        internal bool CreateCourse(string courseID, string courseName, string courseDes)
        {
            courseID = courseID.ToLower();
            if (!Validate.ValidCourseID(courseID))
                return false;
            bool duplicated = CheckCourseDuplicated(courseID, coursesList);
            if (duplicated)
            {
                Console.WriteLine("\nSorry! you are detected to add an already course into the system.");
                return false;
            }
            else
            {
                Course course = new Course(courseID, courseName, courseDes);
                coursesList.Add(courseID, course);
                return true;
            }
        }

        private bool CheckCourseDuplicated(string courseID, Dictionary<string, Course> coursesList)
        {
            foreach (KeyValuePair<string, Course> KeysOfCourseList in coursesList)
            {
                if (KeysOfCourseList.Key.Equals(courseID))
                    return true;
            }
            return false;
        }

        internal int CourseCount()
        {
            return coursesList.Count;
        }

        internal Dictionary<string, Course> RetrieveCourseList()
        {
            return coursesList;
        }

        internal Course SearchCourse(string courseIdToChange)
        {
            if (coursesList.ContainsKey(courseIdToChange))
                return coursesList[courseIdToChange];
            return null;
        }

        internal bool UpdateCourse(string updatedId, string updatedName, string updatedDes)
        {
            if (Validate.ValidCourseID(updatedId))
            {
                if (CreateCourse(updatedId, updatedName, updatedDes))
                    return true;
            }
            else
                Console.WriteLine("Sorry! Update id is not valid.");
            return false;
            
        }

        internal void DeleteCourse(string courseIdToChange)
        {
            coursesList.Remove(courseIdToChange);
        }

        internal List<Course> GetCourses()
        {
            List<Course> r = new List<Course>();
            foreach (KeyValuePair<string, Course> kvp in coursesList)
            {
                r.Add(kvp.Value);
            }
            return r;
        }
    }
}
