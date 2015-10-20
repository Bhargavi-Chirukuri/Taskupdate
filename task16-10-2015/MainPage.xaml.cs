using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace task16_10_2015
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // public  string username;

        public MainPage()
        {
            this.InitializeComponent();


        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var path = ApplicationData.Current.LocalFolder.Path + "/mydb.db";
            var con = new SQLiteAsyncConnection(path);
        }



        private async void loginbtn_Click(object sender, RoutedEventArgs e)
        {
            if (unametxt.Text == "trilok" && pwdtxt.Password == "trilok")
            { 
                (App.Current as App).NavigateText = unametxt.Text;
                Dashboard.isAdmin = true;
                this.Frame.Navigate(typeof(Dashboard));
                Dashboard.userName = "trilok";
            }
            else
            {
                var path = ApplicationData.Current.LocalFolder.Path + "/mydb.db";
                var con = new SQLiteAsyncConnection(path);
                person p1 = new person();
                Dashboard.isAdmin = false;
                List<person> allpersons = new List<person>();
                allpersons = await con.QueryAsync<person>("select name,password from person where name=" + "\'" + unametxt.Text + "\'" + "and password=" + "\'" + pwdtxt.Password + "\'");

                if (allpersons.Count == 1)
                {
                    (App.Current as App).NavigateText = unametxt.Text;
                    p1 = allpersons[0];
                    //username = unametxt.Text;
                    Frame.Navigate(typeof(Dashboard));
                    Dashboard.userName = p1.name;
                }


                else
                {
                    var m = new MessageDialog("invalid login").ShowAsync();
                }

            }

        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Registrationpage));
        }


    }

}
