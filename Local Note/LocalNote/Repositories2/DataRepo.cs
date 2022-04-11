using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalNote.Models;
using Microsoft.Data.Sqlite;
using Windows.Storage;

namespace LocalNote.Repositories2
{
    public class DataRepo
    {
        //Initialize the database
        public async static void InitializeDatabase()
        {
            //Create the db file in windows storage
            await ApplicationData.Current.LocalFolder.CreateFileAsync("Note.db", CreationCollisionOption.OpenIfExists);

            //Get full path to db
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Note.db");

            //open connection to db
            using (SqliteConnection conn = new SqliteConnection($"Filename={dbpath}"))//Establish connection to a database
            {
                conn.Open();    //Open connection
                String tableCommand = "CREATE TABLE IF NOT EXISTS NoteTable " +   //Define a SQL command (Cretea table statement)
                    "(Title string PRIMARY KEY, " +
                    "Text string NOT NULL);";
                SqliteCommand cmd = new SqliteCommand(tableCommand, conn); //Create a command object (running our string SQL commnd)
                cmd.ExecuteReader();    //Execute the sql command
            }
        }

        //Add a new record to the database
        public static void AddData(string title, string text)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Note.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO noteTable VALUES (@Entry, @Entry2);";
                insertCommand.Parameters.AddWithValue("@Entry", title);
                insertCommand.Parameters.AddWithValue("@Entry2", text);
                insertCommand.ExecuteReader();
                db.Close();
            }
        }
        public static void EditNotes(string editedText)
        {
            try
            {
                string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Note.db");
                using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
                {
                    db.Open();
                    SqliteCommand updateCommand = new SqliteCommand();
                    updateCommand.Connection = db;
                    // Use parameterized query to prevent SQL injection attacks
                    updateCommand.CommandText = "UPDATE NoteTable SET Text = @Text WHERE Title = @Title;";
                    updateCommand.Parameters.AddWithValue("@Title", App.EditNoteName);
                    updateCommand.Parameters.AddWithValue("@Text", editedText);
                    updateCommand.ExecuteReader();
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        //Get a list of all records from the database
        public static void GetData(ObservableCollection<TitleModel> titles, List<TitleModel> _allTitles)
        {
            try
            {
                string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Note.db");
                using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
                {
                    db.Open();
                    SqliteCommand selectCommand = new SqliteCommand
                        ("SELECT Title, Text from NoteTable", db);
                    SqliteDataReader query = selectCommand.ExecuteReader();
                    while (query.Read())
                    {
                        TitleModel titleModel = new TitleModel(query.GetString(0), new TextModel(query.GetString(0 + 1)));
                        titles.Add(titleModel);
                        _allTitles.Add(titleModel);
                    }
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public static void DeleteNotes(string noteName)
        {
            try
            {
                string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Note.db");
                using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
                {
                    db.Open();
                    SqliteCommand deleteCommand = new SqliteCommand();
                    deleteCommand.Connection = db;
                    // Use parameterized query to prevent SQL injection attacks
                    deleteCommand.CommandText = "DELETE FROM NoteTable WHERE Title = @Title;";
                    deleteCommand.Parameters.AddWithValue("@Title", noteName);
                    deleteCommand.ExecuteReader();
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }



    }
}
