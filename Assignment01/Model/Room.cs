using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assignment01.Utility;

namespace Assignment01.Model
{
    class Room
    {
        private string roomNo;
        public Room() { }

        public Room(string roomNo)
        {
            this.roomNo = roomNo;
        }

        public string RoomNo
        {
            get { return roomNo; }
            set { roomNo = value; }
        }
        public override string ToString()
        {
            return String.Format(roomNo);
        }

    }
}
