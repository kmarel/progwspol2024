using Data;
using System.Diagnostics;
using System.Numerics;

namespace Logic
{
    internal class Table : LogicAPI
    {
        private int width;
        private int height;
        private List<Ball> balls = new List<Ball>();
        private DataAPI _data = DataAPI.createInstance();

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

        public override List<Ball> getBalls() 
        {
            return balls;
        }

        public override void addBallsToTable(int numberOfBalls)
        {
            Random random = new Random();
            int rad = 30;
            Vector2 position;
            for (int i = 0; i < numberOfBalls; i++)
            {
                do
                {
                    position = new Vector2(random.Next(rad, this.width - rad), random.Next(rad, this.height - rad));
                } while (!isWithinTable(position));
                Ball ball = new Ball(position, _data.radius);
                randomizeSpeed(ball);
                balls.Add(ball);
            }
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
    }
}
