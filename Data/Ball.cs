using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Data
{
    internal class Ball : IBall
    {
        private Vector2 position;
        private Vector2 velocity;
        private int radius;
        private int weight;
        private List<IObserver<Vector2>> observers = new List<IObserver<Vector2>>();
        private Thread thread;
        private Stopwatch stopwatch = new Stopwatch();
        private long previousTimeInMilliseconds = 0;
        private float elapsedTimeInSeconds = 0;
        private object velocityLock = new object();
        private Logger logger = Logger.getInstance();
        private string guid = Guid.NewGuid().ToString();

        public float PositionX { get { return position.X; } }
        public float PositionY { get { return position.Y; } }
        public float VelocityX { get { return velocity.X; } }
        public float VelocityY { get { return velocity.Y; } }
        public string GUID { get { return guid; } }


        public Ball(Vector2 position, Vector2 velocity, int radius, int weight)
        {
            this.position = position;
            this.velocity = velocity;
            this.radius = radius;
            this.weight = weight;

            this.thread = new Thread(move);
            thread.IsBackground = true;
            thread.Start();
        }

        public Ball(Vector2 position, int radius, int weight)
        {
            this.position = position;
            velocity = new Vector2(0, 0);
            this.radius = radius;
            this.weight = weight;

            this.thread = new Thread(move);
            thread.IsBackground = true;
            thread.Start();
        }

        public float getElapsedTimeInSeconds()
        {
            return elapsedTimeInSeconds;
        }

        public Vector2 getPosition()
        {
            lock(this)
            {
                return position;
            }
        }
        
        private void setPosition(Vector2 newPosition)
        {
            lock(this)
            {
                position = newPosition;
            }
        }

        public Vector2 getVelocity()
        {
            lock(velocityLock)
            {
                return velocity;
            }
        }

        public void setVelocity(Vector2 newVelocity)
        {
            lock(velocityLock)
            {
                velocity = newVelocity;
            }
        }
        
        private void move()
        {
            stopwatch.Start();

            while (true)
            {
                long timeSampleInMilliseconds = stopwatch.ElapsedMilliseconds;
                elapsedTimeInSeconds = (float)(timeSampleInMilliseconds - previousTimeInMilliseconds) / 1000;
                previousTimeInMilliseconds = timeSampleInMilliseconds;

                Vector2 newPosition = new Vector2(position.X + velocity.X * elapsedTimeInSeconds, 
                                                  position.Y + velocity.Y * elapsedTimeInSeconds);

                setPosition(newPosition);

                logger.log(this);

                foreach (IObserver<Vector2> observer in observers)
                {
                    observer.OnNext(position);
                }
                Thread.Sleep(16);
            }
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

    }
}
