using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
    public class NotesVM : INotifyPropertyChanged
    {
        public ObservableCollection<Notebook> Notebooks { get; set; }
        public ObservableCollection<Note> Notes { get; set; }
        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }
        public EditCommand EditCommand { get; set; }
        public DeleteNotebookCommand DeleteNotebookCommand { get; set; }
        public DeleteNoteCommand DeleteNoteCommand { get; set; }
        public EndEditCommand EndEditCommand { get; set; }

        public event EventHandler SelectedNoteChanged;
        private Notebook selectedNotebook;

        public Notebook SelectedNotebook
        {
            get { return selectedNotebook; }
            set
            {
                selectedNotebook = value;
                OnPropertyChanged("SelectedNotebook");
                GetNotes();
            }
        }

        private Note selectedNote;

        public Note SelectedNote
        {
            get { return selectedNote; }
            set
            {
                selectedNote = value;
                OnPropertyChanged("SelectedNote");
                SelectedNoteChanged?.Invoke(this, new EventArgs());
            }
        }
        private Visibility isVisible;

        public Visibility IsVisible
        {
            get { return isVisible; }
            set
            {
                isVisible = value;
                OnPropertyChanged("IsVisible");
            }
        }


        public NotesVM()
        {
            NewNoteCommand = new NewNoteCommand(this);
            NewNotebookCommand = new NewNotebookCommand(this);
            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();
            EditCommand = new EditCommand(this);
            EndEditCommand = new EndEditCommand(this);
            DeleteNotebookCommand = new DeleteNotebookCommand(this);
            DeleteNoteCommand = new DeleteNoteCommand(this);
            IsVisible = Visibility.Collapsed;
            GetNotebooks();
        }

        public async Task CreateNote(string notebookID)
        {
            Note newNote = new Note()
            {
                NotebookID = notebookID,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Title = $"Note for {DateTime.Now.ToString()}"
            };
            await DatabaseHelper.Insert(newNote);
            GetNotes();
        }

        public async Task CreateNotebook()
        {
            Notebook newNotebook = new Notebook()
            {
                Name = "New Notebook",
                UserId = App.UserID
            };
            await DatabaseHelper.Insert(newNotebook);
            GetNotebooks();
        }

        public async Task GetNotebooks()
        {
            IEnumerable<Notebook> notebooks = (await DatabaseHelper.Read<Notebook>()).Where(n => n.UserId == App.UserID).ToList();
            Notebooks.Clear();
            foreach (var notebook in notebooks)
            {
                Notebooks.Add(notebook);
            }
        }

        private async Task GetNotes()
        {
            if (selectedNotebook != null)
            {
                IEnumerable<Note> notes = (await DatabaseHelper.Read<Note>()).Where(n => n.NotebookID == SelectedNotebook.ID).ToList();
                Notes.Clear();
                foreach (var note in notes)
                {
                    Notes.Add(note);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void StartEditing()
        {
            IsVisible = Visibility.Visible;
        }

        public void StopEditing(Notebook notebook)
        {
            IsVisible = Visibility.Collapsed;
            DatabaseHelper.Update(notebook);
            GetNotebooks();
        }

        public void UpdateSelectedNote()
        {
            DatabaseHelper.Update(selectedNote);
        }

        public void DeleteNotebook(Notebook notebook)
        {
            DatabaseHelper.Delete(notebook);
            GetNotebooks();
        }

        public void DeleteNote(Note note)
        {
            DatabaseHelper.Delete(note);
            GetNotebooks();
        }
    }
}