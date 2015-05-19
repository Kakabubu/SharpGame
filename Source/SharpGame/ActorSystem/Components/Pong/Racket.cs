using System;

namespace GameFramework
{
    class Racket : ActorComponent
    {
        public ConsoleKey Up;
        public ConsoleKey Down;
        private int speed;

        public override void Awake()
        {}
        public override void Start()
        {
            Parent.Name = this.GetType().Name;
        }

        public override void Update(float deltaTime)
        {
            if (Input.IsKeyDown(Up) && Parent.Position.y > 0)
                MoveUp(deltaTime*speed);
            if (Input.IsKeyDown(Down) && Parent.Position.y < Game.Resolution.y - 1)
                MoveDown(deltaTime*speed);
        }

        public override void OnDestroy()
        {
            if (Parent.Name == this.GetType().Name)
                Parent.Name = "empty";
        }
        public override void OnCollide(Actor Exciter)
        {
            Console.Beep();
        }
        public void MoveUp(float distance)
        {
            Parent.Position = (Parent.Position.Move(0,-distance,0));
        }
        public void MoveDown(float distance)
        {
            Parent.Position = Parent.Position.Move(0,distance,0);
        }
        public void SetControlsUp(ConsoleKey key)
        {
            Up = key;
        }
        public void SetControlsDown(ConsoleKey key)
        {
            Down = key;
        }

    }
}
