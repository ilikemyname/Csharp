using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assignment01.View;
using Assignment01.Model;

namespace Assignment01.Controller
{
    class TestHarness
    {
        private CourseManagementView cmv;
        private LecturerManagementView lmv;
        private RoomManagementView rmv;
        private ClassManagementView clmv;
        private StudentManagementView smv;
        private CourseController cc;
        private LecturerController lc;
        private RoomController rc;
        private ClassController clc;
        private StudentController sc;
        public TestHarness()
        {
            cc = new CourseController();
            lc = new LecturerController();
            rc = new RoomController();
            clc = new ClassController();
            sc = new StudentController();
            cmv = new CourseManagementView(cc);
            lmv = new LecturerManagementView(lc);
            rmv = new RoomManagementView(rc);
            clmv = new ClassManagementView(clc, cc, lc, rc);
            smv = new StudentManagementView(sc, cc, clc, lc, rc);
        }

        internal void Run()
        {
            Console.WriteLine("\t\t\t\t Test Harness Result");
            TestCourseManagement();

            TestLecturerManagement();

            TestRoomManagement();

            TestStudentManagement();

            TestClassManagement();

            Console.Write("[Enter] to come back main menu...");
            Console.ReadLine();
        }

        private void TestClassManagement()
        {
            TestAddClass();
            TestDisplayEmptyClassList();
            TestDisplayClassList();
            TestEditClass();
        }

        private void TestEditClass()
        {
            Course course = cc.SearchCourse("cosc2323");
            Lecturer lecturer = lc.SearchLecturer("v3222222");
            string className = course.Id + "-" + lecturer.Id;
            Class aClass = clc.SearchClass(className);
            DateTime startTime = DateTime.Parse("16:30");
            DateTime endTime = DateTime.Parse("18:00");
            Room room = rc.SearchRoom("1.1.2");
            string dayOfWeek = "Monday";
            ClassPeriod classPeriod = new ClassPeriod(room, dayOfWeek , startTime ,endTime );
            clc.DeleteClassName(className);
            if (clc.ConflictTime(classPeriod))
            {
                Console.WriteLine("Fail! This class has period that clashed with following class: ");
                Console.WriteLine("\t\t(Class name ({0})): ", course.Id + "-" + lecturer.Id);
                Console.WriteLine("\tClass period: {0}", classPeriod.ToString());
            }
            else
            {
                clc.CreateClass(course, lecturer, classPeriod);
                Console.WriteLine("Pass! This class is updated, no period conflicted.");
                Console.WriteLine("\t\t(Class name ({0})): ", course.Id + "-" + lecturer.Id);
                Console.WriteLine("\tClass period: {0}", classPeriod.ToString());
            }
        }

        private void TestDisplayClassList()
        {
            Console.WriteLine("Testing display class list");
            clmv.DisplayClasses();
            Console.WriteLine();
        }

        private void TestDisplayEmptyClassList()
        {
            Console.WriteLine("Testing display empty class list");
            ClassManagementView cmv = new ClassManagementView(new ClassController(), new CourseController(), new LecturerController(), new RoomController());
            cmv.DisplayClasses();
            Console.WriteLine();
        }

        private void TestAddClass()
        {
            Console.WriteLine("Testing Add Class");
            Console.WriteLine();
            Console.WriteLine("\t\t\t\tAdding class no conflict to others");
            Course course = cc.SearchCourse("cosc2323");
            Lecturer lecturer = lc.SearchLecturer("v3222222");
            Room room = rc.SearchRoom("1.1.2");
            DateTime startTime = DateTime.Parse("8:00");
            DateTime endTime = DateTime.Parse("9:00");
            string dayOfWeek = "monday";
            ClassPeriod classPeriod = new ClassPeriod(room, dayOfWeek ,startTime, endTime);
            for (int i = 0; i < 2; i++)
            {
                if (clc.ConflictTime(classPeriod))
                {
                    Console.WriteLine("Fail! This class has period that clashed with following class: ");
                    Console.WriteLine("\t\t(Class name ({0})): ", course.Id + "-" + lecturer.Id);
                    Console.WriteLine("\tClass period: {0}", classPeriod.ToString());
                }
                else
                {
                    clc.CreateClass(course, lecturer, classPeriod);
                    Console.WriteLine("Pass! This class is added, no period conflicted.");
                }
            }
            Console.WriteLine();
        }

        private void TestStudentManagement()
        {
            TestAddStudent();
            TestDisplayEmptyStudentList();
            TestDisplayStudentList();
            TestEditStudent();
        }

        private void TestEditStudent()
        {
            Console.WriteLine("Testing Edit Student");
            string[] id = new string[] { "s3230237", "l3222222", "v3444444", "3232323", "asdf" };
            string studentIDToDelete = "";
            for (int i = 0; i < id.Count(); i++)
            {
                if (sc.SearchStudent(id[i]) != null)
                {
                    Console.WriteLine("\t\t Found Student {0} !", id[i]);
                    studentIDToDelete = id[i];
                    sc.DeleteStudent(id[i]);
                }
            }
            id = new string[] { "s3230237", "v", "3111111", "v31234", "v1234567"};
            string fn = "huy"; string ln = "le"; string mn = "phat";
            for (int i = 0; i < id.Count(); i++)
            {
                if (!studentIDToDelete.Equals(id[i]))
                {
                    sc.DeleteStudent(studentIDToDelete);
                }
                else
                {
                    sc.DeleteStudent(id[i]);
                }
                Console.WriteLine("New student infomation(id, first name, last name, middle name)\n({0}, {1}, {2}, {3})", id[i], fn, ln, mn);
                if (sc.UpdateStudent(id[i], fn, mn, ln))
                {
                    Console.WriteLine("Updated student {0}!", id[i]);
                    Console.WriteLine();
                    TestDisplayStudentList();
                }
                else
                {
                    Console.WriteLine("Fail to update student {0}!", id[i]);
                    Console.WriteLine();
                    TestDisplayStudentList();
                }
            }
            Console.WriteLine();
        }

        private void TestDisplayStudentList()
        {
            Console.WriteLine("Testing Display Student List");
            smv.DisplayStudent();
            Console.WriteLine();
        }

        private void TestDisplayEmptyStudentList()
        {
            Console.WriteLine("Testing Display Empty Student List");
            StudentManagementView smv = new StudentManagementView(new StudentController(), new CourseController(), new ClassController(), new LecturerController(), new RoomController());
            smv.DisplayStudent();
            Console.WriteLine();
        }

        private void TestAddStudent()
        {
            Console.WriteLine("Testing Add Student");
            string[] id = new string[] { "s3230237", "l3222222", "v3444444", "3232323", "asdf" };
            string name = "name";
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("(id, first name, last name, middle name) ({0}, {1}, {2}, {3})", id[i], name, name, name);
                if (sc.CreateStudent(id[i], name, name, name))
                {
                    Console.WriteLine("Added Student {0}", id[i]);
                    Console.WriteLine();
                }
                else
                    Console.WriteLine();
            }
            Console.WriteLine();
        }

        private void TestRoomManagement()
        {
            TestAddRoom();
            TestDisplayEmptyRoomList();
            TestDisplayRoomList();
            TestEditRoom();
        }

        private void TestEditRoom()
        {
            Console.WriteLine("Testing Edit Room");
            string[] room = new string[] { "1.1.1", "1.1", "1", "1.1.15", "o", "o.o.o" };
            string roomIDToDelete = "1.1.1";
            string[] newRoom = new string[] {"1.1.2", "1.1.15"};
            rc.DeleteRoom(roomIDToDelete);
            for(int i=0; i<2; i++)
            {
                if(rc.UpdateRoom(newRoom[i]))
                {
                    Console.WriteLine("New room {0} is not yet added.", newRoom[i]);
                    Console.WriteLine("Pass! Old room {0} is updated to {1}.", roomIDToDelete, newRoom[i]);
                }
                else
                {
                    Console.WriteLine("New room {0} has added already.", newRoom[i]);
                    Console.WriteLine("Fail! Old room {0} could not be updated to {1}", roomIDToDelete, newRoom[i]);
                }
            }
            Console.WriteLine();
            TestDisplayRoomList();
            Console.WriteLine();
        }

        private void TestDisplayRoomList()
        {
            Console.WriteLine("Testing Display Room List");
            rmv.DisplayRoom();
            Console.WriteLine();
        }

        private void TestDisplayEmptyRoomList()
        {
            Console.WriteLine("Testing Display Empty Room List");
            RoomManagementView rmv = new RoomManagementView(new RoomController());
            rmv.DisplayRoom();
            Console.WriteLine();
        }

        private void TestAddRoom()
        {
            Console.WriteLine("Testing Add Room");
            string[] room = new string[] { "1.1.1", "1.1", "1", "1.1.15", "o", "o.o.o" };
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("\t Room name:({0}) ", room[i]);
                if (rc.CreateRoom(room[i]))
                {
                    Console.WriteLine("\t\tAdded Room ({0})", room[i]);
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("\t\tFail to add Room ({0})", room[i]);
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }

        private void TestLecturerManagement()
        {
            TestAddLecturer();
            TestDisplayEmptyLecturerList();
            TestDisplayLecturerList();
            TestEditLecturer();
        }

        private void TestEditLecturer()
        {
            Console.WriteLine("Testing Edit Lecturer");
            string[] id = new string[] { "v3111111", "v", "3111111", "v31234", "v1234567" };
            string lecturerIDToDelete = "";
            for (int i = 0; i < id.Count(); i++)
            {
                if (lc.SearchLecturer(id[i]) != null)
                {
                    Console.WriteLine("\t\t Found Lecturer {0} !", id[i]);
                    lecturerIDToDelete = id[i];
                    lc.DeleteLecturer(id[i]);
                }
            }
            id = new string[] { "v3111111", "v", "3111111", "v31234", "v1234567", "v3222222" };
            string fn = "huy"; string ln = "le"; string mn = "phat";
            for (int i = 0; i < id.Count(); i++)
            {
                if (!lecturerIDToDelete.Equals(id[i]))
                {
                    lc.DeleteLecturer(lecturerIDToDelete);
                }
                else
                {
                    lc.DeleteLecturer(id[i]);
                }
                Console.WriteLine("New lecturer infomation(id, first name, last name, middle name)\n({0}, {1}, {2}, {3})", id[i], fn, ln, mn);
                if (lc.UpdateLecturer(id[i], fn, mn, ln))
                {
                    Console.WriteLine("Updated lecturer {0}!", id[i]);
                    Console.WriteLine();
                    TestDisplayLecturerList();
                }
                else
                {
                    Console.WriteLine("Fail to update lecturer {0}!", id[i]);
                    Console.WriteLine();
                    TestDisplayLecturerList();
                }
            }
            Console.WriteLine();
        }

        private void TestDisplayLecturerList()
        {
            Console.WriteLine("Testing Display Lecturer List");
            lmv.DisplayLecturer();
            Console.WriteLine();
        }

        private void TestDisplayEmptyLecturerList()
        {
            Console.WriteLine("Testing Display Empty Lecturer List");
            LecturerController nLC = new LecturerController();
            LecturerManagementView nLMV = new LecturerManagementView(nLC);
            nLMV.DisplayLecturer();
            Console.WriteLine();
        }

        private void TestAddLecturer()
        {
            Console.WriteLine("Testing Add Lecturer");

            string[] id = new string[] { "v3111111", "v", "3111111", "v31234", "v1234567" };
            string[] name = new string[] { "name", "name", "name", "name", "name" };            
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("(id, first name, last name, middle name) ({0}, {1}, {2}, {3})", id[i], name[i], name[i], name[i]);
                if (lc.CreateLecturer(id[i], name[i], name[i], name[i]))
                {
                    Console.WriteLine("\tPass!");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("\tFail!");
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }

        private void TestCourseManagement()
        {
            TestAddCourse();
            TestDisplayEmptyCourseList();
            TestDisplayCourseList();
            TestEditCourse();
        }

        private void TestEditCourse()
        {
            Console.WriteLine("Testing Edit Course");
            string[] id = new[] { "cosc2121", "cosc2345", "cosc" };
            string courseIDToDelete = "";
            for (int i = 0; i < id.Count(); i++)
            {
                if (cc.SearchCourse(id[i]) != null)
                {
                    Console.WriteLine("\t\t Found Course {0} !", id[i]);
                    courseIDToDelete = id[i];
                    cc.DeleteCourse(id[i]);
                }
            }
            id = new[] { "cosc2121", "cosc2121", "cosc2323" };
            string[] name = new string[] { "ECommerce", "ECommerce", "ECommerce" };
            string[] desc = new string[] { "aaa", "j2ee", "j2ee" };
            for (int i = 0; i < id.Count(); i++)
            {
                if (!courseIDToDelete.Equals(id[i]))
                {
                    cc.DeleteCourse(courseIDToDelete);
                }
                else
                {
                    cc.DeleteCourse(id[i]);
                }
                Console.WriteLine("New course infomation(id, name, description)\n({0}, {1}, {2})",id[i], name[i], desc[i]);
                if (cc.UpdateCourse(id[i], name[i], desc[i]))
                {
                    Console.WriteLine("Updated course {0}!", id[i]);
                    Console.WriteLine();
                    TestDisplayCourseList();
                }
                else
                {
                    Console.WriteLine("Fail to update course {0}!", id[i]);
                    Console.WriteLine();
                    TestDisplayCourseList();
                }
            }
            Console.WriteLine();
        }

        private void TestDisplayCourseList()
        {
            Console.WriteLine("Testing Display Course List");
            cmv.DisplayCourses();
            Console.WriteLine();
        }

        private void TestDisplayEmptyCourseList()
        {
            Console.WriteLine("Testing Display Empty Course List");
            CourseController nCC = new CourseController();
            CourseManagementView nCMV = new CourseManagementView(nCC);
            nCMV.DisplayCourses();
            Console.WriteLine();
        }

        private void TestAddCourse()
        {
            Console.WriteLine("Testing Add Course");

            string[] id = new string[] { "cosc2121", "2121", "cosc", "COSC2121", "COSC2020" };
            string[] name = new string[] { "aaa", "aaa", "aaa", "aaa", "aaa"};
            string[] desc = new string[] { "aaa", "aaa", "aaa", "aaa", "aaa"};
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("(id, name, description) ({0}, {1}, {2})", id[i], name[i], desc[i]);
                if (cc.CreateCourse(id[i], name[i], desc[i]))
                {
                    Console.WriteLine("\tPass!");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("\tFail!");
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }
    }
}
