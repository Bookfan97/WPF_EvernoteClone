using System;
using System.Collections.Generic;
using System.Text;
using EvernoteClone.Model;
using EvernoteClone.ViewModel.Commands;

namespace EvernoteClone.ViewModel
{
    internal class LoginVM
    {
        private User user;

        public User LocalUser
        {
            get { return user; }
            set { user = value; }
        }

        public RegisterCommand RegisterCommand { get; set; }

        public LoginCommand LoginCommand { get; set; }

        public LoginVM()
        {
            RegisterCommand = new RegisterCommand(this);
            LoginCommand = new LoginCommand(this);
        }
    }
}