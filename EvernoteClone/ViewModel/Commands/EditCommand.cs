﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class EditCommand: ICommand
    {
        public NotesVM ViewModel { get; set; }

       public EditCommand(NotesVM view)
       {
           ViewModel = view;
       }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.StartEditing();
        }

        public event EventHandler CanExecuteChanged;
    }
}
