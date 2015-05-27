using System;

namespace SharpGame
{
    class Racket : ActorComponent
    {
        public ConsoleKey Up;
        public ConsoleKey Down;
        private int speed;
        float Acseleration;

        public override void Awake()
        {
            speed = 3;
            Acseleration = .6f;
        }
        public override void Start()
        {
            Actor.Name = this.GetType().Name;
        }

        public override void Update(float deltaTime)
        {
            if (Input.IsKeyDown(Up) && Actor.LocalPosition.y > 0)
            {
                MoveUp(deltaTime * speed*Acseleration);
                Acseleration ++;
            }
            else if (Input.IsKeyDown(Down) && Actor.LocalPosition.y < Game.Resolution.y - 1)
            {
                MoveDown(deltaTime * speed*Acseleration);
                Acseleration ++;
            }
            else Acseleration = .6f;
        }

        public override void OnDestroy()
        {
            if (Actor.Name == this.GetType().Name)
                Actor.Name = "empty";
        }
        public override void OnCollide(Actor Exciter)
        { }

        public void MoveUp(float distance)
        {
            Actor.LocalPosition = (Actor.LocalPosition.Move(0, -distance, 0));
        }
        public void MoveDown(float distance)
        {
            Actor.LocalPosition = Actor.LocalPosition.Move(0, distance, 0);
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
