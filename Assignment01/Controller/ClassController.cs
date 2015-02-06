using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assignment01.Model;
using Assignment01.Utility;

namespace Assignment01.Controller
{
    class ClassController
    {
        private Dictionary<string, Class> classList = new Dictionary<string, Class>();
        private const string minST = "8:00";
        private const string maxST = "17:30";
        private const string minET = "8:30";
        private const string maxET = "18:00";
        public bool CreateClass(Course course, Lecturer lecturer, ClassPeriod classPeriod)
        {
            Class aClass = new Class(lecturer, course);
            if(!classList.ContainsKey(course.Id+"-"+lecturer.Id))
            {
                aClass.AddPeriod(classPeriod);
                string className = course.Id;
                classList.Add(course.Id + "-" + lecturer.Id, aClass);
                return true;
            }
            //else
            //{
                Class alreadyClass = classList[course.Id + "-" + lecturer.Id];
                alreadyClass.AddPeriod(classPeriod);
                classList[course.Id+"-"+lecturer.Id] = alreadyClass;
                return true;
            //}
            //return false;
        }

        public Class SearchClass(string name)
        {
            if(classList.ContainsKey(name))
            {
                return classList[name];
            }
            return null;
        }

        public bool CheckAlreadyClass(Class c)
        {
                foreach (KeyValuePair<string, Class> KeyValOfClass in classList)
                {
                    Class alreadyClass = KeyValOfClass.Value;
                    if (alreadyClass.Lecturer.Id.Equals(c.Lecturer.Id) && alreadyClass.Course.Id.Equals(c.Course.Id))
                    {
                        return true;
                    }
                }
                return false;
        }

        internal bool ValidateClassTime(DateTime startTime, DateTime endTime)
        {
            DateTime minStartTime = DateTime.Parse(minST);
            DateTime maxStartTime = DateTime.Parse(maxST);
            DateTime minEndTime = DateTime.Parse(minET);
            DateTime maxEndTime = DateTime.Parse(maxET);
            if (((DateTime.Compare(startTime, minStartTime) < 0) || (DateTime.Compare(startTime, maxStartTime) > 0))
                || ((DateTime.Compare(endTime, minEndTime) < 0) || (DateTime.Compare(endTime, maxEndTime) > 0)) 
                || (DateTime.Compare(startTime, endTime) >= 0))
                return false;
            return true;
        }

        internal int ClassesCount()
        {
            return classList.Count;
        }

        internal bool ConflictTime(ClassPeriod classPeriod)
        {
            string room = classPeriod.Room.RoomNo;

            bool conflict = false;
            foreach (KeyValuePair<string, Class> KeyValOfClass in classList)
            {
                //string lecturerId = KeyValOfClass.Value.Lecturer.Id;
                foreach (ClassPeriod cp in KeyValOfClass.Value.ClassPeriodList)
                {
                    string alreadyRoom = cp.Room.RoomNo;

                    if (room.Equals(alreadyRoom))
                    {
                        conflict = false;
                        //conflict = Validate.TimeConFlict(classPeriod, cp);
                        conflict = cp.TimeConflict(classPeriod);
                        if (conflict) { break; }
                    }
                }
                if (conflict) { break; }
            }
            return conflict;
        }

        internal bool ConflictLecturer(string lid, ClassPeriod classPeriod)
        {
            lid = lid.ToLower();
            Room room = classPeriod.Room;
            bool cf = false;
            foreach (KeyValuePair<string, Class> KeyValOfClass in classList)
            {
                string lecturerId = KeyValOfClass.Value.Lecturer.Id;
                foreach (ClassPeriod cp in KeyValOfClass.Value.ClassPeriodList)
                {
                    if (lecturerId.Equals(lid))
                    {
                        cf = false;
                        cf = cp.TimeConflict(classPeriod);
                        if (cf)
                            break;
                    }
                }
                if (cf)
                    break;
            }
            return cf;
        }

        internal Dictionary<string, Class> RetrieveClassList()
        {
            return classList;
        }

        internal bool AddStudent(Student student, Class enrolClass)
        {
            string key = enrolClass.ToString();
            if (classList[key].EnrolStudent(student))
                return true;
            return false;
        }

        //public List<Class> ListClass(string courseID)
        //{
        //    var listClass = new List<Classes>();
        //    foreach (KeyValuePair<string, Classes> cList in classes)
        //    {
        //        if (cList.Value.Course.Id.EqualsIgnoreCase(courseID))
        //        {
        //            var c = classes[cList.Key];
        //            listClass.Add(c);
        //        }
        //    }
        //    return listClass;
        //}

        internal List<Class> GetEnrolledClasses(Student student)
        {
            List<Class> result = new List<Class>();
            foreach (KeyValuePair<string, Class> kvp in classList)
            {
                List<Student> stdList = kvp.Value.StudentList;
                foreach (Student std in stdList)
                {
                    if (std.Id.Equals(student.Id.ToLower()))
                        result.Add(kvp.Value);
                }
            }
            return result;
        }

        public List<Class> RetrieveClassesListOfCourse(string crid)
        {
            List<Class> classesCourse = new List<Class>();
            foreach (KeyValuePair<string, Class> KeyValClass in classList)
            {
                if (KeyValClass.Value.Course.Id.Equals(crid))
                {
                    classesCourse.Add(KeyValClass.Value);
                }
            }
            return classesCourse;
        }

        internal void DeleteClassName(string editClassName)
        {
            classList.Remove(editClassName);
        }

        internal bool UpdateClass(Lecturer lecturerInfo, Course courseInfo, ClassPeriod nCp)
        {
            return CreateClass(courseInfo, lecturerInfo, nCp);
        }

        internal void CreateClass(Course course, Lecturer lecturer, List<ClassPeriod> list)
        {
            string className = course.Id + "-" + lecturer.Id;
            Class c = new Class(lecturer, course);
            c.ClassPeriodList = list;
        }
    }
}
