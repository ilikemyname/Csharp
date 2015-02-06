using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assignment01.Utility;

namespace Assignment01.Model
{
    public class Course
    {
        private string id;
        private string courseName;
        private string courseDes;
        public Course() { }

        public Course(string id, string courseName, string courseDes)
        {
            // TODO: Complete member initialization
            this.id = id;
            this.courseName = courseName;
            this.courseDes = courseDes;
        }
        public string Description { get { return courseDes; } set { courseDes = value; } }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name { get { return courseName; } set { courseName = value; } }

        public override string ToString()
        {
            return String.Format(courseName);
        }
    }
}
