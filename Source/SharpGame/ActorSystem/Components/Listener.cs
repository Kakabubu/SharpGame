using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFramework
{
    class Listener:ActorComponent
    {
        public override void Awake()
        { }
        public override void Start()
        {
            Console.WriteLine("Press '1' to play the Pong");
            Console.WriteLine("Press '2' not to play the tetris");
            Console.WriteLine("Press 'Esc' to exit the game");
        }
        public override void Update(float deltaTime)
        {
            if (Input.IsKeyDown(ConsoleKey.D1))
            {
                Parent.Parent.OnDestroy();
                Pong pong = new Pong(); 
            }
            if (Input.IsKeyDown(ConsoleKey.D2))
            {
                Parent.Parent.OnDestroy();
                Tetris tetris = new Tetris(); 
            }
            if (Input.IsKeyDown(ConsoleKey.Escape))
            {
                Parent.Parent.OnDestroy();
            }
        }   
    }
}
