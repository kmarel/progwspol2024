using Data;
using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace Logic
{
    internal class Table : LogicAPI
    {
        private int width;
        private int height;
        private List<IBall> balls = new List<IBall>();
        private DataAPI _data = DataAPI.createInstance();

        public Table(DataAPI dataAPI) 
        {
            _data = dataAPI;
            this.width = _data.width;
            this.height = _data.height;
        }

        public Table()
        {
            this.width = _data.width;
            this.height = _data.height;
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
        
        private bool isOnOtherBall(Vector2 position)
        {
            foreach (Ball ball in balls)
            {
                if (Vector2.Distance(ball.getPosition() + ball.getVelocity(), position) <= 2 * _data.radius + 5)
                {
                    return true;
                }
            }
            return false;
        }

        public override void addBallsToTable(int numberOfBalls)
        {
            Random random = new Random();
            int rad = 50;
            Vector2 position;
            for (int i = 0; i < numberOfBalls; i++)
            {
                do
                {
                    position = new Vector2(random.Next(rad, this.width - rad), random.Next(rad, this.height - rad));
                } while (!isWithinTable(position) || isOnOtherBall(position));
                Ball ball = new Ball(DataAPI.createBall(position, 10, 10), balls);
                randomizeSpeed(ball);
                balls.Add(ball);
            }
        }

        private void randomizeSpeed(Ball ball)
        {
            Random random = new Random();
            ball.setVelocity(new Vector2((float)random.NextDouble() * 100, (float)random.NextDouble() * 100));
        }

        private bool isWithinTable(Vector2 position)
        {
            if(position.X >= 0 && position.X <= width && position.Y >= 0 && position.Y <= height)
                return true;
            else
                return false;
        }

    }
}
