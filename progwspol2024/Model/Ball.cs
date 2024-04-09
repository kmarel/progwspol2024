using System.Windows.Shapes;

namespace Model
{
    public class Ball
    {

        private Ellipse visualization;
        private Logic.Ball actualBall;

        public Ball(Ellipse visualization, Logic.Ball actualBall)
        {
            this.visualization = visualization;
            this.actualBall = actualBall;
        }

    }

}
