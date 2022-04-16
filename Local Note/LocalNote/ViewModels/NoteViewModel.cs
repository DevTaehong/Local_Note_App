using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment2.Commands;
using LocalNote.Commands;
using LocalNote.Models;
using LocalNote.Repositories2;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Search;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LocalNote.ViewModels
{
    public class NoteViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public SaveCommand saveCommand = new SaveCommand();
        public DeleteCommand deleteCommand = new DeleteCommand();
        public ExitCommand exitCommand = new ExitCommand();
        public List<TitleModel> _allTitles = new List<TitleModel>();
        public ObservableCollection<TitleModel> Titles { get; set; }
        public string SelectedNoteText { get; set; }
        public string SelectedNoteTitles { get; set; }
        private TitleModel _selectedTitle;
        private string _filter;
        public NoteRepo2 NoteRepo = new NoteRepo2();
        public DataRepo DataRepo = new DataRepo();
        
        

        // Constructor
        public NoteViewModel()
        {
            Titles = new ObservableCollection<TitleModel>();

            deleteCommand.OnNoteDeleted += DeleteCommand_OnNoteDeleted;
            saveCommand.OnNoteCreated += SaveCommand_OnNoteCreated;
            saveCommand.OnNoteEdited += EditCommand_OnNoteEdited;
            SelectedNoteTitles = "Untitled Note";

            PerformFiltering();
            //NoteRepo.ReadFile(Titles, _allTitles);
            DataRepo.GetData(Titles, _allTitles);
        }

        private void AddCommand_CanExecuteChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void EditCommand_OnNoteEdited(object sender, EventArgs e)
        {
            TitleModel title = _allTitles.Find(x => x.NoteTitle.Contains(App.EditNoteName));
            title.NoteTexts = new TextModel(App.ContentTextBox);
        }

        private void DeleteCommand_OnNoteDeleted(object sender, EventArgs e)
        {
            // source: https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.contains?view=net-6.0
            TitleModel title =_allTitles.Find(x => x.NoteTitle.Contains(App.CommandBarLable));
            Titles.Remove(title);
            _allTitles.Remove(title);
        }

        //This event fires when the saveCommand object raises it's "A new note was created" event.
        private async void SaveCommand_OnNoteCreated(object sender, EventArgs e)
        {
            //If a note object exists, add it to the _allNotes list
            if (saveCommand.TheTitle != null)
            {
                //Source: https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.contains?view=net-6.0
                if (_allTitles.Exists(x => x.NoteTitle == saveCommand.TheTitle.NoteTitle)){
                    ContentDialog failDialog = new ContentDialog()
                    {
                        Content = "Cannot have two notes with the same name.",
                        Title = "Saving failed.",
                        PrimaryButtonText = "OK"
                    };
                    await failDialog.ShowAsync();
                }
                else
                {
                    Titles.Add(saveCommand.TheTitle);
                    _allTitles.Add(saveCommand.TheTitle);

                    // Show the user a confirmation
                    ContentDialog savedDialog = new ContentDialog()
                    {
                        Content = "Notes saved successfully to file, hurray!",
                        Title = "Save Successful",
                        PrimaryButtonText = "OK"
                    };
                    await savedDialog.ShowAsync();
                }
            }
        }

        public TitleModel SelectedTitle
        {
            get { return _selectedTitle; }
            set
            {
                _selectedTitle = value;
                if (value == null)
                {
                    SelectedNoteText = "";
                    SelectedNoteTitles = "Untitled Note";
                }
                else
                {
                    SelectedNoteText = value.NoteTexts.NoteText;
                    SelectedNoteTitles = value.NoteTitle;
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedNoteText"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedNoteTitles"));
            }
        }

        public string Filter
        {
            get { return _filter; }
            set
            {
                if (_filter == value) { return; }
                _filter = value;
                // Call our filtering method
                PerformFiltering();
                // Invoke the PropertyChanged 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Filter"));
            }
        }

        private void PerformFiltering()
        {
            if (_filter == null)
            {
                _filter = "";
            }
            //If _filter has a value (ie. user entered something in Filter textbox)
            //Lower-case and trim string
            var lowerCaseFilter = Filter.ToLowerInvariant().Trim();

            //Use LINQ query to get all notemodel names that match filter text, as a list
            var result =
                _allTitles.Where(d => d.NotesAsString.ToLowerInvariant()
                .Contains(lowerCaseFilter))
                .ToList();

            //Get list of values in current filtered list that we want to remove
            //(ie. don't meet new filter criteria)
            var toRemove = Titles.Except(result).ToList();

            //Loop to remove items that fail filter
            foreach (var x in toRemove)
            {
                Titles.Remove(x);
            }
            var resultCount = result.Count;
            // Add back in correct order.
            for (int i = 0; i < resultCount; i++)
            {
                var resultItem = result[i];
                if (i + 1 > Titles.Count || !Titles[i].Equals(resultItem))
                {
                    Titles.Insert(i, resultItem);
                }
            }
        }

    }
}
