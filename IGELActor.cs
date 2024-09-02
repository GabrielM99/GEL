namespace Fusyon.GEL
{
	public interface IGELActor
	{
		void OnCreate();
		void OnStart();
		void OnUpdate(float deltaTime);
		void OnFixedUpdate(float deltaTime);
		void OnDestroy();
	}
}