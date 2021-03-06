﻿using SQLite;
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
    public sealed partial class Dashboard : Page
    {
        public static bool isAdmin = false;
        public static string userName = string.Empty;
        public Dashboard()
        {
            this.InitializeComponent();
            // MainPage mp = new MainPage();
            //if(mp!=null)
            // tblock.Text = mp.username;

        }
       
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            tblock.Text = !string.IsNullOrEmpty((App.Current as App).NavigateText) ? (App.Current as App).NavigateText : string.Empty;
            var path = ApplicationData.Current.LocalFolder.Path + "/mydb.db";
            var con = new SQLiteAsyncConnection(path);

            //con.QueryAsync<person>("select name from person where name= ") + "";

            //con.UpdateAsync("update person set password='"+npwd.Text+"' where password='"+epwd.Text+"' ");
            //person p1 = new person();
            //List<person> allpersons1 = new List<person>();
            //allpersons1 = await con.QueryAsync<person>("select name from person where name=" + "\'" + tblock.Text + "\'");
            //if (allpersons1.Count == 1)
            //{
            //    p1 = allpersons1[0];

            //}
            //await con.QueryAsync<person>("alter TABLE person add column isConfirmed integer ");
            if (isAdmin)
            {
                users.Visibility = Visibility.Visible;
                List<person> notConfirmedUsers = await con.QueryAsync<person>("select name from person where isConfirmed = 0");
                users.ItemsSource = notConfirmedUsers;
            }
            else
            {
                users.Visibility = Visibility.Collapsed;
            }

        }

        //private async void savebtn_Click(object sender, RoutedEventArgs e)
        //{
        //    var path = ApplicationData.Current.LocalFolder.Path + "/mydb.db";
        //    var con = new SQLiteAsyncConnection(path);
            //person p1 = new person();
            ////p1.password = npwd.Password;

            //List<person> allpersons1 = new List<person>();
            //allpersons1 = await con.QueryAsync<person>("select name from person");
            //foreach (person a1 in allpersons1)
            //{
            //    tblock.Text = a1.name;
            //}
            //person p1 = new person();
            //List<person> allpersons1 = new List<person>();
            //allpersons1 = await con.QueryAsync<person>("select name from person where name=" + "\'" + tblock.Text + "\'");
            //if (allpersons1.Count == 1)
            //{
            //    p1 = allpersons1[0];

            //}
            //allpersons1=await con.QueryAsync<person>("update person set password='" + npwd.Password + "' where name='" + tblock.Text + "' ");
            //if (allpersons1.Count == 1)
            //{
            //    p1 = allpersons1[0];
            //}
           // if (npwd.Password == "" || epwd.Password == "")
           // {
               // var md = new MessageDialog("password field cannot be empty").ShowAsync();
            //if(npwd.Password !="")
            //{
            //    await con.QueryAsync<person>("update person set password='" + npwd.Password + "' where name='" + tblock.Text + "' ");
            //    var m = new MessageDialog("data updated").ShowAsync();
            //}
            //    else if(cmailtxtbox.Text != "")
            //    {
            //         await con.QueryAsync<person>("update person set email='" + cmailtxtbox.Text + "' where name='" + tblock.Text + "' ");
            //    }

            //else if (cmobtxt.Text != "")
            //{
            //    await con.QueryAsync<person>("update person set phone='" + cmobtxt.Text + "' where name='" + tblock.Text + "' ");

            //}
            //var m = new MessageDialog("email updated").ShowAsync();

           // }


            //else
            //{
            //    var m = new MessageDialog("data updated").ShowAsync();
            //}
            

       // }

        private void frndsearchbtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Searchusers));
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            Button confirmButton = sender as Button;
            int row = Convert.ToInt32(confirmButton.GetValue(Grid.RowProperty));
            Grid parentControl = (Grid)confirmButton.Parent;//grdusers is assigned to parentcontrol variable 
            TextBlock txtUserName = (TextBlock)parentControl.Children[row * 2];
            var path = ApplicationData.Current.LocalFolder.Path + "/mydb.db";
            var con = new SQLiteAsyncConnection(path);
            con.QueryAsync<person>("update person set isConfirmed=1 where name = '" + txtUserName.Text + "'");
        }

        private void Logoutbtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

       
        
        private  void cemailbtn_Click(object sender, RoutedEventArgs e)
        {
            cmailtxtbox.Visibility = Visibility.Visible;
            Esave.Visibility = Visibility.Visible;
           // npwd.Visibility = Visibility.Collapsed;
            //var path = ApplicationData.Current.LocalFolder.Path + "/mydb.db";
            //var con = new SQLiteAsyncConnection(path);
           
            
        }

        private void changepwdbtn_Click(object sender, RoutedEventArgs e)
        {
            npwd.Visibility = Visibility.Visible;
            pSave.Visibility = Visibility.Visible;
            //cmailtxtbox.Visibility = Visibility.Collapsed;
        }

        private void cmobbtn_Click(object sender, RoutedEventArgs e)
        {
            cmobtxt.Visibility = Visibility.Visible;
            MSave.Visibility = Visibility.Visible;
        }

        private async void pSave_Click(object sender, RoutedEventArgs e)
        {
            var path = ApplicationData.Current.LocalFolder.Path + "/mydb.db";
            var con = new SQLiteAsyncConnection(path);
           // await con.QueryAsync<person>("update person set password='" + npwd.Password + "' where name='" + tblock.Text + "' ");
           // var m = new MessageDialog("password updated").ShowAsync();
            if (npwd.Password == "")
            {
                var m3 = new MessageDialog("Password filed cannot be empty").ShowAsync();
            }
            else
            {
                await con.QueryAsync<person>("update person set password='" + npwd.Password + "' where name='" + tblock.Text + "' ");
                var m = new MessageDialog("password updated").ShowAsync();
            }
        }

        private async void Esave_Click(object sender, RoutedEventArgs e)
        {
            var path = ApplicationData.Current.LocalFolder.Path + "/mydb.db";
            var con = new SQLiteAsyncConnection(path);
            if (cmailtxtbox.Text == "")
            {
                var m4 = new MessageDialog("email field cannot be empty").ShowAsync();
            }
            else
            {
                await con.QueryAsync<person>("update person set email='" + cmailtxtbox.Text + "' where name='" + tblock.Text + "' ");
                var m1 = new MessageDialog("email updated").ShowAsync();
            }
        }

        private async void MSave_Click(object sender, RoutedEventArgs e)
        {
            var path = ApplicationData.Current.LocalFolder.Path + "/mydb.db";
            var con = new SQLiteAsyncConnection(path);
            if (cmobtxt.Text == "")
            {
                var m5 = new MessageDialog("mobile number field cannot be empty").ShowAsync();
            }
            else
            {
                await con.QueryAsync<person>("update person set phone='" + cmobtxt.Text + "' where name='" + tblock.Text + "' ");
                var m2 = new MessageDialog("mobile number updated").ShowAsync();
            }
        }

       

      
    }
}
