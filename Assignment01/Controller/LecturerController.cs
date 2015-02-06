using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assignment01.Model;
using Assignment01.Utility;

namespace Assignment01.Controller
{
    class LecturerController
    {
        private Dictionary<string, Lecturer> lecturerList = new Dictionary<string, Lecturer>();

        internal bool CreateLecturer(string lecturerID, string firstName, string middleName, string lastName)
        {
            lecturerID = lecturerID.ToLower();
            if(!Validate.ValidLecturerID(lecturerID))
                return false;
            bool duplicated = CheckLecturerDuplicated(lecturerID);
            if (duplicated)
            {
                Console.WriteLine("\nSorry! the program detected that this lecturer was added already.");
                return false;
            }
            else
            {
                Lecturer lecturer = new Lecturer(lecturerID, firstName, middleName, lastName);
                lecturerList.Add(lecturerID, lecturer);
                return true;
            }
        }

        private bool CheckLecturerDuplicated(string lecturerID)
        {
            if (lecturerList.ContainsKey(lecturerID))
                return true;
            return false;
        }

        internal int LecturerCount()
        {
            return lecturerList.Count;
        }

        internal Dictionary<string, Lecturer> RetrieveLecturerList()
        {
            return lecturerList;
        }

        internal Lecturer SearchLecturer(string idToDel)
        {
            if (lecturerList.ContainsKey(idToDel))
                return lecturerList[idToDel];
            return null;
        }

        internal void DeleteLecturer(string idToDel)
        {
            if (SearchLecturer(idToDel) != null)
                lecturerList.Remove(idToDel);
        }

        internal bool UpdateLecturer(string updatedId, string updatedFName, string updatedMName, string updatedLName)
        {
            if (Validate.ValidLecturerID(updatedId))
            {
                if (SearchLecturer(updatedId) == null)
                {
                    CreateLecturer(updatedId, updatedFName, updatedMName, updatedLName);
                    return true;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Sorry! the program detected that this lecturer was added already.");
                    return false;
                }
            }
            else
                Console.WriteLine("Sorry! Update id is not valid.");
            return false;
        }

        public List<Lecturer> GetLecturers()
        {
            List<Lecturer> result = new List<Lecturer>();
            foreach(KeyValuePair<string, Lecturer> kvp in lecturerList)
            {
                result.Add(kvp.Value);
            }
            return result;
        }


    }
}
