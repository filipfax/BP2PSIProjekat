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

namespace UserInterface.EntityCreateUpdaters
{
    /// <summary>
    /// Interaction logic for ProizvodnjaCU.xaml
    /// </summary>
    public partial class ProizvodnjaCU : Window
    {
        private bool isUpdate = false;
        private MainWindow mw = null;
        public ProizvodnjaCU(MainWindow mw, PROIZVODNJA pr = null)
        {
            this.mw = mw;
            InitializeComponent();
            this.ProizCB.ItemsSource = mw.GetProizvodjacIDs();
            this.TelCB.ItemsSource = mw.GetTelDeoIDs();


            if (pr != null)
            {
                PrepareEdit(pr);
            }

        }

        private void PrepareEdit(PROIZVODNJA s)
        {
           



            this.ProizCB.SelectedItem = s.PROIZVODJACID_PROIZV;
            this.ProizCB.IsEnabled = false;

            this.TelCB.SelectedItem = s.TELEFONSKI_DEOID_DEO;
            this.TelCB.IsEnabled = false;

            

            this.CreateBtn.Content = "Update";
            this.isUpdate = true;


        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                PROIZVODNJA s = new PROIZVODNJA();

                if (ProizCB.SelectedItem != null)
                    s.PROIZVODJACID_PROIZV = int.Parse((string)ProizCB.SelectedItem.ToString());
                if (TelCB.SelectedItem != null)
                    s.TELEFONSKI_DEOID_DEO = int.Parse((TelCB.SelectedItem.ToString()));
                

                if (!isUpdate)
                {
                    mw.CreateProizvodnja(s);
                }
                else
                {
                    mw.UpdateProizvodnja(s);
                }
                this.Close();


            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool ValidateInput()
        {

            
            ProizGRD.Content = "";
            TelGRD.Content = "";



            bool isValid = true;

            

            if (ProizCB.SelectedItem == null)
            {
                isValid = false;
                ProizGRD.Content = "Morate izabrati vrednost!";
            }

           
            if (TelCB.SelectedItem == null)
            {
                isValid = false;
                TelGRD.Content = "Morate izabrati vrednost!";
            }



            return isValid;
        }

    }


}
