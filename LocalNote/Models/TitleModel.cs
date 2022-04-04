using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalNote.Models
{
    public class TitleModel
    {
        public string NoteTitle { get; }
        public TextModel NoteTexts { get; set; }
        public string NotesAsString => string.Join(",", NoteTitle);
        public TitleModel(string noteTitle, TextModel texts)
        {
            NoteTitle = noteTitle;
            NoteTexts = texts;
        }



    }
}
