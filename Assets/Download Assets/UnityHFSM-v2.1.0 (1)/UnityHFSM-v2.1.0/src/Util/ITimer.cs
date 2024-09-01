namespace UnityHFSM
{
	public interface ITimer
	{
		float Elapsed
		{
			get;
		}

		void Reset();

		void Pause();

		void Resume();
	}
}
