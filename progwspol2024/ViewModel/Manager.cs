using Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public class Manager : INotifyPropertyChanged
    {

        private Model.ModelAPI modelAPI;

        private int numberOfBallsToCreate = 0;

        public int boardWidth;
        public int boardHeight;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Model.Ball> Balls { get; } = new ObservableCollection<Model.Ball>();

        public string BallsAmountText
        {
            get { return numberOfBallsToCreate.ToString(); }
            set
            {
                int.TryParse(value, out numberOfBallsToCreate);
                OnPropertyChanged();
            }
        }

        public CustomCommand CreateBallsCommand => new CustomCommand(execute => createBalls());

        public Manager()
        {
            modelAPI = Model.ModelAPI.createTableInstance();

            boardWidth = modelAPI.getBoardWidth();
            boardHeight = modelAPI.getBoardHeight();
        }

        private void createBalls()
        {
            modelAPI.createBalls(numberOfBallsToCreate);
            
            foreach(Model.Ball ball in modelAPI.getBalls())
            {
                Balls.Add(ball);
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
