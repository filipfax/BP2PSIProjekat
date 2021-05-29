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
    /// Interaction logic for ServisCU.xaml
    /// </summary>
    public partial class ServisCU : Window
    {
        bool isUpdate = false;
        MainWindow mw = null;
        public ServisCU(MainWindow mw, SERVIS servis = null )
        {
            InitializeComponent();
            this.mw = mw;

            if (servis != null)
            {
               
                PrepareEdit(servis);
            }
              
        }

        private void PrepareEdit(SERVIS s)
        {
            this.IDTB.Text = s.SERV_ID.ToString();
            this.IDTB.IsEnabled = false;

            this.AdrTB.Text = s.ADRESA;

            this.BRZapTB.Text = s.BR_ZAP.ToString();

            this.RadTB.Text = s.RAD_VRM;

            this.TelBrojTB.Text = s.TELBROJ.ToString();

            this.WebTB.Text = s.WEB_STR;

            this.CreateBtn.Content = "Update";
            this.isUpdate = true;

        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {

                SERVIS s = new SERVIS
                {
                    SERV_ID = isUpdate == true ? int.Parse(IDTB.Text) : 0,
                    WEB_STR = WebTB.Text,
                    BR_ZAP = int.Parse(BRZapTB.Text),
                    TELBROJ = int.Parse(TelBrojTB.Text),
                    ADRESA = AdrTB.Text,
                    RAD_VRM = RadTB.Text,
                   
                    
                };

                if (!isUpdate)
                {
                    mw.CreateServis(s);
                }
                else
                {
                    mw.UpdateServis(s);
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
            AdrGRD.Content = "";
            BrZapGRD.Content = "";
            IDGRD.Content = "";
            RadGRD.Content = "";
            TelBrGRD.Content = "";
            WebGRD.Content = "";




            bool isValid = true;

           

            if (WebTB.Text.Equals(string.Empty)){
                isValid = false;
                WebGRD.Content = "Polje ne sme biti prazno!";
            }

            if (BRZapTB.Text.Equals(string.Empty) || !int.TryParse(BRZapTB.Text, out int num2))
            {
                isValid = false;
                BrZapGRD.Content = "Mora biti validan broj!";
            }

            if (TelBrojTB.Text.Equals(string.Empty) || !int.TryParse(TelBrojTB.Text, out int num3))
            {
                isValid = false;
                TelBrGRD.Content = "Mora biti validan broj!";
            }

            if (AdrTB.Text.Equals(string.Empty)){
                isValid = false;
                AdrGRD.Content = "Polje ne sme biti prazno!";
            }

            if (RadTB.Text.Equals(string.Empty))
            {
                isValid = false;
                RadGRD.Content = "Polje ne sme biti prazno!";
            }

            return isValid;
        }
    }
}
