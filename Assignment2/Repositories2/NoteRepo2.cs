using Assignment2.Models;
using Assignment2.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;

namespace Assignment2.Repositories2
{
    public class NoteRepo2
    {
        // The class responsible for dealing with my external data (text file)
        private static StorageFolder _notesFolder = ApplicationData.Current.LocalFolder;

        public async static void EditNotes(string editedText)
        {
            string fileName = App.EditNoteName + ".txt";
            try
            {
                StorageFile editFile = await _notesFolder.GetFileAsync(fileName);
                await FileIO.WriteTextAsync(editFile, editedText);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public async static void DeleteNotes(string noteName)
        {
            String fileName = noteName + ".txt";
            try
            {
               StorageFile storage  = await _notesFolder.GetFileAsync(fileName);
               await storage.DeleteAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
       
        public async static void SaveNotesToFile(String newNoteTitle)
        {
            // Build a File name
            String filename = newNoteTitle + ".txt";
            // Do the save (Using windows storage)
            try
            {
                StorageFile noteFile = await _notesFolder.CreateFileAsync(filename, CreationCollisionOption.FailIfExists);
                await FileIO.AppendTextAsync(noteFile, App.ContentTextBox);
            }
            catch (Exception)
            {
                Debug.WriteLine("File save error occurred!");
            }
        }

        public async void ReadFile(ObservableCollection<TitleModel> titles, List<TitleModel> _allTitles)
        {
           
            try
            {
                IReadOnlyList<StorageFile> sortedItems = await _notesFolder.GetFilesAsync(CommonFileQuery.DefaultQuery, 0, 100);
                foreach (StorageFile file in sortedItems)
                {
                    string text = await FileIO.ReadTextAsync(file);
                    TitleModel titleModel = new TitleModel(file.DisplayName, new TextModel(text));
                    titles.Add(titleModel);
                    _allTitles.Add(titleModel);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
