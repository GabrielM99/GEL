using UnityEngine;

namespace Fusyon.GEL.Unity
{
	public class GELUnityLogger : IGELLogger
	{
		public void Info(object message)
		{
			Debug.Log(message);
		}

		public void Warn(object message)
		{
			Debug.LogWarning(message);
		}

		public void Error(object message)
		{
			Debug.LogError(message);
		}
	}
}