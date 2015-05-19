using System;
using System.Collections.Generic;

namespace GameFramework
{
    class ScoreCounter:ActorComponent
    {
        private int Score;
        private bool IsLeft = true;
        public override void Awake()
        {
            Score = 0;
        }
        public override void Start()
        {
            if (IsLeft)
            {
                Actor.Name = "ScoreCounterLeft";  
            }
            else
            {
                Actor.Name = "ScoreCounterRight";
            }
            Actor.WorldPosition = new Vector3(0, 0, 0);
        }
        public override void Update(float deltaTime)
        {
            char [] arr=Score.ToString().ToCharArray();///omg I'm idiot
            foreach (char ch in arr)
                if (IsLeft)
                {
                    Game.Graphics.PointChange(Game.Resolution.x / 2 + 1, 0,
                        new GraphicsPrimitive(ch, 1, ConsoleColor.Black));
                }
                else
                {
                    Game.Graphics.PointChange(Game.Resolution.x / 2 - 1, 0,
                        new GraphicsPrimitive(ch, 1, ConsoleColor.Black));
                }
        }
        public override void OnCollide(Actor Exciter)
        {
            if (Exciter.Name == "Ball")
                Score++;
        }
        public void ForRightPlayer(bool yes)
        {
            IsLeft = !yes;
        }
        public void ForRightPlayer()
        {
            IsLeft = false;
        }
        public char ScoreGet()
        {
            return (char)Score;
        }
    }
}
