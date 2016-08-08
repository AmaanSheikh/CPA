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
using MySql.Data.MySqlClient;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CitizenPortalApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class frmPool : Page
    {
        MySqlConnection connection;
        MySqlCommand command;
        MySqlDataReader reader;
       
        public frmPool()
        {
            this.InitializeComponent();
        }
        
        
        private void Grid_Loading(FrameworkElement sender, object args)
        {
            string query = "SELECT * FROM pool_db ORDER BY id DESC LIMIT 1; ";
            connection =new MySqlConnection(DBHelper.ConnectionString());
            connection.Open();
            command = new  MySqlCommand(query,connection);
            command.Connection = connection;
           

            //

            try
            {



                reader = command.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        txtPoolText.Text= (reader["p_des"].ToString());
                        rbOne.Content = (reader["one"].ToString());
                      rbTwo.Content = (reader["two"].ToString());
                        rbThree.Content = (reader["three"].ToString());
                        rbFour.Content = (reader["four"].ToString());

                    }
                }
            }
            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message);


            }
            finally
            {
                connection.Close();
            }

        }
    }
}
