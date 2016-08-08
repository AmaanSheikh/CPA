using MySql.Data.MySqlClient;
using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CitizenPortalApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class frmComplaintReceived : Page
    {
        public frmComplaintReceived()
        {
            this.InitializeComponent();
        }
        MySqlConnection connection;
        MySqlCommand command;
        MySqlDataReader reader;
        double longitude;
        double latitude;
        byte[] blobImage;
        string image = null;
        
        public async void location()
        {
            var geoLocator = new Geolocator();
            geoLocator.DesiredAccuracy = PositionAccuracy.High;
            var Geopositionpos = await geoLocator.GetGeopositionAsync();
           // latitude = Geopositionpos.Coordinate.Point.Position.Latitude;
            //longitude = Geopositionpos.Coordinate.Point.Position.Longitude;
            
            var myPoint = new Geopoint(new BasicGeoposition { Latitude = latitude, Longitude = longitude });
            MapIcon myPOI = new MapIcon { Location = myPoint, NormalizedAnchorPoint = new Point(0.5, 1.0), Title = "MyPosition", ZIndex = 0 };
            //Polyline to draw on Map
            // add to map and center it
            myMap.MapElements.Add(myPOI);
            myMap.Center = myPoint;
            myMap.ZoomLevel = 10;
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {

            connection = new MySqlConnection(DBHelper.ConnectionString());
            connection.Open();

            string query = "select distt,depart,c_des,longitude,latitude,c_image from complaint_db where id=0";

            command = new MySqlCommand(query, connection);

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                distt.Text = reader[0].ToString();
                depart.Text = reader[1].ToString();
                txtComplain.Text = reader[2].ToString();
                longitude = Convert.ToDouble(reader[3].ToString());
                latitude = Convert.ToDouble(reader[4].ToString());
                blobImage = (byte[])reader["c_image"];

                image = Convert.ToBase64String(blobImage);

            }



            connection.Close();
            location();
        }





    }

}
