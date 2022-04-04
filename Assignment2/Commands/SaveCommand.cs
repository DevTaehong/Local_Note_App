using Assignment2.Dialogs;
using Assignment2.Models;
using Assignment2.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace Assignment2.Commands
{
    public class SaveCommand : ICommand
    {
        public TitleModel TheTitle { get; set; }

        public event EventHandler CanExecuteChanged;

        //Create an event and delegate (ie. handler), to raise an event when a new note is created.
        public event NoteCreatedHandler OnNoteCreated;
        public delegate void NoteCreatedHandler(object sender, EventArgs e);

        public event NoteEditedHandler OnNoteEdited;
        public delegate void NoteEditedHandler(object sender, EventArgs e);

       

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {


            SaveAfterEditDialog saveAfterEditDialog = new SaveAfterEditDialog();
            ContentDialogResult EditResult = await saveAfterEditDialog.ShowAsync();
            if (EditResult == ContentDialogResult.Primary)
            {
                try
                {
                    Repositories2.NoteRepo2.EditNotes(App.ContentTextBox);
                    OnNoteEdited.Invoke(this, new EventArgs());
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }


            else
            {
                SaveNoteDialog snd = new SaveNoteDialog();
                ContentDialogResult SaveResult = await snd.ShowAsync();
                if (SaveResult == ContentDialogResult.Primary)
                {
                    // Code to do the saving...
                    try
                    {
                        Repositories2.NoteRepo2.SaveNotesToFile(snd.newNoteTitle);

                        TheTitle = snd.title;
                        OnNoteCreated?.Invoke(this, new EventArgs());
                    }
                    catch (Exception)
                    {
                        Debug.WriteLine("File save error occurred!");
                    }
                }
            }
                
                
 
        }
    }
}
