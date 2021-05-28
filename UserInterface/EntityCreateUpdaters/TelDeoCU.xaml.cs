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
    /// Interaction logic for TelDeoCU.xaml
    /// </summary>
    public partial class TelDeoCU : Window
    {
        private bool isUpdate = false;
        private MainWindow mw = null;

        public TelDeoCU(MainWindow mw, TELEFONSKI_DEO td = null)
        {
            InitializeComponent();
            this.mw = mw;
            this.OrigCB.ItemsSource = new List<string> { "Da", "Ne" };

            if (td != null)
            {
                PrepareEdit(td);
            }
        }

            private void PrepareEdit(TELEFONSKI_DEO td)
            {
                this.IDTB.Text = td.ID_DEO.ToString();
                this.IDTB.IsEnabled = false;

                this.OrigCB.SelectedItem = td.ORIG== true ? "Da": "Ne";


                this.TipTB.Text = td.TIP;

                


                this.CreateBtn.Content = "Update";
                this.isUpdate = true;


            }

            private void CreateBtn_Click(object sender, RoutedEventArgs e)
            {
                if (ValidateInput())
                {
                    TELEFONSKI_DEO s = new TELEFONSKI_DEO
                    {
                        ID_DEO = int.Parse(IDTB.Text),
                        TIP = TipTB.Text,





                    };

                    if (OrigCB.SelectedItem != null)
                      s.ORIG = this.OrigCB.SelectedItem.ToString() == "Da" ? true: false;


                if (!isUpdate)
                    {
                        mw.CreateTelDeo(s);
                    }
                    else
                    {
                        mw.UpdateTelDeo(s);
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
                TipGRD.Content = "";
                OrigGRD.Content = "";
                



                bool isValid = true;

                if (IDTB.Text.Equals(string.Empty) || !int.TryParse(IDTB.Text, out int num))
                {
                    isValid = false;
                    IDGRD.Content = "Mora biti validan broj!";
                }

                if (OrigCB.SelectedItem == null)
                {
                    isValid = false;
                    OrigGRD.Content = "Morate izabrati vrednost!";
                }

                if (TipTB.Text.Equals(string.Empty))
                {
                    isValid = false;
                    TipGRD.Content = "Polje ne sme biti prazno!";
                }             

                return isValid;
            
        }
    }
}
