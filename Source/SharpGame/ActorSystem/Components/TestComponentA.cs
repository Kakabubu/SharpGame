using System;

namespace SharpGame
{
    public class TestComponentA : ActorComponent
    {
        public override void Awake()
        {
            Console.WriteLine("TestComponentA Awake()");
        }

        public override void Start()
        {
            Console.WriteLine("TestComponentA Start()");
        }

        public override void Update(float deltaTime)
        {
            Console.WriteLine("TestComponentA Update({0})", deltaTime);

            if (Input.IsKeyDown(ConsoleKey.Escape))
            {
                Console.WriteLine("Escape was pressed, exiting...");
                Game.EnqueueExit();
            }
        }

        public override void OnDestroy()
        {
            Console.WriteLine("TestComponentA OnDestroy()");
        }
    }
}
