using BlApi;
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
    /// Interaction logic for CustumerListWindow.xaml
    /// </summary>
    public partial class CustumerListWindow : Window
    {
        BlApi.IBL myBL;
        public CustumerListWindow(BlApi.IBL bl)
        {
            myBL = bl;
            InitializeComponent();
            this.CustumerListView.ItemsSource = myBL.ShowCustomerList();
        }


        private void CustomerInActionView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CustomerToList? cusTL = CustumerListView.SelectedItem as CustomerToList;
            if (cusTL != null)
            {
                new CustumerInActionView(cusTL, myBL, this).Show();
            }
        }

        private void btnAddCustumer_Click(object sender, RoutedEventArgs e)
        {
            new CustumerInActionView(myBL,this).Show();
        }

        private void btnAddCustumer_cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        //ParcelsList_MouseDoubleClick
      
    }
}