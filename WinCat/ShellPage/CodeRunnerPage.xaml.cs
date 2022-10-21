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
using WinCat.Model;
using WinCat.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinCat.ShellPage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CodeRunnerPage : Page
    {
        public CodeRunnerPage()
        {
            this.InitializeComponent();
            CodeRunnerViewModel viewModel = new CodeRunnerViewModel();
            this.DataContext = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            ((CodeRunnerViewModel)DataContext).AddScript();
        }

        private async void scriptPicker_Click(object sender, RoutedEventArgs e)
        {
            var filePicker = new FileOpenPicker();

            //Get the Window's HWND
            // https://stackoverflow.com/questions/71432263/how-to-retrieve-the-window-handle-of-the-current-winui-3-mainwindow-from-a-page
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle((Application.Current as App)?.Window as Shell);

            // Associate the HWND with the file picker
            WinRT.Interop.InitializeWithWindow.Initialize(filePicker, hwnd);

            // Use file picker like normal!
            filePicker.FileTypeFilter.Add("*");
            StorageFile file = await filePicker.PickSingleFileAsync();
            scriptPath.Text = file?.Path ?? string.Empty;
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            var a = 1;
            a++;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            ((CodeRunnerViewModel)DataContext).RemoveScript((Script)((Button)sender).DataContext);
        }
    }
}
