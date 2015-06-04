namespace SharpGame
{
    class ShattleOnCollide : ActorComponent
    {
        public override void Start()
        {
            if (Actor.Parent == null)
            { OnDestroy(); }
        
        }
        public override void Update(float deltaTime)
        {
        }

        public override void OnCollide(Actor Exciter)
        {
            switch (Exciter.Name)
            {
                case "SS":
                    Actor.Parent.LocalPosition += Vector3.Down;
                    Actor.Parent.OnCollide(Exciter);
                    break;
                case "Projectile":
                    Actor.Scene.ForDestroy(Actor);
                    break;
                default: break;
            }
        }
    }
}
