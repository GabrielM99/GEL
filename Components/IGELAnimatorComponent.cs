namespace Fusyon.GEL
{
	public interface IGELAnimatorComponent : IGELEntityComponent
	{
		void Play(string name, float speed = 1f);
		bool IsPlaying(string name);
	}
}