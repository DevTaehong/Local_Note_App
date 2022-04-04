using LocalNote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Assignment2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class About : Page
    {
        
        public About()
        {
            this.InitializeComponent();

            // Get Dynamic app info from package minifest
            Package package = Package.Current;
            PackageId packageId = package.Id;
            PackageVersion version = packageId.Version;
            String appName = package.DisplayName;
            String pub = package.PublisherDisplayName;
            String description = Package.Current.Description;
            String versionDescription = Package.Current.Description;

            appInfo.Text = string.Format("Title: " + appName
                + "\nCompany: " + pub
                + "\nVersion: {0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision
                + "\nDescription: This application manages notes like nobody's business. And I mean nobody."  
                + "\nCopyright: Copyright © 2022");
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
            base.OnNavigatedTo(e);
        }
        private void OnBackRequested(object sender, BackRequestedEventArgs backRequestedEventArgs)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
            backRequestedEventArgs.Handled = true;
        }
    }
}
