using System;
using System.Linq;
using UnityEngine;

namespace Fusyon.GEL.Unity
{
	[DisallowMultipleComponent]
	public class GELObject : MonoBehaviour, IGELObject
	{
		[SerializeField] private GELComponent[] m_Components;

		public GELEntity Entity { get; set; }
		public System.Numerics.Vector3 Position { get => transform.position.ToGEL(); set => transform.position = value.ToUnity(); }
		public System.Numerics.Vector3 Rotation { get => transform.eulerAngles.ToGEL(); set => transform.eulerAngles = value.ToUnity(); }
		public System.Numerics.Vector3 Scale { get => transform.localScale.ToGEL(); set => transform.localEulerAngles = value.ToUnity(); }
		public bool Visible { get => gameObject.activeSelf; set => gameObject.SetActive(value); }

		public GELComponent[] Components { get => m_Components; private set => m_Components = value; }

		private void Reset()
		{
			Components = GetComponents<GELComponent>();
		}

		private void Awake()
		{
			GELComponent[] components = GetComponents<GELComponent>();

			if (Components.Length != components.Length)
			{
				Components = components;
			}
		}
	}
}