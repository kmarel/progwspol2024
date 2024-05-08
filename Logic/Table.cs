using Data;
using System.Diagnostics;
using System.Numerics;

namespace Logic
{
    internal class Table : LogicAPI
    {
        private int width;
        private int height;
        private List<IBall> balls = new List<IBall>();
        private DataAPI _data = DataAPI.createInstance();

        private bool isPhysicsEnabled = false;

        public Table() 
        {
            this.width = _data.width;
            this.height = _data.height;

            Task.Run(() => handlePhysics());
        }

        public override int getWidth()
        { 
            return width; 
        }

        public override int getHeight() 
        { 
            return height;
        }

        public override void setWidth(int _width)
        {
            this.width = _width;
        }

        public override void setHeight(int _height)
        {
            this.height = _height;
        }

        public override List<IBall> getBalls() 
        {
            return balls;
        }

        public override void addBallsToTable(int numberOfBalls)
        {
            Random random = new Random();
            int rad = 100;
            Vector2 position;
            for (int i = 0; i < numberOfBalls; i++)
            {
                do
                {
                    position = new Vector2(random.Next(rad, this.width - rad), random.Next(rad, this.height - rad));
                } while (!isWithinTable(position));
                Ball ball = new Ball(DataAPI.createBall(position, 10, 10));
                randomizeSpeed(ball);
                balls.Add(ball);
            }
            isPhysicsEnabled = true;
        }

        internal override void randomizeSpeed(Ball ball)
        {
            Random random = new Random();
            ball.setVelocity(new Vector2((float)random.NextDouble() * 6 - 3, (float)random.NextDouble() * 6 - 3));
        }

        public override bool isWithinTable(Vector2 position)
        {
            if(position.X >= 0 && position.X <= width && position.Y >= 0 && position.Y <= height)
                return true;
            else
                return false;
        }

        private void handlePhysics()
        {
            while(true)
            {
                if(!isPhysicsEnabled)
                {
                    continue;
                }
                foreach (Ball ball in balls)
                {

                    if (ball.getPosition().X > width - ball.getRadius())
                    {
                        ball.setVelocity(new Vector2(ball.getVelocity().X * (-1), ball.getVelocity().Y));
                        ball.setPosition(new Vector2(width - ball.getRadius(), ball.getPosition().Y));
                    }

                    if (ball.getPosition().X < 0 + ball.getRadius())
                    {
                        ball.setVelocity(new Vector2(ball.getVelocity().X * (-1), ball.getVelocity().Y));
                        ball.setPosition(new Vector2(0 + ball.getRadius(), ball.getPosition().Y));
                    }

                    if (ball.getPosition().Y > height - ball.getRadius())
                    {
                        ball.setVelocity(new Vector2(ball.getVelocity().X, ball.getVelocity().Y * (-1)));
                        ball.setPosition(new Vector2(ball.getPosition().X, height - ball.getRadius()));
                    }

                    if (ball.getPosition().Y < 0 + ball.getRadius())
                    {
                        ball.setVelocity(new Vector2(ball.getVelocity().X, ball.getVelocity().Y * (-1)));
                        ball.setPosition(new Vector2(ball.getPosition().X, 0 + ball.getRadius()));
                    }
                }
            }
        }

    }
}
