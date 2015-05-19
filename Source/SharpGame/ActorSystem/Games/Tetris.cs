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
            figure.AddEntity(new Piece());
            figure.AddEntity(new ASCIIPainter());
            figure.AddEntity(new Collider());

            stack = new Actor();
            stack.AddEntity(new Stack());
            figure.AddEntity(new ASCIIPainter());
            figure.AddEntity(new Collider());

            StackSize = new Resolution();
            field = new Actor();
            figure.AddEntity(new GameInterface());
            figure.AddEntity(new ASCIIPainter());
            figure.AddEntity(new Collider());

            score = new Actor();
            /**/

            nextFigure = new Actor();
            /**/

            scene = new Scene();
            game = new Game();
        }

    }
}
