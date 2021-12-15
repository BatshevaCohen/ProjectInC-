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
        IBL.BO.IBL bL;
        public DroneListWindow(IBL.BO.IBL bl)
        {
            this.bL = bl;
            InitializeComponent();
            comboStatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            comboWeghitSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.Weight)) ;
        }

        private void comboStatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DroneStatuses droneStatuses = (DroneStatuses)comboStatusSelector.SelectedItem;
            this.DronesListView.ItemsSource = bL.ShowDroneList().Where(x => x.DroneStatuses == droneStatuses);
        }

        private void comboWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Weight weight = (Weight)comboWeghitSelector.SelectedItem;
            this.DronesListView.ItemsSource = bL.ShowDroneList().Where(x => x.Weight == weight);
        }
        
        private void btnAddDrone_Click(object sender, RoutedEventArgs e)
        {
            DroneWindow wnd = new DroneWindow();
            bool? result = wnd.ShowDialog();
            if (result != null)
            {
                MessageBox.Show(wnd.Drone.ToString());
            }
        }
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DronesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            IBL.BO.DroneToList? droneToList = DronesListView.SelectedItem as IBL.BO.DroneToList;
            if (droneToList != null)
            {

                DroneInActionView wnd = new DroneInActionView(droneToList,bL);

                bool? result = wnd.ShowDialog();
                
            }
        }

        /// <summary>
        /// clear status comboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearStatusComboBox_Click(object sender, RoutedEventArgs e)
        {
            comboStatusSelector.Text = "";
        }

        /// <summary>
        /// clear weight comboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearWeightComboBox_Click(object sender, RoutedEventArgs e)
        {

        }



        //private void DronesListView_SourceUpdated(object sender, DataTransferEventArgs e)
        //{

        //}

        //private void ClearFilledComboBox_Click(object sender, RoutedEventArgs e)
        //{
        //    comboStatusSelector.Items.Clear();
        //}
    }
}
