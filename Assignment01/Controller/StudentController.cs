using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assignment01.Model;
using Assignment01.Utility;

namespace Assignment01.Controller
{
    class StudentController
    {
        private Dictionary<string, Student> studentList = new Dictionary<string, Student>();

        internal bool CreateStudent(string studentID, string firstName, string middleName, string lastName)
        {
            studentID = studentID.ToLower();
            if (!Validate.ValidStudentId(studentID))
                return false;
            bool duplicated = CheckStudentDuplicated(studentID);
            if (duplicated)
            {
                Console.WriteLine("\nSorry! the program detected that this student was added already.");
                return false;
            }
            else
            {
                Student student = new Student(studentID, firstName, middleName, lastName);
                studentList.Add(studentID, student);
                return true;
            }
        }

        private bool CheckStudentDuplicated(string studentID)
        {
            if (studentList.ContainsKey(studentID))
                return true;
            return false;
        }


        internal int StudentCount()
        {
            return studentList.Count;
        }

        internal Student SearchStudent(string studentIdToChange)
        {
            if(studentList.ContainsKey(studentIdToChange))
                return studentList[studentIdToChange];
            return null;
        }

        internal void DeleteStudent(string studentIdToChange)
        {
            studentList.Remove(studentIdToChange);
        }

        internal bool UpdateStudent(string updatedId, string updatedFName, string updatedMName, string updatedLName)
        {
            if (Validate.ValidStudentId(updatedId))
            {
                if (SearchStudent(updatedId) == null)
                {
                    CreateStudent(updatedId, updatedFName, updatedMName, updatedLName);
                    return true;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Sorry! the program detected that this student was added already.");
                    return false;
                }
            }
            else
                Console.WriteLine("Sorry! Update id is not valid.");
            return false;
        }

        internal Dictionary<string, Student> RetrieveStudentList()
        {
            return studentList;
        }

        internal bool CheckClashTime(Class enrolClass, Student student, ClassController classController)
        {
            List<Class> enrolledClassList = classController.GetEnrolledClasses(student);
            bool conflicted = false;
            if(enrolledClassList.Count != 0)
            {
                // Check each class period in class have enrolled by student against that of will enrol class
                foreach (Class sClass in enrolledClassList)
                {
                    //List<ClassPeriod> sCP = sClass.ClassPeriodList;
                    foreach (ClassPeriod eCP in enrolClass.ClassPeriodList)
                    {
                        foreach (ClassPeriod sCP in sClass.ClassPeriodList)
                        {
                            //if(sCP.Room.RoomNo.Equals(eCP.Room.RoomNo))
                            //{
                                if (sCP.TimeConflict(eCP))
                                    conflicted = true;
                                    break;
                            //}
                        }
                        if (conflicted)
                            break;
                    }
                    if (conflicted)
                        break;
                }
            }
            return conflicted;
        }

        internal bool EnrolClass(Class enrolClass, Student student, ClassController classController)
        {
            string key = student.Id;
            studentList[key].AddClass(enrolClass);
            if (classController.AddStudent(student, enrolClass))
                return true;
            return false;
        }
    }
}
