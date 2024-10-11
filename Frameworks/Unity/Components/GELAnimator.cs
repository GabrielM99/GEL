using UnityEngine;

namespace Fusyon.GEL.Unity
{
	[RequireComponent(typeof(Animator))]
	public class GELAnimator : GELComponent<Animator>, IGELAnimator
	{
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
	}
}