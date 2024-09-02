namespace Fusyon.GEL
{
	public interface IGELComponent : IGELActor
	{
		GELEntity Entity { get; set; }
	}
}