using UnityEngine;

namespace Fusyon.GEL.Unity
{
	public class UnityInput : IInput
	{
		public Vector2 MousePosition => Input.mousePosition;

		public bool IsButtonPressed(string name)
		{
			return Input.GetButton(name);
		}

		public bool WasButtonPressed(string name)
		{
			return Input.GetButtonDown(name);
		}

		public bool WasButtonReleased(string name)
		{
			return Input.GetButtonUp(name);
		}

		public float GetAxis(string positiveButton, string negativeButton = null)
		{
			if (negativeButton != null)
			{
				float positiveValue = IsButtonPressed(positiveButton) ? 1f : 0f;
				float negativeValue = IsButtonPressed(positiveButton) ? 1f : 0f;
				return positiveValue - negativeValue;
			}

			return Input.GetAxisRaw(positiveButton);
		}
	}
}