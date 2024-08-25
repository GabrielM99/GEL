using UnityEngine;

namespace Fusyon.GEL
{
	public interface IInput
	{
		Vector2 MousePosition { get; }

		bool IsButtonPressed(string name);
		bool WasButtonPressed(string name);
		bool WasButtonReleased(string name);
		float GetAxis(string positiveButton, string negativeButton = null);
	}
}