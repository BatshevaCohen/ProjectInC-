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
using DalApi;
using BO;
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneListWindow.xaml
    /// </summary>
    public partial class DroneListWindow : Window
    {
        BO.IBL bL;
        public DroneListWindow(BO.IBL bl)
        {
            this.bL = bl;
            InitializeComponent();
            comboStatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            comboWeghitSelector.ItemsSource = Enum.GetValues(typeof(BO.Weight));
        }

        /// <summary>
        /// Combo box for drone status selecting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboStatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboStatusSelector.SelectedItem != null)
            {
                DroneStatuses droneStatuses = (DroneStatuses)comboStatusSelector.SelectedItem;
                this.DronesListView.ItemsSource = bL.ShowDroneList().Where(x => x.DroneStatuses == droneStatuses);
            }
        }

        /// <summary>
        /// Combo box for weight selecting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboWeghitSelector.SelectedItem != null)
            {
                Weight weight = (Weight)comboWeghitSelector.SelectedItem;
                this.DronesListView.ItemsSource = bL.ShowDroneList().Where(x => x.Weight == weight);
            }
        }

        /// <summary>
        /// Add drone button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddDrone_Click(object sender, RoutedEventArgs e)
        {
            new DroneInActionView(this, bL).Show();
        }

        /// <summary>
        ///  Close button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// double-clicking on one of the drones on the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DronesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.DroneToList? droneToList = DronesListView.SelectedItem as BO.DroneToList;
            if (droneToList != null)
            {
                new DroneInActionView(droneToList, bL, this).Show();
            }
        }

        /// <summary>
        /// Clear the status comboBox and the listView of the drones
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearStatusComboBox_Click(object sender, RoutedEventArgs e)
        {
            comboStatusSelector.Text = "";
            DronesListView.ItemsSource = null;
        }

        /// <summary>
        /// Clear the weight comboBox and the listView of the drones
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearWeightComboBox_Click(object sender, RoutedEventArgs e)
        {
            comboWeghitSelector.Text = "";
            DronesListView.ItemsSource = null;
        }
    }
}
