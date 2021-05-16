using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EvernoteClone.Annotations;
using EvernoteClone.Model;
using EvernoteClone.ViewModel.Commands;
using EvernoteClone.ViewModel.Helpers;

namespace EvernoteClone.ViewModel
{
    internal class LoginVM: INotifyPropertyChanged
    {
        private User user;
        private bool isShowingRegister = false;
        public User User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        private string username;

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                User = new User
                {
                    Username = username,
                    Password = this.Password,
                    Name = this.Name,
                    Lastname = this.Lastame,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged("Username");
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                password = value; 
                User = new User
                {
                    Username = this.Username,
                    Password = password,
                    Name = this.Name,
                    Lastname = this.Lastame,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged("Password");
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                User = new User
                {
                    Username = this.Username,
                    Password = this.Password,
                    Name = name,
                    Lastname = this.Lastame,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged("Name");
            }
        }

        private string lastname;
        public string Lastame
        {
            get { return lastname; }
            set
            {
                lastname = value;
                User = new User
                {
                    Username = this.Username,
                    Password = this.Password,
                    Name = this.Name,
                    Lastname = lastname,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged("Lastame");
            }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                User = new User
                {
                    Username = this.Username,
                    Password = this.Password,
                    Name = this.Name,
                    Lastname = this.Lastame,
                    ConfirmPassword = confirmPassword
                };
                OnPropertyChanged("ConfirmPassword");
            }
        }

        private Visibility LoginVisibility;
        public Visibility LoginVis
        {
            get { return LoginVisibility; }
            set
            {
                LoginVisibility = value;
                OnPropertyChanged("LoginVisibility");
            }
        }
        
        private Visibility registerVisibility;
        public Visibility RegisterVisibility
        {
            get { return registerVisibility; }
            set
            {
                registerVisibility = value;
                OnPropertyChanged("RegisterVisibility");
            }
        }

        public RegisterCommand RegisterCommand { get; set; }

        public LoginCommand LoginCommand { get; set; }
        public ShowRegisterCommand ShowRegisterCommand { get; set; }

        public LoginVM()
        {
            LoginVisibility = Visibility.Visible;
            RegisterVisibility = Visibility.Collapsed;
            RegisterCommand = new RegisterCommand(this);
            LoginCommand = new LoginCommand(this);
            ShowRegisterCommand = new ShowRegisterCommand(this);
            User = new User();
        }

        public void SwitchViews()
        {
            isShowingRegister = !isShowingRegister;
            if (isShowingRegister)
            {
                RegisterVisibility = Visibility.Visible;
                LoginVisibility = Visibility.Collapsed;
            }
            else
            {
                RegisterVisibility = Visibility.Collapsed;
                LoginVisibility = Visibility.Visible;
            }
        }

        public void Login()
        {
            //TODO: Login
        }
        
        public async Task Register()
        {
            await FirebaseAuthHelper.Register(User);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}