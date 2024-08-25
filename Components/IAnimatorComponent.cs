namespace Fusyon.GEL
{
	public interface IAnimatorComponent : IEntityComponent
	{
		void Play(string name, float speed = 1f);
		bool IsPlaying(string name);
	}
}