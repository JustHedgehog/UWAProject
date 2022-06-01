using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace UWA_Projekt { 

    public sealed partial class MainPage : Page
    {
        Level level = Level.Easy;
        private const string FILE_NAME = "./BestScore.txt";
        TextData textData = new TextData { DifficultLevelText = "Poziom trudności : Easy" };

        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = textData;
        }

        private void DifficultLevel_Click(object sender, RoutedEventArgs e)
        {
            if (DifficultLevelButton.Content.Equals("Poziom trudności : Easy"))
            {
                textData.DifficultLevelText = "Poziom trudności : Medium";
                level = Level.Medium;
            }
            else if (DifficultLevelButton.Content.Equals("Poziom trudności : Medium"))
            {
                textData.DifficultLevelText = "Poziom trudności : Hard";
                level = Level.Hard;
            }
            else if (DifficultLevelButton.Content.Equals("Poziom trudności : Hard"))
            {
                textData.DifficultLevelText = "Poziom trudności : Easy";
                level = Level.Easy;
            }
            Refresh();
        }

        private void Refresh()
        {
            this.DataContext = null;
            this.DataContext = textData;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(Game));
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private async void ReadBestScore()
        {
            try
            {
                using (StreamReader sr = new StreamReader(FILE_NAME))
                {
                    BestScoreTextBlock.Text =  sr.ReadLine().ToString();
                }
            }
            catch (Exception ex)
            {
                var dialog = new MessageDialog(ex.Message);
                await dialog.ShowAsync();
            }
        }
    }
}
