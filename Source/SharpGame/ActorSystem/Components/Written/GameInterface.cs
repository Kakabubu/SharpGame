using System;

namespace SharpGame
{
    public class GameInterface:ActorComponent
    {
        /*must draw score, and so on*/
        public override void Awake()
        { }
        public override void Start()
        {
            Actor.Name = this.GetType().Name;
            Console.BackgroundColor = ConsoleColor.White;
        }

        public override void Update(float deltaTime)
        {
            if (Input.IsKeyDown(ConsoleKey.Escape))
            {
                Console.WriteLine("Escape was pressed, exiting...");
                Game.EnqueueExit();
            }
        }

        public override void OnDestroy()
        { }

        public override void OnCollide(Actor Actor)
        { }

        /*
         * here must be emmm... in fact the main point is to set the name of actor, 
         * so other components would be able to initialize it and return the right information
         * maby it wolud be better to connect score painter, field, etc. here.
         * but it calls neccessary to change the principles of colliding with borders, and counting score */
    }
}
