using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using EvernoteClone.Model;

namespace EvernoteClone.ViewModel.Commands
{
    //Command for the login button
    public class LoginCommand : ICommand
    {
        public LoginVM VM { get; set; }

        public LoginCommand(LoginVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            User user = parameter as User;
            if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                return false;
            }

            return true;
        }

        public void Execute(object parameter)
        {
            VM.Login();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}