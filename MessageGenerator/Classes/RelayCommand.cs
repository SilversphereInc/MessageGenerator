using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MessageGenerator.Classes
{
    class RelayCommand : ICommand
    {
        private Action<object> _Action;

        public RelayCommand(Action<object> action)
        {
            _Action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (parameter != null)
            {
                _Action(parameter);
            }
        }
    }
}
