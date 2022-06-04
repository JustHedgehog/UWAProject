using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWA_Projekt
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class SessionPage : Page
    {
        String[] splitted,temp;
        List<String> splitted2 = new List<string>();
        List<SessionData> sessionDatas = new List<SessionData>();
        Windows.Storage.ApplicationDataContainer localStorage = Windows.Storage.ApplicationData.Current.LocalSettings;
        public SessionPage()
        {
            this.InitializeComponent();
            if (localStorage.Values["Recording"] != null)
            {
                splitted = localStorage.Values["Recording"].ToString().Split('-');
                for (int i = 0; i < splitted.Length; i++)
                {
                    temp = splitted[i].ToString().Split(';').Select(s => s).Where(s => !s.Equals("")).ToArray();
                    foreach (String s in temp)
                        splitted2.Add(s);

                }
                for (int i = 0; i < splitted2.Count(); i += 3)
                {
                    sessionDatas.Add(new SessionData(splitted2[i], splitted2[i + 1], splitted2[i + 2]));
                }

                foreach (var e in splitted2)
                    System.Diagnostics.Debug.WriteLine(e);

                sessionList.ItemsSource = sessionDatas;
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
