using PersianCalendarExtensions;
using System;
using System.Globalization;
using Xunit;

namespace PersianCalendarExtensionsTest
{
    public class PersianCalendarExtensionsTests
    {
        readonly DateTime now = DateTime.Now;
        readonly PersianCalendar persianCalendar = new PersianCalendar();

        [Theory]
        [InlineData("1397/11/80")]
        [InlineData("1397/13/08")]
        [InlineData("97/11/08")]
        public void Should_throw_ArgumentException_when_persian_date_incorrect(string persianDate)
        {
            // Arrange
            // Act
            Action act = () => persianCalendar.IsValidDate(persianDate);
            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void Should_convert_gregorian_to_persian()
        {
            // Arrange
            var date = new DateTime(2019, 1, 28);
            var expected = Utility.ConvertToPersian(date);
            // Act
            var persian = persianCalendar.ConvertToPersian(date);
            // Assert
            Assert.Equal(expected, persian);
        }
        [Fact]
        public void Should_gave_today_as_persian_date()
        {
            // Arrange
            var expected = Utility.ConvertToPersian(now);
            // Act
            var today = persianCalendar.Today();
            // Assert
            Assert.Equal(expected, today);
        }

        [Fact]
        public void Should_convert_persian_to_gregorian()
        {
            // Arrange
            var persianDate = "1397/11/8";
            var expected = Utility.ConvertToGregorian(persianDate);

            // Act
            var gregorian = persianCalendar.ConvertToGregorian(persianDate);
            // Asssert
            
        }

        [Theory]
        [InlineData("1397/10/23", 13971023)]
        [InlineData("1397/11/8" , 13971108)]
        [InlineData("1397/2/15" , 13970215)]
        [InlineData("1397/1/8"  , 13970108)]
        public void Should_GetPersianDateKey(string persianDate, int expectedkey)
        {
            // Arrange
            // Act
            var key = persianCalendar.GetPersianDateKey(persianDate);
            // Assert
            Assert.Equal(expectedkey, key);
        }

        [Fact]
        public void Should_get_perisan_date_day_of_month()
        {
            // Arrange
            int expected = 10;
            string persianDate = $"1397/11/{expected}";
            // Act
            var dayOfMonth = persianCalendar.GetDayOfMonth(persianDate);
            // Assert
            Assert.Equal(expected, dayOfMonth);
        }

        [Fact]
        public void Should_get_perisan_date_month_of_year()
        {
            // Arrange
            int expected = 11;
            string persianDate = $"1397/{expected}/10";
            // Act
            var dayOfMonth = persianCalendar.GetMonth(persianDate);
            // Assert
            Assert.Equal(expected, dayOfMonth);
        }

        [Fact]
        public void Should_get_perisan_date_year()
        {
            // Arrange
            int expected = 1397;
            string persianDate = $"{expected}/11/10";
            // Act
            var year = persianCalendar.GetYear(persianDate);
            // Assert
            Assert.Equal(expected, year);
        }

        [Theory]
        [InlineData("1397/11/10", 10, "1397/11/20")]
        [InlineData("1397/11/10", 25, "1397/12/05")]
        [InlineData("1397/11/10", 60, "1398/01/11")]
        [InlineData("1395/11/10", 60, "1396/01/10")]
        public void Should_add_day_to_perisan_date(string current, int days, string expected)
        {
            // Arrange
            // Act
            var final = persianCalendar.AddDays(current, days);
            // Assert
            Assert.Equal(expected, final);
        }

        [Theory]
        [InlineData("1397/11/11", 1, "1397/12/11")]
        [InlineData("1397/11/11", 3, "1398/02/11")]
        [InlineData("1397/06/11", 1, "1397/07/11")]
        [InlineData("1397/11/30", 1, "1397/12/29")]
        public void Should_add_month_to_persian_date(string current, int months, string expected)
        {
            // Arrange
            // Act
            var final = persianCalendar.AddMonths(current, months);
            // Assert
            Assert.Equal(expected, final);
        }
    }
}
