using System;

namespace Magnetix.TimeInterval
{
	/// <summary>
	/// An ISO-8601 time intervals implementation.
	/// 
	///		[repetition]:	Rn/[interval] | R/[interval]
	///		[interval]:		[datetime_start]/[datetime_end]	|
	///						[datetime_start]/[duration]		|
	///						[duration]/[datetime_end]		|
	///						[duration]
	///		[duration]:		PnYnMnDTnHnMnS
	///		[datetime]:		[date]T[time] | [date]T[time]Z
	///		[date]:			YYYY-MM-DD | YYYY-MM
	///		[time]:			hh:mm:ss | hh:mm
	/// 
	/// </summary>
	public class TimeInterval
	{
		public DateTime? Start { get; internal set; }

		public DateTime? End { get; internal set; }

		public Duration Duration { get; internal set; }

		public int? Repetitions { get; internal set; }

		public bool IsRepeatable
		{
			get
			{
				return Repetitions.HasValue;
			}
		}

		public static TimeInterval Parse(string timeIntervalString)
		{
			return new TimeIntervalParser().Parse(timeIntervalString);
		}
	}
}