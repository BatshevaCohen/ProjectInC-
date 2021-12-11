using IBL.BO;
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
    /// Interaction logic for DroneInActionView.xaml
    /// </summary>
    public partial class DroneInActionView : Window
    {
        private DroneToList? droneToList;

        public DroneInActionView()
        {
            InitializeComponent();
        }

        public DroneInActionView(DroneToList droneToList)
        {
            this.droneToList = droneToList;
            InitializeComponent();
        }

    }
}

