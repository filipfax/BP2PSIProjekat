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
    /// Interaction logic for PopravkaCU.xaml
    /// </summary>
    public partial class PopravkaCU : Window
    {
        private bool isUpdate = false;
        private MainWindow mw = null;
        List<int> ostecenjaIDs = new List<int>();
        List<OSTECENJE> ostecenja =new List<OSTECENJE>();
        public PopravkaCU(MainWindow mw, POPRAVKA pop = null)
        {
            this.mw = mw;

            InitializeComponent();

            
            ostecenja = mw.GetAllOstecenje();
            foreach (OSTECENJE ost in ostecenja)
                ostecenjaIDs.Add(ost.OST_ID);

            this.IDOSCB.ItemsSource = ostecenjaIDs;
            this.MbrCB.ItemsSource = mw.serviseri.Keys.ToList<int>();

            if (pop != null)
            {
                PrepareEdit(pop);
            }
        }

        private void PrepareEdit(POPRAVKA s)
        {
            this.CenTB.Text = s.CENA.ToString();

            this.IDOSCB.SelectedItem = s.OSTECENJEOST_ID;
            this.IDOSCB.IsEnabled = false;

            this.MbrCB.SelectedItem = s.SERVISERMBR.ToString();
            this.MbrCB.SelectedItem = s.SERVISERMBR;
            this.MbrCB.IsEnabled = false;

            this.CreateBtn.Content = "Update";
            this.isUpdate = true;

            MbrCB.SelectedItem = s.SERVISERMBR.ToString();
            MbrCB.SelectedItem = s.SERVISERMBR;


        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                POPRAVKA s = new POPRAVKA
                {
                    CENA = int.Parse(CenTB.Text),
                   





                };

                if (MbrCB.SelectedItem != null)
                    s.SERVISERMBR = int.Parse((string)MbrCB.SelectedItem.ToString());
                if (IDOSCB.SelectedItem != null)
                {
                    s.OSTECENJEOST_ID = (int.Parse(IDOSCB.SelectedItem.ToString()));
                    s.OSTECENJEMOBILNI_TELEFONMOB_ID = (ostecenja.Find(o => o.OST_ID == s.OSTECENJEOST_ID)).MOBILNI_TELEFONMOB_ID;
                }
                if (!isUpdate)
                {
                    mw.CreatePopravka(s);
                }
                else
                {
                    mw.UpdatePopravka(s);
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
            IDGRD.Content = "";
            MbrGRD.Content = "";
          



            bool isValid = true;

            if (CenTB.Text.Equals(string.Empty) || !int.TryParse(CenTB.Text, out int num))
            {
                isValid = false;
                CenaGRD.Content = "Mora biti validan broj!";
            }

            if (IDOSCB.SelectedItem == null)
            {
                isValid = false;
                IDGRD.Content = "Morate izabrati vrednost!";
            }

            if (MbrCB.Text.Equals(string.Empty))
            {
                isValid = false;
                MbrGRD.Content = "Polje ne sme biti prazno!";
            }



         
            return isValid;
        }
    }
}
