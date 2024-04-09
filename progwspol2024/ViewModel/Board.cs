using System.Diagnostics;
using System.Windows.Input;

namespace ViewModel
{
    public class Board : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Debug.WriteLine("Nacisnieto przycisk");
        }
    }
}
