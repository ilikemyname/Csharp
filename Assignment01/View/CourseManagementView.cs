using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assignment01.Controller;
using Assignment01.Utility;
using Assignment01.Model;

namespace Assignment01.View
{
    class CourseManagementView
    {
        private CourseController courseController;
        public CourseManagementView(CourseController courseController)
        {
            this.courseController = courseController;
        }

        internal void AddCourse()
        {
            Console.Write("Please provide a course id: ");
            var courseID = Console.ReadLine();
            while(!Validate.ValidCourseID(courseID))
            {
                Console.WriteLine("Course id violated standard format 4 alphabets prefix 4 digits suffix. Please retry as (COSC2425): ");
                courseID = Console.ReadLine();
            }
            Console.Write("Please provide course name: ");
            var courseName = Console.ReadLine();
            while (courseName == "")
            {
                Console.Write("Blank! Please enter course name: ");
                courseName = Console.ReadLine();
            }

            Console.Write("Please provide course description: ");
            var courseDes = Console.ReadLine();
            while(courseDes == "")
            {
                Console.Write("Blank! Please enter course description: ");
                courseDes = Console.ReadLine();
            }

            bool added = courseController.CreateCourse(courseID, courseName, courseDes);
            if (added)
                Console.WriteLine("\nCongratulation! You added a new \"{0}\" course", courseID);
            else
                Console.WriteLine("\nSorry! You failed to add a course.");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadLine();
        }

        internal void DisplayCourses()
        {
            int coursesCount = courseController.CourseCount();
            if (coursesCount == 0)
                Console.WriteLine("Total number of live course is zero");
            else
            {
                Dictionary<string, Course> courseList = courseController.RetrieveCourseList();
                foreach (KeyValuePair<string, Course> KeysValuesOfCourseList in courseList)
                {
                    Console.WriteLine("\t\tCourse id: {0}\n"+
                                      "\t\tCourse name: {1}\n"+
                                      "\t\tCourse Description: {2}\n",
                                      KeysValuesOfCourseList.Key,
                                      KeysValuesOfCourseList.Value.Name,
                                      KeysValuesOfCourseList.Value.Description);
                }
            }
            Console.WriteLine();
        }

        internal void EditCourse()
        {
            int courseCount = courseController.CourseCount();
            if (courseCount > 0)
            {
                Console.WriteLine("Please enter only one course id as displayed below");
                DisplayCourses();
                Console.Write("> ");
                var courseIdToChange = Console.ReadLine();
                while(courseController.SearchCourse(courseIdToChange) == null)
                {
                    Console.WriteLine("Sorry! The course you wanna edit does not exist. Try again!");
                    Console.Write("> ");
                    courseIdToChange = Console.ReadLine();
                }
                Course oldCourse = courseController.SearchCourse(courseIdToChange);
                Console.Write("You will change old id \"{0}\" to new id or press [Enter] to unchange: ", oldCourse.Id);
                string updatedId = Console.ReadLine();
                Console.Write("You will change old name \"{0}\" to new name or press [Enter] to unchange: ", oldCourse.Name);
                string updatedName = Console.ReadLine();
                Console.Write("You will change old description \"{0}\" to new description or press [Enter] to unchange: ", oldCourse.Description);
                string updatedDes = Console.ReadLine();
                if (updatedId == "")
                    updatedId = oldCourse.Id;
                if (updatedName == "")
                    updatedName = oldCourse.Name;
                if (updatedDes == "")
                    updatedDes = oldCourse.Description;
                // Keep old information of course to modify if it happens error during updating
                string id = oldCourse.Id;
                string name = oldCourse.Name;
                string des = oldCourse.Description;

                // Delete course to edit by its id
                courseController.DeleteCourse(courseIdToChange);

                if (courseController.UpdateCourse(updatedId, updatedName, updatedDes))
                {
                    Console.WriteLine("You updated new information for course id: " + updatedId);
                }
                else
                {
                    courseController.CreateCourse(id, name, des);
                    Console.WriteLine();
                    Console.WriteLine("You failed to update new information for course id: " + id);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Opp! empty course list. You should use selection 1 to add a course.");
            }
            Console.WriteLine("Press[Enter] button to continue...");
            Console.ReadLine();
        }

        internal void DeleteCourse()
        {
            Console.WriteLine("Delete the course");
            Console.WriteLine("Enter only one course id as displayed below to delete that course");
            DisplayCourses();
            Console.Write("> ");
            string idToDel = Console.ReadLine();
            if(idToDel != "")
            {
                if (courseController.SearchCourse(idToDel) != null)
                {
                    courseController.DeleteCourse(idToDel);
                    Console.WriteLine("You have deleted course \"{0}\".", idToDel);
                }
                else
                {
                    Console.WriteLine("Sorry! The course you wanna delete has never existed.");
                }
            }
            Console.WriteLine("Press[Enter] button to continue...");
            Console.ReadLine();
        }
    }
}
