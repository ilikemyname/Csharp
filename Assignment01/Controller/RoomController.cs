using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assignment01.Model;
using Assignment01.Utility;

namespace Assignment01.Controller
{
    class RoomController
    {
        private Dictionary<string, Room> roomList = new Dictionary<string, Room>();

        internal bool CreateRoom(string roomNo)
        {
            roomNo = roomNo.ToLower();
            if(!Validate.ValidRoomName(roomNo))
                return false;
            if (CheckRoomDuplicated(roomNo))
            {
                Console.WriteLine("Sorry! This room has duplicated with the other.");
                return false;
            }
            Room room = new Room(roomNo);
            roomList.Add(roomNo, room);
            return true;
        }

        private bool CheckRoomDuplicated(string roomNo)
        {
            if (roomList.ContainsKey(roomNo))
                return true;
            return false;
        }

        internal int RoomCount()
        {
            return roomList.Count;
        }

        internal Dictionary<string, Room> RetrieveRoomList()
        {
            return roomList;
        }

        internal Room SearchRoom(string roomNameToChange)
        {
            if (roomList.ContainsKey(roomNameToChange))
                return roomList[roomNameToChange];
            return null;
        }

        internal void DeleteRoom(string roomNameToChange)
        {
            roomList.Remove(roomNameToChange);
        }

        internal bool UpdateRoom(string updatedName)
        {
            if (Validate.ValidRoomName(updatedName))
            {
                if (SearchRoom(updatedName) == null)
                {
                    CreateRoom(updatedName);
                    return true;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Sorry! the program detected that this room was added already.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Sorry! you entered an invalid room name!");
            }
            return false;
        }
    }
}
