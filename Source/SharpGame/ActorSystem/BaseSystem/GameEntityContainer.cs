using System.Collections.Generic;
using System.Diagnostics;

namespace SharpGame.Internal
{
    public abstract class GameEntityContainer<TGameEntity> : IGameEntity 
        where TGameEntity : IGameEntity
    {
        protected List<TGameEntity> children;

        protected bool GameStarted { get; private set; }

        public GameEntityContainer()
        {
            children = new List<TGameEntity>();
            GameStarted = false;
        }

        public virtual void AddChild(TGameEntity entity)
        {
            children.Add(entity);
            entity.Awake();

            if (GameStarted)
            {
                entity.Start();
            }
        }

        public virtual void Awake() { }

        public virtual void Start()
        {
            for (int i = 0; i < children.Count; i++)
            {
                children[i].Start();
            }

            GameStarted = true;
        }

        public virtual void Update(float deltaTime)
        {
            for (int i = 0; i < children.Count; i++)
            {
                children[i].Update(deltaTime);
            }
        }

		public virtual void Draw(float deltaTime)
		{
			for (int i = 0; i < children.Count; i++)
			{
				children[i].Draw(deltaTime);
			}
		}

        public virtual void OnDestroy()
        {
            for (int i = 0; i < children.Count; i++)
            {
                children[i].OnDestroy();
            }

            children.Clear();
        }
        public virtual void OnCollide(Actor Exciter)
        {
            for (int i = 0; i < children.Count; i++)
            {
                children[i].OnCollide(Exciter);
            }
        }
    }
}
