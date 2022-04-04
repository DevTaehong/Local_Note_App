using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalNote.Models
{
    public class TextModel
    {
        public string NoteText { get;}

        public TextModel(string noteText)
        {
            NoteText = noteText;
        }
    }
}
