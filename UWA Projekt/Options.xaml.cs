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

    public sealed partial class Options : Page
    {
        private const string FILE_NAME = "./BestScore.txt";
        TextData textData = new TextData { DifficultLevelText = "Poziom trudności : Easy" };
        Windows.Storage.ApplicationDataContainer localStorage = Windows.Storage.ApplicationData.Current.LocalSettings;

        public Options()
        {
            this.InitializeComponent();
            this.DataContext = textData;
            Refresh();
            

        }

        private void DifficultLevel_Click(object sender, RoutedEventArgs e)
        {
            if(localStorage.Values["difficulty"].ToString() == "Easy")
            {
                localStorage.Values["difficulty"] = Level.Medium.ToString();
            }
            else if (localStorage.Values["difficulty"].ToString() == "Medium")
            {
                localStorage.Values["difficulty"] = Level.Hard.ToString();
            }
            else if (localStorage.Values["difficulty"].ToString() == "Hard")
            {
                localStorage.Values["difficulty"] = Level.Easy.ToString();
            }
            Refresh();
        }

        private void Refresh()
        {
            //this.DataContext = null;
            //this.DataContext = textData;
                if ((bool)localStorage.Values["readingText"] == false)
                {
                    txtBtn.Content = "CZYTANIE TEXTU: OFF";
                }
                else
                {
                    txtBtn.Content = "CZYTANIE TEXTU: ON";
                }

            if (localStorage.Values["difficulty"].ToString() == "Easy")
            {
                DifficultLevelButton.Content = "Poziom trudności : Easy";
            }
            else if (localStorage.Values["difficulty"].ToString() == "Medium")
            {
                DifficultLevelButton.Content = "Poziom trudności : Medium";
            }
            else if (localStorage.Values["difficulty"].ToString() == "Hard")
            {
                DifficultLevelButton.Content = "Poziom trudności : Hard";
            }

        }



        private void ReadingTextButton_Click(object sender, RoutedEventArgs e)
        {
            localStorage.Values["readingText"] = !(bool)localStorage.Values["readingText"];
            Refresh();
            
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
