using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PersianCalendarExtensionsTest
{
    internal class Utility
    {
        static PersianCalendar persianCalendar = new PersianCalendar();
        public static string ConvertToPersian(DateTime now)
        {
            int year = persianCalendar.GetYear(now);
            int month = persianCalendar.GetMonth(now);
            int day = persianCalendar.GetDayOfMonth(now);
            return string.Format("{0:D4}/{1:D2}/{2:D2}", year, month, day);
        }

        internal static DateTime ConvertToGregorian(string persianDate)
        {
            try
            {
                string[] dateSegments = persianDate.Split('/');
                int year = 0;
                if (dateSegments[0].Length == 4)
                    int.TryParse(dateSegments[0], out year);

                int month = 0;
                if (dateSegments[1].Length <= 2 && dateSegments[1].Length > 0)
                    int.TryParse(dateSegments[1], out month);

                int day = 0;
                if (dateSegments[2].Length <= 2 && dateSegments[2].Length > 0)
                    int.TryParse(dateSegments[2], out day);
                if (day == 0 || month == 0 || year == 0)
                    throw new ArgumentException();
                return new DateTime(year, month, day, new PersianCalendar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
