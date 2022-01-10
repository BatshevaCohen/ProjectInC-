using BO;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelInActionView.xaml
    /// </summary>
    public partial class ParcelInActionView : Window
    {
        BlApi.IBL myBl;
        Parcel parcel;
        public ParcelInActionView(BlApi.IBL bl)
        {
            InitializeComponent();
            ADDParcelGrid.Visibility = Visibility.Visible;
            btnOK.Visibility = Visibility.Visible;
            myBl = bl;
            parcel = new();
            DataContext = parcel;
            WeightComboBox.ItemsSource = Enum.GetValues(typeof(Weight));
            PriorityComboBox.ItemsSource = Enum.GetValues(typeof(Priority));
            StatusComboBox.ItemsSource = Enum.GetValues(typeof(ParcelStatus));
            IEnumerable<CustomerInParcel> c = from customer in myBl.ShowCustomerList()
                                              let cs = new CustomerInParcel { Id = customer.Id, Name = customer.Name }
                                              select cs;
            senderComboBox.ItemsSource = c;
            ReciverComboBox.ItemsSource = c;
        }
        public ParcelInActionView(ParcelListWindowe parcelListWindowe, BlApi.IBL bl)
        {
            InitializeComponent();
            btnOK.Visibility = Visibility.Visible;

            btnOK.Visibility = Visibility.Visible;
            parcel = new BO.Parcel();
            DataContext = parcel;

        }
        public ParcelInActionView(ParcelToList parTL, BlApi.IBL bl, ParcelListWindowe parcelListWindowe)
        {
            InitializeComponent();
            Grid_ShowParcel.Visibility = Visibility.Visible;
            btnUpdateParcel.Visibility = Visibility.Visible;
            btnShowDrone.Visibility = Visibility.Visible;
            btnShowCustumer.Visibility = Visibility.Visible;
            myBl = bl;
            Parcel p = myBl.GetParcel(parTL.Id);
            DataContext = p;
        }


        private void btnUpdateParcel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnShowDrone_Click(object sender, RoutedEventArgs e)
        {
            if(IdDroneParcelTXB.Text != "")
            {
                Drone drone = myBl.GetDrone(Int32.Parse(IdDroneParcelTXB.Text));
                DroneToList drTL = new()
                {
                    Id=drone.Id,
                    Battery=drone.Battery,
                    DroneStatuses=drone.DroneStatuses,
                    Weight=drone.Weight,
                    Model=drone.Model,
                    ParcelNumberTransferred=drone.ParcelInTransfer.Id,
                    
                };
                drTL.Location = new()
                {
                    Latitude=drone.Location.Latitude,
                    Longitude=drone.Location.Longitude,

                };
               new DroneInActionView(drTL, myBl).Show();
            }
            else
            {
                MessageBox.Show("there are not drone suply!!");
            }
        }

        private void btnShowCustumer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                myBl.AddParcel(parcel);
                MessageBox.Show("Parcel added succesfuly!");
                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
    }
}