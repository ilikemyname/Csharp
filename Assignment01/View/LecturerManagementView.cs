using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assignment01.Controller;
using Assignment01.Utility;
using Assignment01.Model;

namespace Assignment01.View
{
    class LecturerManagementView
    {
        private LecturerController lecturerController;
        public LecturerManagementView(LecturerController lecturerController)
        {
            this.lecturerController = lecturerController;
        }

        internal void AddLecturer()
        {
            Console.Write("Please provide a lecturer id: ");
            var lecturerID = Console.ReadLine();
            while (!Validate.ValidLecturerID(lecturerID))
            {
                Console.WriteLine("Course id violated standard format 'v3' prefix, 6 digits suffix. Please retry as (v3xxxxxx): ");
                lecturerID = Console.ReadLine();
            }

            Console.Write("Please provide first name: ");
            var firstName = Console.ReadLine();
            while (firstName == "")
            {
                Console.Write("Blank! Please enter first name: ");
                firstName = Console.ReadLine();
            }
            while(!Validate.ValidNameString(firstName))
            {
                Console.WriteLine("First name does not allow space and number");
                firstName = Console.ReadLine();
            }

            Console.Write("Please provide middle name: ");
            var middleName = Console.ReadLine();
            while (middleName == "")
            {
                Console.Write("Blank! Please enter middle name: ");
                middleName = Console.ReadLine();
            }
            while (!Validate.ValidNameString(middleName))
            {
                Console.WriteLine("Middle name does not allow space and number");
                middleName = Console.ReadLine();
            }

            Console.Write("Please provide last name: ");
            var lastName = Console.ReadLine();
            while (lastName == "")
            {
                Console.Write("Blank! Please enter last name: ");
                lastName = Console.ReadLine();
            }
            while (!Validate.ValidNameString(lastName))
            {
                Console.WriteLine("Last name does not allow space and number");
                lastName = Console.ReadLine();
            }

            bool added = lecturerController.CreateLecturer(lecturerID, firstName, middleName, lastName);
            if (added)
                Console.WriteLine("\nCongratulation! You added a new lecturer has id: " + lecturerID);
            else
                Console.WriteLine("\nSorry! You failed to add a lecturer");
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

        internal void EditLecturer()
        {
            int lecturerCount = lecturerController.LecturerCount();
            if (lecturerCount > 0)
            {
                Console.WriteLine("Please enter only one course id as displayed below");
                DisplayLecturer();
                Console.Write("> ");
                var lecturerIdToChange = Console.ReadLine();
                while (lecturerController.SearchLecturer(lecturerIdToChange) == null)
                {
                    Console.WriteLine("Sorry! The lecturer you wanna edit does not exist. Try again!");
                    Console.Write("> ");
                    lecturerIdToChange = Console.ReadLine();
                }
                Lecturer oldLecturer = lecturerController.SearchLecturer(lecturerIdToChange);
                Console.Write("You will change old id \"{0}\" to new id or press [Enter] to unchange: ", oldLecturer.Id);
                string updatedId = Console.ReadLine();
                Console.Write("You will change old first name \"{0}\" to new name or press [Enter] to unchange: ", oldLecturer.FirstName);
                string updatedFName = Console.ReadLine();
                Console.Write("You will change old middle name \"{0}\" to new name or press [Enter] to unchange: ", oldLecturer.MiddleName);
                string updatedMName = Console.ReadLine();
                Console.Write("You will change old last name \"{0}\" to new name or press [Enter] to unchange: ", oldLecturer.LastName);
                string updatedLName = Console.ReadLine();

                if (updatedId == "")
                    updatedId = oldLecturer.Id;
                if (updatedFName == "")
                    updatedFName = oldLecturer.FirstName;
                if (updatedMName == "")
                    updatedMName = oldLecturer.MiddleName;
                if (updatedLName == "")
                    updatedLName = oldLecturer.LastName;

                // Keep old information of lecturer to modify if it happens error during updating
                string id = oldLecturer.Id;
                string fName = oldLecturer.FirstName;
                string mName = oldLecturer.MiddleName;
                string lName = oldLecturer.LastName;

                // Delete course to edit by its id
                lecturerController.DeleteLecturer(lecturerIdToChange);

                if (lecturerController.UpdateLecturer(updatedId, updatedFName, updatedMName, updatedLName))
                {
                    Console.WriteLine("\nYou updated new information for lecturer id: " + updatedId);
                }
                else
                {
                    // Recreate old lecturer if updating fail
                    lecturerController.CreateLecturer(id, fName, mName, lName);
                    Console.WriteLine("\nYou failed to update new information for lecturer id: " + id);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Opp! empty lecturer list. You should use selection 1 to add a lecturer.");
            }
            Console.WriteLine("Press[Enter] button to continue...");
            Console.ReadLine();
        }

        internal void DeleteLecturer()
        {
            int lecturerCount = lecturerController.LecturerCount();
            if (lecturerCount > 0)
            {
                Console.WriteLine("Delete the lecturer");
                Console.WriteLine("Enter only one lecturer id as displayed below to delete that lecturer");
                DisplayLecturer();
                Console.Write("> ");
                string idToDel = Console.ReadLine();
                if (idToDel != "")
                {
                    if (lecturerController.SearchLecturer(idToDel) != null)
                    {
                        lecturerController.DeleteLecturer(idToDel);
                        Console.WriteLine("You have deleted lecturer \"{0}\".", idToDel);
                    }
                    else
                    {
                        Console.WriteLine("Sorry! The lecturer you wanna delete has never existed.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Opp! empty lecturer list. You should use selection 1 to add a lecturer.");
            }
            Console.WriteLine("Press[Enter] button to continue...");
            Console.ReadLine();
        }

        internal void DisplayLecturer()
        {
            int lecturersCount = lecturerController.LecturerCount();
            if (lecturersCount == 0)
                Console.WriteLine("Total number of added lecturer is zero");
            else
            {
                Dictionary<string, Lecturer> lecturerList = lecturerController.RetrieveLecturerList();
                foreach (KeyValuePair<string, Lecturer> KeysValuesOfLectureList in lecturerList)
                {
                    Console.WriteLine("\t\tLecturer id: {0}\n" +
                                      "\t\tLecturer first name: {1}\n" +
                                      "\t\tLecturer middle name: {2}\n" +
                                      "\t\tLecturer last name: {3}\n",
                                      KeysValuesOfLectureList.Key,
                                      KeysValuesOfLectureList.Value.FirstName,
                                      KeysValuesOfLectureList.Value.MiddleName,
                                      KeysValuesOfLectureList.Value.LastName);
                }
            }
            Console.WriteLine();
        }
    }
}
