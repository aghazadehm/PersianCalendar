using System;
using System.Globalization;

namespace PersianCalendarExtensions
{
    public static class PersianCalendarExtensions
    {
        const int MinLength = 8;
        const int MaxLength = 10;

        public static bool IsValidDate(this PersianCalendar persianCalendar, string persianDate)
        {
            if (string.IsNullOrWhiteSpace(persianDate)
                || persianDate.Length < MinLength
                || persianDate.Length > MaxLength)
                return false;
            var segments = persianDate.Split('/');
            if (segments.Length != 3)
                return false;
            if (int.TryParse(segments[0], out int y)
                && int.TryParse(segments[1], out int m)
                && int.TryParse(segments[2], out int d))
            {
                try
                {
                    var date = new DateTime(y, m, d, persianCalendar);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
                return false;
        }
        public static string Today(this PersianCalendar persianCalendar)
        {
            return persianCalendar.ConvertToPersian(DateTime.Today);
        }
        public static string ConvertToPersian(this PersianCalendar persianCalendar, DateTime date)
        {
            int year = persianCalendar.GetYear(date);
            int month = persianCalendar.GetMonth(date);
            int day = persianCalendar.GetDayOfMonth(date);
            return string.Format("{0:D4}/{1:D2}/{2:D2}", year, month, day);
        }
        public static DateTime ConvertToGregorian(this PersianCalendar persianCalendar, string persianDate)
        {
            if (!IsValidDate(persianCalendar, persianDate)) throw new ArgumentException();
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
        private static DateTime ToDateTime(string persianDate)
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
        public static int GetParsiDateKey(this PersianCalendar persianCalendar, string persianDate)
        {
            if (!IsValidDate(persianCalendar, persianDate)) throw new ArgumentException();
            var date = ConvertToGregorian(persianCalendar, persianDate);
            var persian = ConvertToPersian(persianCalendar, date);
            return Convert.ToInt32(persianDate.Replace("/", ""));
        }
    }
}
