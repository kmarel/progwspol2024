using System.Numerics;

namespace Logic
{
    public class Ball
    {

        private Vector2 position;
        private Vector2 velocity;
        private int radius;

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
        }

        public int getRadius() 
        {
            return radius;
        }

    }
}
