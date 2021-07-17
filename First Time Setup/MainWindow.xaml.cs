using System.IO;
using System.Windows;
using System.Diagnostics;

namespace First_Time_Setup
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string configFolder = @"C:\ProgramData\eRaksha\";
        string configPath = @"C:\ProgramData\eRaksha\config.cfg";
        string browserPath = @"C:\Users\Desktop 1\Desktop\eRaksha Project\eRaksha Browser\bin\Debug\eRaksha Browser.exe";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(configPath, txtUsername.Text);
            
            foreach(var process in Process.GetProcessesByName("eRaksha Browser"))
            {
                process.Kill();
            }

            Process.Start(browserPath);

            this.Close();
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            if(Directory.Exists(configFolder))
            {
                File.Create(configPath);
            }
            else
            {
                if (!File.Exists(configPath))
                {
                    File.Create(configPath);
                }
            }
        }
    }
}
