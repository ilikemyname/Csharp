using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Assignment01.Utility
{
    static class Validate
    {
        internal static bool ValidCourseID(string value)
        {
            var courseIdPattern = @"^\w{4}\d{4}$";
            var exp = new Regex(courseIdPattern);
            return exp.IsMatch(value);
        }
        internal static bool ValidLecturerID(string value)
        {
            var lecturerIdPattern = @"^v3\d{6}$";
            var exp = new Regex(lecturerIdPattern);
            return exp.IsMatch(value);
        }

        internal static bool ValidNameString(string value)
        {
            var stringPattern = @"^\w+$";
            var exp = new Regex(stringPattern);
            return exp.IsMatch(value);
        }

        internal static bool ValidStudentId(string value)
        {
            var studentIdPattern = @"^s3\d{6}$";
            var exp = new Regex(studentIdPattern);
            return exp.IsMatch(value);
        }

        internal static bool ValidWeekDayString(string s)
        {
            var weekDayPattern = @"^monday|tuesday|wednesday|thursday|friday$";
            var exp = new Regex(weekDayPattern);
            return exp.IsMatch(s);
        }

        internal static bool ValidRoomName(string s)
        {
            var roomNamePattern = @"^\d\.\d\.\d+$";
            var exp = new Regex(roomNamePattern);
            return exp.IsMatch(s);
        }

        internal static bool ValidMinute(string value)
        {
            string[] parts = value.Split(':');
            try
            {
                var uncheckedHour = Int32.Parse(parts[0]);
                var uncheckedMinute = Int32.Parse(parts[1]);
                if (uncheckedMinute != 0 && uncheckedMinute != 30)
                    return false;
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }

        internal static bool TimeConFlict(Model.ClassPeriod classPeriod, Model.ClassPeriod cp)
        {
            throw new NotImplementedException();
        }
    }
}
