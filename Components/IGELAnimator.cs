namespace Fusyon.GEL
{
	public interface IGELAnimator : IGELComponent
	{
		void Play(string name, float speed = 1f);
		bool IsPlaying(string name);
	}
}