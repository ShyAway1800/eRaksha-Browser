using System.IO;
using System.Windows;
using System.Diagnostics;
using System;

namespace First_Time_Setup
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string configFolder = @"C:\ProgramData\eRaksha\";
        string configPath = @"C:\ProgramData\eRaksha\config.cfg";
        string instaconfig = @"C:\ProgramData\eRaksha\insta.cfg";
        string webconfig = @"C:\ProgramData\eRaksha\web.cfg";
        string browserPath = AppDomain.CurrentDomain.BaseDirectory + @"\eRaksha Browser.exe";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(configPath, txtUsername.Text);
            
            if(tikInstagram.IsChecked == true)
                File.Create(instaconfig);
            

            if(tikWhatsapp.IsChecked == true)
                File.Create(webconfig);

            foreach(var process in Process.GetProcessesByName("eRaksha Browser"))
                process.Kill();

            Process.Start(browserPath);

            this.Close();
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        { 
            if(Directory.Exists(configFolder))
            {
                if(!File.Exists(configPath))
                {
                    File.Create(configPath);
                }
            }
            else
            {
                Directory.CreateDirectory(configFolder);
                File.Create(configPath);
            }

            if(File.Exists(webconfig))
            {
                tikWhatsapp.IsChecked = true;
            }

            if(File.Exists(instaconfig))
            {
                tikInstagram.IsChecked = true;
            }
        }
    }
}
