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

namespace CitizenPortalApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class frmLogin : Page
    {
        public frmLogin()
        {
            this.InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if(txtName.Text=="amaan" && txtpass.Password=="123" )
            {
                Frame.Navigate(typeof(MainPage));
            }
            else
            {
                textBlock2.Text = "Invalid username or password";
            }
        }

        private void textBlock2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(frmSignUp));
        }
    }
}
