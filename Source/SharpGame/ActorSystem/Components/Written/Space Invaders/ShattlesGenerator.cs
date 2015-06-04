namespace SharpGame
{
    class ShattlesGenerator:ActorComponent
    {
        Resolution Res;
        private int rows;
        private bool GoingLeft;
        private float Speed;
        

        public override void Awake()
        {
            GoingLeft = false;
            Speed = 1.5f;
        }

        public override void Start()
        {
            Actor.LocalPosition = new Vector3((Res.x/2),0,0);
            Res = Game.Resolution;
            rows = 5;
            for (int y = 1; y <= rows; y++)
                for (int x = 10; x <= Res.x - 4; x += 2)
                {
                    Actor act = new Actor("Shattle1");
                    act.LocalPosition = new Vector3(x - Actor.LocalPosition.x,y*2,0);
                    act.AddComponent(new ShattleOnCollide());
                    act.AddComponent(new ASCIIPainter());
                    act.AddComponent(new Collider());
                    Actor.AddChild(act);
                }
        }

        public override void Update(float D)
        {
            Move(GoingLeft, Speed*D);
        }

        public override void OnCollide(Actor Exciter)
        {
            GoingLeft=!GoingLeft;
            Actor.LocalPosition += Vector3.Down/5;
        }

        private void Move(bool GoingLeft, float Dist)
        {
            if (GoingLeft)
                Actor.LocalPosition += Vector3.Left * Dist;
            else Actor.LocalPosition += Vector3.Right * Dist;
        }
    }
}
