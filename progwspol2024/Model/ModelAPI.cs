using System.ComponentModel;
using System.Numerics;

namespace Model
{


    public interface IBall : INotifyPropertyChanged, IObserver<Vector2>
    {
        int Diameter { get; }
        float XRelativeToCanvas { get; set; }
        float YRelativeToCanvas { get; set; }

    }
    public abstract class ModelAPI
    {

        public static ModelAPI createTableInstance()
        {
            return new Table();
        }

        public abstract void startGame();

        public abstract void createBalls(int amount);

        public abstract List<IBall> getBalls();

        public abstract int getBoardWidth();
        public abstract int getBoardHeight();

    }
}
