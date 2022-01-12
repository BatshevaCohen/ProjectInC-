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
        /// <summary>
        /// option to add parcel detailes
        /// </summary>
        /// <param name="bl"></param>
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
            //select option of custumer id and name to reciver or sender parcel
            StatusComboBox.ItemsSource = Enum.GetValues(typeof(ParcelStatus));
            IEnumerable<CustomerInParcel> c = from customer in myBl.ShowCustomerList()
                                              let cs = new CustomerInParcel { Id = customer.Id, Name = customer.Name }
                                              select cs;
            senderComboBox.ItemsSource = c;
            ReciverComboBox.ItemsSource = c;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parcelListWindowe"></param>
        /// <param name="bl"></param>
        public ParcelInActionView(ParcelListWindowe parcelListWindowe, BlApi.IBL bl)
        {
            InitializeComponent();
            btnOK.Visibility = Visibility.Visible;           
            parcel = new BO.Parcel();
            DataContext = parcel;

        }
        /// <summary>
        /// to show the parcel datailes
        /// </summary>
        /// <param name="parTL"></param>
        /// <param name="bl"></param>
        /// <param name="parcelListWindowe"></param>
        public ParcelInActionView(ParcelToList parTL, BlApi.IBL bl, ParcelListWindowe parcelListWindowe)
        {
            InitializeComponent();

            Grid_ShowParcel.Visibility = Visibility.Visible;
            btnUpdateParcel.Visibility = Visibility.Visible;
            btnShowDrone.Visibility = Visibility.Visible;
            btnShowCustumerReciver.Visibility = Visibility.Visible;
            btnShowCustumerSender.Visibility = Visibility.Visible;
            myBl = bl;
            Parcel p = myBl.GetParcel(parTL.Id);
            if (p.DroneInParcel == null)
            {
                //וסורי אין לי כוח לעשות את זה lלהחביא את הלייבלים וטקסטבוקס של הרחפן שמשוייך לחבילה 
            }
            DataContext = p;
        }


        private void btnUpdateParcel_Click(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// open drone windowes parcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowDrone_Click(object sender, RoutedEventArgs e)
        {
            if (IdDroneParcelTXB.Text != "")
            {
                Drone drone = myBl.GetDrone(Int32.Parse(IdDroneParcelTXB.Text));
                DroneToList drTL = new()
                {
                    Id = drone.Id,
                    Battery = drone.Battery,
                    DroneStatuses = drone.DroneStatuses,
                    Weight = drone.Weight,
                    Model = drone.Model,
                    ParcelNumberTransferred = drone.ParcelInTransfer.Id,

                };
                drTL.Location = new()
                {
                    Latitude = drone.Location.Latitude,
                    Longitude = drone.Location.Longitude,

                };
                new DroneInActionView(drTL, myBl).Show();
            }
            else
            {
                MessageBox.Show("there is no drone to supply!!");
            }
        }
        /// <summary>
        /// open cusrumer reciver window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowCustumerReciver_Click(object sender, RoutedEventArgs e)
        {
            Customer c = myBl.GetCustomer(Int32.Parse(ReciverIdTXB.Text));
            CustomerToList cTL = new()
            {
                Id = c.Id,
                Name = c.Name,
                Phone = c.Phone,
            };
            new CustumerInActionView(cTL, myBl, null).Show();
        }
        /// <summary>
        /// open custumer sender window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowCustumerSender_Click(object sender, RoutedEventArgs e)
        {
            Customer c = myBl.GetCustomer(Int32.Parse(SenderIdTXB.Text));
            CustomerToList cTL = new()
            {
                Id = c.Id,
                Name = c.Name,
                Phone = c.Phone,
            };
            new CustumerInActionView(cTL, myBl, null).Show();
        }
        /// <summary>
        /// Close window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        /// <summary>
        /// add the parcel to layer bl=>dal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                myBl.AddParcel(parcel);
                MessageBox.Show("Parcel added succesfuly!");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        /// <summary>
        /// remove parcel if its not suplly yet to drone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveParcel_Click(object sender, RoutedEventArgs e)
        {
            Parcel p = myBl.GetParcel(Int32.Parse(ParcelIDTextBox.Text));
            if(p.DroneInParcel!=null)
            {
                myBl.RemoveParcel(p.Id);
                MessageBox.Show("parcel removed sucssecfully!");
            }
            else
            {
                MessageBox.Show("parcel is waiting to drone and can't be removed");
            }
        }
    }
}