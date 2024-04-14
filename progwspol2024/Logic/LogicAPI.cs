using Data;
using System.ComponentModel;
using System.Numerics;

namespace Logic
{

    public interface IBall : IObservable<Vector2>
    {

        Vector2 getPosition();
        void setPosition(Vector2 newPosition);
        Vector2 getVelocity();
        void setVelocity(Vector2 newVelocity);
        void move();
        int getRadius();

    }

    public interface IGameManager
    {
        void moveBalls();

    }

    public abstract class LogicAPI
    {
        public abstract int getWidth();

        public abstract int getHeight();

        public abstract void setWidth(int _width);

        public abstract void setHeight(int _height);

        public abstract List<IBall> getBalls();

        public abstract void addBallsToTable(int numberOfBalls);

        internal abstract void randomizeSpeed(Ball ball);

        public abstract bool isWithinTable(Vector2 position);

        public static LogicAPI createTableInstance()
        {
            return new Table();
        }

        public static IGameManager createGameManagerInstance(LogicAPI table) 
        {
            return new GameManager(table);
        }

    }
}
