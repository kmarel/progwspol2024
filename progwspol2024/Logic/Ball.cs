using System.Numerics;

namespace Logic
{
    public class Ball
    {

        private Vector2 position;
        private Vector2 velocity;

        public Ball(Vector2 position, Vector2 velocity)
        {
            this.position = position;
            this.velocity = velocity;
        }

        public Ball(Vector2 position)
        {
            this.position = position;
            velocity = new Vector2(0, 0);
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
        
    }
}
