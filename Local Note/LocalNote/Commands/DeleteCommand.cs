using LocalNote.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace LocalNote.Commands
{
    public class DeleteCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        //public MainPage MainPage = new MainPage();

        //Create an event and delegate (ie. handler), to raise an event when a note is deleted.
        public event NoteDeletedHandler OnNoteDeleted;
        public delegate void NoteDeletedHandler(object sender, EventArgs e);
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            DeleteNoteDialog dnd = new DeleteNoteDialog();
            ContentDialogResult result = await dnd.ShowAsync();
            if(result == ContentDialogResult.Primary)
            {
                try
                {
                    Repositories2.DataRepo.DeleteNotes(App.CommandBarLable);
                    OnNoteDeleted?.Invoke(this, new EventArgs());
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            
        }
    }
}
