using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGame
{
    class Stack : ActorComponent
    {
        private ConsoleColor[,] buffer;
        private Resolution Size;
        public override void Awake()
        {
            Size = new Resolution(10, 25);
            buffer = new ConsoleColor[Size.y,Size.x];
        }
        public override void Start()
        {
            Actor.Name = this.GetType().Name;
        }
        public override void Update(float deltaTime)
        { }
        public override void OnCollide(Actor Exciter)
        {
            switch (Exciter.Name)
            {
                case "Piece":
                    StackPlace(Exciter);
                    break;
                default:
                    break;
            }
        }

        private void StackPlace(Actor Piece)
        {
            ;
        }
    }
}
