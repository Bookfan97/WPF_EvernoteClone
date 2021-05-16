using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using EvernoteClone.Model;

namespace EvernoteClone.ViewModel.Commands
{
    public class RegisterCommand : ICommand
    {
        public LoginVM VM { get; set; }

        public RegisterCommand(LoginVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            User user = parameter as User;
            if (user == null || string.IsNullOrEmpty(user.Username) || 
                string.IsNullOrEmpty(user.Password) || 
                string.IsNullOrEmpty(user.ConfirmPassword) || 
                user.Password != user.ConfirmPassword)
            {
                return false;
            }

            return true;
        }

        public void Execute(object parameter)
        {
            VM.Register();
        }

        public event EventHandler CanExecuteChanged
		{
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}