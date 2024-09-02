namespace Fusyon.GEL
{
	public interface IGELBehaviour : IGELObject
	{
		IGELEntity Entity { get; set; }
		void Bind(IGELEntity entity);
	}
}