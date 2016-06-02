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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SportzMagazine.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            MainFrame.Navigate(typeof(Home));
        }
        private void AppBarHomeBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(Home));
        }

        private void AppBarBackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
            }
        }

        private void AppBarForwardBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoForward)
            {
                MainFrame.GoForward();
            }
        }

        private void AppBarSubscribeBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(SubscriptionTypeSelectorView));
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(SubscriptionClerkOptionsView));
        }
    }
}
