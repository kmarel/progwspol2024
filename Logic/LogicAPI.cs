﻿using Data;
using System.ComponentModel;
using System.Numerics;

namespace Logic
{

    public interface IBall : IObservable<Vector2>, IObserver<Vector2>
    {

        Vector2 getPosition();

        void setVelocity(Vector2 newVelocity);

        Vector2 getVelocity();

        int getRadius();

        float getElapsedTimeInSeconds();

    }

    public abstract class LogicAPI
    {
        public abstract int getWidth();

        public abstract int getHeight();

        public abstract void setWidth(int _width);

        public abstract void setHeight(int _height);

        public abstract List<IBall> getBalls();

        public abstract void addBallsToTable(int numberOfBalls);

        public static LogicAPI createTableInstance()
        {
            return new Table();
        }

        public static LogicAPI createTableInstance(DataAPI dataAPI)
        {
            return new Table(dataAPI);
        }

    }
}
