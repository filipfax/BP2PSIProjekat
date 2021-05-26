using BP2Projekat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for MusterijaCU.xaml
    /// </summary>
    public partial class MusterijaCU : Window
    {
        bool isUpdate = false;
        MainWindow mw = null;
        public MusterijaCU(MainWindow mw, MUSTERIJA musterija = null)
        {
            this.mw = mw;
            InitializeComponent();
            if (musterija != null)
            {

                PrepareEdit(musterija);
            }
        }


        private void PrepareEdit(MUSTERIJA s)
        {
            this.IDTB.Text = s.MUS_ID.ToString();
            this.IDTB.IsEnabled = false;

            this.ImeTB.Text = s.IME;

            this.PrezTB.Text = s.PRZ;


            this.TelTB.Text = s.BR_TEL.ToString();

            this.EmailTB.Text = s.EMAIL;


            this.CreateBtn.Content = "Update";
            this.isUpdate = true;

        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {

                MUSTERIJA s = new MUSTERIJA
                {
                    MUS_ID = int.Parse(IDTB.Text),
                    IME = ImeTB.Text,
                    PRZ = PrezTB.Text,
                    BR_TEL = int.Parse(TelTB.Text),
                    EMAIL = EmailTB.Text,



                };

                if (!isUpdate)
                {
                    mw.CreateMusterija(s);
                }
                else
                {
                    mw.UpdateMusterija(s);
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
            ImeGRD.Content = "";
            PrezGRD.Content = "";
            TelBrGRD.Content = "";
            EmailGRD.Content = "";

            bool isValid = true;

            if (IDTB.Text.Equals(string.Empty) || !int.TryParse(IDTB.Text, out int num))
            {
                isValid = false;
                IDGRD.Content = "Mora biti validan broj!";
            }

            if (ImeTB.Text.Equals(string.Empty))
            {
                isValid = false;
                ImeGRD.Content = "Polje ne sme biti prazno!";
            }

            if (PrezTB.Text.Equals(string.Empty))
            {
                isValid = false;
                PrezGRD.Content = "Polje ne sme biti prazno!";
            }



            if (TelTB.Text.Equals(string.Empty) || !int.TryParse(TelTB.Text, out int num3))
            {
                isValid = false;
                TelBrGRD.Content = "Mora biti validan broj!";
            }

            if (EmailTB.Text.Equals(string.Empty) || !Regex.IsMatch(EmailTB.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                isValid = false;
                EmailGRD.Content = "Email mora biti validan!";
            }


            return isValid;
        }

    }
}
