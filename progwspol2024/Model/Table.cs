using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Documents;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Model
{
    internal class Table : ModelAPI
    {
        private Logic.GameManager gameManager;
        Logic.LogicAPI logicAPI;

        private List<Ball> balls = new List<Ball>();

        public Table()
        {
            this.logicAPI = Logic.LogicAPI.createTableInstance();
            gameManager = new Logic.GameManager(logicAPI);
        }  

        public override void startGame()
        {
            Task.Run(gameManager.moveBalls);
        }

        public override void createBalls(int amount)
        {
            logicAPI.addBallsToTable(amount);

            foreach(Logic.Ball ball in logicAPI.getBalls())
            {
                balls.Add(new Ball(ball));
            }

        }

        public override List<Ball> getBalls()
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
