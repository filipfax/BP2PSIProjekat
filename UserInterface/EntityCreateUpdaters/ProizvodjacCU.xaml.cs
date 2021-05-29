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
    /// Interaction logic for ProizvodjacCU.xaml
    /// </summary>
    public partial class ProizvodjacCU : Window
    {
        private bool isUpdate = false;
        private MainWindow mw = null;
        public ProizvodjacCU(MainWindow mw, PROIZVODJAC pr = null)
        {

            InitializeComponent();
            this.mw = mw;


            if (pr != null)
            {
                PrepareEdit(pr);
            }
        }

        private void PrepareEdit(PROIZVODJAC pr)
        {
            this.IDTB.Text = pr.ID_PROIZV.ToString();
            this.IDTB.IsEnabled = false;

            this.NazTB.Text = pr.NAZ;


            this.TelBRTB.Text = pr.TELBROJ.ToString();


            this.CreateBtn.Content = "Update";
            this.isUpdate = true;


        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                PROIZVODJAC s = new PROIZVODJAC
                {
                    ID_PROIZV = int.Parse(IDTB.Text),
                    NAZ = NazTB.Text,
                    TELBROJ = int.Parse(TelBRTB.Text)

                };




                if (!isUpdate)
                {
                    mw.CreateProizvodjac(s);
                }
                else
                {
                    mw.UpdateProizvodjac(s);
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


            NazGRD.Content = "";
            TelBrGRD.Content = "";




            bool isValid = true;




            if (NazTB.Text.Equals(string.Empty))
            {
                isValid = false;
                NazGRD.Content = "Polje ne sme biti prazno!";
            }

            if (TelBRTB.Text.Equals(string.Empty) || !int.TryParse(TelBRTB.Text, out int num3))
            {
                isValid = false;
                TelBrGRD.Content = "Mora biti validan broj!";
            }

            return isValid;

        }

    }
}
