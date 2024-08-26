using UnityEngine;

namespace Fusyon.GEL.Unity
{
	[RequireComponent(typeof(Animator))]
	public class GELAnimator : GELComponent<Animator>, IGELAnimatorComponent
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
				return Base.GetCurrentAnimatorStateInfo(i).IsName(name);
			}

			return false;
		}
	}
}