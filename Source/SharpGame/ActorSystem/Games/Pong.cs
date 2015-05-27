using System;

namespace SharpGame
{
    public class Pong
    {
        public string Name { get; private set; }
        
        private Actor LeftPlayer, RightPlayer, ball, field, LeftCounter, RightCounter;
        private Scene scene;
        private Game game;

        public Pong ()
        {
            Name = "Pong";
            LeftPlayer = new Actor();
            Racket racket = new Racket();
            racket.SetControlsUp(ConsoleKey.W);
            racket.SetControlsDown(ConsoleKey.S);
            LeftPlayer.AddComponent(racket);
            LeftPlayer.AddComponent(new ASCIIPainter());
            LeftPlayer.AddComponent(new Collider());
            
            
            RightPlayer = new Actor();
            Racket racket2 = new Racket();
            racket2.SetControlsUp(ConsoleKey.UpArrow);
            racket2.SetControlsDown(ConsoleKey.DownArrow);         
            RightPlayer.AddComponent(racket2);
            RightPlayer.AddComponent(new ASCIIPainter());
            RightPlayer.AddComponent(new Collider());

            
            ball = new Actor();
            ball.AddComponent(new Ball());
            ball.AddComponent(new Collider());
            ball.AddComponent(new ASCIIPainter());
            
            
            field = new Actor();
            field.AddComponent(new GameInterface()); 
            field.AddComponent(new Collider());
            
            
            LeftCounter= new Actor();
            LeftCounter.AddComponent(new ScoreCounter());
            //LeftCounter.AddEntity(new ASCIIPainter());
            LeftCounter.AddComponent(new Collider());


            RightCounter = new Actor();
            ScoreCounter ScoreCounter = new ScoreCounter();
            ScoreCounter.ForRightPlayer();
            RightCounter.AddComponent(ScoreCounter);
            //RightCounter.AddEntity(new ASCIIPainter());
            RightCounter.AddComponent(new Collider());

            scene = new Scene();
            SetPositions();
            AddAllToScene();

            game = new Game();
            game.TargetFPS = 30;
            game.Initialize(Name);            
            
            game.Run(scene);
        }

        private void SetPositions()
        {
            LeftPlayer.WorldPosition = new Vector3(1.1F, 10, 0);
            RightPlayer.WorldPosition = new Vector3(38.9F, 10, 0);
        }

        private void AddAllToScene()
        {
            scene.AddActor(LeftPlayer);
            scene.AddActor(RightPlayer);
            scene.AddActor(ball);
            scene.AddActor(field);
            scene.AddActor(LeftCounter);
            scene.AddActor(RightCounter);
        }
    }
}
