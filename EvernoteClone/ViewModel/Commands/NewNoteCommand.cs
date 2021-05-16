﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using EvernoteClone.Model;

namespace EvernoteClone.ViewModel.Commands
{
    public class NewNoteCommand : ICommand
    {
        public NotesVM ViewModel { get; set; }

        public NewNoteCommand(NotesVM viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            Notebook selectedNotebook = parameter as Notebook;
            return selectedNotebook != null ? true : false;
        }

        public void Execute(object parameter)
        {
            Notebook selectedNotebook = parameter as Notebook;
            ViewModel.CreateNote(selectedNotebook.ID);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}