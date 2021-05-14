using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using EvernoteClone.Model;

namespace EvernoteClone.ViewModel.Commands
{
    public class EndEditCommand: ICommand
    {
        public NotesVM ViewModel { get; set; }

        public EndEditCommand(NotesVM viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Notebook notebook = parameter as Notebook;
            if(notebook != null)
            {
                ViewModel.StopEditing(notebook);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
