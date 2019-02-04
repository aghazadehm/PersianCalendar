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
        public static int GetPersianDateKey(this PersianCalendar persianCalendar, string persianDate)
        {
            if (!IsValidDate(persianCalendar, persianDate)) throw new ArgumentException();
            var date = ConvertToGregorian(persianCalendar, persianDate);
            var evaluatedPersian = ConvertToPersian(persianCalendar, date);
            return Convert.ToInt32(evaluatedPersian.Replace("/", ""));
        }
        public static string GetDate(this PersianCalendar persianCalendar, int persianDateKey)
        {
            var key = persianDateKey.ToString();
            var date = key.Substring(0, 4) +
                "/" + key.Substring(4, 2) +
                "/" + key.Substring(6, 2);
            if(!IsValidDate(persianCalendar, date)) throw new ArgumentException();
            return date;
        }
        public static int GetDayOfMonth(this PersianCalendar persianCalendar, string persianDate)
        {
            if (!IsValidDate(persianCalendar, persianDate)) throw new ArgumentException();
            var date = ConvertToGregorian(persianCalendar, persianDate);
            return persianCalendar.GetDayOfMonth(date);
        }
        public static int GetMonth(this PersianCalendar persianCalendar, string persianDate)
        {
            if (!IsValidDate(persianCalendar, persianDate)) throw new ArgumentException();
            var date = ConvertToGregorian(persianCalendar, persianDate);
            return persianCalendar.GetMonth(date);
        }
        public static int GetYear(this PersianCalendar persianCalendar, string persianDate)
        {
            if (!IsValidDate(persianCalendar, persianDate)) throw new ArgumentException();
            var date = ConvertToGregorian(persianCalendar, persianDate);
            return persianCalendar.GetYear(date);
        }
        public static string AddDays(this PersianCalendar persianCalendar, string persianDate, int days)
        {
            if (!IsValidDate(persianCalendar, persianDate)) throw new ArgumentException();
            var date = ConvertToGregorian(persianCalendar, persianDate);
            return ConvertToPersian(persianCalendar, date.AddDays(days));
        }
        public static string AddMonths(this PersianCalendar persianCalendar, string persianDate, int months)
        {
            if (!IsValidDate(persianCalendar, persianDate)) throw new ArgumentException();
            var init = ConvertToGregorian(persianCalendar, persianDate);
            var final = persianCalendar.AddMonths(init, months);
            return ConvertToPersian(persianCalendar, final);
        }
        public static string AddYear(this PersianCalendar persianCalendar, string persianDate, int number)
        {
            try
            {
                DateTime initDate = ConvertToGregorian(persianCalendar, persianDate);
                DateTime finalDate = persianCalendar.AddYears(initDate, number);
                string finalPersianDate = ConvertToPersian(persianCalendar, finalDate);
                return finalPersianDate;
            }
            catch
            {
                return null;
            }
        }
        public static int GetWeekOfYear(this PersianCalendar persianCalendar, string persianDate)
        {
            return 0;
        }
        public static int GetDayOfWeek(this PersianCalendar persianCalendar, string persianDate)
        {
            try
            {
                int day = 0;
                DateTime date = ConvertToGregorian(persianCalendar, persianDate);

                switch (date.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        day = 2;
                        break;
                    case DayOfWeek.Monday:
                        day = 3;
                        break;
                    case DayOfWeek.Tuesday:
                        day = 4;
                        break;
                    case DayOfWeek.Wednesday:
                        day = 5;
                        break;
                    case DayOfWeek.Thursday:
                        day = 6;
                        break;
                    case DayOfWeek.Friday:
                        day = 7;
                        break;
                    case DayOfWeek.Saturday:
                        day = 1;
                        break;
                }
                return day;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static string GetDayOfWeekName(this PersianCalendar persianCalendar, string persianDate)
        {
            string dayOfWeek = "";
            DateTime date = ConvertToGregorian(persianCalendar, persianDate);
            switch (date.DayOfWeek)
            {
                case System.DayOfWeek.Sunday:
                    dayOfWeek = "یک شنبه";
                    break;
                case System.DayOfWeek.Monday:
                    dayOfWeek = "دوشنبه";
                    break;
                case System.DayOfWeek.Tuesday:
                    dayOfWeek = "سه شنبه";
                    break;
                case System.DayOfWeek.Wednesday:
                    dayOfWeek = "چهار شنبه";
                    break;
                case System.DayOfWeek.Thursday:
                    dayOfWeek = "پنج شنبه";
                    break;
                case System.DayOfWeek.Friday:
                    dayOfWeek = "جمعه";
                    break;
                case System.DayOfWeek.Saturday:
                    dayOfWeek = "شنبه";
                    break;
            }
            return dayOfWeek;
        }
        public static int GetDayOfYear(this PersianCalendar persianCalendar, string persianDate)
        {
            DateTime date = ConvertToGregorian(persianCalendar, persianDate);
            return persianCalendar.GetDayOfYear(date);
        }
        public static bool IsLeapYear(this PersianCalendar persianCalendar, int parsiYear)
        {
            return persianCalendar.IsLeapYear(parsiYear);
        }
        public static bool IsLeapMonth(this PersianCalendar persianCalendar, int parsiYear, int parsiMonth)
        {
            return persianCalendar.IsLeapMonth(parsiYear, parsiMonth);
        }

    }

}
