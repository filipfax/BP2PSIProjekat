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
    /// Interaction logic for OstecenjeCU.xaml
    /// </summary>
    public partial class OstecenjeCU : Window
    {
        private bool isUpdate = false;
        private MainWindow mw = null;
        
        public OstecenjeCU(MainWindow mw, OSTECENJE ost = null)
        {
            InitializeComponent();
            this.mw = mw;

            this.MobCB.ItemsSource = mw.GetMobTelIDs();
            this.TipCB.ItemsSource = new List<string> { "Softversko", "Fizicko" };

            if (ost != null)
            {
                PrepareEdit(ost);
            }

        }

        private void PrepareEdit(OSTECENJE s)
        {
            this.IDTB.Text = s.OST_ID.ToString();
            this.IDGRD.IsEnabled = false;

            this.OpTB.Text = s.OPIS_OST.ToString();

         
            this.TipCB.SelectedItem = s.TIP_OST.ToString();

            this.MobCB.SelectedItem = s.MOBILNI_TELEFONMOB_ID;


            this.CreateBtn.Content = "Update";
            this.isUpdate = true;

           
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                OSTECENJE s = new OSTECENJE
                {
                    OST_ID = int.Parse(IDTB.Text),
                    OPIS_OST = OpTB.Text,
                   




                };

                if (MobCB.SelectedItem != null)
                    s.MOBILNI_TELEFONMOB_ID = int.Parse((string)MobCB.SelectedItem.ToString());
                if (TipCB.SelectedItem != null)
                    s.TIP_OST = (TipCB.SelectedItem.ToString());

                if (!isUpdate)
                {
                    mw.CreateOstecenje(s);
                }
                else
                {
                    mw.UpdateOstecenje(s);
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

            IDGRD.Content = "";
            MobGRD.Content = "";
            OpisGRD.Content = "";
            TipGRD.Content = "";
           


            bool isValid = true;

            if (IDTB.Text.Equals(string.Empty) || !int.TryParse(IDTB.Text, out int num))
            {
                isValid = false;
                IDGRD.Content = "Mora biti validan broj!";
            }

            if (MobCB.SelectedItem == null)
            {
                isValid = false;
                MobGRD.Content = "Morate izabrati vrednost!";
            }

            if (OpTB.Text.Equals(string.Empty))
            {
                isValid = false;
                OpisGRD.Content = "Polje ne sme biti prazno!";
            }

         

            if (TipCB.SelectedItem == null)
            {
                isValid = false;
                TipGRD.Content = "Morate izabrati tip ostecenja!";
            }

            return isValid;
        }
    }
}
