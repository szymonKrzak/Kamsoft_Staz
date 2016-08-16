using Common.SimpleDataBase;
using Common.SQLDataBase;
using MBOne.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MBOne.Views
{
    /// <summary>
    /// Interaction logic for ViewMBOne.xaml
    /// </summary>
    public partial class ViewMBOne : UserControl
    {
        public ViewMBOne(MBOneViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;       
        }
    }
}
