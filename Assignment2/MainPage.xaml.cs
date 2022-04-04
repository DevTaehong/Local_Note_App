using Assignment2.Commands;
using Assignment2.Dialogs;
using Assignment2.Models;
using Assignment2.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Assignment2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public NoteViewModel NoteViewModel { get; set; }

        public MainPage()
        {
            this.InitializeComponent(); 
            
            NoteViewModel = new NoteViewModel();
            NoteContentTextBox.Focus(FocusState.Programmatic);
        }

        public void EditAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            NoteContentTextBox.IsReadOnly = false;
            EditButton.IsEnabled = false;
            SaveButton.IsEnabled = true;
            NoteContentTextBox.Focus(FocusState.Programmatic);
            App.EditNoteName = NoteViewModel.SelectedTitle.NoteTitle;
            App.ContentTextBox = NoteContentTextBox.Text;
        }

        // Source: https://stackoverflow.com/questions/34458244/unselect-gridview-item-on-click-if-already-selected/34478110#34478110
        private async void listViewItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            EditButton.IsEnabled = true;
            NoteContentTextBox.IsReadOnly = true;
            SaveButton.IsEnabled= false;
            DeleteButton.IsEnabled = true;

            var listView = sender as ListView;
            if (e.ClickedItem == listView.SelectedItem)
            {
                await Task.Delay(100);
                listView.SelectedItem = null;
                SaveButton.IsEnabled = true;
                EditButton.IsEnabled = false;
                NoteContentTextBox.IsReadOnly = false;

                // Source: https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.controls.control.focus?view=winrt-22000
                NoteContentTextBox.Focus(FocusState.Programmatic);
            }       
        }

        private void AddAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            commandBarLable.Text = "Untitled Note";
            MylistView.SelectedItem = null;
            NoteContentTextBox.IsReadOnly = false;
            NoteContentTextBox.Focus(FocusState.Programmatic);
            SaveButton.IsEnabled = true;
            EditButton.IsEnabled = false;
        }
        private void DeleteAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            App.CommandBarLable = commandBarLable.Text;
        }
        public void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Source: https://social.msdn.microsoft.com/Forums/sqlserver/en-US/448127b7-9958-434f-97c5-67844a7f2e5b/how-to-declare-dynamic-global-variable-in-appxamlcs-or-session-mgmt-replacement-in-silverlight?forum=silverlightstart
            App.ContentTextBox = NoteContentTextBox.Text;
            NoteContentTextBox.IsReadOnly = true;
            EditButton.IsEnabled = true;
            SaveButton.IsEnabled = false;
            this.Focus(FocusState.Programmatic);
            NoteContentTextBox.Text = "";
        }

        private async void AboutAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            AboutDialog aboutDialog = new AboutDialog();
            await aboutDialog.ShowAsync();
        }

        private void ExitAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
