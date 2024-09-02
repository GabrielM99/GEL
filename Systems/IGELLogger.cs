namespace Fusyon.GEL
{
	public interface IGELLogger
	{
		void Info(object message);
		void Warn(object message);
		void Error(object message);
	}
}