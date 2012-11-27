using System;
using NUnit.Framework;

namespace TimeInterval.Tests
{
	[TestFixture]
	public class OccuranceTests
	{
		[Test]
		public void UnitUnderTest_Scenario_ExpectedBehavior()
		{
			var dateTime = TimeInterval.Parse("R/2011-10-25T00:05:00/P0Y0M0DT0H2M0S").NextOccurance();

			Console.WriteLine(dateTime);

		}
	}

	public static class TimeIntervalExtensions
	{
		public static DateTime NextOccurance(this TimeInterval timeInterval)
		{
			if( timeInterval.Start.HasValue==false )
			{
				throw new InvalidOperationException("Unable to compute next occurance without a start date");
			}

			var next = timeInterval.Start.Value;

			// TODO -- fix calculations for far past dates

			while( next<DateTime.Now )
			{
				next = next.AddYears(timeInterval.Duration.Years)
					.AddMonths(timeInterval.Duration.Months)
					.AddDays(timeInterval.Duration.Days)
					.AddHours(timeInterval.Duration.Hours)
					.AddMinutes(timeInterval.Duration.Minutes)
					.AddSeconds(timeInterval.Duration.Seconds);
			}

			return next;
		}
	}
}