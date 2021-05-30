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
using UserInterface.Models;
namespace UserInterface.PFTViews
{
    /// <summary>
    /// Interaction logic for SPOJoin.xaml
    /// </summary>
    public partial class SPOJoin : Window
    {
        public SPOJoin(List<UserInterface.Models.SPOJoin> items)
        {
            InitializeComponent();
            this.JoinDG.ItemsSource = items;
        }
    }
}
