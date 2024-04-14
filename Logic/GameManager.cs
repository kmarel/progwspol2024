using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal class GameManager : IGameManager
    {
        private bool isRunning;
        private LogicAPI table;

        public GameManager(LogicAPI table) 
        {
            this.table = table;
        }

        public void moveBalls()
        {
            isRunning = true;
            while(isRunning)
            {
                foreach (Ball ball in table.getBalls())
                {
                    if(ball.getPosition().X > 755 || ball.getPosition().X < 0)
                    {
                        ball.setVelocity(new Vector2(ball.getVelocity().X * (-1), ball.getVelocity().Y));
                    }
                    if(ball.getPosition().Y > 290 || ball.getPosition().Y < 0)
                    {
                        ball.setVelocity(new Vector2(ball.getVelocity().X, ball.getVelocity().Y * (-1)));
                    }
                    ball.move();
                    
                }
                Thread.Sleep(16);
            }
        }
    }
}
