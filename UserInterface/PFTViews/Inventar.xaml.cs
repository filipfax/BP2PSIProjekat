using BP2Projekat;
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
    /// Interaction logic for Inventar.xaml
    /// </summary>
    public partial class Inventar : Window
    {
        public Inventar(List<Models.Inventar> lista )
        {
            InitializeComponent();
            this.ServisniAlatDG.ItemsSource = lista;
        }
    }
}
