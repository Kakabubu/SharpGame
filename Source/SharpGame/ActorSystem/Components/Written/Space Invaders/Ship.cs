using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGame
{
    class Ship : ActorComponent
    {
        ConsoleKey Left, Right, Shoot;
        int Speed;
        public override void Awake()
        {
            Shoot = ConsoleKey.Spacebar;
            Right = ConsoleKey.RightArrow;
            Left = ConsoleKey.LeftArrow;
            Speed = 10;
        }

        public override void Start()
        {
            Actor.Name = this.GetType().Name;
            Actor.LocalPosition = new Vector3(Game.Resolution.x / 2, Game.Resolution.y - 1, 0);
        }

        public override void Update(float D)
        {
            if (Input.IsKeyDown(Left) && Actor.LocalPosition.x > 0)
            { 
                Move(Vector3.Left * D * Speed); 
            }
            if (Input.IsKeyDown(Right) && Actor.LocalPosition.x < Game.Resolution.x)
            { 
                Move(Vector3.Right * D * Speed); 
            }
            if (Input.IsKeyDown(Shoot))
            { 
                Fire(); 
            }
        }

        public override void OnCollide(Actor Exciter)
        { }

        private void Move(Vector3 v)
        {
            Actor.LocalPosition += v;
        }

        private void Fire()
        {
            Actor bullet = new Actor();
            bullet.Name = "Projectile";
            bullet.AddComponent(new Projectile());
            bullet.AddComponent(new ASCIIPainter());
            bullet.LocalPosition = Actor.LocalPosition ;
            Actor.Scene.AddActor(bullet);
        }
    }
}
