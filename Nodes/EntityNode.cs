namespace Fusyon.GEL
{
	public class EntityNode : EntityNode<IEntity> { }

	public class EntityNode<T> : Node where T : IEntity
	{
		private T m_Entity;

		public T Entity
		{
			get => m_Entity;

			set
			{
				if (value != null)
				{
					if (value.Equals(m_Entity))
					{
						return;
					}

					value.Node = this;
				}

				if (m_Entity != null)
				{
					Game.EntityManager.Destroy(m_Entity);
				}

				m_Entity = value;
			}
		}

		protected internal override void OnEnable()
		{
			base.OnEnable();

			if (Entity != null)
			{
				Entity.Visible = true;
			}
		}

		protected internal override void OnDisable()
		{
			base.OnDisable();

			if (Entity != null)
			{
				Entity.Visible = false;
			}
		}

		protected internal override void OnUpdate(float deltaTime)
		{
			base.OnUpdate(deltaTime);
			Entity?.OnUpdate(deltaTime);
		}

		protected internal override void OnFixedUpdate(float deltaTime)
		{
			base.OnFixedUpdate(deltaTime);
			Entity?.OnFixedUpdate(deltaTime);
		}

		protected internal override void OnDestroy()
		{
			base.OnDestroy();
			Game.EntityManager.Destroy(Entity);
		}
	}
}