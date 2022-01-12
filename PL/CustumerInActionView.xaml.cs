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
    /// Interaction logic for CustumerInActionView.xaml
    /// </summary>
    public partial class CustumerInActionView : Window
    {
        private CustomerToList custumerToList;
        Customer cusm;
        BlApi.IBL myBl;
        public CustumerInActionView(BlApi.IBL bl, CustumerListWindow custumerListWindow)
        {
            myBl = bl;
            InitializeComponent();
            AddCustumerGrid.Visibility = Visibility.Visible;
            btnAddCustumer_cencel.Visibility = Visibility.Visible;
            btnOKCustumer.Visibility = Visibility.Visible;
            DataContext = cusm;
            // CustumerListWindow.CustumerListView.Items.Refresh();

        }
        public CustumerInActionView(CustomerToList cusTL, BlApi.IBL bL, CustumerListWindow custumerListWindow)
        {
            InitializeComponent();
            UpdatCustumereGrid.Visibility = Visibility.Visible;

            btnAddCustumer_cencel.Visibility = Visibility.Visible;
            btnUpdateCustumer.Visibility = Visibility.Visible;
            myBl = bL;

            Customer cst = new()
            {
                Id = cusTL.Id,
                Name = cusTL.Name,
                Phone = cusTL.Phone,
            };
            Customer c = myBl.GetCustomer(cusTL.Id);
            cst.Location = new()
            {
                Latitude = c.Location.Latitude,
                Longitude = c.Location.Longitude
            };
            cst.ReceiveParcels = new List<ParcelCustomer>();
            if (c.ReceiveParcels != null || c.SentParcels != null)
            {
                lblS.Visibility = Visibility.Visible;
                lblR.Visibility = Visibility.Visible;
                listVReciverParcel.Visibility = Visibility.Visible;
                listVSenderParcel.Visibility = Visibility.Visible;
            }
            if (c.ReceiveParcels != null)
            {
                foreach (var item in c.ReceiveParcels)
                {
                    listVReciverParcel.Visibility = Visibility.Visible;
                    ListViewItem newItem = new ListViewItem();
                    newItem.Content = item;
                    listVReciverParcel.Items.Add(newItem.Content);
                }
            }
            else
            {
                listVReciverParcel.Visibility = Visibility.Visible;
            }
            cst.SentParcels = new List<ParcelCustomer>();
            if (c.SentParcels != null)
            {

                foreach (var item in c.SentParcels)
                {
                    listVSenderParcel.Visibility = Visibility.Visible;
                    ListViewItem newItem = new ListViewItem();
                    newItem.Content = item;
                    listVSenderParcel.Items.Add(newItem);
                }
            }
            else
            {
                listVSenderParcel.Visibility = Visibility.Visible;
            }
            DataContext = cst;


        }


        private void btnUpdateCustumer_Click(object sender, RoutedEventArgs e)
        {

            UpdatCustumereGrid.Visibility = Visibility.Visible;
            String CustomerName = (nameTextBox.Text);
            String CustomerPhone = (phoneTextBox.Text);
            Customer c= myBl.GetCustomer(Int32.Parse(idCusTextBox.Text));
            if (CustomerName == c.Name && CustomerPhone == c.Phone)
            {
                MessageBox.Show("Please update name or phone number");
            }
            else 
            {
                try
                {
                    myBl.UpdateCustomer(Int32.Parse(idCusTextBox.Text), CustomerName, CustomerPhone);
                    MessageBox.Show("your datails updated succesfully!");
                }
                catch(Exception ex){MessageBox.Show(ex.Message);}
              
            }
        }

        private void btnOKCustumer_Click(object sender, RoutedEventArgs e)
        {
            Customer cusm = new Customer()
            {
                Id = Int32.Parse(idCusTextBoxAdd.Text),
                Name = (nameTextBoxAdd.Text),
                Phone = phoneTextBoxAdd.Text,
            };
            cusm.Location = new()
            {
                Latitude = double.Parse(latitudeTextBoxAdd.Text),
                Longitude = double.Parse(longitudeTextBoxAdd.Text),
            };
            try
            {
                myBl.AddCustomer(cusm);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddCustumer_cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void ParcelsList_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            ParcelCustomer parcelCustomer;
            if (listVReciverParcel.SelectedItem != null)
                parcelCustomer = listVReciverParcel.SelectedItem as ParcelCustomer;
            else
                parcelCustomer = listVSenderParcel.SelectedItem as ParcelCustomer;
            if (parcelCustomer != null)
            {
                //  new ParcelInActionView(myBL, myBL.GetParcel(parcelCustomer.Id)).Show();
            }
        }
    }

}
