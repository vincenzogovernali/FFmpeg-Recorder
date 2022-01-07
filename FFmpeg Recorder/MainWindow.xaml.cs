using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;

namespace FFmpeg_Recorder
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void record(object sender, RoutedEventArgs e)
        {
            String urlText = url.Text;
            if(urlText.Length > 0 && Uri.IsWellFormedUriString(urlText, UriKind.Absolute))
            {

                int time = int.Parse(chron.Text);

                
                Process proc = new Process();
                proc.StartInfo.FileName = "ffmpeg.exe";
                proc.StartInfo.Arguments = " -i \"" + urlText +"\" output.mp4";
                proc.StartInfo.UseShellExecute = true;
                proc.Start();
                if (time > 0)
                {
                    Thread thread = new Thread(()=>chronStop(time * 60000, proc));
                    thread.Start();
                }
            }
            else
            {
                MessageBox.Show("Url non valido!", "Errore!");
            }
        }


        private void chronStop(int sleep, Process proc)
        {
            Thread.Sleep(sleep);
            proc.CloseMainWindow();
            MessageBox.Show("Stream Registrato :)");
        }

        private void insertTime(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(chron.Text, "[^0-9]"))
            {
                MessageBox.Show("Inserisci un numero!");
                chron.Text = chron.Text.Remove(chron.Text.Length - 1);
            }
        }
    }
}
