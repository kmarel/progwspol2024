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

        private bool isPhysicsEnabled = false;

        public Table(DataAPI dataAPI) 
        {
            _data = dataAPI;
            this.width = _data.width;
            this.height = _data.height;

            handlePhysics();
        }

        public Table()
        {
            this.width = _data.width;
            this.height = _data.height;

            handlePhysics();
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
        
        public override bool isOnOtherBall(Vector2 position)
        {
            foreach (Ball ball in balls)
            {
                if (Vector2.Distance(ball.getPosition(), position) <= 2 * ball.getRadius())
                {
                    return true;
                }
            }
            return false;
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
                    
                } while (!isWithinTable(position) && !isOnOtherBall(position));
                Ball ball = new Ball(DataAPI.createBall(position, 10, 10));
                randomizeSpeed(ball);
                randomizeMass(ball);
                balls.Add(ball);
            }
            isPhysicsEnabled = true;
        }

        internal override void randomizeSpeed(Ball ball)
        {
            Random random = new Random();
            ball.setVelocity(new Vector2((float)random.NextDouble() * 6 - 3, (float)random.NextDouble() * 6 - 3));
        }

        internal override void randomizeMass(Ball ball)
        {
            Random random = new Random();
            ball.setWeight((int)random.NextDouble() * 10 - 5);
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
            Task.Run(() => handleCollisionsWithBalls());
            Task.Run(() => handleCollisionsWithWalls());
        }

        private void handleCollisionsWithWalls()
        {
            while(true)
            {
                if (!isPhysicsEnabled)
                {
                    continue;
                }
                foreach (Ball ball in balls)
                {
                    if (ball.getPosition().X > width - ball.getRadius() - 20)
                    {
                        lock(ball.getLock())
                        {
                            ball.setPosition(new Vector2(width - ball.getRadius() - 20, ball.getPosition().Y));
                            ball.setVelocity(new Vector2(ball.getVelocity().X * (-1), ball.getVelocity().Y));
                        }
                    }

                    if (ball.getPosition().X < 0 + ball.getRadius())
                    {
                        lock (ball.getLock())
                        {
                            ball.setPosition(new Vector2(0 + ball.getRadius(), ball.getPosition().Y));
                            ball.setVelocity(new Vector2(ball.getVelocity().X * (-1), ball.getVelocity().Y));
                        }
                    }

                    if (ball.getPosition().Y > height - ball.getRadius() - 20)
                    {
                        lock(ball.getLock())
                        {
                            ball.setPosition(new Vector2(ball.getPosition().X, height - ball.getRadius() - 20));
                            ball.setVelocity(new Vector2(ball.getVelocity().X, ball.getVelocity().Y * (-1)));
                        }
                    }

                    if (ball.getPosition().Y < 0 + ball.getRadius())
                    {
                        lock(ball.getLock())
                        {
                            ball.setPosition(new Vector2(ball.getPosition().X, 0 + ball.getRadius()));
                            ball.setVelocity(new Vector2(ball.getVelocity().X, ball.getVelocity().Y * (-1)));
                        }
                    }
                }
            }
        }

        private Tuple<Ball, Ball>? whichBallsCollide()
        {
            foreach (Ball ball1 in balls)
            {
                foreach (Ball ball2 in balls)
                {
                    if (ball1 == ball2) { continue; }
                    var distance = Vector2.Distance(ball1.getPosition(), ball2.getPosition());
                    if (distance <= ball1.getRadius() * 2)
                    {
                        return new Tuple<Ball, Ball>(ball1, ball2);
                    }
                }
            }
            return null;
        }

        private void handleCollisionsWithBalls()
        {
            while(true)
            {
                if (!isPhysicsEnabled)
                {
                    continue;
                }
                Tuple<Ball, Ball>? collision = whichBallsCollide();
                if (collision != null)
                {
                    Ball ball1 = collision.Item1;
                    Ball ball2 = collision.Item2;

                    Vector2 relativeVelocity = ball1.getVelocity() - ball2.getVelocity();
                    Vector2 unitNormal = Vector2.Normalize(ball1.getPosition() - ball2.getPosition());
                    float dotProduct = Vector2.Dot(relativeVelocity, unitNormal);
                    Vector2 impulse = dotProduct * unitNormal * (2 * ball1.getWeight() * ball2.getWeight() / (ball1.getWeight() + ball2.getWeight()));

                    Vector2 previousVelocityBall1 = new Vector2(ball1.getVelocity().X, ball1.getVelocity().Y);
                    Vector2 previousVelocityBall2 = new Vector2(ball2.getVelocity().X, ball2.getVelocity().Y);

                    lock(ball1.getLock())
                    {
                        lock(ball2.getLock()) 
                        {
                            ball1.setVelocity(ball1.getVelocity() - impulse / ball1.getWeight());
                            ball2.setVelocity(ball2.getVelocity() + impulse / ball2.getWeight());

                            ball1.setPosition(-previousVelocityBall1 + ball1.getPosition());
                            ball2.setPosition(-previousVelocityBall2 + ball2.getPosition());
                        }
                    }
                }
            }
        }

    }
}
