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
        public void Should_convert_date_to_persian()
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
        [InlineData("1397/10/23", "13971023")]
        [InlineData("1397/11/8" , "13971108")]
        [InlineData("1397/2/15" , "13970215")]
        [InlineData("1397/1/8"  , "13970108")]
        public void Should_GetPersianDateKey(string persianDate, string expectedkey)
        {
            // Arrange
            // Act
            var key = persianCalendar.GetPersianDateKey(persianDate);
            // Assert
        }
    }
}
