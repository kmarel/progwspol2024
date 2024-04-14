using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Windows.Shapes;

namespace Model
{
    internal class Ball : IBall
    {

        private Logic.IBall ballLogic;

        public int Diameter
        {
            get { return ballLogic.getRadius() * 2; }
        }

        public float XRelativeToCanvas
        {
            get { return ballLogic.getPosition().X; }
            set
            {
                ballLogic.setPosition(new Vector2(value, ballLogic.getPosition().Y));
                OnPropertyChanged();
            }
        }

        public float YRelativeToCanvas
        {
            get { return ballLogic.getPosition().Y; }
            set
            {
                ballLogic.setPosition(new Vector2(ballLogic.getPosition().X, value));
                OnPropertyChanged();
            }
        }

        public Ball(Logic.IBall ballLogic) 
        {
            this.ballLogic = ballLogic;
            ballLogic.Subscribe(this);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if(PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
            XRelativeToCanvas = value.X;
            YRelativeToCanvas = value.Y;
        }
    }

}
