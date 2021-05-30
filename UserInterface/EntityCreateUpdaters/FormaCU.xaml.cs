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
    /// Interaction logic for FormaCU.xaml
    /// </summary>
    public partial class FormaCU : Window
    {
        bool isUpdate = false;
        MainWindow mw = null;
        public FormaCU(MainWindow mw, FORMA forma=null)
        {
            this.mw = mw;
            InitializeComponent();

            this.MusCB.ItemsSource = mw.GetMusterijaIDs();
            this.MbrCB.ItemsSource = mw.GetSluzbeniciIDs();
            

            if (forma != null)
                PrepareEdit(forma);
        }

        private void PrepareEdit(FORMA fo)
        {
            this.IDTB.Text = fo.FORM_ID.ToString();
            this.IDTB.IsEnabled = false;

            this.MolbaTB.Text = fo.DOD_MOLBA;

            this.OpisTB.Text = fo.OPIS_OST;

            this.DateDP.SelectedDate = DateTime.Parse(fo.DAT_ISP);

            this.MbrCB.SelectedItem =fo.SLUZBENIKMBR;

            this.MusCB.SelectedItem = fo.MUSTERIJAMUS_ID;


            this.CreateBtn.Content = "Update";
            this.isUpdate = true;

        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {

                FORMA f = new FORMA
                {
                    FORM_ID = isUpdate == true ? int.Parse(IDTB.Text) : 0,
                    OPIS_OST = OpisTB.Text,     
                    DOD_MOLBA = MolbaTB.Text,
                    DAT_ISP  = DateDP.Text

                };

                

                if (MusCB.SelectedItem != null)
                    f.MUSTERIJAMUS_ID = int.Parse((string)MusCB.SelectedItem.ToString());
                if (MbrCB.SelectedItem != null)
                    f.SLUZBENIKMBR = int.Parse((string)MbrCB.SelectedItem.ToString());

                if (!isUpdate)
                {
                    mw.CreateForma(f);
                }
                else
                {
                    mw.UpdateForma(f);
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
            DateGRD.Content = "";
            MusGRD.Content = "";
            OpisGRD.Content = "";
            SluzGRD.Content = "";
            

            bool isValid = true;



            if (DateDP.Text.Equals(string.Empty))
            {
                isValid = false;
                DateGRD.Content = "Morate izabrati datum!";
            }

            if (!DateTime.TryParse(DateDP.Text, out DateTime tes))
            {
                isValid = false;
                DateGRD.Content = "Morate izabrati validan datum!";
            }

            if (OpisTB.Text.Equals(string.Empty))
            {
                isValid = false;
                OpisGRD.Content = "Polje ne sme biti prazno!";
            }

            

            if (MusCB.SelectedItem == null)
            {
                isValid = false;
                MusGRD.Content = "Morate izabrati musteriju!";
            }

            if (MbrCB.SelectedItem == null)
            {
                isValid = false;
                SluzGRD.Content = "Morate izabrati sluzbenika!!";
            }



            return isValid;
        }
    }
}
