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

namespace Register_Diagnose
{
    /// <summary>
    /// Interaction logic for RD_UI.xaml
    /// </summary>
    public partial class RD_UI : Window
    {
        public SieveViewModel dataModel { get; set; }

        public RD_UI()
        {
            InitializeComponent();
            dataModel = new SieveViewModel();
            this.DataContext = dataModel;
            lstNumbers.ItemsSource = dataModel.displayList.Items;
        }
    }
}
