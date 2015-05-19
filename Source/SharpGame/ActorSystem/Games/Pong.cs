using System;

namespace GameFramework
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
            LeftPlayer.AddEntity(racket);
            LeftPlayer.AddEntity(new ASCIIPainter());
            LeftPlayer.AddEntity(new Collider());
            
            
            RightPlayer = new Actor();
            Racket racket2 = new Racket();
            racket2.SetControlsUp(ConsoleKey.UpArrow);
            racket2.SetControlsDown(ConsoleKey.DownArrow);         
            RightPlayer.AddEntity(racket2);
            RightPlayer.AddEntity(new ASCIIPainter());
            RightPlayer.AddEntity(new Collider());

            
            ball = new Actor();
            ball.AddEntity(new Ball());
            ball.AddEntity(new Collider());
            ball.AddEntity(new ASCIIPainter());
            
            
            field = new Actor();
            field.AddEntity(new GameInterface()); 
            field.AddEntity(new Collider());
            
            
            LeftCounter= new Actor();
            LeftCounter.AddEntity(new ScoreCounter());
            //LeftCounter.AddEntity(new ASCIIPainter());
            LeftCounter.AddEntity(new Collider());


            RightCounter = new Actor();
            ScoreCounter ScoreCounter = new ScoreCounter();
            ScoreCounter.ForRightPlayer();
            RightCounter.AddEntity(ScoreCounter);
            //RightCounter.AddEntity(new ASCIIPainter());
            RightCounter.AddEntity(new Collider());

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
            LeftPlayer.Position = new Vector3(1.1F, 10, 0);
            RightPlayer.Position = new Vector3(38.9F, 10, 0);
        }

        private void AddAllToScene()
        {
            scene.AddEntity(LeftPlayer);
            scene.AddEntity(RightPlayer);
            scene.AddEntity(ball);
            scene.AddEntity(field);
            scene.AddEntity(LeftCounter);
            scene.AddEntity(RightCounter);
        }
    }
}
