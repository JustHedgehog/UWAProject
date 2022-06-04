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
        Windows.Storage.ApplicationDataContainer localStorage = Windows.Storage.ApplicationData.Current.LocalSettings;
        Level DifficultyLevel;

        public MainPage()
        {
            this.InitializeComponent();
            DifficultyLevel = (Level)Enum.Parse(typeof(Level), localStorage.Values["difficulty"].ToString());
            ReadBestScore();
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

        private void ReadBestScore()
        {

            switch (DifficultyLevel)
            {
                case Level.Easy:
                    if (localStorage.Values["easyBestScore"] != null)
                        BestScoreTextBlock.Text = localStorage.Values["easyBestScore"].ToString();
                    break;

                case Level.Medium:
                    if (localStorage.Values["mediumBestScore"] != null)
                        BestScoreTextBlock.Text = localStorage.Values["mediumBestScore"].ToString();
                    break;

                case Level.Hard:
                    if (localStorage.Values["hardBestScore"] != null)
                        BestScoreTextBlock.Text = localStorage.Values["hardBestScore"].ToString();
                    break;
            }
        }

        private void OptionButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Options));
        }

        private void SessionButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SessionPage));
        }
    }
}
