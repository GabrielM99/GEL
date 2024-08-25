namespace Fusyon.GEL
{
	public interface IResources
	{
		T Load<T>(string path);
	}
}