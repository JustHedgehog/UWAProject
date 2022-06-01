using System;
using System.Collections.Generic;
using System.IO;

using System.Threading.Tasks;

using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace UWA_Projekt
{

    public sealed partial class Game : Page
    {
        GameData gameContext = new GameData { Time = 75, Score = 0, MistakeCounter = 0 };
        int delay;
        double lastScore;
        Level DifficultyLevel;
        Boolean Exit = false;
        List<string> dataList = new List<string>();
        List<string> tempList = new List<string>();
        private const string FILE_NAME_DATA = "./Dane.txt"; // to musi być z xml
        private const string FILE_NAME_BESTSCORE = "./BestScore.txt";

        public Game()
        {
            InitializeComponent();
            DifficultyLevel = Level.Easy; // tu też local storage
            lastScore = 100; //tu z local storage
            setDelay();
            ChangeProgressBar();
            ReadData();
            this.DataContext = gameContext;
        }

        private void setDelay()
        {
            switch (DifficultyLevel)
            {
                case Level.Easy:
                    {
                        delay = 300;
                        break;
                    }
                case Level.Medium:
                    {
                        delay = 200;
                        break;
                    }
                case Level.Hard:
                    {
                        delay = 50;
                        break;
                    }
            }
        }

        private void Refresh()
        {
            this.DataContext = null;
            this.DataContext = gameContext;

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            WriteData();
            Frame frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(MainPage));
            Exit = true;
        }

        private async void ChangeProgressBar()
        {
            await Task.Run(() =>
            {
                try
                {

                    while (gameContext.Time > 0 && Exit == false)
                    {
                        System.Diagnostics.Debug.WriteLine("Time  " + gameContext.Time);

                        Task.Delay(delay).Wait();
                        ProgressBar.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                        {
                            ProgressBar.Value--;
                        }).AsTask().ConfigureAwait(false);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }

                InputTextBox.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { 
                    InputTextBox.IsEnabled = false; 
                }).AsTask().ConfigureAwait(false);

                var dialog = new MessageDialog("Koniec gry");

                Dispatcher.RunAsync(CoreDispatcherPriority.Normal,async () =>
                {
                   await dialog.ShowAsync();
                }).AsTask().ConfigureAwait(false); ; 
            });
        }


        private async void InputTextBox_KeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e) // tu gdzieś trza dodać syntezator mowy
        {
            if (e.Key == Windows.System.VirtualKey.Space )
            {
                if (InputTextBox.Text.Equals(dataList[0] + " "))
                {
                    if (dataList.Count == tempList.Count / 2)
                    {
                        dataList.RemoveAt(0);
                        for (int i = 0; i < tempList.Count; i++)
                        {
                            gameContext.Text += tempList[i] + " ";

                        }
                        dataList.AddRange(tempList);
                        Refresh();
                    }
                    else
                    {
                        dataList.RemoveAt(0);
                    }

                    gameContext.Text = TextAreaBlock.Text.ToString().Substring(InputTextBox.Text.Length);
                    switch (DifficultyLevel)
                    {
                        case Level.Easy:
                            {
                                gameContext.Score += InputTextBox.Text.Length;
                                await ProgressBar.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                                    ProgressBar.Value += 2;
                                });
                                break;
                            }
                        case Level.Medium:
                            {
                                gameContext.Score += InputTextBox.Text.Length * 1.5;
                                await ProgressBar.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                                    ProgressBar.Value += 3;
                                });
                                break;
                            }
                        case Level.Hard:
                            {
                                gameContext.Score += InputTextBox.Text.Length * 2.25;
                                await ProgressBar.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                                    ProgressBar.Value += 4.5;
                                });
                                break;
                            }
                    }

                    Refresh();
                    InputTextBox.Text = "";
                }
                else
                {
                    gameContext.MistakeCounter++;
                    Refresh();
                }
            }
        }

        private async void ReadData() //z xmla trza wczytywać więc do przerobienia
        {
            try
            {
                using (StreamReader sr = new StreamReader(FILE_NAME_DATA))
                {
                    while (!sr.EndOfStream)
                    {
                        dataList.Add(sr.ReadLine());
                    }

                    tempList.AddRange(dataList);

                    for (int i = 0; i < dataList.Count; i++)
                    {
                        gameContext.Text += dataList[i] + " ";
                    }
                    Refresh();
                }
            }
            catch (Exception ex)
            {
                var dialog = new MessageDialog(ex.Message);
                await dialog.ShowAsync();
            }
        }

        private async void WriteData()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(FILE_NAME_BESTSCORE))
                {
                    if (gameContext.Score > lastScore)
                        sw.WriteLine(gameContext.Score);
                    else
                        sw.WriteLine(lastScore);
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
