using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;

namespace LocalNote.Commands
{
    public class EditCommand : ICommand
    {
        //private ViewModels.NoteViewModel _nvm;

        public event EventHandler CanExecuteChanged;

        //public EditCommand(ViewModels.NoteViewModel NVM)
        //{
        //    this._nvm = NVM;
        //}

        //public void Fire_CanExcuteChanged()
        //{
        //    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        //}

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {

        }
    }
}
