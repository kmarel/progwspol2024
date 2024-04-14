using System.Numerics;

namespace Logic
{
    public class Ball : IObservable<Vector2>
    {

        private Vector2 position;
        private Vector2 velocity;
        private int radius;
        private List<IObserver<Vector2>> observers = new List<IObserver<Vector2>>();

        public Ball(Vector2 position, Vector2 velocity, int radius)
        {
            this.position = position;
            this.velocity = velocity;
            this.radius = radius;
        }

        public Ball(Vector2 position, int radius)
        {
            this.position = position;
            velocity = new Vector2(0, 0);
            this.radius = radius;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public void setPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        public Vector2 getVelocity()
        {
            return velocity;
        }

        public void setVelocity(Vector2 newVelocity)
        {
            velocity = newVelocity;
        }

        public void move()
        {
            position = new Vector2(position.X + velocity.X, position.Y + velocity.Y);
            foreach(var observer in observers) 
            {
                observer.OnNext(position);
            }
        }

        public int getRadius() 
        {
            return radius;
        }

        public IDisposable Subscribe(IObserver<Vector2> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return null;
        }
    }
}
