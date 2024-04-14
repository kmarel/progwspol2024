using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public class CustomCommand : ICommand
    {

        private Action<object?> execute;


        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            execute(parameter);
        }

        public CustomCommand(Action<object?> execute)
        {
            this.execute = execute;
        }

    }
}
