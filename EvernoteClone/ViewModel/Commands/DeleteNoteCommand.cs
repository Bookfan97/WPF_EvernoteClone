using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using EvernoteClone.Model;

namespace EvernoteClone.ViewModel.Commands
{
    public class DeleteNoteCommand: ICommand
    {
        public NotesVM ViewModel { get; set; }

        public DeleteNoteCommand(NotesVM view)
        {
            ViewModel = view;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Note note = parameter as Note;
            if(note != null)
            {
                ViewModel.DeleteNote(note);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
