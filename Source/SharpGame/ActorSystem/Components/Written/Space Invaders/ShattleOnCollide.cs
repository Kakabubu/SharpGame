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
            if (!Actor.InBounds())
                Actor.Parent.OnCollide(Actor);
        }
    }
}
