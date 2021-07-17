using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CefSharp.Wpf;
using HtmlAgilityPack;
using System.Diagnostics;
using System.Collections.Generic;
using System;

namespace eRaksha_Browser
{
    public partial class MainWindow : Window
    {
        public string browserPath;
        List<string> WebPages;
        int Current = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void browser_Loaded(object sender, RoutedEventArgs e)
        {
            WebPages = new List<string>();
            WebPages.Add("https://www.duckduckgo.com");

            browserPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            MenuItem newtab = new MenuItem();
            newtab.Click += newtabClicked;
            newtab.Header = "New Tab";
            newtab.Width = 184;

            MenuItem newwin = new MenuItem();
            newwin.Click += newwinClicked;
            newwin.Header = "New Window";
            newwin.Width = 184;

            MenuItem closetab = new MenuItem();
            closetab.Click += Closetab_Click;
            closetab.Header = "Close Tab";
            closetab.Width = 184;

            Menu.Items.Add(newtab);
            Menu.Items.Add(newwin);
            Menu.Items.Add(closetab);

            var webGet = new HtmlWeb();
            var document = webGet.Load(viewer.Address);
            var title = document.DocumentNode.SelectSingleNode("html/head/title").InnerText;
            tab1.Header = title;

            txtUri.Text = viewer.Address;
        }

        public void newwinClicked(object sender, RoutedEventArgs e)
        {
            Process.Start(browserPath);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            search();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            Menu.PlacementTarget = btnHome;
            Menu.Placement = System.Windows.Controls.Primitives.PlacementMode.Right;
            Menu.HorizontalOffset = 0;
            Menu.IsOpen = true;
        }

        public void newtabClicked(object sender, RoutedEventArgs e)
        {
            var webGet = new HtmlWeb();
            var document = webGet.Load(viewer.Address);
            string tabName = document.DocumentNode.SelectSingleNode("html/head/title").InnerText;

            TabItem tabitem = new TabItem();
            tabSystem.Items.Add(tabitem);
            tabitem.Header = tabName;
            ChromiumWebBrowser viewer1 = new ChromiumWebBrowser();
            tabitem.Content = viewer1;
            viewer1.Address = "https://duckduckgo.com";
            var webGet1 = new HtmlWeb();
            var document1 = webGet.Load(viewer1.Address);
            var title = document1.DocumentNode.SelectSingleNode("html/head/title").InnerText;
            tabitem.Header = title;
        }

        private void Closetab_Click(object sender, RoutedEventArgs e)
        {
            tabSystem.Items.Remove(tabSystem.SelectedItem);
        }

        private void btnHome_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void txtUri_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                search();
            }
        }

        void search(bool addToList = true)
        {
            if (tabSystem.SelectedItem == null)
            {
                CreateNewTab();
                
                string strUrl = "https://www.google.com/search?q=" + txtUri.Text;
                viewer.Address = strUrl;
                var webGet = new HtmlWeb();
                var document = webGet.Load(strUrl);
                var title = document.DocumentNode.SelectSingleNode("html/head/title").InnerText;
                tab1.Header = title;

                MenuItem hisotry = new MenuItem();
                hisotry.Click += Hisotry_Click;
                hisotry.Header = strUrl;
                hisotry.Width = 184;

                History.Items.Add(hisotry);

                if (addToList)
                {
                    Current++;
                    WebPages.Add(strUrl);
                }
            }
            else
            {
                string strUrl = "https://www.google.com/search?q=" + txtUri.Text;
                viewer.Address = strUrl;
                var webGet = new HtmlWeb();
                var document = webGet.Load(strUrl);
                var title = document.DocumentNode.SelectSingleNode("html/head/title").InnerText;
                tab1.Header = title;

                MenuItem hisotry = new MenuItem();
                hisotry.Click += Hisotry_Click;
                hisotry.Header = strUrl;
                hisotry.Width = 184;

                History.Items.Add(hisotry);

                if (addToList)
                {
                    Current++;
                    WebPages.Add(strUrl);
                }
            }
        }

        private void Hisotry_Click(object sender, RoutedEventArgs e)
        {
            MenuItem history = (MenuItem)sender;
            LoadWebPages(history.Header.ToString());
        }

        private void viewer_AddressChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var webGet = new HtmlWeb();
            var document = webGet.Load(viewer.Address);
            var title = document.DocumentNode.SelectSingleNode("html/head/title").InnerText;
            tab1.Header = title;

            txtUri.Text = viewer.Address;
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            if (WebPages.Count != 0)
            {
                History.PlacementTarget = btnHistory;
                History.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
                History.HorizontalOffset = -155;
                History.IsOpen = true;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if((WebPages.Count + Current - 1) >= WebPages.Count)
            {
                Current--;
                LoadWebPages(WebPages[Current], false);
            }
        }

        private void btnForward_Click(object sender, RoutedEventArgs e)
        {
            if((WebPages.Count- Current - 1) != 0)
            {
                Current++;
                LoadWebPages(WebPages[Current], false);
            }
        }

        void LoadWebPages(string Link, bool addToList = true)
        {
            txtUri.Text = Link;
            viewer.Address = Link;

            MenuItem hisotry = new MenuItem();
            hisotry.Click += Hisotry_Click;
            hisotry.Header = Link;
            hisotry.Width = 184;

            History.Items.Add(hisotry);

            if(addToList)
            {
                Current++;
                WebPages.Add(Link);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadWebPages(WebPages[Current]);
        }

        private void txtUri_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            txtUri.SelectAll();
        }

        void CreateNewTab()
        {
            var webGet = new HtmlWeb();
            var document = webGet.Load(viewer.Address);
            string tabName = document.DocumentNode.SelectSingleNode("html/head/title").InnerText;

            TabItem tabitem = new TabItem();
            tabSystem.Items.Add(tabitem);
            tabitem.Header = tabName;
            ChromiumWebBrowser viewer1 = new ChromiumWebBrowser();
            tabitem.Content = viewer1;
            viewer1.Address = "https://duckduckgo.com";
            var webGet1 = new HtmlWeb();
            var document1 = webGet.Load(viewer1.Address);
            var title = document1.DocumentNode.SelectSingleNode("html/head/title").InnerText;
            tabitem.Header = title;
        }

        private void btnWelcome_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void btnWelcome_Click(object sender, RoutedEventArgs e)
        {
            if (WelcomeMenu.Visibility == Visibility.Hidden)
            {
                WelcomeMenu.Visibility = Visibility.Visible;
            }
            else
            {
                WelcomeMenu.Visibility = Visibility.Hidden;
            }
        }

        private void WelcomeMenu_MouseLeave(object sender, MouseEventArgs e)
        {
            WelcomeMenu.Visibility = Visibility.Hidden;
        }

        private void WelcomeMenu_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            lblDate.Content = currentTime;
        }
    }
}