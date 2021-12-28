using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BO;

namespace PL
{
  
    /// <summary>
    /// Interaction logic for StationInActionView.xaml
    /// </summary>
    public partial class StationInActionView : Window
    {
        BO.Station station;
        private StationToList? StationToList;
        BO.IBL mybl;
        public StationInActionView()
        {
            InitializeComponent();
        }
        public StationInActionView(StationToList stationToL, BO.IBL bL, StationListWindow stationListWindow)
        {
           
            //List<Station> s = new List<Station>();
            //Station st = new()
            //{
            //    Id = 33333,
            //    AvailableChargingSpots = 6,

            //};
            //s.Add(st);
            //for (int i = 0; i < 10; ++i)
            //{
            //    List<int> ts = new List<int>();
            //    ListBoxItem newItem = new ListBoxItem();
            //    newItem.Content = "Item " + i;
            //     ts.Add(0);
            //    //lb.Items.Add(newItem);
            //     lb.ItemsSource = ts;
            //}
            //ListView garbageLV = new ListView();
            //garbageLV.ItemsSource = s;

            //// Add the ListView to a parent container in the visual tree (that you created in the corresponding XAML file)
            //sPanel.Children.Add(garbageLV);


            InitializeComponent();
            mybl = bL;
            station = new Station();
            this.StationToList = stationToL;
          
            //List<string> st = new List<string>();
          
            Location location = mybl.GetStation(stationToL.Id).Location;
            station.Location = new()
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };
            List<DroneInCharging> droneInCharging = mybl.GetStation(stationToL.Id).droneInChargings;
            if (station.droneInChargings.Count() > 0)
            {
                foreach (DroneInCharging item in droneInCharging)
            {
                station.droneInChargings.Add(item);
            }
                
                //Binding binding = new Binding();
                //binding.Source = this;
                //PropertyPath path = new PropertyPath('Station');
                //binding.Path = path;
                //BindingOperations.SetBinding(station.Id, lb.ItemsSource, binding);



                //// Create a new ListView (or GridView) for the UI, add content by setting ItemsSource
                //ListView DroneInChargingLV = new ListView();
                //DroneInChargingLV.ItemsSource = droneInCharging;

                //// Add the ListView to a parent container in the visual tree (that you created in the corresponding XAML file)
                //droneInChargingPanel.Children.Add(DroneInChargingLV);

            }
            else
            {

                MessageBox.Show("bla bla");
            }
            DataContext = station;

        }
    }
}
