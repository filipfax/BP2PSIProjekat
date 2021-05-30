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
    /// Interaction logic for NabavkaCU.xaml
    /// </summary>
    public partial class NabavkaCU : Window
    {
        private bool isUpdate = false;
        private MainWindow mw = null;
        public NabavkaCU(MainWindow mw, NABAVKA nab = null)
        {
            InitializeComponent();
            this.mw = mw;
            this.SluzCB.ItemsSource = mw.GetSluzbeniciIDs();
            this.TelCB.ItemsSource = mw.GetTelDeoIDs();
            mw.CreatePopravkeCombinedIDs();
            this.PopCB.ItemsSource = mw.popravkecombinedIDs;
            

           

            if (nab != null)
            {
                PrepareEdit(nab);
            }

        }

        private void PrepareEdit(NABAVKA s)
        {
            this.CenaTB.Text = s.CENA.ToString();
            



            this.SluzCB.SelectedItem = s.SLUZBENIKMBR;
            this.SluzCB.IsEnabled = false;

            this.TelCB.SelectedItem = s.TELEFONSKI_DEOID_DEO;
            this.TelCB.IsEnabled = false;

            this.PopCB.SelectedItem =mw.popravkecombinedIDs.First(e=> e.Equals($"{s.POPRAVKAOSTECENJEOST_ID}-{s.POPRAVKAOSTECENJEMOBILNI_TELEFONMOB_ID}-{s.POPRAVKASERVISERMBR}"));


            this.CreateBtn.Content = "Update";
            this.isUpdate = true;


        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                NABAVKA s = new NABAVKA
                {
                    CENA = int.Parse(CenaTB.Text)
                };

                if (SluzCB.SelectedItem != null)
                    s.SLUZBENIKMBR = int.Parse((string)SluzCB.SelectedItem.ToString());
                if (TelCB.SelectedItem != null)
                    s.TELEFONSKI_DEOID_DEO = int.Parse((TelCB.SelectedItem.ToString()));
                if (PopCB.SelectedItem != null)
                {
                    s.POPRAVKAOSTECENJEOST_ID = int.Parse(PopCB.SelectedItem.ToString().Split('-')[0]);
                    s.POPRAVKAOSTECENJEMOBILNI_TELEFONMOB_ID = int.Parse(PopCB.SelectedItem.ToString().Split('-')[1]);
                    s.POPRAVKASERVISERMBR = int.Parse(PopCB.SelectedItem.ToString().Split('-')[2]);
                }

                if (!isUpdate)
                {
                    mw.CreateNabavka(s);
                }
                else
                {
                    mw.UpdateNabavka(s);
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

            CenaGRD.Content = "";
            PopGRD.Content = "";
            SluzGRD.Content = "";
            TelGRD.Content = "";



            bool isValid = true;

            if (CenaTB.Text.Equals(string.Empty) || !int.TryParse(CenaTB.Text, out int num))
            {
                isValid = false;
                CenaGRD.Content = "Mora biti validan broj!";
            }

            if (PopCB.SelectedItem == null)
            {
                isValid = false;
                PopGRD.Content = "Morate izabrati vrednost!";
            }

            if (SluzCB.SelectedItem == null)
            {
                isValid = false;
                SluzGRD.Content = "Morate izabrati vrednost!";
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
