using MySql.Data.MySqlClient;
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
using MySql.Data;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CitizenPortalApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class frmPoolAdmin : Page
    {
        MySqlConnection connection;
        MySqlCommand command;
        MySqlDataReader reader;
        public frmPoolAdmin()
        {
            this.InitializeComponent();
        }


        private async void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string query = "insert into pool_db(id, p_des, one, two, three, four) values(" + txtNum.Text + ", '" + txtPoolText.Text + "', '" + txtOne.Text + "', '" + txtTwo.Text + "', '" + txtThree.Text + "', '" + txtFour.Text + "')";


                connection = new MySqlConnection(DBHelper.ConnectionString());
                connection.Open();

                // This is command class which will handle the query and connection object. 

                command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
                connection.Close();
                var dialog = new MessageDialog("Pool has been Submited & has displayed to Audience");
                await dialog.ShowAsync();
                nextCode();
            }
            catch(Exception ex)
            {
                var dialog = new MessageDialog(ex.Message);
                await dialog.ShowAsync();
            }
        }

        private void Grid_Loading(FrameworkElement sender, object args)
        {
            nextCode();

        }
        public void nextCode()
        {
            string query = "select max(id)+1 as nextCode from pool_db";
            connection = new MySqlConnection(DBHelper.ConnectionString());
            connection.Open();
            command = new MySqlCommand(query, connection);
            command.CommandType = CommandType.Text;
            object obj = command.ExecuteScalar();
            txtNum.Text = obj.ToString();

        }
    }
}
