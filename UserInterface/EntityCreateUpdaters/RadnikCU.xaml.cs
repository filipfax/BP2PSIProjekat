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
    /// Interaction logic for SluzbenikCU.xaml
    /// </summary>
    public partial class RadnikCU : Window
    {
        bool isUpdate = false;
        MainWindow mw = null;
        List<int> mbrs = new List<int>();
        public RadnikCU(MainWindow mw, RADNIK radnik = null)
        {
            InitializeComponent();
            this.mw = mw;

            this.ServisCB.ItemsSource = mw.GetServisIDs();
            this.NadCB.ItemsSource = mw.GetRadnikIDs();

            if (radnik != null)
            {

                PrepareEdit(radnik);
            }

          
        }

        private void PrepareEdit(RADNIK s)
        {
            this.MBRTB.Text = s.MBR.ToString();
            this.MBRTB.IsEnabled = false;

            this.PLTTB.Text = s.PLT.ToString();

            this.IMETB.Text = s.IME;

            this.PREZTB.Text = s.PRZ;

            this.ServisCB.SelectedItem = s.SERVISSERV_ID.ToString();

            this.NadCB.SelectedItem = s.NADREDJEN;

            

            this.CreateBtn.Content = "Update";
            this.isUpdate = true;

            List<int> str = (List<int>)this.NadCB.ItemsSource;
            str.Remove(s.MBR);
            this.NadCB.ItemsSource = str;

        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {

                RADNIK s = new RADNIK
                {
                    MBR = int.Parse(MBRTB.Text),
                    PLT = int.Parse(PLTTB.Text),
                    IME = IMETB.Text,
                    PRZ = PREZTB.Text,
                    


                };

                if (NadCB.SelectedItem != null)
                    s.RADNIKMBR = int.Parse((string)NadCB.SelectedItem.ToString());
                if (ServisCB.SelectedItem != null)
                    s.SERVISSERV_ID = int.Parse(ServisCB.SelectedItem.ToString());

                if (!isUpdate)
                {

                    mw.CreateRadnik(s);
                }
                else
                {
                    mw.UpdateRadnik(s);
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
            bool isValid = true;

            if (MBRTB.Text.Equals(string.Empty) || !int.TryParse(MBRTB.Text, out int num))
            {
                isValid = false;
                MbrGRD.Content = "Mora biti validan broj!";
            }

            if (PLTTB.Text.Equals(string.Empty) || !int.TryParse(PLTTB.Text, out int num2))
            {
                isValid = false;
                PltGRD.Content = "Mora biti validan broj!";
            }



            if (IMETB.Text.Equals(string.Empty))
            {
                isValid = false;
                ImeGRD.Content = "Polje ne sme biti prazno!";
            }

            if (PREZTB.Text.Equals(string.Empty))
            {
                isValid = false;
                PrezGRD.Content = "Polje ne sme biti prazno!";
            }





            return isValid;
        }
    }
}
