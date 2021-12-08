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
            comboStatusSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.DroneStatuses));
        }

        private void comboStatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DroneStatuses droneStatuses = (DroneStatuses)comboStatusSelector.SelectedItem;
            this.DronesListView.ItemsSource = fakeList.Where(x => x.Status == droneStatuses);
        }
    }
}
