namespace Fusyon.GEL
{
	public interface IGELResources
	{
		T Load<T>(string path) where T : IGELResource;
	}
}