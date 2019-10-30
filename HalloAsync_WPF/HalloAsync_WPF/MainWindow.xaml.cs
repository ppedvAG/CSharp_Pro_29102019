using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HalloAsync_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartOhneThread(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                pb1.Value = i;
                Thread.Sleep(1000);
            }
        }

        private void StartTask(object sender, RoutedEventArgs e)
        {
            herbert.IsEnabled = false;

            Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    pb1.Dispatcher.Invoke(() => pb1.Value = i);
                    Thread.Sleep(10);
                }
                herbert.Dispatcher.Invoke(() => herbert.IsEnabled = !false);
            });

        }

        private void StartTaskMitTS(object sender, RoutedEventArgs e)
        {
            ilma.IsEnabled = false;
            var ts = TaskScheduler.FromCurrentSynchronizationContext();  //gui thread 'einfangen'
            cts = new CancellationTokenSource();

            var myTask = Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(100);
                    Task.Factory.StartNew(() => pb1.Value = i, cts.Token, TaskCreationOptions.None, ts); ;
                    if (cts.IsCancellationRequested)
                        //cleanup
                        break;
                }
                Task.Factory.StartNew(() => ilma.IsEnabled = true, CancellationToken.None, TaskCreationOptions.None, ts); ;
            });
            myTask.ContinueWith(t => MessageBox.Show($"Error: {t.Exception.Message}"), cts.Token, TaskContinuationOptions.OnlyOnFaulted, ts);
            myTask.ContinueWith(t => MessageBox.Show("Hurra"), cts.Token, TaskContinuationOptions.OnlyOnRanToCompletion, ts);
            myTask.ContinueWith(t => MessageBox.Show($"Erfolgreich abgebrochen"), CancellationToken.None, TaskContinuationOptions.OnlyOnCanceled, ts);
        }

        CancellationTokenSource cts;
        private void Abbrechen(object sender, RoutedEventArgs e)
        {
            cts?.Cancel();
        }

        private async void StartAsyncAwait(object sender, RoutedEventArgs e)
        {
            start.IsEnabled = false;
            for (int i = 0; i < 100; i++)
            {
                pb1.Value = i;
                await Task.Delay(100);
            }
            start.IsEnabled = !false;
        }

        private async void CountEmployees(object sender, RoutedEventArgs e)
        {
            var conString = "Server=.\\SQLEXPRESS;Database=Northwind;Trusted_Connection=true";
            using (var con = new SqlConnection(conString))
            {
                await con.OpenAsync();
                using var cmd = con.CreateCommand();
                cmd.CommandText = "WAITFOR DELAY '00:00:10';SELECT COUNT(*) FROM Employees";
                MessageBox.Show($"Mitarbeiter: { await cmd.ExecuteScalarAsync()}");
            }
            //MessageBox.Show("DB Connection hergestellt");

        }

        private async void AlteLangsameFunktion(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show($"Werte aus alter langsamer Funktion: {string.Join(",", AlteLangsameFunktion(98))}");
            MessageBox.Show($"Werte aus alter langsamer Funktion: {string.Join(",", await AlteLangsameFunktionAsync(98))}");
        }

        private Task<IEnumerable<int>> AlteLangsameFunktionAsync(long value)
        {
            return Task.Run(() => AlteLangsameFunktion(value));
        }

        private IEnumerable<int> AlteLangsameFunktion(long value)
        {
            Thread.Sleep(5000);
            return new[] { 23, 56, 66, 21, -9, 9374, (int)value * 2 };
        }
    }
}
