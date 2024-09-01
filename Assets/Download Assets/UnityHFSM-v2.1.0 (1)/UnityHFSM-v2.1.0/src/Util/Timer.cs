using UnityEngine;

namespace UnityHFSM
{
	/// <summary>
	/// Default timer that calculates the elapsed time based on Time.time.
	/// </summary>
	public class Timer : ITimer
	{
		public float startTime;
		public bool isPaused;
		public float durationToPause;
		
		public float Elapsed
		{
			get
			{
				return (Time.time - startTime) + durationToPause;
			}
		}

		public void Reset()
		{
			startTime = Time.time;
			
			if (isPaused == false)
			{
				durationToPause = 0;
			}
			else
			{
				Resume();
			}
			
		}

		public void Pause()
		{
			var previusDurationToPause = durationToPause;
			isPaused = true;
			durationToPause = Time.time - startTime + previusDurationToPause;
		}

		public void Resume()
		{
			isPaused = false;
		}

		public static bool operator >(Timer timer, float duration)
			=> timer.Elapsed > duration;

		public static bool operator <(Timer timer, float duration)
			=> timer.Elapsed < duration;

		public static bool operator >=(Timer timer, float duration)
			=> timer.Elapsed >= duration;

		public static bool operator <=(Timer timer, float duration)
			=> timer.Elapsed <= duration;
	}
}
