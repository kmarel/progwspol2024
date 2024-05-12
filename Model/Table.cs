using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Documents;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Model
{
    internal class Table : ModelAPI
    {
        Logic.LogicAPI logicAPI;

        private List<IBall> balls = new List<IBall>();

        public Table()
        {
            logicAPI = Logic.LogicAPI.createTableInstance();
        }  

        public override void createBalls(int amount)
        {

            logicAPI.addBallsToTable(amount);

            foreach(Logic.IBall ball in logicAPI.getBalls())
            {
                balls.Add(new Ball(ball));
            }

        }

        public override List<IBall> getBalls()
        {
            return balls;
        }

        public override int getBoardWidth()
        {
            return logicAPI.getWidth();
        }

        public override int getBoardHeight()
        {
            return logicAPI.getHeight();
        }
    }
}
