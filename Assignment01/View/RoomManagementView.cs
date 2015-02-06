using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assignment01.Utility;
using Assignment01.Model;

namespace Assignment01.View
{
    class RoomManagementView
    {
        private Controller.RoomController roomController;

        public RoomManagementView(Controller.RoomController roomController)
        {
            this.roomController = roomController;
        }

        internal void AddRoom()
        {
            Console.Write("Please provide a room represent name(x.x.x)(1.1.15): ");
            var roomNo = Console.ReadLine();
            while (!Validate.ValidRoomName(roomNo))
            {
                Console.WriteLine("Room name only allows digit at 'x' tight to format(x.x.x).Enter: ");
                roomNo = Console.ReadLine();
            }
            bool added = roomController.CreateRoom(roomNo);
            if (added)
                Console.WriteLine("Congratulation! You added a new \"{0}\" room.", roomNo);
            else
                Console.WriteLine("Sorry! You failed to add a room \"{0}\"", roomNo);
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

        internal void EditRoom()
        {
            int roomCount = roomController.RoomCount();
            if (roomCount > 0)
            {
                Console.WriteLine("Please enter only one room name as displayed below");
                DisplayRoom();
                Console.Write("> ");
                var roomNameToChange = Console.ReadLine();
                while (roomController.SearchRoom(roomNameToChange) == null)
                {
                    Console.WriteLine("Sorry! The room you wanna edit does not exist. Try again!");
                    Console.Write("> ");
                    roomNameToChange = Console.ReadLine();
                }
                Room oldRoom = roomController.SearchRoom(roomNameToChange);
                Console.Write("You will change old name \"{0}\" to new name or press [Enter] to unchange: ", oldRoom.RoomNo);
                string updatedName = Console.ReadLine();
                if (updatedName == "")
                    updatedName = oldRoom.RoomNo;
                // Keep old name of room to modify if it happens error during updating
                string name = oldRoom.RoomNo;

                // Delete course to edit by its id
                roomController.DeleteRoom(roomNameToChange);

                if (roomController.UpdateRoom(updatedName))
                {
                    Console.WriteLine("You updated new information for room name: " + updatedName);
                }
                else
                {
                    // this condition will run if update name duplicate with other room name
                    roomController.CreateRoom(name);
                    Console.WriteLine("You failed to update new information for room name: " + name);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Opp! empty room list. You should use selection 1 to add a room.");
            }
            Console.WriteLine("Press[Enter] button to continue...");
            Console.ReadLine();
        }

        internal void DeleteRoom()
        {
            Console.WriteLine("Please enter only one room name as displayed below");
            DisplayRoom();
            Console.Write("> ");
            string idToDel = Console.ReadLine();
            if (idToDel != "")
            {
                if (roomController.SearchRoom(idToDel) != null)
                {
                    roomController.DeleteRoom(idToDel);
                    Console.WriteLine("You have deleted course \"{0}\".", idToDel);
                }
                else
                {
                    Console.WriteLine("Sorry! The room you wanna delete has never existed.");
                }
            }
            Console.WriteLine("Press[Enter] button to continue...");
            Console.ReadLine();
        }

        internal void DisplayRoom()
        {
            int roomsCount = roomController.RoomCount();
            if (roomsCount == 0)
                Console.WriteLine("Total number of live room is zero");
            else
            {
                Dictionary<string, Room> roomList = roomController.RetrieveRoomList();
                foreach (KeyValuePair<string, Room> KeysValuesOfRoomList in roomList)
                {
                    Console.WriteLine("\t\tRoom name: {0}", KeysValuesOfRoomList.Key);
                }
            }
            Console.WriteLine();
        }
    }
}
