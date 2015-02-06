using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assignment01.Utility;

namespace Assignment01.Model
{
    class Student
    {
        private string id, firstName, lastName, middleName;
        private List<Class> myClasses = new List<Class>();
        public Student() { }

        public Student(string id, string firstName, string middleName, string lastName)
        {
            this.id = id;
            this.firstName = firstName;
            this.middleName = middleName;
            this.lastName = lastName;
        }

        public void AddClass(Class c)
        {
            myClasses.Add(c);
        }

        public List<Class> GetMyClasses()
        {
            return myClasses;
        }

        public void RemoveClass(Class c)
        {
            myClasses.Remove(c);
        }

        public string Id
        {
            get { return id; }
            set
            {
                id = value;
            }
        }
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
            }
        }
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
            }
        }
        public string MiddleName
        {
            get { return middleName; }
            set
            {
                middleName = value;
            }
        }
        public override string ToString()
        {
            return String.Format(firstName + " " + middleName + " " + lastName);
        }
    }
}
