using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EvernoteClone.ViewModel;

namespace EvernoteClone.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private LoginVM viewModel;
        public LoginWindow()
        {
            InitializeComponent();
            viewModel = Resources["Vm"] as LoginVM;
            viewModel.Authenticated += viewModel_Authenticated;
        }

        private void viewModel_Authenticated(object? sender, EventArgs e)
        {
            Close();
        }
    }
}
