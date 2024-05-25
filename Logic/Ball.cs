using Data;
using System.Numerics;


namespace Logic
{
    internal class Ball : IBall
    {

        private List<IObserver<Vector2>> observers = new List<IObserver<Vector2>>();
        private Data.IBall ballData;
        private DataAPI _data = DataAPI.createInstance();
        private List<IBall> otherBalls;

        public Ball(Data.IBall ballData, List<IBall> otherBalls)
        {
            this.ballData = ballData;
            this.otherBalls = otherBalls;
            ballData.Subscribe(this);
        }

        public Vector2 getPosition()
        {
            return ballData.getPosition();
        }

        public void setVelocity(Vector2 newVelocity)
        {
            ballData.setVelocity(newVelocity);
        }

        public Vector2 getVelocity()
        {
            return ballData.getVelocity();
        }

        public int getWeight()
        {
            return ballData.getWeight();
        }

        public void setWeight(int newWeight)
        {
            ballData.setWeight(newWeight);
        }

        public IDisposable Subscribe(IObserver<Vector2> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber(observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<Vector2>> _observers;
            private IObserver<Vector2> _observer;

            public Unsubscriber(List<IObserver<Vector2>> observers, IObserver<Vector2> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (!(_observer == null)) _observers.Remove(_observer);
            }
        }

        public void OnCompleted()
        {

        }

        public void OnError(Exception error)
        {

        }

        private void handleCollisionsWithWalls(Vector2 value)
        {
            if (value.X > _data.width - _data.radius - 20)
            {
                setVelocity(new Vector2(getVelocity().X * (-1), getVelocity().Y));
            }

            if (value.X < 0 + _data.radius)
            {
                setVelocity(new Vector2(getVelocity().X * (-1), getVelocity().Y));
            }

            if (value.Y > _data.height - _data.radius - 20)
            {
                setVelocity(new Vector2(getVelocity().X, getVelocity().Y * (-1)));
            }

            if (value.Y < 0 + _data.radius)
            {
                setVelocity(new Vector2(getVelocity().X, getVelocity().Y * (-1)));
            }
        }

        private void handleCollisionsWithBalls()
        {
            lock(otherBalls)
            {

            foreach (Ball ball in otherBalls)
            {
                if (ball == this) { continue; }
                Ball collision = ball;
                //lock(collision)
                //{
                    
                
                var distance = Vector2.Distance(getPosition(), ball.getPosition());
                if (distance <= _data.radius * 2)
                {
                    //lock (collision)
                    //{
                        //lock (this) // PAMIETAC ZE TO THIS TO NIE TO SAMO CO THIS W DATA!!!!!
                        //{

                            Ball ball2 = collision;
                            
                            Vector2 relativeVelocity = getVelocity() - ball2.getVelocity();
                            Vector2 unitNormal = Vector2.Normalize(getPosition() - ball2.getPosition());
                            float dotProduct = Vector2.Dot(relativeVelocity, unitNormal);
                            Vector2 impulse = dotProduct * unitNormal * (2 * getWeight() * ball2.getWeight() / (getWeight() + ball2.getWeight()));

                            //Vector2 previousVelocityBall1 = new Vector2(getVelocity().X, getVelocity().Y);
                            //Vector2 previousVelocityBall2 = new Vector2(ball2.getVelocity().X, ball2.getVelocity().Y);

                            setVelocity(getVelocity() - impulse / getWeight());
                            ball2.setVelocity(ball2.getVelocity() + impulse / ball2.getWeight());
                        //}
                    //}
                }

                //}
            }

            }
        }

        public void OnNext(Vector2 value)
        {
            foreach (var observer in observers)
            {

                observer.OnNext(value);

                handleCollisionsWithWalls(value);
                handleCollisionsWithBalls();

                
            }
        }

        public int getRadius()
        {
            return _data.radius;
        }

    }
}
