using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFramework
{
    class Tetris
    {
        private Actor figure, stack, field, score, nextFigure;
        private Scene scene;
        private Game game;
        private Resolution StackSize;

        public Tetris()
        {
            figure = new Actor();
            figure.AddComponent(new Piece());
            figure.AddComponent(new ASCIIPainter());
            figure.AddComponent(new Collider());

            stack = new Actor();
            stack.AddComponent(new Stack());
            figure.AddComponent(new ASCIIPainter());
            figure.AddComponent(new Collider());

            StackSize = new Resolution();
            field = new Actor();
            figure.AddComponent(new GameInterface());
            figure.AddComponent(new ASCIIPainter());
            figure.AddComponent(new Collider());

            score = new Actor();
            /**/

            nextFigure = new Actor();
            /**/

            scene = new Scene();
            game = new Game();
        }

    }
}
