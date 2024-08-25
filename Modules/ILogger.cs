namespace Fusyon.GEL
{
	public interface ILogger
	{
		void Info(object message);
		void Warn(object message);
		void Error(object message);
	}
}