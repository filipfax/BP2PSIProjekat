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
    /// Interaction logic for MobTelCU.xaml
    /// </summary>
    public partial class MobTelCU : Window
    {
        bool isUpdate = false;
        MainWindow mw = null;
        public MobTelCU(MainWindow mw, MOBILNI_TELEFON mt=null)
        {

            this.mw = mw;
            InitializeComponent();
            this.OpSisCB.ItemsSource = new List<string>{"Android", "IOS", "Windows" };
            this.MusCB.ItemsSource = mw.GetMusterijaIDs();
            if (mt != null)
            {

                PrepareEdit(mt);
            }
        }

        private void PrepareEdit(MOBILNI_TELEFON mt)
        {
            this.IDTB.Text = mt.MOB_ID.ToString();
            this.IDTB.IsEnabled = false;

            this.ModelTB.Text = mt.MODEL;

            this.MusCB.SelectedItem = mt.MUSTERIJAMUS_ID.ToString();


            this.OpSisCB.SelectedItem = mt.OP_SIS;

            this.ProizvTB.Text = mt.PROIZV;


            this.CreateBtn.Content = "Update";
            this.isUpdate = true;

        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {

                MOBILNI_TELEFON s = new MOBILNI_TELEFON
                {
                    MOB_ID = isUpdate == true ? int.Parse(IDTB.Text) : 0,
                    PROIZV = ProizvTB.Text,
                    MODEL = ModelTB.Text,
                    



                };

                if (MusCB.SelectedItem != null)
                    s.MUSTERIJAMUS_ID = int.Parse((string)MusCB.SelectedItem.ToString());
                if (OpSisCB.SelectedItem != null)
                    s.OP_SIS = (OpSisCB.SelectedItem.ToString());

                if (!isUpdate)
                {
                    mw.CreateMobTel(s);
                }
                else
                {
                    mw.UpdateMobTel(s);
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
            ModGRD.Content = "";
            MusGRD.Content = "";
            OpSisGRD.Content = "";
            ProizvGRD.Content = "";

            bool isValid = true;

          

            if (ModelTB.Text.Equals(string.Empty))
            {
                isValid = false;
                ModGRD.Content = "Polje ne sme biti prazno!";
            }

            if (ProizvTB.Text.Equals(string.Empty))
            {
                isValid = false;
                ProizvGRD.Content = "Polje ne sme biti prazno!";
            }



            if (MusCB.SelectedItem == null)
            {
                isValid = false;
                MusGRD.Content = "Morate izabrati musteriju!";
            }

            if (OpSisCB.SelectedItem == null)
            {
                isValid = false;
                OpSisGRD.Content = "Morate izabrati op. sistem!!";
            }



            return isValid;
        }
    }
}
