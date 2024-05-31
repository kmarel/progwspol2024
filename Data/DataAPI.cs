using System.Numerics;

namespace Data
{
    public interface IBall : IObservable<Vector2>
    {
        Vector2 getPosition();
        Vector2 getVelocity();
        void setVelocity(Vector2 newVelocity);
        float getElapsedTimeInSeconds();
    }
    public abstract class DataAPI
    {
        public abstract int radius { get; }
        public abstract int height { get; }
        public abstract int width { get; }
        public abstract int weight { get; }

        public static DataAPI createInstance()
        {
            return new Data();
        }

        public static IBall createBall(Vector2 position, Vector2 velocity, int radius, int weight)
        {
            return new Ball(position, velocity, radius, weight);
        }

        public static IBall createBall(Vector2 position, int radius, int weight)
        {
            return new Ball(position, radius, weight);
        }
    }
}
