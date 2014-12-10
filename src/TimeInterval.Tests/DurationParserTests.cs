using System;
using NUnit.Framework;

namespace TimeInterval.Tests
{
    [TestFixture]
    public class DurationParserTests
    {
        /*
         */

        [TestCase("P1Y", 1, 0, 0, 0, 0, 0)]
        [TestCase("P1M", 0, 1, 0, 0, 0, 0)]
        [TestCase("P1D", 0, 0, 1, 0, 0, 0)]
        [TestCase("P1H", 0, 0, 0, 0, 0, 0, ExpectedException = typeof (ArgumentException))]
        [TestCase("P1S", 0, 0, 0, 0, 0, 0, ExpectedException = typeof (ArgumentException))]
        [TestCase("PT1H", 0, 0, 0, 1, 0, 0)]
        [TestCase("PT1M", 0, 0, 0, 0, 1, 0)]
        [TestCase("PT1S", 0, 0, 0, 0, 0, 1)]
        [TestCase("P1Y2M3D", 1, 2, 3, 0, 0, 0)]
        [TestCase("PT4H5M6S", 0, 0, 0, 4, 5, 6)]
        [TestCase("P1Y2M3DT4H5M6S", 1, 2, 3, 4, 5, 6)]
        public void Can_parse_duration(string durationString, int years, int months, int days, int hours, int minutes, int seconds)
        {
            var duration = Duration.Parse(durationString);

            Assert.AreEqual(years, duration.Years);
            Assert.AreEqual(months, duration.Months);
            Assert.AreEqual(days, duration.Days);
            Assert.AreEqual(hours, duration.Hours);
            Assert.AreEqual(minutes, duration.Minutes);
            Assert.AreEqual(seconds, duration.Seconds);
        }
    }
}