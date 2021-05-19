using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using EvernoteClone.Model;

namespace EvernoteClone.ViewModel.Commands
{
    public class DeleteNotebookCommand: ICommand
    {
        public NotesVM ViewModel { get; set; }

        public DeleteNotebookCommand(NotesVM view)
        {
            ViewModel = view;
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
                ViewModel.DeleteNotebook(notebook);
            }
        }

        public event EventHandler CanExecuteChanged;

        protected virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
