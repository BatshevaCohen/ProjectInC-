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
using IBL;
using IBL.BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneListWindow.xaml
    /// </summary>
    public partial class DroneListWindow : Window
    {
        List<IBL.BO.DroneToList> fakeList = new List<DroneToList>()
        {
            new DroneToList()
            {
                 Id = 3000,
                 Model = "BBTT67H",
                 Battery = 0.50,
                 DroneStatuses = DroneStatuses.Available,
                 ParcelNumberTransferred = 0,
                 Location = new Location{ Latitude=32,  Longitude=31},
                 Weight = Weight.Heavy
            },
            new DroneToList()
            {
                 Id = 45000,
                 Model = "ExDrone2",
                 Battery = 0.50,
                 DroneStatuses = DroneStatuses.Maintenance,
                 ParcelNumberTransferred = 0,
                 Location = new Location{ Latitude=32,  Longitude=31},
                 Weight = Weight.Light
            },
            new DroneToList()
            {
                 Id = 4000,
                 Model = "ExDrone3",
                 Battery = 0.50,
                 DroneStatuses = DroneStatuses.Available,
                 ParcelNumberTransferred = 0,
                 Location = new Location{ Latitude=32,  Longitude=31},
                 Weight = Weight.Heavy
            },
            new DroneToList()
            {
                 Id = 5000,
                 Model = "ExDrone4",
                 Battery = 0.50,
                 DroneStatuses = DroneStatuses.Available,
                 ParcelNumberTransferred = 0,
                 Location = new Location{ Latitude=32,  Longitude=31},
                 Weight = Weight.Heavy
            },
            new DroneToList()
            {
                 Id = 5500,
                 Model = "ExDrone5",
                 Battery = 0.50,
                 DroneStatuses = DroneStatuses.Available,
                 ParcelNumberTransferred = 0,
                 Location = new Location{ Latitude=32,  Longitude=31},
                 Weight = Weight.Heavy
            },
        };

        IBL.BO.IBL bL;
        public DroneListWindow(IBL.BO.IBL bl)
        {
            this.bL = bl;
            InitializeComponent();
            comboStatusSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.DroneStatuses));
        }

        private void comboStatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DroneStatuses droneStatuses = (DroneStatuses)comboStatusSelector.SelectedItem;
            this.DronesListView.ItemsSource = fakeList.Where(x => x.DroneStatuses == droneStatuses);
        }

        private void comboWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Weight weight = (Weight)comboWeghitSelector.SelectedItem;
            this.DronesListView.ItemsSource= fakeList.Where(x => x.Weight == weight);
        }
    }
}
