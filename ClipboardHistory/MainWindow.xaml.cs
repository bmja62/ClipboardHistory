using System;
using System.Threading;
using System.Timers;
using System.Windows;

namespace ClipboardHistory
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitTimer();

        }

        private void InitTimer()
        {
            var timer = new System.Timers.Timer
            {
                Interval = 1000,
                Enabled = true
            };
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Thread thread = new Thread(GetClipboardData);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }

        private string lastText;
        private void GetClipboardData()
        {
            if (Clipboard.ContainsText())
            {
                var cliptext = Clipboard.GetText(TextDataFormat.Text);
                if (cliptext != lastText)
                {
                    lastText = cliptext;

                    Dispatcher.Invoke(() =>
                    {
                        TextBox1.Text += Environment.NewLine + cliptext;
                    });
                }
            }
        }
    }
}
