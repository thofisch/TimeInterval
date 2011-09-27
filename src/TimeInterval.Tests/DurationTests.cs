using System;
using NUnit.Framework;

namespace TimeInterval.Tests
{
	[TestFixture]
	public class DurationTests
	{
		private const string StartTimeString = "2009-08-04T12:00:00";
		private const string EndTimeString = "2010-08-04T12:30:00";
		private const string DurationString = "P1Y2M3DT4H5M6S";

		private static readonly DateTime StartTime = new DateTime(2009, 8, 4, 12, 0, 0);
		private static readonly DateTime EndTime = new DateTime(2010, 8, 4, 12, 30, 0);

		[Test]
		public void Test_TimeIntervalWithRepeatOnceStartDuration_IsRepeatable()
		{
			var s = string.Format("R1/{0}/{1}", StartTimeString, DurationString);

			var timeInterval = TimeInterval.Parse(s);

			Assert.AreEqual(true, timeInterval.IsRepeatable);
		}

		[Test]
		public void Test_TimeIntervalWithRepeatStartDuration_IsRepeatable()
		{
			var s = string.Format("R/{0}/{1}", StartTimeString, DurationString);

			var timeInterval = TimeInterval.Parse(s);

			Assert.AreEqual(true, timeInterval.IsRepeatable);
		}

		[Test]
		public void Test_TimeIntervalWithRepeatStartDuration_HasRepeatCountZeroe()
		{
			var s = string.Format("R/{0}/{1}", StartTimeString, DurationString);

			var timeInterval = TimeInterval.Parse(s);

			Assert.AreEqual(0, timeInterval.Repetitions);
		}

		[Test]
		public void Test_TimeIntervalWithRepeatOnceStartDuration_RepeatsOnce()
		{
			var s = string.Format("R1/{0}/{1}", StartTimeString, DurationString);

			var timeInterval = TimeInterval.Parse(s);

			Assert.AreEqual(1, timeInterval.Repetitions);
		}

		[Test]
		public void Test_TimeIntervalWithStartDuration_NoRepeats()
		{
			var s = string.Format("{0}/{1}", StartTimeString, DurationString);

			var timeInterval = TimeInterval.Parse(s);

			Assert.AreEqual(false, timeInterval.IsRepeatable);
		}

		[Test]
		public void Test_TimeIntervalWithStartDuration_HasCorrectStartTime()
		{
			var s = string.Format("{0}/{1}", StartTimeString, DurationString);

			var timeInterval = TimeInterval.Parse(s);

			Assert.AreEqual(StartTime, timeInterval.Start);
		}

		[Test]
		public void Test_TimeIntervalWithStartDuration_HasNoEndTime()
		{
			var s = string.Format("{0}/{1}", StartTimeString, DurationString);

			var timeInterval = TimeInterval.Parse(s);

			Assert.AreEqual(null, timeInterval.End);
		}

		[Test]
		public void Test_TimeIntervalWithStartDuration_HasCorrectDuration()
		{
			var s = string.Format("{0}/{1}", StartTimeString, DurationString);

			var timeInterval = TimeInterval.Parse(s);

			Assert.AreEqual(DurationString, timeInterval.Duration.ToString());
		}

		[Test]
		public void Test_TimeIntervalWithDurationEnd_HasCorrectDuration()
		{
			var s = string.Format("{0}/{1}", DurationString, EndTimeString);

			var timeInterval = TimeInterval.Parse(s);

			Assert.AreEqual(DurationString, timeInterval.Duration.ToString());
		}

		[Test]
		public void Test_TimeIntervalWithDurationEnd_HasCorrectEndTime()
		{
			var s = string.Format("{0}/{1}", DurationString, EndTimeString);

			var timeInterval = TimeInterval.Parse(s);

			Assert.AreEqual(EndTime, timeInterval.End);
		}

		[Test]
		public void Test_TimeIntervalWithDurationEnd_HasNoStartTime()
		{
			var s = string.Format("{0}/{1}", DurationString, EndTimeString);

			var timeInterval = TimeInterval.Parse(s);

			Assert.AreEqual(null, timeInterval.Start);
		}

		[Test]
		public void Test_TimeIntervalWithStartEnd_HasCorrectStartTime()
		{
			var s = string.Format("{0}/{1}", StartTimeString, EndTimeString);

			var timeInterval = TimeInterval.Parse(s);

			Assert.AreEqual(StartTime, timeInterval.Start);
		}

		[Test]
		public void Test_TimeIntervalWithStartEnd_HasCorrectEndTime()
		{
			var s = string.Format("{0}/{1}", StartTimeString, EndTimeString);

			var timeInterval = TimeInterval.Parse(s);

			Assert.AreEqual(EndTime, timeInterval.End);
		}

		[Test]
		public void Test_TimeIntervalWithStartEnd_HasNoDuration()
		{
			var s = string.Format("{0}/{1}", StartTimeString, EndTimeString);

			var timeInterval = TimeInterval.Parse(s);

			Assert.AreEqual(null, timeInterval.Duration);
		}

		[Test]
		public void Test_TimeIntervalWithDuration_HasNoStartTime()
		{
			var timeInterval = TimeInterval.Parse(DurationString);

			Assert.AreEqual(null, timeInterval.Start);
		}

		[Test]
		public void Test_TimeIntervalWithDuration_HasNoEndTime()
		{
			var timeInterval = TimeInterval.Parse(DurationString);

			Assert.AreEqual(null, timeInterval.End);
		}

		[Test]
		public void Test_TimeIntervalWithDuration_HasCorrectDuration()
		{
			var timeInterval = TimeInterval.Parse(DurationString);

			Assert.AreEqual(DurationString, timeInterval.Duration.ToString());
		}
	}
}