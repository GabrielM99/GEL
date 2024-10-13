using System;

namespace Fusyon.GEL
{
	public interface IGELAnimationPlayer
	{
		Action<object> OnEventTriggered { get; set; }

		void Play(string name, float speed = 1f);
		bool IsPlaying(string name);
		void SetBool(string name, bool value);
		void SetInt(string name, int value);
		void SetFloat(string name, float value);

		public void ListenEvent<T>(Action<T> callback)
		{
			OnEventTriggered += (e) => callback?.Invoke((T)e);
		}
	}
}