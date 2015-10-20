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
    public sealed partial class Searchusers : Page
    {
        public Searchusers()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var path = ApplicationData.Current.LocalFolder.Path + "/mydb.db";
            var con = new SQLiteAsyncConnection(path);
        }

        private async void enterbtn_Click(object sender, RoutedEventArgs e)
        {
            var path = ApplicationData.Current.LocalFolder.Path + "/mydb.db";
            var con = new SQLiteAsyncConnection(path);
            person loginPerson = (await con.QueryAsync<person>("select isConfirmed from person where name='" + Dashboard.userName + "'"))[0];

            if (loginPerson.isConfirmed == 1)
            {
                List<person> all = new List<person>();
                all = await con.QueryAsync<person>("select name,email,country,gender,phone from person where name='" + searchboxtxt.Text + "' ");
                String res = "";
                foreach (person a in all)
                {
                    res += "\n\n" + a.name + "\n\n" + a.email + "\n\n" + a.country + "\n\n" + a.gender + "\n\n" + a.phone;
                }
                restxtblock.Text = res;
            }
            else
            {
                var md1 = new MessageDialog("No permission to search for users").ShowAsync();
        
            }

            
        }


        private void gobackbth_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Dashboard));
        }
    }
}
