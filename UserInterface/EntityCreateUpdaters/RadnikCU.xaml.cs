using BP2Projekat;
using System;
using System.Collections.Generic;
using System.Windows;

namespace UserInterface.EntityCreateUpdaters
{
    /// <summary>
    /// Interaction logic for SluzbenikCU.xaml
    /// </summary>
    public partial class RadnikCU : Window
    {
        private bool isUpdate = false;
        private MainWindow mw = null;
        private List<int> mbrs = new List<int>();

        public RadnikCU(MainWindow mw, RADNIK radnik = null)
        {
            InitializeComponent();
            this.mw = mw;

            this.ServisCB.ItemsSource = mw.GetServisIDs();
            this.NadCB.ItemsSource = mw.GetRadnikIDs();
            this.TipCB.ItemsSource = new List<string> { "Sluzbenik", "Serviser" };

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

            this.ServisCB.SelectedItem = s.SERVISSERV_ID;

            this.NadCB.SelectedItem = s.NADREDJEN;

            this.CreateBtn.Content = "Update";
            this.isUpdate = true;

            List<int> str = (List<int>)this.NadCB.ItemsSource;
            str.Remove(s.MBR);
            this.NadCB.ItemsSource = str;

            try
            {
                if (mw.GetServiseriIDs().Contains(s.MBR))
                {
                    this.TipCB.SelectedItem = "Serviser";
                }
                else
                {
                    this.TipCB.SelectedItem = "Sluzbenik";
                }
            }catch(Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                if (TipCB.SelectedItem.ToString().Equals("Sluzbenik"))
                {
                    SLUZBENIK s = new SLUZBENIK
                    {
                        MBR = isUpdate == true ? int.Parse(MBRTB.Text) : 0,
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
                        mw.CreateSluzbenik(s);
                    }
                    else
                    {
                        mw.UpdateSluzbenik (s);
                    }
                    this.Close();
                }
                else if (TipCB.SelectedItem.ToString().Equals("Serviser"))
                {
                    SERVISER s = new SERVISER
                    {
                       
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
                        mw.CreateServiser(s);
                    }
                    else
                    {
                        mw.UpdateServiser(s);
                    }
                    this.Close();
                }

                
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool ValidateInput()
        {

            ImeGRD.Content = "";
            MbrGRD.Content = "";
            NadrGRD.Content = "";
            PltGRD.Content = "";
            PrezGRD.Content = "";
            ServGRD.Content = "";
            TipGrd.Content = "";


            bool isValid = true;

          

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

            if (TipCB.SelectedItem == null)
            {
                isValid = false;
                TipGrd.Content = "Morate izabrati tip radnika!";
            }

            return isValid;
        }
    }
}