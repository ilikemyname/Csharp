using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assignment01.Utility;

namespace Assignment01.Model
{
    class Lecturer
    {
        private string id, firtName, lastName, middleName;
        public Lecturer() { }

        public Lecturer(string id, string firstName, string middleName, string lastName)
        {
            this.id = id;
            this.firtName = firstName;
            this.middleName = middleName;
            this.lastName = lastName;
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
            get { return firtName; }
            set
            {
                firtName = value;
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
            return String.Format(firtName + " " + middleName + " " + lastName);
        }
    }
}
