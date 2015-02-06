using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assignment01.Model.Exception;

namespace Assignment01.Model
{
    class ClassPeriod
    {
        private Room room;
        private string dayOfWeek;
        private DateTime startTime, endTime;
        public ClassPeriod(Room room, string dayOfWeek, DateTime startTime, DateTime endTime)
        {
            this.room = room;
            this.dayOfWeek = dayOfWeek;
            this.startTime = startTime;
            this.endTime = endTime;
        }
        public Room Room
        {
            get { return room; }
            set { room = value; }
        }
        public string DayOfWeek
        {
            get { return dayOfWeek; }
            set { dayOfWeek = value; }
        }
        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }
        public override string ToString()
        {
            return string.Format("Week Day: " + DayOfWeek + " - Start Time: " + StartTime.ToShortTimeString() + " - End Time: " + EndTime.ToShortTimeString());
        }


        internal bool TimeConflict(ClassPeriod classPeriod)
        {
            DateTime cpST = classPeriod.StartTime;
            DateTime cpET = classPeriod.EndTime;
            string cpDayOfWeek = classPeriod.DayOfWeek;
            if ((cpDayOfWeek.Equals(this.DayOfWeek)) &&
                (((cpST.CompareTo(this.StartTime) == 0) && (cpET.CompareTo(this.EndTime) == 0)) ||
                (((cpST.CompareTo(this.StartTime) >= 0) && (cpST.CompareTo(this.EndTime) < 0)) &&
                ((cpET.CompareTo(this.StartTime) > 0) && (cpET.CompareTo(this.EndTime) <= 0))) ||
                ((cpST.CompareTo(this.StartTime) <= 0) && ((cpET.CompareTo(this.StartTime) > 0) && (cpET.CompareTo(this.EndTime) <= 0))) ||
                (((cpST.CompareTo(this.StartTime) >= 0) && (cpST.CompareTo(this.EndTime) < 0)) && (cpET.CompareTo(this.EndTime) >= 0))))
                return true;
            return false;
        }
    }
}























//private float startTime, endTime;
//        private const float MinStartTime = 8;
//        private const float MaxEndTime = 18;
//        private Room room;
//        private int dayOfWeek;

//        public ClassPeriod(Room room, int dayOfWeek, float startTime, float endTime)
//        {
//            // TODO: Complete member initialization
//            this.room = room;
//            this.dayOfWeek = dayOfWeek;
//            this.startTime = startTime;
//            this.endTime = endTime;
//        }

//        public Room ClassRoom { get; set; }

//        public int DayOfWeek
//        {
//            get { return dayOfWeek; }
//            set
//            {
//                if (!Enum.IsDefined(typeof(DayOfWeek), value)) throw new TimeException("Class day must be in range of Monday to Friday.");
//                dayOfWeek = value;
//            }
//        }

//        public float StartTime
//        {
//            get { return startTime; }
//            set
//            {
//                if (value < MinStartTime || value > (MaxEndTime - 0.5F))
//                {
//                    throw new TimeException("Start time class must be in range of 8:00(8) and 17:30(17.5).");
//                }
//                startTime = value;
//            }
//        }

//        public float EndTime
//        {
//            get { return endTime; }
//            set
//            {
//                if(value < (MinStartTime + 0.5F) || value > MaxEndTime) 
//                {
//                    throw new TimeException("End time class must be in range of 8:30(8.5) and 18:00(18).");
//                }
//                else if(value <= startTime)
//                {
//                    throw new TimeException("End time class cannot be earlier than Start time class.");
//                }
//                endTime = value;
//            }
//        }

//        public bool CheckClashedClass(ClassPeriod otherClassPeriod)
//        {
//            if (otherClassPeriod.ClassRoom.Equals(ClassRoom))
//            {
//                if (otherClassPeriod.dayOfWeek == this.dayOfWeek || otherClassPeriod.StartTime < this.EndTime ||
//                    otherClassPeriod.EndTime > this.StartTime)
//                    return true;
//                return false;
//            }
//            else return false;
//        }
//    }