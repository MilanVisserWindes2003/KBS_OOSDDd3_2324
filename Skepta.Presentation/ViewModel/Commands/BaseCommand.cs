using System;
using System.Windows.Input;

namespace Skepta.Presentation.ViewModel.Commands
{
    //Implements ICommand, it has two classes one where the return type is given and where it is not
    //In the class where the type is not given it has 2 constructors. One that executes without the check to be able to execute and the other where it does have it.
    //The CanExecute class checks if you are allowed to execute the command
    //The Execute class executes the command
    public class BaseCommand<T> : ICommand
    {
        private Action<T>? execute;

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public BaseCommand(Action<T> execute)
        {
            this.execute = execute;
        }

        public virtual void Execute(object parameter)
        {
            execute?.Invoke((T)parameter);
        }
    }

    public class BaseCommand : ICommand
    {
        private Action? execute;

        private Func<bool>? canExecute;
        public event EventHandler? CanExecuteChanged;

        public BaseCommand(Action execute)
        {
            this.execute = execute;
            canExecute = null;
        }


        public BaseCommand(Action execute, Func<bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            if (canExecute != null)
            {
                return canExecute.Invoke();
            }

            return true;
        }

        public virtual void Execute(object? parameter)
        {
            execute?.Invoke();
        }
    }

}
