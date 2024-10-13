using System;
using UnityEngine;

namespace Fusyon.GEL.Unity
{
	[RequireComponent(typeof(Animator))]
	public class GELAnimator : GELBehaviour<Animator>, IGELAnimationPlayer
	{
		public Action<object> OnEventTriggered { get; set; }

		public void Play(string name, float speed = 1f)
		{
			Base.Play(name);
			Base.speed = speed;
		}

		public bool IsPlaying(string name)
		{
			for (int i = 0; i < Base.layerCount; i++)
			{
				if (Base.GetCurrentAnimatorStateInfo(i).IsName(name))
				{
					return true;
				}
			}

			return false;
		}

		public void SetBool(string name, bool value)
		{
			Base.SetBool(name, value);
		}

		public void SetInt(string name, int value)
		{
			Base.SetInteger(name, value);
		}

		public void SetFloat(string name, float value)
		{
			Base.SetFloat(name, value);
		}
	}
}