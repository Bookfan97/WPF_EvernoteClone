using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    class ShowRegisterCommand: ICommand
    {
        public LoginVM ViewModel { get; set; }

        public ShowRegisterCommand(LoginVM loginVM)
        {
            ViewModel = loginVM;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.SwitchViews();
        }

        public event EventHandler CanExecuteChanged;
    }
}
