using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WinCat.ShellPage;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinCat
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Shell : Window
    {
        public Shell()
        {
            this.InitializeComponent();
        }

        private async void mainNav_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                mainContentFrame.Navigate(typeof(MainSettingPage));
            }
            else
            {
                try
                {
                    var selectedItem = (NavigationViewItem)args.SelectedItem;
                    string selectedItemTag = ((string)selectedItem.Tag);
                    string pageName = "WinCat.ShellPage." + selectedItemTag;
                    Type pageType = Type.GetType(pageName);
                    mainContentFrame.Navigate(pageType);
                }
                catch (System.ArgumentNullException)
                {
                    ContentDialog dialog = new()
                    {
                        // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
                        XamlRoot = sender.XamlRoot,
                        Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style,
                        Title = "Page not found",
                        PrimaryButtonText = "Ok",
                        DefaultButton = ContentDialogButton.Primary,
                        Content = new PageNotFoundPage()
                    };
                    _ = await dialog.ShowAsync();

                }

            }
        }
    }
}
