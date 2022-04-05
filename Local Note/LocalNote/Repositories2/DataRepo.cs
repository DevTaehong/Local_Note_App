using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        //Get a list of all records from the database
        public static List<String> GetData()
        {
            List<String> entries = new List<string>();
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Note.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Title from NoteTable", db);
                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                { 
                    entries.Add(query.GetString(0)); 
                }
                db.Close();
            }
            return entries;
        }

    }
}
