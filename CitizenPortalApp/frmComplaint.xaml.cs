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
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Media.Imaging;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Media.Editing;
using Windows.Media.Core;
using MySql.Data.MySqlClient;
using Windows.UI.Popups;
using System.Text;
using System.IO.Compression;
using Windows.Storage.Pickers;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CitizenPortalApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class frmComplaint : Page
    {
        byte[] byteArrayimage = null;
        byte[] byteArrayvideo = null;
        string image = null;
        byte[] video = null;
        byte[] abc = null;
        byte[] abcd;
        SoftwareBitmapSource bitmapSource = null;
       
        public frmComplaint()
        {
            this.InitializeComponent();
        }
        MediaComposition mediaComposition;
        MediaStreamSource mediaStreamSource;
        CameraCaptureUI captureUI;
        

        private void Hamburger_Click(object sender, RoutedEventArgs e)
        {
            MySpllitView.IsPaneOpen = !MySpllitView.IsPaneOpen;
        }

        private void ListBoxItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void ListBoxItem_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(frmComplaint));
        }

        public async void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            await FileService.OpenImageFile();
          
           
            // pic();

        }
        public async void pic()
        {
            //create camera instance with camera capture ui 
            captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);
            StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);
            if (photo == null)
            {
                // User cancelled photo capture
                return;
            }
           
            //return the captured results to fram via bitmap

            IRandomAccessStream stream = await photo.OpenAsync(FileAccessMode.Read);
            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
            SoftwareBitmap softwareBitmap = await decoder.GetSoftwareBitmapAsync();
            SoftwareBitmap softwareBitmapBGR8 = SoftwareBitmap.Convert(softwareBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
            bitmapSource = new SoftwareBitmapSource();
            await bitmapSource.SetBitmapAsync(softwareBitmapBGR8);
            
            //done with image capture.

            imageControl.Source = bitmapSource;
            
            //Convert.ToBase64String(bitmapSource);

            
        }
       

        
        public async void video1()
        {
            //create camera instance with camera capture ui 
            captureUI = new CameraCaptureUI();
            captureUI.VideoSettings.Format = CameraCaptureUIVideoFormat.Mp4;
            captureUI.VideoSettings.MaxDurationInSeconds = 60;
            StorageFile video = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Video);
            if (video == null)
            {
                return;
            }

            mediaComposition = new MediaComposition();
            MediaClip mediaClip = await MediaClip.CreateFromFileAsync(video);
            mediaComposition.Clips.Add(mediaClip);
            mediaStreamSource = mediaComposition.GeneratePreviewMediaStreamSource((int)mediaElement.ActualWidth, (int)mediaElement.ActualHeight);
            mediaElement.SetMediaStreamSource(mediaStreamSource);
            using (var inputStream = await video.OpenSequentialReadAsync())
            {
                var readStream = inputStream.AsStreamForRead();

                byteArrayvideo = new byte[readStream.Length];
                await readStream.ReadAsync(byteArrayvideo, 0, byteArrayvideo.Length);
                // video = byteArrayvideo;


            }

        }


        private void ListBoxItem_Tapped_2(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        //    string query = "insert into complaint_db(id,depart,distt,c_des,c_image,longitude,latitude) values(" + lblID.Text + ",'" + cbxDepart.SelectionBoxItem + "','" + cbDistt.SelectionBoxItem + "','" + txtDes.Text + "'," + bitmapSource.ToString() + ",'" + txtLongitude.Text + "','" + txtLatitude.Text + "')";

        //    string query =
        //   MySqlConnection MyConn2 = new MySqlConnection(DBHelper.ConnectionString());
        //    MyConn2.Open();

        //    This is command class which will handle the query and connection object. 

        //    MySqlCommand MyCommand2 = new MySqlCommand(query, MyConn2);
        //MyCommand2.ExecuteNonQuery();
        //    MyConn2.Close();





        }



        private async void gps_Click(object sender, RoutedEventArgs e)
        {
            var geoLocator = new Geolocator();
            geoLocator.DesiredAccuracy = PositionAccuracy.High;
            var Geopositionpos = await geoLocator.GetGeopositionAsync();
            var latitude = Geopositionpos.Coordinate.Point.Position.Latitude;
            var longitude = Geopositionpos.Coordinate.Point.Position.Longitude;
            txtLatitude.Text = latitude.ToString();
            txtLongitude.Text = longitude.ToString();
            var myPoint = new Geopoint(new BasicGeoposition { Latitude = latitude, Longitude = longitude });
            MapIcon myPOI = new MapIcon { Location = myPoint, NormalizedAnchorPoint = new Point(0.5, 1.0), Title= "MyPosition" , ZIndex = 0 };
            //Polyline to draw on Map
            // add to map and center it
            myMap.MapElements.Add(myPOI);
            myMap.Center = myPoint;
            myMap.ZoomLevel = 10;
           
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void button1_Copy_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(frmComplaintReceived));
        }
    }
}
