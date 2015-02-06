using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assignment01.Model;
using Assignment01.Utility;

namespace Assignment01.View
{
    class ClassManagementView
    {
        private Controller.ClassController classController;
        private Controller.CourseController courseController;
        private Controller.LecturerController lecturerController;
        private Controller.RoomController roomController;
        private CourseManagementView cmv;
        private LecturerManagementView lmv;
        private RoomManagementView rmv;

        public ClassManagementView(Controller.ClassController classController, Controller.CourseController courseController, Controller.LecturerController lecturerController, Controller.RoomController roomController)
        {
            // TODO: Complete member initialization
            this.classController = classController;
            this.courseController = courseController;
            this.lecturerController = lecturerController;
            this.roomController = roomController;
            cmv = new CourseManagementView(courseController);
            lmv = new LecturerManagementView(lecturerController);
            rmv = new RoomManagementView(roomController);
        }

        internal void AddClass()
        {
            Console.WriteLine("Add new class");
            Course course = null;
            Lecturer lecturer = null;
            if(courseController.CourseCount() == 0)
            {
                Console.WriteLine("There are no courses yet. You should add some courses firstly.");
            }
            else if(lecturerController.LecturerCount() == 0)
            {
                Console.WriteLine("There are no lecturers yet. You should add some lecturers firstly.");
            }
            else if (roomController.RoomCount() == 0)
            {
                Console.WriteLine("There are no rooms yet. You should add some rooms firstly");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("\t\tCourse List\t\t");
                cmv.DisplayCourses();
                Console.Write("Enter course id: ");
                string courseId = Console.ReadLine();
                while (courseController.SearchCourse(courseId) == null)
                {
                    Console.Write("This course never exists. Enter course id: ");
                    courseId = Console.ReadLine();
                }
                course = courseController.SearchCourse(courseId);

                Console.WriteLine();
                Console.WriteLine("\t\tLecturer List\t\t");
                lmv.DisplayLecturer();
                Console.Write("Enter lecturer id: ");
                string lecturerId = Console.ReadLine();
                while (lecturerController.SearchLecturer(lecturerId) == null)
                {
                    Console.Write("This lecturer never exists. Enter lecturer id: ");
                    lecturerId = Console.ReadLine();
                }
                lecturer = lecturerController.SearchLecturer(lecturerId);
                
                ClassPeriod classPeriod = CreateClassPeriod();

                if(classController.ClassesCount() > 0)
                {
                    while (true)
                    {
                        // check whether new class period will clash with others in classes list
                        if (classController.ConflictTime(classPeriod))
                        {
                            Console.WriteLine("Your chosen class Period has clashed with the others.");
                            Console.WriteLine("Please justify to other class period!");
                            classPeriod = CreateClassPeriod();
                        }
                        else if (classController.ConflictLecturer(lecturer.Id, classPeriod))
                        {
                            Console.WriteLine("Sorry! At your chosen class time, Lecturer has id \"{0}\" will teaching other class", lecturer.Id);
                            Console.WriteLine("Please justify to other class period!");
                            classPeriod = CreateClassPeriod();
                        }
                        else
                            break;
                    }
                }
                if (classController.CreateClass(course, lecturer, classPeriod))
                {
                    Console.WriteLine("Congratulation! you added a new class.");
                }
                else
                {
                    Console.WriteLine("Sorry! you failed to add a class.");
                    Console.WriteLine("This class is taken by other lecturer.");
                }
            }
            Console.WriteLine("[Enter] to continue...");
            Console.ReadLine();
        }

        private ClassPeriod CreateClassPeriod()
        {
            ClassPeriod classPeriod = null;
            Console.WriteLine();
            Console.WriteLine("\t\tRoom List\t\t");
            rmv.DisplayRoom();
            Console.Write("\nEnter room name: ");
            string roomName = Console.ReadLine();
            Room room = roomController.SearchRoom(roomName);
            while (roomController.SearchRoom(roomName) == null)
            {
                Console.Write("\nThis room never exists. Enter room id: ");
                roomName = Console.ReadLine();
            }

            Console.Write("\nEnter a day of week(Monday - Friday): ");
            string dayOfWeek = Console.ReadLine();
            dayOfWeek = dayOfWeek.ToLower();
            while (!Validate.ValidWeekDayString(dayOfWeek))
            {
                Console.Write("\nInvalid day of week. Please enter again(Monday - Friday): ");
                dayOfWeek = Console.ReadLine();
            }
            dayOfWeek = dayOfWeek.ToLower();
            bool isLogic = false;
            while (!isLogic)
            {
                try
                {
                    Console.Write("\nEnter start time class(HH:MM)\nTime must satisfy block of 30 minutes: ");
                    var st = Console.ReadLine();
                    DateTime startTime = DateTime.Parse(st);
                    while (!Validate.ValidMinute(st))
                    {
                        Console.WriteLine("Please follow format(HH:MM) and block of 30 minutes(eg. 9:30)");
                        Console.Write("> ");
                        st = Console.ReadLine();
                        startTime = DateTime.Parse(st);
                    }

                    Console.Write("\nEnter end time class(HH:MM)\nTime must satisfy block of 30 minutes: ");
                    var et = Console.ReadLine();
                    DateTime endTime = DateTime.Parse(et);
                    while (!Validate.ValidMinute(et))
                    {
                        Console.WriteLine("Please follow format(HH:MM) and block of 30 minutes(eg. 9:30)");
                        Console.Write("> ");
                        et = Console.ReadLine();
                        endTime = DateTime.Parse(et);
                    }
                    if (classController.ValidateClassTime(startTime, endTime))
                    {
                        classPeriod = new ClassPeriod(room, dayOfWeek, startTime, endTime);
                        isLogic = true;
                        //return classPeriod;
                    }
                    else
                    {
                        Console.WriteLine("*************** Instruction *********************");
                        Console.WriteLine("Start time or end time you entered was not logic.");
                        Console.WriteLine("Minimun start time must be 8:00");
                        Console.WriteLine("Maximun start time must be 17:30");
                        Console.WriteLine("Minimun end time must be 8:30");
                        Console.WriteLine("Minimun end time must be 18:00");
                        Console.WriteLine("Start time must be earlier than end time.");
                        Console.WriteLine("*************************************************");
                    }
                }
                catch (Exception)
                {
                    isLogic = false;
                }
            }
            return classPeriod;
            //return classPeriod;
        }

        internal void EditClass()
        {
            if (classController.ClassesCount() == 0)
            {
                Console.WriteLine("No class found! you need to add class.");
                Console.WriteLine("[Enter] to continue...");
                Console.ReadLine();
            }
            else
            {
                DisplayClasses();
                Console.Write("Enter only one class name as shown above: ");
                string editClassName = Console.ReadLine();
                Class expectedClass = classController.SearchClass(editClassName);
                while (expectedClass == null)
                {
                    Console.Write("\nNot class found! Enter class name again: ");
                    editClassName = Console.ReadLine();
                    expectedClass = classController.SearchClass(editClassName);
                }
                /* Backup information of class that will be updated.
                 * Recreate that class if something go wrong during update process
                 */
                var lecturerInfo = expectedClass.Lecturer;
                var courseInfo = expectedClass.Course;
                string className = expectedClass.ToString();
                List<ClassPeriod> cpList = expectedClass.ClassPeriodList;

                //Console.WriteLine("*******Lecturer id list******** ");
                //List<Lecturer> lecturerList = lecturerController.GetLecturers();
                //foreach (Lecturer l in lecturerList)
                //{
                //    Console.WriteLine(l.Id);
                //}
                LecturerManagementView lmv = new LecturerManagementView(lecturerController);
                Console.WriteLine("\n\t\t Lecturer list");
                lmv.DisplayLecturer();
                Console.Write("\nEnter one of above id to change \"{0}\"[Enter to keep]: ", lecturerInfo.Id);
                string nLecId = Console.ReadLine();
                Lecturer lecturer = null;
                if (nLecId != "")
                {
                    lecturer = lecturerController.SearchLecturer(nLecId);
                    while (lecturer == null)
                    {
                        Console.Write("\nNot found this lecture! Enter again: ");
                        nLecId = Console.ReadLine();
                        lecturer = lecturerController.SearchLecturer(nLecId);
                    }
                    lecturerInfo = lecturer;
                }

                //Console.WriteLine("*******Course id list******** ");
                //List<Course> courseList = courseController.GetCourses();
                //foreach (Course c in courseList)
                //{
                //    Console.WriteLine(c.Id);
                //}
                CourseManagementView cmv = new CourseManagementView(courseController);
                Console.WriteLine("\n\t\t Course list");
                cmv.DisplayCourses();
                Console.Write("\nEnter one of above id to change \"{0}\"[Enter to keep]: ", courseInfo.Id);
                string nCourseId = Console.ReadLine();
                Course course = null;
                if (nCourseId != "")
                {
                    course = courseController.SearchCourse(nCourseId);
                    while (course == null)
                    {
                        Console.Write("\nNot found this course! Enter again: ");
                        nCourseId = Console.ReadLine();
                        course = courseController.SearchCourse(nCourseId);
                    }
                    courseInfo = course;
                }

                Console.WriteLine();
                Console.WriteLine("********** Class Periods ***********");
                int classPeriodCount = cpList.Count;
                for (int i = 0; i < classPeriodCount; i++)
                {
                    Console.WriteLine("\nClass period number \"{0}\"", i + 1);
                    Console.WriteLine(cpList[i].ToString());
                    Console.WriteLine();
                }
                int number = 0;
                bool valid = false;
                while(!valid)
                {
                    try
                    {
                        Console.Write("\nEnter period number to update: ");
                        number = int.Parse(Console.ReadLine());
                        valid = true;
                    }   
                    catch (Exception)
                    {
                        Console.Write("\nUnexisted period number!Try again: ");
                    }
                }
                number -= 1;
                ClassPeriod backupCP = cpList[number];
                cpList.RemoveAt(number);

                ClassPeriod nCp = CreateClassPeriod();
                bool conflicted = true;
                while(conflicted)
                {
                    if(cpList.Count == 0)
                    {
                        conflicted = false;
                    }
                    foreach (ClassPeriod cp in cpList)
                    {
                        conflicted = nCp.TimeConflict(cp);
                        if (conflicted)
                            break;
                    }
                    if (conflicted)
                    {
                        Console.WriteLine("\nNew period has clashed with other remaining period of class to update.");
                        nCp = CreateClassPeriod();
                    }
                }
                while (true)
                {
                    if (classController.ConflictTime(nCp))
                    {
                        Console.WriteLine("\nNew period has clashed with other period of different classes.");
                        nCp = CreateClassPeriod();
                    }
                    else if (classController.ConflictLecturer(nLecId, nCp))
                    {
                        Console.WriteLine("\nNew lecturer \"{0}\" teaches a class at the same time of new period.");
                        nCp = CreateClassPeriod();
                    }
                    else
                        break;
                }
                cpList.Add(nCp);
                classController.DeleteClassName(editClassName);
                if (classController.UpdateClass(lecturerInfo, courseInfo, nCp))
                {
                    Console.WriteLine("\nUpdated class({0})", lecturerInfo.Id + courseInfo.Id);
                    Console.Write("[Enter] to continue...");
                    Console.ReadLine();
                }
                else
                {
                    classController.CreateClass(expectedClass.Course, expectedClass.Lecturer, backupCP);
                    Console.WriteLine("\nUpdate failed!");
                    Console.Write("[Enter] to continue...");
                    Console.ReadLine();
                }
            }
        }

        //internal void DeleteClass()
        //{
        //    throw new NotImplementedException();
        //}

        internal void DisplayClasses()
        {
            if (classController.ClassesCount() > 0)
            {
                Console.WriteLine("Class list:");
                Dictionary<string, Class> classList = classController.RetrieveClassList();
                foreach (KeyValuePair<string, Class> keyValueOfClass in classList)
                {
                    Console.WriteLine("** Class Name   : {0}", keyValueOfClass.Key);
                    Console.WriteLine("** Course       : {0}", keyValueOfClass.Value.Course.Id);
                    Console.WriteLine("** Lecturer     : {0}", keyValueOfClass.Value.Lecturer.Id);
                    Console.WriteLine("** Period ");
                    foreach (ClassPeriod cp in keyValueOfClass.Value.ClassPeriodList)
                    {
                        Console.WriteLine("\t Room: {0}", cp.Room.RoomNo);
                        Console.WriteLine("\t Time: ");
                        Console.WriteLine("\t\t Day        : {0}", cp.DayOfWeek);
                        Console.WriteLine("\t\t Start time : {0}", cp.StartTime.ToShortTimeString());
                        Console.WriteLine("\t\t End time   : {0}", cp.EndTime.ToShortTimeString());
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
            }
            else
                Console.WriteLine("There is no class yet!");
        }

        internal void PrintClassOfCourse(string crid)
        {
            List<Class> classesCourse = classController.RetrieveClassesListOfCourse(crid);
            foreach (Class cl in classesCourse)
            {
                Console.WriteLine("**** Class    ->  {0}", cl.ToString());
                Console.WriteLine("**** Lecturer ->  {0}", cl.Lecturer);
                Console.WriteLine("**** Course   ->  {0}", cl.Course);
                Console.WriteLine("**** Time  ***** ");
                foreach(ClassPeriod cp in cl.ClassPeriodList)
                {
                    Console.WriteLine("\t\t Room        -> {0}", cp.Room);
                    Console.WriteLine("\t\t Day of week -> {0}", cp.DayOfWeek);
                    Console.WriteLine("\t\t From {0} to {1}", cp.StartTime.ToShortTimeString(), cp.EndTime.ToShortTimeString());
                }
                Console.Write("\n\n");
            }
            Console.WriteLine();
        }
    }
}



//int dayOfWeeek = Transform.ToDayOfWeekNumber(dayOfWeekString);

//                Console.WriteLine("Enter start time class by format(HH:MM)");
//                Console.Write("Valid start time is (8:00-17:30): ");
//                var startTime = Console.ReadLine();
//                while(!Validate.ValidMinute(startTime))
//                {
//                    Console.WriteLine("Invalid start time! it allows block of 30 minutes only.");
//                    Console.Write("Try to enter start time again(HH:MM): ");
//                    startTime = Console.ReadLine();
//                }

//                Console.WriteLine("Enter end time class by format(HH:MM)");
//                Console.Write("Valid end time is (8:30-18:00): ");
//                var endTime = Console.ReadLine();
//                while (!Validate.ValidMinute(endTime))
//                {
//                    Console.WriteLine("Invalid end time! it allows block of 30 minutes only.");
//                    Console.Write("Try to enter end time again(HH:MM): ");
//                    endTime = Console.ReadLine();
//                }
//                float sTime = Transform.ToStartTimeFloat(startTime);
//                float eTime = Transform.ToEndTimeFloat(endTime);
//                try
//                {
//                    ClassPeriod classPeriod = new ClassPeriod(room, dayOfWeeek, sTime, eTime);
//                    classController.CreateClass(lecturer, course, classPeriod);
//                }
//                catch(Exception ex)
//                {
//                    Console.WriteLine(ex.Message);
//                }

                
//                Console.WriteLine("You added a new class.");
//                Console.Write("[Enter] to continue...");
//                Console.ReadLine();