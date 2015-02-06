using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Assignment01.Model
{
    class Class
    {
        private List<Student> studentList = new List<Student>();
        private Lecturer lecturer;
        private Course course;
        private List<ClassPeriod> classPeriodList = new List<ClassPeriod>();
        //private string className;

        public Class(Lecturer lecturer, Course course)
        {
            this.Course = course;
            this.Lecturer = lecturer;
        }

        //public string ClassName
        //{
        //    get { return className; }
        //    set
        //    {
        //        className = value;
        //    }

        //}

        public void AddPeriod(ClassPeriod newPeriod)
        {
            classPeriodList.Add(newPeriod);
        }

        public void RemovePeriod(ClassPeriod period)
        {
            classPeriodList.Remove(period);
        }

        public Lecturer Lecturer { get { return lecturer; } set { lecturer = value; } }
        public Course Course { get { return course; } set { course = value; } }

        public List<ClassPeriod> ClassPeriodList
        {
            get { return classPeriodList; }
            set { classPeriodList = value; }
        }

        public bool EnrolStudent(Student s)
        {
            studentList.Add(s);
            return true;
        }
        public void WithDrawStudent(Student s)
        {
            studentList.Remove(s);
        }
        public int StudentCount()
        {
            return studentList.Count;
        }
        public List<Student> StudentList
        {
            get { return studentList; }
        }
        public override string ToString()
        {
            return string.Format(course.Id + "-" + lecturer.Id);
        }

        //internal void DeleteStudent(Student student)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
