using System.Numerics;


namespace Logic
{
    internal class Ball : IBall
    {

        private List<IObserver<Vector2>> observers = new List<IObserver<Vector2>>();
        private Data.IBall ballData;

        public Ball(Data.IBall ballData)
        {
            this.ballData = ballData;
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

        public void setPosition(Vector2 newPosition)
        {
            ballData.setPosition(newPosition);
        }

        public Vector2 getVelocity()
        {
            return ballData.getVelocity();
        }

        public int getRadius()
        {
            return ballData.getRadius();
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

        public void OnNext(Vector2 value)
        {
            foreach (var observer in observers)
            {
                observer.OnNext(value);
            }
        }

    }
}
