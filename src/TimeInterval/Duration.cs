using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TimeInterval
{
	/// <summary>
	/// ISO-8601 Duration only supports the format PnYnMnDTnHnMnS
	/// </summary>
	public class Duration
	{
		private static readonly Regex PartPattern = new Regex(@"((?<value>([0-9]+(\.[0-9]+)?|\.[0-9]+))(?<designator>.))", RegexOptions.Compiled);

		public Duration(int years, int months, int days) : this(years, months, days, 0, 0, 0)
		{
		}

		public Duration(int years, int months, int days, int hours, int minutes, int seconds)
		{
			Years = years;
			Months = months;
			Days = days;
			Hours = hours;
			Minutes = minutes;
			Seconds = seconds;
		}

		private Duration()
		{
		}

		public int Years { get; set; }
		public int Months { get; set; }
		public int Days { get; set; }
		public int Hours { get; set; }
		public int Minutes { get; set; }
		public int Seconds { get; set; }

		public static Duration Parse(string durationString)
		{
			if (!durationString.StartsWith(Designators.PeriodDesignator))
			{
				throw new ArgumentException("Must begin with the period designator " + Designators.PeriodDesignator);
			}

			var duration = new Duration();

			durationString = durationString.Substring(Designators.PeriodDesignator.Length);

			var timeIndex = durationString.IndexOf(Designators.TimeDesignator);
			var dateString = durationString;
			if( timeIndex!=-1 )
			{
				dateString = durationString.Substring(0, timeIndex);
				var timeString = durationString.Substring(timeIndex + 1);
				SetTimeParts(duration, timeString);
			}

			SetDateParts(duration, dateString);

			return duration;
		}

		private static void SetDateParts(Duration duration, string dateString)
		{
			var parts = GetParts(dateString);

			var designators = "YMD";

			foreach(var part in parts)
			{
				var index = designators.IndexOf(part.Designator);
				if( index==-1 )
				{
					throw new ArgumentException("Unknown date designator or illegal position: " + part.Designator);
				}

				designators = designators.Substring(index+1);

				switch( part.Designator )
				{
					case Designators.YearDesignator:
						duration.Years = part.Value;
						break;
					case Designators.MonthDesignator:
						duration.Months = part.Value;
						break;
					case Designators.DayDesignator:
						duration.Days = part.Value;
						break;
				}
			}
		}

		private static void SetTimeParts(Duration duration, string timeString)
		{
			var parts = GetParts(timeString);

			var designators = "HMS";

			foreach (var part in parts)
			{
				var index = designators.IndexOf(part.Designator);
				if (index == -1)
				{
					throw new ArgumentException("Unknown time designator or illegal position: " + part.Designator);
				}

				designators = designators.Substring(index + 1);
				switch (part.Designator)
				{
					case Designators.HourDesignator:
						duration.Hours = part.Value;
						break;
					case Designators.MinuteDesignator:
						duration.Minutes = part.Value;
						break;
					case Designators.SecondDesignator:
						duration.Seconds = part.Value;
						break;
				}
			}
		}

		private static IEnumerable<Part> GetParts(string partString)
		{
			return PartPattern
				.Matches(partString)
				.Cast<Match>()
				.Select(m => new Part(int.Parse(m.Groups["value"].Value), m.Groups["designator"].Value));
		}

		#region Private Part Class
		private class Part
		{
			private readonly int _value;
			private readonly string _designator;

			public Part(int value, string designator)
			{
				_value = value;
				_designator = designator;
			}

			public int Value
			{
				get
				{
					return _value;
				}
			}

			public string Designator
			{
				get
				{
					return _designator;
				}
			}
		}
		#endregion

		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.Append(Designators.PeriodDesignator);

			AppendDesignator(sb, Designators.YearDesignator, Years);
			AppendDesignator(sb, Designators.MonthDesignator, Months);
			AppendDesignator(sb, Designators.DayDesignator, Days);

			if( Hours>0 || Minutes>0 || Seconds>0 )
			{
				sb.Append(Designators.TimeDesignator);

				AppendDesignator(sb, Designators.HourDesignator, Hours);
				AppendDesignator(sb, Designators.MinuteDesignator, Minutes);
				AppendDesignator(sb, Designators.SecondDesignator, Seconds);
			}

			return sb.ToString();
		}

		private static void AppendDesignator(StringBuilder sb, string designator, double value)
		{
			if( value<=0 )
			{
				return;
			}

			sb.AppendFormat("{0}{1}", value, designator);
		}
	}
}