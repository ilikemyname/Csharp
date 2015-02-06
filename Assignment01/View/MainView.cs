using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assignment01.Model.Exception;
using Assignment01.Controller;

namespace Assignment01.View
{
    class MainView
    {
        private ClassManagementView classManagementView;
        private RoomManagementView roomManagementView;
        private CourseManagementView courseManagementView;
        private LecturerManagementView lecturerManagementView;
        private StudentManagementView studentManagementView;
        private ClassController classController = new ClassController();
        private RoomController roomController = new RoomController();
        private StudentController studentController = new StudentController();
        private CourseController courseController = new CourseController();
        private LecturerController lecturerController = new LecturerController();

        public MainView()
        {
            classManagementView = new ClassManagementView(classController, courseController, lecturerController, roomController);
            roomManagementView = new RoomManagementView(roomController);
            studentManagementView = new StudentManagementView(studentController, courseController, classController, lecturerController, roomController);
            courseManagementView = new CourseManagementView(courseController);
            lecturerManagementView = new LecturerManagementView(lecturerController);
        }

        public void ProcessMainView()
        {
            bool validDecision = false;
            while (!validDecision)
            {
                Console.Clear();
                DisplayMainSelection();
                Console.Write("Enter an option(1-3): ");
                int decision = 0;
                try
                {
                    decision = System.Int32.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    decision = 0;
                }
                if (decision >= 1 && decision <= 3)
                    validDecision = true;
                Console.WriteLine();
                switch (decision)
                {
                    case 1:
                        ProcessManagementSelection();
                        break;
                    case 2:
                        TestHarness testHarness = new TestHarness();
                        testHarness.Run();
                        ProcessMainView();
                        break;
                    case 3:
                        terminateProgram();
                        break;
                }
            }
        }

        private void ProcessManagementSelection()
        {
            try
            {
                bool validDecision = false;
                int mdecision = 0;
                while (!validDecision)
                {
                    Console.Clear();
                    DisplayManagementSelection();
                    Console.Write("Enter an option(1-7): ");
                    try
                    {
                        mdecision = System.Int32.Parse(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        mdecision = 0;
                    }

                    if (mdecision >= 1 && mdecision <= 7)
                        validDecision = true;
                    Console.WriteLine();
                    switch (mdecision)
                    {
                        case 1:
                            ManageCourse();
                            break;
                        case 2:
                            ManageLecturer();
                            break;
                        case 3:
                            ManageStudent();
                            break;
                        case 4:
                            ManageRoom();
                            break;
                        case 5:
                            ManageClass();
                            break;
                        case 6:
                            ManageReport();
                            break;
                        case 7:
                            terminateProgram();
                            break;
                    }
                }
            }
            catch (SelectionException se)
            {
                se = new SelectionException("Invalid Exception. Follow the instruction.");
                Console.WriteLine(se.Message);
                ProcessManagementSelection();
            }
        }

        private void ManageReport()
        {
            bool validDecision = false;
            int decision = 0;
            while (!validDecision)
            {
                Console.Clear();
                Console.WriteLine("\t\t* You are working on report management *");
                Console.WriteLine("\t\t* 1. Enrolment Report                  *");
                Console.WriteLine("\t\t* 2. Back to main menu                 *");
                Console.WriteLine("\t\t****************************************");
                Console.Write("Enter an option(1-2): ");
                try
                {
                    decision = int.Parse(Console.ReadLine());
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
                        ReportController rc = new ReportController(courseController, classController);
                        rc.PrintEnrolReport();
                        ManageReport();
                        break;
                    case 2:
                        decision = 2;
                        Console.WriteLine("You select backing to main management selection");
                        ProcessManagementSelection();
                        break;
                }
            }
        }

        private void ManageClass()
        {
            bool validDecision = false;
            int decision = 0;
            while (!validDecision)
            {
                Console.Clear();
                Console.WriteLine("\t\t* You are working on class management *");
                Console.WriteLine("\t\t* 1. Add class                        *");
                Console.WriteLine("\t\t* 2. Modify class                     *");
                Console.WriteLine("\t\t* 3. Display classes                  *");
                Console.WriteLine("\t\t* 4. Back to main menu                *");
                Console.WriteLine("\t\t***************************************");
                Console.Write("Enter an option(1-4): ");
                try
                {
                    decision = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    decision = 0;
                }

                if (decision >= 1 && decision <= 4)
                    validDecision = true;
                Console.WriteLine();
                switch (decision)
                {
                    case 1:
                        classManagementView.AddClass();
                        ManageClass();
                        break;
                    case 2:
                        classManagementView.EditClass();
                        ManageClass();
                        break;
                    case 3:
                        classManagementView.DisplayClasses();
                        Console.Write("[Enter] to continue...");
                        Console.ReadLine();
                        ManageClass();
                        break;
                    case 4:
                        decision = 4;
                        Console.WriteLine("You select backing to main management selection");
                        ProcessManagementSelection();
                        break;
                }
            }
        }

        private void ManageRoom()
        {
            bool validSelection = false;
            while (!validSelection)
            {
                Console.Clear();
                Console.WriteLine("\t\t* You are working on room management *");
                Console.WriteLine("\t\t* 1. Add room                        *");
                Console.WriteLine("\t\t* 2. Modify room                     *");
                Console.WriteLine("\t\t* 3. Delete room                     *");
                Console.WriteLine("\t\t* 4. Display rooms                   *");
                Console.WriteLine("\t\t* 5. Back to main menu               *");
                Console.WriteLine("\t\t**************************************");
                Console.Write("Enter an option(1-5): ");
                int decision = 0;
                try
                {
                    decision = System.Int32.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    decision = 0;
                }
                //var decision = System.Int32.Parse(Console.ReadLine());
                if (decision >= 1 && decision <= 5)
                    validSelection = true;
                Console.WriteLine();
                switch (decision)
                {
                    case 1:
                        roomManagementView.AddRoom();
                        ManageRoom();
                        break;
                    case 2:
                        roomManagementView.EditRoom();
                        ManageRoom();
                        break;
                    case 3:
                        roomManagementView.DeleteRoom();
                        ManageRoom();
                        break;
                    case 4:
                        roomManagementView.DisplayRoom();
                        Console.Write("[Enter] to continue...");
                        Console.ReadLine();
                        ManageRoom();
                        break;
                    case 5:
                        Console.WriteLine("You select backing to main management selection");
                        ProcessManagementSelection();
                        break;
                }
            }
        }

        private void ManageStudent()
        {
            bool validSelection = false;
            while (!validSelection)
            {
                Console.Clear();
                Console.WriteLine("\t\t* You are working on student management *");
                Console.WriteLine("\t\t* 1. Add student                        *");
                Console.WriteLine("\t\t* 2. Modify student                     *");
                Console.WriteLine("\t\t* 3. Delete student                     *");
                Console.WriteLine("\t\t* 4. Display students                   *");
                Console.WriteLine("\t\t* 5. Add students to classes            *");
                Console.WriteLine("\t\t* 6. Edit students                      *");
                Console.WriteLine("\t\t* 7. Back to main menu                  *");
                Console.WriteLine("\t\t******************************************");
                Console.Write("Enter an option(1-7): ");
                int decision = 0;
                try
                {
                    decision = System.Int32.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    decision = 0;
                }
                //var decision = System.Int32.Parse(Console.ReadLine());
                if (decision >= 1 && decision <= 7)
                    validSelection = true;
                Console.WriteLine();
                switch (decision)
                {
                    case 1:
                        studentManagementView.AddStudent();
                        ManageStudent();
                        break;
                    case 2:
                        studentManagementView.EditStudent();
                        ManageStudent();
                        break;
                    case 3:
                        studentManagementView.DeleteStudent();
                        ManageStudent();
                        break;
                    case 4:
                        studentManagementView.DisplayStudent();
                        Console.Write("[Enter] to continue...");
                        Console.ReadLine();
                        ManageStudent();
                        break;
                    case 5:
                        studentManagementView.EnrolStudent();
                        ManageStudent();
                        break;
                    case 6:
                        studentManagementView.WithdrawStudent();
                        ManageStudent();
                        break;
                    case 7:
                        Console.WriteLine("You select backing to main management selection");
                        ProcessManagementSelection();
                        break;
                }
            }
        }

        private void ManageLecturer()
        {
            bool validSelection = false;
            while (!validSelection)
            {
                Console.Clear();
                Console.WriteLine("\t\t* You are working on lecturer management *");
                Console.WriteLine("\t\t* 1. Add lecturer                        *");
                Console.WriteLine("\t\t* 2. Modify lecturer                     *");
                Console.WriteLine("\t\t* 3. Delete lecturer                     *");
                Console.WriteLine("\t\t* 4. Display lecturers                   *");
                Console.WriteLine("\t\t* 5. Back to main menu                   *");
                Console.WriteLine("\t\t******************************************");
                Console.Write("Enter an option(1-5): ");
                int decision = 0;
                try
                {
                    decision = System.Int32.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    decision = 0;
                }
                //var decision = System.Int32.Parse(Console.ReadLine());
                if (decision >= 1 && decision <= 5)
                    validSelection = true;
                Console.WriteLine();
                switch (decision)
                {
                    case 1:
                        lecturerManagementView.AddLecturer();
                        ManageLecturer();
                        break;
                    case 2:
                        lecturerManagementView.EditLecturer();
                        ManageLecturer();
                        break;
                    case 3:
                        lecturerManagementView.DeleteLecturer();
                        ManageLecturer();
                        break;
                    case 4:
                        lecturerManagementView.DisplayLecturer();
                        Console.Write("[Enter] to continue...");
                        Console.ReadLine();
                        ManageLecturer();
                        break;
                    case 5:
                        Console.WriteLine("You select backing to main management selection");
                        ProcessManagementSelection();
                        break;
                }
            }
        }

        private void ManageCourse()
        {

            bool validSelection = false;
            while (!validSelection)
            {
                Console.Clear();
                Console.WriteLine("\t\t* You are working on course management *");
                Console.WriteLine("\t\t* 1. Add course                        *");
                Console.WriteLine("\t\t* 2. Modify course                     *");
                Console.WriteLine("\t\t* 3. Delete course                     *");
                Console.WriteLine("\t\t* 4. Display courses                   *");
                Console.WriteLine("\t\t* 5. Back to main menu                 *");
                Console.WriteLine("\t\t****************************************");
                Console.Write("Enter an option(1-5): ");
                int decision = 0;
                try
                {
                    decision = System.Int32.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    decision = 0;
                }
                //var decision = System.Int32.Parse(Console.ReadLine());
                if (decision >= 1 && decision <= 5)
                    validSelection = true;
                Console.WriteLine();
                switch (decision)
                {
                    case 1:
                        courseManagementView.AddCourse();
                        ManageCourse();
                        break;
                    case 2:
                        courseManagementView.EditCourse();
                        ManageCourse();
                        break;
                    case 3:
                        courseManagementView.DeleteCourse();
                        ManageCourse();
                        break;
                    case 4:
                        courseManagementView.DisplayCourses();
                        Console.Write("[Enter] to continue...");
                        Console.ReadLine();
                        ManageCourse();
                        break;
                    case 5:
                        Console.WriteLine("You select backing to main management selection");
                        ProcessManagementSelection();
                        break;
                }
            }
        }

        private void terminateProgram()
        {
            Console.WriteLine("You terminated the program. See you again!");
        }

        private void DisplayMainSelection()
        {
            Console.WriteLine("\t\t********************************");
            Console.WriteLine("\t\t* Welcome To School Management *");
            Console.WriteLine("\t\t* 1.Use manualy                *");
            Console.WriteLine("\t\t* 2.Run Test Harness           *");
            Console.WriteLine("\t\t* 3.Terminate program          *");
            Console.WriteLine("\t\t********************************");
        }

        private void DisplayManagementSelection()
        {
            Console.WriteLine("\t\t* Management Selection *");
            Console.WriteLine("\t\t* 1. Manage Course     *");
            Console.WriteLine("\t\t* 2. Manage Lecturer   *");
            Console.WriteLine("\t\t* 3. Manage Student    *");
            Console.WriteLine("\t\t* 4. Manage Room       *");
            Console.WriteLine("\t\t* 5. Manage Class      *");
            Console.WriteLine("\t\t* 6. Manage Report     *");
            Console.WriteLine("\t\t* 7. Quit              *");
            Console.WriteLine("\t\t************************");
        }
    }
}
