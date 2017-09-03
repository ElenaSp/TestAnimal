
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace TestAnimal
{
    class ActionViewModel<T> : ViewModelBase,ICommand
    {
        private string v;
        private ActionViewModel<string> insertCommand;

        public Action<T> TargetExecuteMethod
        { get; set; }

        public Func<T, bool> TargetCanExecuteMethod
        { get; set; }

        public ActionViewModel(string name,Action<T> executeMethod)
        {
            TargetExecuteMethod = executeMethod;
            DisplayName = name;
        }

        public ActionViewModel(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            TargetExecuteMethod = executeMethod;
            TargetCanExecuteMethod = canExecuteMethod;
        }

        public ActionViewModel(string v, ActionViewModel<string> insertCommand)
        {
            this.v = v;
            this.insertCommand = insertCommand;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        #region ICommand Members

        bool ICommand.CanExecute(object parameter)
        {

            if (TargetCanExecuteMethod != null)
            {
                T tparm = (T)parameter;
                return TargetCanExecuteMethod(tparm);
            }

            if (TargetExecuteMethod != null)
            {
                return true;
            }

            return false;
        }
 

        public event EventHandler CanExecuteChanged = delegate { };

        void ICommand.Execute(object parameter)
        {
            if (TargetExecuteMethod != null)
            {
                TargetExecuteMethod((T)parameter);
            }
        }

        #endregion
    }
}