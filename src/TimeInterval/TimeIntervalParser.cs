using System;
using System.Linq;

namespace Magnetix.TimeInterval
{
	public class TimeIntervalParser
	{
		private DateTime? _start;
		private DateTime? _end;
		private Duration _duration;
		private int? _repetitions;

		public TimeInterval Parse(string timeIntervalString)
		{
			if( string.IsNullOrEmpty(timeIntervalString) )
			{
				throw new ArgumentNullException("timeIntervalString");
			}

			var parts = timeIntervalString.Split(new[] {Designators.Solidus}, StringSplitOptions.RemoveEmptyEntries);
			if( parts.Length>3 || parts.Length<1 )
			{
				throw new ArgumentException("Incorrect format", "timeIntervalString");
			}

			if( parts.Length==3 )
			{
				if( !parts[0].StartsWith(Designators.RepetitionDesignator) )
				{
					throw new ArgumentException(string.Format("Missing repetition designator '{0}'", Designators.RepetitionDesignator));
				}

				if( parts[0].Length>1 )
				{
					int repetitions;
					if( !int.TryParse(parts[0].Substring(1), out repetitions) )
					{
						throw new ArgumentException("Repetition count must be an integer ");
					}
					_repetitions = repetitions;
				}
				else
				{
					_repetitions = 0;
				}
			}

			var hasStart = false;
			var hasDuration = false;
			var hasEnd = false;
			var count = 0;

			foreach(var part in parts.Skip(_repetitions.HasValue ? 1 : 0))
			{
				if( part.StartsWith(Designators.PeriodDesignator) )
				{
					_duration = Duration.Parse(part);
					hasDuration = true;
				}
				else
				{
					var dateTime = DateTime.Parse(part);
					if( count==0 )
					{
						_start = dateTime;
						hasStart = true;
					}
					else
					{
						_end = dateTime;
						hasEnd = true;
					}
				}

				count++;
			}

			if( hasStart && hasEnd==false && hasDuration==false )
			{
				throw new ArgumentException("start must have end or duration");
			}
			if( hasEnd && hasStart==false && hasDuration==false )
			{
				throw new ArgumentException("end must have start or duration");
			}

			return new TimeInterval {Duration = _duration, End = _end, Start = _start, Repetitions = _repetitions};
		}
	}
}