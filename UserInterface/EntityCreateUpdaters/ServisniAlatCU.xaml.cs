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
    /// Interaction logic for ServisniAlatCU.xaml
    /// </summary>
    public partial class ServisniAlatCU : Window
    {
        bool isUpdate = false;
        MainWindow mw = null;
        public ServisniAlatCU(MainWindow mw, SERVISNI_ALAT servisnialat = null)
        {
            InitializeComponent();
            this.mw = mw;

            this.ServisCB.ItemsSource = mw.GetServisIDs();
            this.RadCB.ItemsSource = mw.GetServiseriIDs();
            this.TipCB.ItemsSource = new List<String> { "Softverski", "Fizicki" };

            if (servisnialat != null)
            {

                PrepareEdit(servisnialat);
            }

        }


        private void PrepareEdit(SERVISNI_ALAT s)
        {
            this.IDTB.Text = s.ALAT_ID.ToString();
            this.IDTB.IsEnabled = false;

            this.NazTb.Text = s.NAZ;

            this.TipCB.SelectedItem = s.TIP;

            this.ServisCB.SelectedItem = s.SERVISSERV_ID;

            this.KolTB.Text = s.KLC.ToString();

            this.RadCB.SelectedItem = s.SERVISERMBR;

            

            this.CreateBtn.Content = "Update";
            this.isUpdate = true;

        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {

                SERVISNI_ALAT s = new SERVISNI_ALAT
                {
                    ALAT_ID = isUpdate == true ? int.Parse(IDTB.Text) : 0,
                    NAZ = NazTb.Text,     
                    KLC = int.Parse(KolTB.Text),
                    


                };
                if (TipCB.SelectedItem != null)
                    s.TIP = (TipCB.SelectedItem.ToString());
                if (RadCB.SelectedItem != null)
                    s.SERVISERMBR = int.Parse((string)RadCB.SelectedItem.ToString());
                else
                {
                    s.SERVISERMBR = null;
                }
                if (ServisCB.SelectedItem != null)
                    s.SERVISSERV_ID = int.Parse(ServisCB.SelectedItem.ToString());

                if (!isUpdate)
                {
                    mw.CreateServisniAlat(s);
                }
                else
                {
                    mw.UpdateServisniAlat(s);
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
            KolGRD.Content = "";
            NazGRD.Content = "";
            RadGRD.Content = "";
            ServGRD.Content = "";
            TipGRD.Content = "";

            bool isValid = true;

          

            if (NazTb.Text.Equals(string.Empty))
            {
                isValid = false;
                NazGRD.Content = "Polje ne sme biti prazno!";
            }

            
            if (TipCB.SelectedItem==null )
            {
                isValid = false;
                TipGRD.Content = "Mora biti izabrana vrednost!";
            }

           
            if (ServisCB.SelectedItem == null)
            {
                isValid = false;
                ServGRD.Content = "Mora biti izabrana vrednost!";
            }

            if (KolTB.Text.Equals(string.Empty) || !int.TryParse(KolTB.Text, out int num2))
            {
                isValid = false;
                KolGRD.Content = "Mora biti validan broj!";
            }

            
            

            return isValid;
        }
    }
}
