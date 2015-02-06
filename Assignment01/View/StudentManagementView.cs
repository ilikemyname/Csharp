using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assignment01.Utility;
using Assignment01.Model;
using Assignment01.Controller;

namespace Assignment01.View
{
    class StudentManagementView
    {
        private StudentController studentController;
        private CourseController courseController;
        private ClassController classController;
        private LecturerController lecturerController;
        private RoomController roomController;

        public StudentManagementView(StudentController studentController, CourseController courseController, ClassController classController, LecturerController lecturerController, RoomController roomController)
        {
            this.studentController = studentController;
            this.courseController = courseController;
            this.classController = classController;
            this.lecturerController = lecturerController;
            this.roomController = roomController;
        }

        internal void AddStudent()
        {
            Console.Write("Please provide a student id: ");
            var studentID = Console.ReadLine();
            while (!Validate.ValidStudentId(studentID))
            {
                Console.WriteLine("Student id violated standard format 's3' prefix, 6 digits suffix. Please retry as (s3xxxxxx): ");
                studentID = Console.ReadLine();
            }

            Console.Write("Please provide first name: ");
            var firstName = Console.ReadLine();
            while (!Validate.ValidNameString(firstName))
            {
                Console.WriteLine("First name does not allow space and number");
                firstName = Console.ReadLine();
            }

            Console.Write("Please provide middle name: ");
            var middleName = Console.ReadLine();
            while (!Validate.ValidNameString(middleName))
            {
                Console.WriteLine("Middle name does not allow space and number");
                middleName = Console.ReadLine();
            }

            Console.Write("Please provide last name: ");
            var lastName = Console.ReadLine();
            while (!Validate.ValidNameString(lastName))
            {
                Console.WriteLine("Last name does not allow space and number");
                lastName = Console.ReadLine();
            }

            bool added = studentController.CreateStudent(studentID, firstName, middleName, lastName);
            if (added)
                Console.WriteLine("\nCongratulation! You added a new student has id: " + studentID);
            else
                Console.WriteLine("\nSorry! You failed to add a student " + studentID);
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

        internal void EditStudent()
        {
            int studentCount = studentController.StudentCount();
            if (studentCount > 0)
            {
                Console.WriteLine("Please enter only one student id as displayed below");
                DisplayStudent();
                Console.Write("> ");
                var studentIdToChange = Console.ReadLine();
                while (studentController.SearchStudent(studentIdToChange) == null)
                {
                    Console.WriteLine("Sorry! The student you wanna edit does not exist. Try again!");
                    Console.Write("> ");
                    studentIdToChange = Console.ReadLine();
                }
                Student oldStudent = studentController.SearchStudent(studentIdToChange);
                Console.Write("You will change old id \"{0}\" to new id or press [Enter] to unchange: ", oldStudent.Id);
                string updatedId = Console.ReadLine();
                Console.Write("You will change old first name \"{0}\" to new name or press [Enter] to unchange: ", oldStudent.FirstName);
                string updatedFName = Console.ReadLine();
                Console.Write("You will change old middle name \"{0}\" to new name or press [Enter] to unchange: ", oldStudent.MiddleName);
                string updatedMName = Console.ReadLine();
                Console.Write("You will change old last name \"{0}\" to new name or press [Enter] to unchange: ", oldStudent.LastName);
                string updatedLName = Console.ReadLine();

                if (updatedId == "")
                    updatedId = oldStudent.Id;
                if (updatedFName == "")
                    updatedFName = oldStudent.FirstName;
                if (updatedMName == "")
                    updatedMName = oldStudent.MiddleName;
                if (updatedLName == "")
                    updatedLName = oldStudent.LastName;

                // Keep old information of student to modify if it happens error during updating
                string id = oldStudent.Id;
                string fName = oldStudent.FirstName;
                string mName = oldStudent.MiddleName;
                string lName = oldStudent.LastName;

                // Delete course to edit by its id
                studentController.DeleteStudent(studentIdToChange);

                if (studentController.UpdateStudent(updatedId, updatedFName, updatedMName, updatedLName))
                {
                    Console.WriteLine("You updated new information for student id: " + updatedId);
                }
                else
                {
                    // Recreate old lecturer if updating fail
                    studentController.CreateStudent(id, fName, mName, lName);
                    Console.WriteLine("You failed to update new information for student id: " + id);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Opp! empty student list. You should use selection 1 to add a student.");
            }
            Console.WriteLine("Press[Enter] button to continue...");
            Console.ReadLine();
        }

        internal void DeleteStudent()
        {
            Console.WriteLine("Delete the student");
            Console.WriteLine("Enter only one student id as displayed below to delete that student");
            DisplayStudent();
            Console.Write("> ");
            string idToDel = Console.ReadLine();
            if (idToDel != "")
            {
                if (studentController.SearchStudent(idToDel) != null)
                {
                    studentController.DeleteStudent(idToDel);
                    Console.WriteLine("You have deleted student \"{0}\".", idToDel);
                }
                else
                {
                    Console.WriteLine("Sorry! The student you wanna delete has never existed.");
                }
            }
            Console.WriteLine("Press[Enter] button to continue...");
            Console.ReadLine();
        }

        internal void DisplayStudent()
        {
            int studentsCount = studentController.StudentCount();
            if (studentsCount == 0)
                Console.WriteLine("Total number of added student is zero");
            else
            {
                Dictionary<string, Student> studentList = studentController.RetrieveStudentList();
                foreach (KeyValuePair<string, Student> KeysValuesOfStudentList in studentList)
                {
                    Console.WriteLine("\t\tStudent id: {0}\n" +
                                      "\t\tStudent first name: {1}\n" +
                                      "\t\tStudent middle name: {2}\n" +
                                      "\t\tStudent last name: {3}\n",
                                      KeysValuesOfStudentList.Key,
                                      KeysValuesOfStudentList.Value.FirstName,
                                      KeysValuesOfStudentList.Value.MiddleName,
                                      KeysValuesOfStudentList.Value.LastName);
                }
            }
            Console.WriteLine();
        }

        internal void EnrolStudent()
        {
            if (courseController.CourseCount() == 0)
            {
                Console.WriteLine("Please add course!");
            }
            else if (lecturerController.LecturerCount() == 0)
            {
                Console.WriteLine("Please add lecture!");
            }
            else if (roomController.RoomCount() == 0)
            {
                Console.WriteLine("Please add room!");
            }
            else if (studentController.StudentCount() == 0)
            {
                Console.WriteLine("Please add some students!");
            }
            else if (classController.ClassesCount() == 0)
            {
                Console.WriteLine("Please add class!");
            }
            else
            {
                Console.WriteLine("\t\t** Student List **");
                DisplayStudent();
                Console.Write("\nSelect one student id: ");
                string id = Console.ReadLine();
                while (studentController.SearchStudent(id) == null)
                {
                    Console.WriteLine("student not found!Please try again");
                    Console.Write("> ");
                    id = Console.ReadLine();
                }
                Student student = studentController.SearchStudent(id);

                Console.WriteLine("\t\t** Course List **");
                CourseManagementView coursemv = new CourseManagementView(courseController);
                coursemv.DisplayCourses();
                Console.Write("\nSelect one course id: ");
                string crid = Console.ReadLine();
                while (courseController.SearchCourse(crid) == null)
                {
                    Console.WriteLine("\ncourse not found!Please try again");
                    Console.Write("> ");
                    crid = Console.ReadLine();
                }
                Course course = courseController.SearchCourse(crid);

                ClassManagementView classmv = new ClassManagementView(classController, courseController, lecturerController, roomController);
                classmv.PrintClassOfCourse(crid);
                Console.Write("Please enter a class name to enrol in: ");
                string className = Console.ReadLine();
                //while (classController.SearchClass(className) == null)
                //{
                //    Console.Write("\nClass not found. Try again");
                //    Console.Write("> ");
                //    className = Console.ReadLine();
                //}
                //Class enrolClass = classController.SearchClass(className);
                Class enrolClass = null;
                bool clashed = false;
                while (true)
                {
                    enrolClass = classController.SearchClass(className);
                    if (classController.SearchClass(className) == null)
                    {
                        Console.Write("\nClass not found. Try again");
                        Console.Write("> ");
                        className = Console.ReadLine();
                        if (className == "")
                        {
                            clashed = true;
                            break;
                        }
                    }
                    else if (studentController.CheckClashTime(enrolClass, student, classController))
                    {
                        Console.WriteLine("You select a class has period that clashed with your current classes.");
                        Console.Write("You should select different class: ");
                        className = Console.ReadLine();
                        if (className == "")
                        {
                            clashed = true;
                            break;
                        }
                    }
                    else
                        break;
                }


                //while (studentController.CheckClashTime(enrolClass, student, classController))
                //{
                //    Console.WriteLine("You select a class has period that clashed with your current classes.");
                //    Console.Write("You should select different class [Enter] to exit: ");
                //    className = Console.ReadLine();
                //    if (className != "")
                //    {
                //        while (classController.SearchClass(className) == null)
                //        {
                //            Console.Write("\nClass not found. Try again");
                //            Console.Write("> ");
                //            className = Console.ReadLine();
                //        }
                //        enrolClass = classController.SearchClass(className);
                //        if (student.GetMyClasses().Count > 4)
                //        {
                //            Console.WriteLine("You are not allowed to add class any more.");
                //        }
                //    }
                //    else
                //        break;
                //}
                if (clashed)
                {
                    Console.WriteLine("Student \"{0}\" failed to enrol class \"{1}\".", id, enrolClass.ToString());
                }
                else
                {
                    if (studentController.EnrolClass(enrolClass, student, classController))
                        Console.WriteLine("Student \"{0}\" enrolled to class \"{1}\".", id, enrolClass.ToString());
                }

            }
            Console.Write("[Enter] to continue...");
            Console.ReadLine();
        }

        internal void WithdrawStudent()
        {
            //ConsoleKeyInfo keypress;
            if (studentController.StudentCount() == 0)
            {
                Console.WriteLine("Student list is empty. Please add some student first!");
                Console.WriteLine("[Enter] to continue...");
                Console.ReadLine();
            }
            else if(classController.ClassesCount() == 0)
            {
                Console.WriteLine("Please come back and add class first!");
                Console.WriteLine("[Enter] to continue...");
                Console.ReadLine();
            }
            else
            {
                DisplayStudent();
                Console.Write("Enter only one student id as shown above to make change for that student: ");
                string studentIdChange = Console.ReadLine();
                while (studentController.SearchStudent(studentIdChange) == null)
                {
                    Console.Write("Not found student! Enter student id again: ");
                    studentIdChange = Console.ReadLine();
                    //found = controller.FindStudent(studentIdChange);
                }
                Student student = studentController.SearchStudent(studentIdChange);

                List<Class> studentClassList = student.GetMyClasses();
                if (studentClassList.Count > 0)
                {

                    for (int i = 0; i < studentClassList.Count; i++)
                    {
                        Console.WriteLine("Class name: {0}", studentClassList[i].ToString());
                        Class myEnrolClass = studentClassList[i];

                        bool validDecision = false;
                        while (!validDecision)
                        {
                            Console.WriteLine("\t\t* 1. Edit class");
                            Console.WriteLine("\t\t* 2. Delete class");
                            Console.Write("Enter an option(1-2): ");
                            int decision = 0;
                            try
                            {
                                decision = System.Int32.Parse(Console.ReadLine());
                            }
                            catch (Exception)
                            {
                                decision = 0;
                            }
                            if (decision >= 1 && decision <= 2)
                                validDecision = true;
                            Console.WriteLine();
                            switch (decision)
                            {
                                case 1:
                                    ClassManagementView cmv = new ClassManagementView(classController, courseController, lecturerController, roomController);
                                    cmv.DisplayClasses();
                                    Console.Write("Enter an above class name to enrol it: ");
                                    string classname = Console.ReadLine();
                                    while (classController.SearchClass(classname) == null)
                                    {
                                        Console.Write("Not found class! Try again: ");
                                        classname = Console.ReadLine();
                                    }
                                    Class foundClass = classController.SearchClass(classname);
                                    while (studentController.CheckClashTime(foundClass, student, classController))
                                    {
                                        Console.WriteLine("You selected a class that has clashed with other classes.");
                                        Console.Write("Enter class name again: ");
                                        classname = Console.ReadLine();
                                        while (classController.SearchClass(classname) == null)
                                        {
                                            Console.Write("Not found class! Try again: ");
                                            classname = Console.ReadLine();
                                        }
                                        foundClass = classController.SearchClass(classname);
                                    }
                                    myEnrolClass.WithDrawStudent(student);
                                    student.RemoveClass(myEnrolClass);
                                    if (studentController.EnrolClass(foundClass, student, classController))
                                    {
                                        Console.WriteLine("You are enrolled that class.");
                                    }
                                    else
                                    {
                                        myEnrolClass.EnrolStudent(student);
                                        student.AddClass(myEnrolClass);
                                        Console.WriteLine("You could not enrol to that class.");
                                    }
                                    break;
                                case 2:
                                    myEnrolClass.WithDrawStudent(student);
                                    student.RemoveClass(myEnrolClass);
                                    Console.WriteLine("You are withdrawn from that class.");
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("There are no enrolment records for \"{0}\" student", student.Id);
                }
            }
        }
    }
}
