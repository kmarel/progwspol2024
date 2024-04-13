using Data;
using System.Numerics;

namespace Logic
{
    public abstract class LogicAPI
    {
        public abstract int getWidth();

        public abstract int getHeight();

        public abstract void setWidth(int _width);

        public abstract void setHeight(int _height);

        public abstract List<Ball> getBalls();

        public abstract void addBallsToTable(int numberOfBalls);

        internal abstract void randomizeSpeed(Ball ball);

        public abstract bool isWithinTable(Vector2 position);

        public static LogicAPI createTableInstance()
        {
            return new Table();
        }

    }
}
