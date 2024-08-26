using System.Numerics;

namespace Fusyon.GEL
{
	public interface IGELInput
	{
		Vector3 MousePosition { get; }

		bool IsButtonPressed(string name);
		bool WasButtonPressed(string name);
		bool WasButtonReleased(string name);
		float GetAxis(string positiveButton, string negativeButton = null);
	}
}