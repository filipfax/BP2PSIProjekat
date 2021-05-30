using BP2Projekat;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserInterface.Models;
using UserInterface.PFTViews;
using UserInterface.EntityCreateUpdaters;

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ProjekatModelDBContext dBContext;
        public Dictionary<int, SLUZBENIK> sluzbenici;
        public Dictionary<int, SERVISER> serviseri;
        public List<string> popravkecombinedIDs;
        public MainWindow()
        {
            InitializeComponent();
            dBContext = new ProjekatModelDBContext();
            LoadAllServis();
            serviseri = new Dictionary<int, SERVISER>();
            sluzbenici = new Dictionary<int, SLUZBENIK>();
            popravkecombinedIDs = new List<string>();

            this.DeleteServisBtn.IsEnabled = false;
            this.UpdateServisBtn.IsEnabled = false;
        }

        private void EntityTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ServisTab.IsSelected)
            {
                LoadAllServis();
            }
            if (this.RadnikTab.IsSelected)
            {
                LoadAllRadnik();
            }
            if (this.ServisniAlatTab.IsSelected)
            {
                LoadAllServisniAlat();
            }
            if (this.MusterijaTab.IsSelected)
            {
                LoadAllMusterija();
            }
            if (this.MobTelTab.IsSelected)
            {
                LoadAllMobTel();
            }
            if (this.OstecenjeTab.IsSelected)
            {
                LoadAllOstecenje();
            }
            if (this.OstecenjeTab.IsSelected)
            {
                LoadAllOstecenje();
            }
            if (this.TelDeoTab.IsSelected)
            {
                LoadAllTelDeo();
            }
            if (this.PopravkaTab.IsSelected)
            {
                LoadAllPopravka();
            }
            if (this.ProizvodjacTab.IsSelected)
            {
                LoadAllProizvodjac();
            }
            if (this.NabavkaTab.IsSelected)
            {
                LoadAllNabavka();
            }
            if (this.ProizvodnjaTab.IsSelected)
            {
                LoadAllProizvodnja();
            }
            if (this.FormaTab.IsSelected)
            {
                LoadAllForma();
            }
        }


        #region Servis
        public void LoadAllServis()
        {
            try
            {

                var query = from b in dBContext.SERVIS1
                            orderby b.SERV_ID
                            select b;

                this.ServisDG.ItemsSource = query.ToList<SERVIS>();
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
         
        }

        public List<int> GetServisIDs()
        {
            var query = from b in dBContext.SERVIS1
                        orderby b.SERV_ID
                        select b;

            List<SERVIS> serv = query.ToList<SERVIS>();
            List<int> retval = new List<int>();
            foreach (SERVIS s in serv)
                retval.Add(s.SERV_ID);

            return retval;
        }
        public void CreateServis(SERVIS s)
        {
            try
            {
                dBContext.SERVIS1.Add(s);
                dBContext.SaveChanges();
                LoadAllServis();
            }catch(Exception e)
            {
                Console.WriteLine($"Greska pri dodavanju entiteta: {e.Message}");
            }
        }
        public void UpdateServis(SERVIS serv)
        {
            try
            {
                var result = dBContext.SERVIS1.SingleOrDefault(s => s.SERV_ID == serv.SERV_ID);
                if (result != null)
                {
                    result.WEB_STR = serv.WEB_STR ;
                    result.RAD_VRM = serv.RAD_VRM;
                    result.ADRESA = serv.ADRESA;
                    result.BR_ZAP = serv.BR_ZAP;
                    result.TELBROJ = serv.TELBROJ;
                    dBContext.SaveChanges();
                }
                LoadAllServis();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Greska pri dodavanju entiteta: {e.Message}");
            }
        }

        private void AddServisBtn_Click(object sender, RoutedEventArgs e)
        {
            ServisCU  scu = new ServisCU(this);
            scu.ShowDialog();

        }

        private void UpdateServisBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.ServisDG.SelectedItem != null)
            {
                int selectedid = (this.ServisDG.SelectedItem as SERVIS).SERV_ID;
                ServisCU scu = new ServisCU(this, dBContext.SERVIS1.Find(selectedid));
                scu.ShowDialog();
            }
        }

        private void DeleteServisBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.ServisDG.SelectedItem != null)
            {
                int selectedid = (this.ServisDG.SelectedItem as SERVIS).SERV_ID;
                dBContext.SERVIS1.Remove(dBContext.SERVIS1.Find(selectedid));
                dBContext.SaveChanges();
                this.ServisDG.SelectedItem = null;
                LoadAllServis();
                this.DeleteServisBtn.IsEnabled = false;

            }
        }

        private void ServisDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ServisDG.SelectedItem != null)
            {
                this.DeleteServisBtn.IsEnabled = true;
                this.UpdateServisBtn.IsEnabled = true;
            }
            else
            {
                this.DeleteServisBtn.IsEnabled = false;
                this.UpdateServisBtn.IsEnabled = false;
            }
        }

        #endregion Servis


        #region Radnik
        public void LoadAllRadnik()
        {
            try
            {

                var query = from b in dBContext.RADNICI
                            orderby b.MBR
                            select b;

                this.RadnikDG.ItemsSource = query.ToList<RADNIK>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public List<int> GetRadnikIDs()
        {
            var query = from b in dBContext.RADNICI
                        orderby b.MBR
                        select b;

            List<RADNIK> serv = query.ToList<RADNIK>();
            List<int> retval = new List<int>();
            foreach (RADNIK s in serv)
                retval.Add(s.MBR);

            return retval;
        }

        public void CreateRadnik(SERVISER r)
        {
            try
            {
                dBContext.RADNICI.Add(r);
                dBContext.SaveChanges();
                LoadAllRadnik();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Greska pri dodavanju entiteta: {e.Message}");
            }
        }

        public void UpdateRadnik(RADNIK r)
        {
            try
            {
                var result = dBContext.RADNICI.SingleOrDefault(rad => rad.MBR == r.MBR);
                if (result != null)
                {
                    result.PLT = r.PLT;
                    result.IME = r.IME;
                    result.PRZ = r.PRZ;
                    result.SERVISSERV_ID = r.SERVISSERV_ID;
                    result.RADNIKMBR = r.RADNIKMBR;
                    dBContext.SaveChanges();
                }
                LoadAllRadnik();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Greska pri dodavanju entiteta: {e.Message}");
            }
        }

        public void CreateSluzbenik(SLUZBENIK r)
        {
            try
            {
                dBContext.RADNICI.Add(r);
                dBContext.SaveChanges();
                LoadAllRadnik();
               
            }
            catch (Exception e)
            {
                Console.WriteLine($"Greska pri dodavanju entiteta: {e.Message}");
                MessageBox.Show($"Greska pri dodavanju entiteta: {e.InnerException.InnerException.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void UpdateSluzbenik(SLUZBENIK r)
        {
            try
            {
                var result = dBContext.RADNICI.SingleOrDefault(rad => rad.MBR == r.MBR);
                if (result != null)
                {
                    result.PLT = r.PLT;
                    result.IME = r.IME;
                    result.PRZ = r.PRZ;
                    result.SERVISSERV_ID = r.SERVISSERV_ID;
                    result.RADNIKMBR = r.RADNIKMBR;
                    dBContext.SaveChanges();
                }
                sluzbenici[r.MBR] = r;
                LoadAllRadnik();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Greska pri izmeni entiteta: {e.Message}");
            }
        }

        public void CreateServiser(SERVISER r)
        {
            try
            {
                dBContext.RADNICI.Add(r);
                dBContext.SaveChanges();
               
                LoadAllRadnik();
            }
            catch (Exception e)
            {
                
                Console.WriteLine($"Greska pri dodavanju entiteta: {e.InnerException.Message}");
                MessageBox.Show($"Greska pri dodavanju entiteta: {e.InnerException.InnerException.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                
            }
        }

        public void UpdateServiser(SERVISER r)
        {
            try
            {
                var result = dBContext.RADNICI.SingleOrDefault(rad => rad.MBR == r.MBR);
                if (result != null)
                {
                    result.PLT = r.PLT;
                    result.IME = r.IME;
                    result.PRZ = r.PRZ;
                    result.SERVISSERV_ID = r.SERVISSERV_ID;
                    result.RADNIKMBR = r.RADNIKMBR;
                    dBContext.SaveChanges();
                }
                serviseri[r.MBR] = r;
                LoadAllRadnik();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Greska pri izmeni entiteta: {e.Message}");
                MessageBox.Show($"Greska pri izmeni entiteta: {e.InnerException.InnerException.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddRadnikBtn_Click(object sender, RoutedEventArgs e)
        {
            RadnikCU rcu = new RadnikCU(this);
            rcu.ShowDialog();

        }

        private void UpdateRadnikBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.RadnikDG.SelectedItem != null)
            {
                int selectedid = (this.RadnikDG.SelectedItem as RADNIK).MBR;
                RadnikCU rcu = new RadnikCU(this, dBContext.RADNICI.Find(selectedid));
                rcu.ShowDialog();
            }
        }

        private void DeleteRadnikBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.RadnikDG.SelectedItem != null)
            {
                int selectedid = (this.RadnikDG.SelectedItem as RADNIK).MBR;
                dBContext.RADNICI.Remove(dBContext.RADNICI.Find(selectedid));
                dBContext.SaveChanges();
                this.RadnikDG.SelectedItem = null;
                LoadAllRadnik();
                this.DeleteRadnikBtn.IsEnabled = false;

            }
        }

        private void SPOJoinBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.RadnikDG.SelectedItem != null)
            {
                int selectedid = (this.RadnikDG.SelectedItem as RADNIK).MBR;
                GetSPOJoin(selectedid);

            }
        }

        private void RadnikDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.RadnikDG.SelectedItem != null)
            {
                this.DeleteRadnikBtn.IsEnabled = true;
                this.UpdateRadnikBtn.IsEnabled = true;
                this.SPOJoinBtn.IsEnabled = true;
            }
            else
            {
                this.DeleteRadnikBtn.IsEnabled = false;
                this.UpdateRadnikBtn.IsEnabled = false;
                this.SPOJoinBtn.IsEnabled = false;
            }
        }
        #endregion Radnik

        #region ServisniAlat
        public void LoadAllServisniAlat()
        {
            try
            {

                var query = from b in dBContext.SERVISNI_ALATI
                            orderby b.ALAT_ID
                            select b;

                this.ServisniAlatDG.ItemsSource = query.ToList<SERVISNI_ALAT>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public List<int> GetServisniAlatIDs()
        {
            var query = from b in dBContext.SERVISNI_ALATI
                        orderby b.ALAT_ID
                        select b;

            List<SERVISNI_ALAT> serv = query.ToList<SERVISNI_ALAT>();
            List<int> retval = new List<int>();
            foreach (SERVISNI_ALAT s in serv)
                retval.Add(s.ALAT_ID);

            return retval;
        }

        public void CreateServisniAlat(SERVISNI_ALAT s)
        {
            try
            {
                dBContext.SERVISNI_ALATI.Add(s);
                dBContext.SaveChanges();
                LoadAllServisniAlat();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Greska pri dodavanju entiteta: {e.Message}");
                MessageBox.Show($"Greska pri dodavanju entiteta: {e.InnerException.InnerException.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void UpdateServisniAlat(SERVISNI_ALAT s)
        {
            try
            {
                var result = dBContext.SERVISNI_ALATI.SingleOrDefault(ser => ser.ALAT_ID == s.ALAT_ID);
                if (result != null)
                {
                    result.NAZ = s.NAZ;
                    result.TIP = s.TIP;
                    result.SERVISSERV_ID = s.SERVISSERV_ID;
                    result.KLC = s.KLC;
                    result.SERVISERMBR = s.SERVISERMBR;
                    dBContext.SaveChanges();
                }
                LoadAllServisniAlat();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Greska pri dodavanju entiteta: {e.Message}");
                MessageBox.Show($"Greska pri dodavanju entiteta: {e.InnerException.InnerException.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddServisniAlatBtn_Click(object sender, RoutedEventArgs e)
        {
            ServisniAlatCU rcu = new ServisniAlatCU(this);
            rcu.ShowDialog();

        }

        private void UpdateServisniAlatBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.ServisniAlatDG.SelectedItem != null)
            {
                int selectedid = (this.ServisniAlatDG.SelectedItem as SERVISNI_ALAT).ALAT_ID;
                ServisniAlatCU rcu = new ServisniAlatCU(this, dBContext.SERVISNI_ALATI.Find(selectedid));
                rcu.ShowDialog();
            }
        }

        private void DeleteServisniAlatBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.ServisniAlatDG.SelectedItem != null)
            {
                int selectedid = (this.ServisniAlatDG.SelectedItem as SERVISNI_ALAT).ALAT_ID;
                dBContext.SERVISNI_ALATI.Remove(dBContext.SERVISNI_ALATI.Find(selectedid));
                dBContext.SaveChanges();
                this.ServisniAlatDG.SelectedItem = null;
                LoadAllServisniAlat();
                this.DeleteServisniAlatBtn.IsEnabled = false;

            }
        }

     

        private void ServisniAlatDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ServisniAlatDG.SelectedItem != null)
            {
                this.DeleteServisniAlatBtn.IsEnabled = true;
                this.UpdateServisniAlatBtn.IsEnabled = true;
            }
            else
            {
                this.DeleteServisniAlatBtn.IsEnabled = false;
                this.UpdateServisniAlatBtn.IsEnabled = false;
            }
        }
        #endregion ServisniAlat

        #region Musterija
        public void LoadAllMusterija()
        {
            try
            {

                var query = from b in dBContext.MUSTERIJE
                            orderby b.MUS_ID
                            select b;

                this.MusterijaDG.ItemsSource = query.ToList<MUSTERIJA>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public List<int> GetMusterijaIDs()
        {
            var query = from b in dBContext.MUSTERIJE
                        orderby b.MUS_ID
                        select b;

            List<MUSTERIJA> serv = query.ToList<MUSTERIJA>();
            List<int> retval = new List<int>();
            foreach (MUSTERIJA s in serv)
                retval.Add(s.MUS_ID);

            return retval;
        }
        public void CreateMusterija(MUSTERIJA s)
        {
            try
            {
                dBContext.MUSTERIJE.Add(s);
                dBContext.SaveChanges();
                LoadAllMusterija();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Greska pri dodavanju entiteta: {e.Message}");
                MessageBox.Show($"Greska pri dodavanju entiteta: {e.InnerException.InnerException.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void UpdateMusterija(MUSTERIJA mus)
        {
            try
            {
                var result = dBContext.MUSTERIJE.SingleOrDefault(s => s.MUS_ID == mus.MUS_ID);
                if (result != null)
                {
                    result.IME = mus.IME;
                    result.PRZ = mus.PRZ;
                    result.BR_TEL = mus.BR_TEL;
                    result.EMAIL = mus.EMAIL;
                    dBContext.SaveChanges();
                }
                LoadAllMusterija();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Greska pri dodavanju entiteta: {e.Message}");
                MessageBox.Show($"Greska pri dodavanju entiteta: {e.InnerException.InnerException.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddMusterijaBtn_Click(object sender, RoutedEventArgs e)
        {
            MusterijaCU scu = new MusterijaCU(this);
            scu.ShowDialog();

        }

        private void UpdateMusterijaBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.MusterijaDG.SelectedItem != null)
            {
                int selectedid = (this.MusterijaDG.SelectedItem as MUSTERIJA).MUS_ID;
                MusterijaCU scu = new MusterijaCU(this, dBContext.MUSTERIJE.Find(selectedid));
                scu.ShowDialog();
            }
        }

        private void DeleteMusterijaBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.MusterijaDG.SelectedItem != null)
            {
                int selectedid = (this.MusterijaDG.SelectedItem as MUSTERIJA).MUS_ID;
                dBContext.MUSTERIJE.Remove(dBContext.MUSTERIJE.Find(selectedid));
                dBContext.SaveChanges();
                this.MusterijaDG.SelectedItem = null;
                LoadAllMusterija();
                this.DeleteMusterijaBtn.IsEnabled = false;

            }
        }

        private void MusterijaDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.MusterijaDG.SelectedItem != null)
            {
                this.DeleteMusterijaBtn.IsEnabled = true;
                this.UpdateMusterijaBtn.IsEnabled = true;
                this.RacunMusterijaBtn.IsEnabled = true;
            }
            else
            {
                this.DeleteMusterijaBtn.IsEnabled = false;
                this.UpdateMusterijaBtn.IsEnabled = false;
                this.RacunMusterijaBtn.IsEnabled = false;
            }
        }
        #endregion Musterija

        #region MobilniTelefon
        public void LoadAllMobTel()
        {
            try
            {

                var query = from b in dBContext.MOBILNI_TELEFONI
                            orderby b.MOB_ID
                            select b;

                this.MobTelDG.ItemsSource = query.ToList<MOBILNI_TELEFON>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public List<int> GetMobTelIDs()
        {
            var query = from b in dBContext.MOBILNI_TELEFONI
                        orderby b.MOB_ID
                        select b;

            List<MOBILNI_TELEFON> mob = query.ToList<MOBILNI_TELEFON>();
            List<int> retval = new List<int>();
            foreach (MOBILNI_TELEFON s in mob)
                retval.Add(s.MOB_ID);

            return retval;
        }

        public void CreateMobTel(MOBILNI_TELEFON s)
        {
            try
            {
                dBContext.MOBILNI_TELEFONI.Add(s);
                dBContext.SaveChanges();
                LoadAllMobTel();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Greska pri dodavanju entiteta: {e.Message}");
                MessageBox.Show($"Greska pri dodavanju entiteta: {e.InnerException.InnerException.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void UpdateMobTel(MOBILNI_TELEFON mob)
        {
            try
            {
                var result = dBContext.MOBILNI_TELEFONI.SingleOrDefault(s => s.MOB_ID == mob.MOB_ID);
                if (result != null)
                {
                    result.MODEL = mob.MODEL;
                    result.MUSTERIJAMUS_ID = mob.MUSTERIJAMUS_ID;
                    result.OP_SIS = mob.OP_SIS;
                    result.PROIZV = mob.PROIZV;
                    dBContext.SaveChanges();
                }
                LoadAllMobTel();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Greska pri dodavanju entiteta: {e.Message}");
                MessageBox.Show($"Greska pri dodavanju entiteta: {e.InnerException.InnerException.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddMobTelBtn_Click(object sender, RoutedEventArgs e)
        {
            MobTelCU mcu = new MobTelCU(this);
            mcu.ShowDialog();

        }

        private void UpdateMobTelBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.MobTelDG.SelectedItem != null)
            {
                int selectedid = (this.MobTelDG.SelectedItem as MOBILNI_TELEFON).MOB_ID;
                MobTelCU mcu = new MobTelCU(this, dBContext.MOBILNI_TELEFONI.Find(selectedid));
                mcu.ShowDialog();
                
            }
        }

        private void DeleteMobTelBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.MobTelDG.SelectedItem != null)
            {
                int selectedid = (this.MobTelDG.SelectedItem as MOBILNI_TELEFON).MOB_ID;
                dBContext.MOBILNI_TELEFONI.Remove(dBContext.MOBILNI_TELEFONI.Find(selectedid));
                dBContext.SaveChanges();
                this.MobTelDG.SelectedItem = null;
                LoadAllMobTel();
                this.DeleteMobTelBtn.IsEnabled = false;

            }
        }

        private void MobTelDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.MobTelDG.SelectedItem != null)
            {
                this.DeleteMobTelBtn.IsEnabled = true;
                this.UpdateMobTelBtn.IsEnabled = true;
            }
            else
            {
                this.DeleteMobTelBtn.IsEnabled = false;
                this.UpdateMobTelBtn.IsEnabled = false;
            }
        }
        #endregion MobilniTelefon

        #region Ostecenje
        public void LoadAllOstecenje()
        {
            try
            {

                var query = from b in dBContext.OSTECENJA
                            orderby b.OST_ID
                            select b;

                this.OstecenjeDG.ItemsSource = query.ToList<OSTECENJE>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public List<OSTECENJE> GetAllOstecenje()
        {
            try
            {

                var query = from b in dBContext.OSTECENJA
                            orderby b.OST_ID
                            select b;

                return query.ToList<OSTECENJE>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<OSTECENJE>();
            }
        }

        public List<int> GetOstecenjeIDs()
        {
            var query = from b in dBContext.OSTECENJA
                        orderby b.OST_ID
                        select b;

            List<OSTECENJE> mob = query.ToList<OSTECENJE>();
            List<int> retval = new List<int>();
            foreach (OSTECENJE s in mob)
                retval.Add(s.OST_ID);

            return retval;
        }
        public void CreateOstecenje(OSTECENJE s)
        {
            try
            {
                dBContext.OSTECENJA.Add(s);
                dBContext.SaveChanges();
                LoadAllOstecenje();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Greska pri dodavanju entiteta: {e.Message}");
                MessageBox.Show($"Greska pri dodavanju entiteta: {e.InnerException.InnerException.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void UpdateOstecenje(OSTECENJE ost)
        {
            try
            {
                var result = dBContext.OSTECENJA.SingleOrDefault(s => s.OST_ID == ost.OST_ID);
                if (result != null)
                {
                    result.TIP_OST = ost.TIP_OST;
                    result.MOBILNI_TELEFONMOB_ID = ost.MOBILNI_TELEFONMOB_ID;
                    result.OPIS_OST = ost.OPIS_OST;
                   
                    dBContext.SaveChanges();
                }
                LoadAllOstecenje();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Greska pri dodavanju entiteta: {e.Message}");
                MessageBox.Show($"Greska pri dodavanju entiteta: {e.InnerException.InnerException.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddOstecenjeBtn_Click(object sender, RoutedEventArgs e)
        {
            OstecenjeCU ocu = new OstecenjeCU(this);
            ocu.ShowDialog();

        }

        private void UpdateOstecenjeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.OstecenjeDG.SelectedItem != null)
            {
                int selectedid = (this.OstecenjeDG.SelectedItem as OSTECENJE).OST_ID;
                int selectedid2 = (this.OstecenjeDG.SelectedItem as OSTECENJE).MOBILNI_TELEFONMOB_ID;
                OstecenjeCU ocu = new OstecenjeCU(this, dBContext.OSTECENJA.Find(selectedid, selectedid2));
                ocu.ShowDialog();

            }
        }

        private void DeleteOstecenjeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.OstecenjeDG.SelectedItem != null)
            {
                int selectedid = (this.OstecenjeDG.SelectedItem as OSTECENJE).OST_ID;
                int selectedid2 = (this.OstecenjeDG.SelectedItem as OSTECENJE).MOBILNI_TELEFONMOB_ID;
                dBContext.OSTECENJA.Remove(dBContext.OSTECENJA.Find(selectedid, selectedid2));
                dBContext.SaveChanges();
                this.OstecenjeDG.SelectedItem = null;
                LoadAllOstecenje();
                this.DeleteOstecenjeBtn.IsEnabled = false;

            }
        }

        private void OstecenjeDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.OstecenjeDG.SelectedItem != null)
            {
                this.DeleteOstecenjeBtn.IsEnabled = true;
                this.UpdateOstecenjeBtn.IsEnabled = true;
            }
            else
            {
                this.DeleteOstecenjeBtn.IsEnabled = false;
                this.UpdateOstecenjeBtn.IsEnabled = false;
            }
        }
        #endregion Ostecenje

        #region TelefonskiDeo
        public void LoadAllTelDeo()
        {
            try
            {

                var query = from b in dBContext.TELEFONSKI_DELOVI
                            orderby b.ID_DEO
                            select b;

                this.TelDeoDG.ItemsSource = query.ToList<TELEFONSKI_DEO>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public List<int> GetTelDeoIDs()
        {
            var query = from b in dBContext.TELEFONSKI_DELOVI
                        orderby b.ID_DEO
                        select b;

            List<TELEFONSKI_DEO> mob = query.ToList<TELEFONSKI_DEO>();
            List<int> retval = new List<int>();
            foreach (TELEFONSKI_DEO s in mob)
                retval.Add(s.ID_DEO);

            return retval;
        }
        public void CreateTelDeo(TELEFONSKI_DEO s)
        {
            try
            {
                dBContext.TELEFONSKI_DELOVI.Add(s);
                dBContext.SaveChanges();
                LoadAllTelDeo();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Greska pri dodavanju entiteta: {e.Message}");
                MessageBox.Show($"Greska pri dodavanju entiteta: {e.InnerException.InnerException.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void UpdateTelDeo(TELEFONSKI_DEO td)
        {
            try
            {
                var result = dBContext.TELEFONSKI_DELOVI.SingleOrDefault(s => s.ID_DEO == td.ID_DEO);
                if (result != null)
                {
                    result.ID_DEO = td.ID_DEO;
                    result.ORIG = td.ORIG;
                    result.TIP = td.TIP;

                    dBContext.SaveChanges();
                }
                LoadAllTelDeo();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Greska pri dodavanju entiteta: {e.Message}");
                MessageBox.Show($"Greska pri dodavanju entiteta: {e.InnerException.InnerException.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddTelDeoBtn_Click(object sender, RoutedEventArgs e)
        {
            TelDeoCU ocu = new TelDeoCU(this);
            ocu.ShowDialog();

        }

        private void UpdateTelDeoBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.TelDeoDG.SelectedItem != null)
            {
                int selectedid = (this.TelDeoDG.SelectedItem as TELEFONSKI_DEO).ID_DEO;
                
                TelDeoCU ocu = new TelDeoCU(this, dBContext.TELEFONSKI_DELOVI.Find(selectedid));
                ocu.ShowDialog();

            }
        }

        private void DeleteTelDeoBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.TelDeoDG.SelectedItem != null)
            {
                int selectedid = (this.TelDeoDG.SelectedItem as TELEFONSKI_DEO).ID_DEO;
              
                dBContext.TELEFONSKI_DELOVI.Remove(dBContext.TELEFONSKI_DELOVI.Find(selectedid));
                dBContext.SaveChanges();
                this.TelDeoDG.SelectedItem = null;
                LoadAllTelDeo();
                this.DeleteTelDeoBtn.IsEnabled = false;

            }
        }

        private void TelDeoDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.TelDeoDG.SelectedItem != null)
            {
                this.DeleteTelDeoBtn.IsEnabled = true;
                this.UpdateTelDeoBtn.IsEnabled = true;
            }
            else
            {
                this.DeleteTelDeoBtn.IsEnabled = false;
                this.UpdateTelDeoBtn.IsEnabled = false;
            }
        }
        #endregion TelefonskiDeo

        #region Popravka
        public void LoadAllPopravka()
        {
            try
            {

                var query = from b in dBContext.POPRAVKAs
                            orderby b.SERVISERMBR
                            select b;

                this.PopravkaDG.ItemsSource = query.ToList<POPRAVKA>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

       
        public void CreatePopravkeCombinedIDs()
        {
            try
            {

                var query = from b in dBContext.POPRAVKAs
                            orderby b.SERVISERMBR
                            select b;

              
                foreach(POPRAVKA p in query.ToList<POPRAVKA>())
                {
                    popravkecombinedIDs.Add( $"{p.OSTECENJEOST_ID}-{p.OSTECENJEMOBILNI_TELEFONMOB_ID}-{p.SERVISERMBR}");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void CreatePopravka(POPRAVKA s)
        {
            try
            {
                dBContext.POPRAVKAs.Add(s);
                dBContext.SaveChanges();
                LoadAllPopravka();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Greska pri dodavanju entiteta: {e.Message}");
                MessageBox.Show($"Greska pri dodavanju entiteta: {e.InnerException.InnerException.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void UpdatePopravka(POPRAVKA pop)
        {
            try
            {
                var result = dBContext.POPRAVKAs.SingleOrDefault(p => p.OSTECENJEOST_ID == pop.OSTECENJEOST_ID && 
                                                                      p.OSTECENJEMOBILNI_TELEFONMOB_ID == pop.OSTECENJEMOBILNI_TELEFONMOB_ID &&
                                                                      p.SERVISERMBR == pop.SERVISERMBR);
                if (result != null)
                {
                    result.CENA = pop.CENA;               
                    dBContext.SaveChanges();
                }
                LoadAllPopravka();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Greska pri dodavanju entiteta: {e.Message}");
                MessageBox.Show($"Greska pri dodavanju entiteta: {e.InnerException.InnerException.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddPopravkaBtn_Click(object sender, RoutedEventArgs e)
        {
            PopravkaCU ocu = new PopravkaCU(this);
            ocu.ShowDialog();

        }

        private void UpdatePopravkaBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.PopravkaDG.SelectedItem != null)
            {
                int selectedid1 = (this.PopravkaDG.SelectedItem as POPRAVKA).OSTECENJEOST_ID;
                int selectedid2 = (this.PopravkaDG.SelectedItem as POPRAVKA).OSTECENJEMOBILNI_TELEFONMOB_ID;
                int selectedid3 = (this.PopravkaDG.SelectedItem as POPRAVKA).SERVISERMBR;

                PopravkaCU ocu = new PopravkaCU(this, dBContext.POPRAVKAs.Find(selectedid1, selectedid2, selectedid3));
                ocu.ShowDialog();

            }
        }

        private void DeletePopravkaBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.PopravkaDG.SelectedItem != null)
            {
                int selectedid1 = (this.PopravkaDG.SelectedItem as POPRAVKA).OSTECENJEOST_ID;
                int selectedid2 = (this.PopravkaDG.SelectedItem as POPRAVKA).OSTECENJEMOBILNI_TELEFONMOB_ID;
                int selectedid3 = (this.PopravkaDG.SelectedItem as POPRAVKA).SERVISERMBR;

                dBContext.POPRAVKAs.Remove(dBContext.POPRAVKAs.Find(selectedid1, selectedid2, selectedid3));
                dBContext.SaveChanges();
                this.PopravkaDG.SelectedItem = null;
                LoadAllPopravka();
                this.DeletePopravkaBtn.IsEnabled = false;

            }
        }

        private void PopravkaDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.PopravkaDG.SelectedItem != null)
            {
                this.DeletePopravkaBtn.IsEnabled = true;
                this.UpdatePopravkaBtn.IsEnabled = true;
            }
            else
            {
                this.DeletePopravkaBtn.IsEnabled = false;
                this.UpdatePopravkaBtn.IsEnabled = false;
            }
        }

        #endregion Popravka

        #region Proizvodjac
        public void LoadAllProizvodjac()
        {
            try
            {

                var query = from b in dBContext.PROIZVODJACI
                            orderby b.ID_PROIZV
                            select b;

                this.ProizvodjacDG.ItemsSource = query.ToList<PROIZVODJAC>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public List<int> GetProizvodjacIDs()
        {
            var query = from b in dBContext.PROIZVODJACI
                        orderby b.ID_PROIZV
                        select b;

            List<PROIZVODJAC> mob = query.ToList<PROIZVODJAC>();
            List<int> retval = new List<int>();
            foreach (PROIZVODJAC s in mob)
                retval.Add(s.ID_PROIZV);

            return retval;
        }

        public void CreateProizvodjac(PROIZVODJAC s)
        {
            try
            {
                dBContext.PROIZVODJACI.Add(s);
                dBContext.SaveChanges();
                LoadAllProizvodjac();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Greska pri dodavanju entiteta: {e.Message}");
                MessageBox.Show($"Greska pri dodavanju entiteta: {e.InnerException.InnerException.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void UpdateProizvodjac(PROIZVODJAC pop)
        {
            try
            {
                var result = dBContext.PROIZVODJACI.SingleOrDefault(p => p.ID_PROIZV == pop.ID_PROIZV);
                if (result != null)
                {
                    result.NAZ = pop.NAZ;
                    result.TELBROJ = pop.TELBROJ;
                    dBContext.SaveChanges();
                }
                LoadAllProizvodjac();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Greska pri dodavanju entiteta: {e.Message}");
                MessageBox.Show($"Greska pri dodavanju entiteta: {e.InnerException.InnerException.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddProizvodjacBtn_Click(object sender, RoutedEventArgs e)
        {
            ProizvodjacCU ocu = new ProizvodjacCU(this);
            ocu.ShowDialog();

        }

        private void UpdateProizvodjacBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.ProizvodjacDG.SelectedItem != null)
            {
                int selectedid1 = (this.ProizvodjacDG.SelectedItem as PROIZVODJAC).ID_PROIZV;
               

                ProizvodjacCU ocu = new ProizvodjacCU(this, dBContext.PROIZVODJACI.Find(selectedid1));
                ocu.ShowDialog();

            }
        }

        private void DeleteProizvodjacBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.ProizvodjacDG.SelectedItem != null)
            {
                int selectedid1 = (this.ProizvodjacDG.SelectedItem as PROIZVODJAC).ID_PROIZV;
              

                dBContext.PROIZVODJACI.Remove(dBContext.PROIZVODJACI.Find(selectedid1));
                dBContext.SaveChanges();
                this.ProizvodjacDG.SelectedItem = null;
                LoadAllProizvodjac();
                this.DeleteProizvodjacBtn.IsEnabled = false;

            }
        }

        private void ProizvodjacDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ProizvodjacDG.SelectedItem != null)
            {
                this.DeleteProizvodjacBtn.IsEnabled = true;
                this.UpdateProizvodjacBtn.IsEnabled = true;
            }
            else
            {
                this.DeleteProizvodjacBtn.IsEnabled = false;
                this.UpdateProizvodjacBtn.IsEnabled = false;
            }
        }

        #endregion Proizvodjac

        #region Nabavka
        public void LoadAllNabavka()
        {
            try
            {

                var query = from b in dBContext.NABAVKAs
                            orderby b.SLUZBENIKMBR
                            select b;

                this.NabavkaDG.ItemsSource = query.ToList<NABAVKA>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }


        public void CreateNabavka(NABAVKA s)
        {
            try
            {
                dBContext.NABAVKAs.Add(s);
                dBContext.SaveChanges();
                LoadAllNabavka();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Greska pri dodavanju entiteta: {e.Message}");
                MessageBox.Show($"Greska pri dodavanju entiteta: {e.InnerException.InnerException.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void UpdateNabavka(NABAVKA pop)
        {
            try
            {
                var result = dBContext.NABAVKAs.SingleOrDefault(p => p.SLUZBENIKMBR == pop.SLUZBENIKMBR && p.TELEFONSKI_DEOID_DEO == pop.TELEFONSKI_DEOID_DEO);
                if (result != null)
                {
                    result.CENA = pop.CENA;
                    result.POPRAVKAOSTECENJEMOBILNI_TELEFONMOB_ID = pop.POPRAVKAOSTECENJEMOBILNI_TELEFONMOB_ID;
                    result.POPRAVKAOSTECENJEOST_ID = pop.POPRAVKAOSTECENJEOST_ID;
                    result.POPRAVKASERVISERMBR = pop.POPRAVKASERVISERMBR;
                    dBContext.SaveChanges();
                }
                LoadAllNabavka();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Greska pri dodavanju entiteta: {e.Message}");
                MessageBox.Show($"Greska pri dodavanju entiteta: {e.InnerException.InnerException.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddNabavkaBtn_Click(object sender, RoutedEventArgs e)
        {
            NabavkaCU ocu = new NabavkaCU(this);
            ocu.ShowDialog();

        }

        private void UpdateNabavkaBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.NabavkaDG.SelectedItem != null)
            {
                int selectedid1 = (this.NabavkaDG.SelectedItem as NABAVKA).SLUZBENIKMBR;
                int selectedid2 = (this.NabavkaDG.SelectedItem as NABAVKA).TELEFONSKI_DEOID_DEO;

                
                NabavkaCU ocu = new NabavkaCU(this, dBContext.NABAVKAs.Find(selectedid2, selectedid1));
                ocu.ShowDialog();

            }
        }

        private void DeleteNabavkaBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.NabavkaDG.SelectedItem != null)
            {
                int selectedid1 = (this.NabavkaDG.SelectedItem as NABAVKA).SLUZBENIKMBR;
                int selectedid2 = (this.NabavkaDG.SelectedItem as NABAVKA).TELEFONSKI_DEOID_DEO;

                dBContext.NABAVKAs.Remove(dBContext.NABAVKAs.Find(selectedid2, selectedid1));
                dBContext.SaveChanges();
                this.NabavkaDG.SelectedItem = null;
                LoadAllNabavka();
                this.DeleteNabavkaBtn.IsEnabled = false;

            }
        }

        private void NabavkaDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.NabavkaDG.SelectedItem != null)
            {
                this.DeleteNabavkaBtn.IsEnabled = true;
                this.UpdateNabavkaBtn.IsEnabled = true;
            }
            else
            {
                this.DeleteNabavkaBtn.IsEnabled = false;
                this.UpdateNabavkaBtn.IsEnabled = false;
            }
        }

        #endregion Nabavka

        #region Proizvodnja
        public void LoadAllProizvodnja()
        {
            try
            {

                var query = from b in dBContext.PROIZVODNJAs
                            orderby b.PROIZVODJACID_PROIZV
                            select b;

                this.ProizvodnjaDG.ItemsSource = query.ToList<PROIZVODNJA>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }


        public void CreateProizvodnja(PROIZVODNJA s)
        {
            try
            {
                dBContext.PROIZVODNJAs.Add(s);
                dBContext.SaveChanges();
                LoadAllProizvodnja();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Greska pri dodavanju entiteta: {e.Message}");
                MessageBox.Show($"Greska pri dodavanju entiteta: {e.InnerException.InnerException.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void UpdateProizvodnja(PROIZVODNJA pop)
        {
            try
            {
                var result = dBContext.PROIZVODNJAs.SingleOrDefault(p => p.PROIZVODJACID_PROIZV == pop.PROIZVODJACID_PROIZV && p.TELEFONSKI_DEOID_DEO == pop.TELEFONSKI_DEOID_DEO);
                if (result != null)
                {
                   
                    dBContext.SaveChanges();
                }
                LoadAllProizvodnja();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Greska pri dodavanju entiteta: {e.Message}");
                MessageBox.Show($"Greska pri dodavanju entiteta: {e.InnerException.InnerException.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddProizvodnjaBtn_Click(object sender, RoutedEventArgs e)
        {
            ProizvodnjaCU ocu = new ProizvodnjaCU(this);
            ocu.ShowDialog();

        }

        private void UpdateProizvodnjaBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.ProizvodnjaDG.SelectedItem != null)
            {
                int selectedid1 = (this.ProizvodnjaDG.SelectedItem as PROIZVODNJA).PROIZVODJACID_PROIZV;
                int selectedid2 = (this.ProizvodnjaDG.SelectedItem as PROIZVODNJA).TELEFONSKI_DEOID_DEO;


                ProizvodnjaCU ocu = new ProizvodnjaCU(this, dBContext.PROIZVODNJAs.Find(selectedid2, selectedid1));
                ocu.ShowDialog();

            }
        }

        private void DeleteProizvodnjaBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.ProizvodnjaDG.SelectedItem != null)
            {
                int selectedid1 = (this.ProizvodnjaDG.SelectedItem as PROIZVODNJA).PROIZVODJACID_PROIZV;
                int selectedid2 = (this.ProizvodnjaDG.SelectedItem as PROIZVODNJA).TELEFONSKI_DEOID_DEO;

                dBContext.PROIZVODNJAs.Remove(dBContext.PROIZVODNJAs.Find(selectedid2, selectedid1));
                dBContext.SaveChanges();
                this.ProizvodnjaDG.SelectedItem = null;
                LoadAllProizvodnja();
                this.DeleteProizvodnjaBtn.IsEnabled = false;

            }
        }

        private void ProizvodnjaDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ProizvodnjaDG.SelectedItem != null)
            {
                this.DeleteProizvodnjaBtn.IsEnabled = true;
                this.UpdateProizvodnjaBtn.IsEnabled = true;
            }
            else
            {
                this.DeleteProizvodnjaBtn.IsEnabled = false;
                this.UpdateProizvodnjaBtn.IsEnabled = false;
            }
        }

        #endregion Proizvodnja

        #region Forma
        public void LoadAllForma()
        {
            try
            {

                var query = from b in dBContext.FORME
                            orderby b.FORM_ID
                            select b;

                this.FormaDG.ItemsSource = query.ToList<FORMA>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }


        public void CreateForma(FORMA s)
        {
            try
            {
                dBContext.FORME.Add(s);
                dBContext.SaveChanges();
                LoadAllForma();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Greska pri dodavanju entiteta: {e.Message}");
                MessageBox.Show($"Greska pri dodavanju entiteta: {e.InnerException.InnerException.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void UpdateForma(FORMA pop)
        {
            try
            {
                var result = dBContext.FORME.SingleOrDefault(p => p.FORM_ID == pop.FORM_ID );
                if (result != null)
                {
                    result.OPIS_OST = pop.OPIS_OST;
                    result.DOD_MOLBA = pop.DOD_MOLBA;
                    result.MUSTERIJAMUS_ID = pop.MUSTERIJAMUS_ID;
                    result.SLUZBENIKMBR = pop.SLUZBENIKMBR;
                    dBContext.SaveChanges();
                }
                LoadAllForma();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Greska pri dodavanju entiteta: {e.Message}");
                MessageBox.Show($"Greska pri dodavanju entiteta: {e.InnerException.InnerException.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddFormaBtn_Click(object sender, RoutedEventArgs e)
        {
            FormaCU ocu = new FormaCU(this);
            ocu.ShowDialog();

        }

        private void UpdateFormaBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.FormaDG.SelectedItem != null)
            {
                int selectedid1 = (this.FormaDG.SelectedItem as FORMA).FORM_ID;
           
               FormaCU ocu = new FormaCU(this, dBContext.FORME.Find(selectedid1));
                ocu.ShowDialog();

            }
        }

        private void DeleteFormaBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.FormaDG.SelectedItem != null)
            {
                int selectedid1 = (this.FormaDG.SelectedItem as FORMA).FORM_ID;
               

                dBContext.FORME.Remove(dBContext.FORME.Find(selectedid1));
                dBContext.SaveChanges();
                this.FormaDG.SelectedItem = null;
                LoadAllForma();
                this.DeleteFormaBtn.IsEnabled = false;

            }
        }

        private void FormaDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.FormaDG.SelectedItem != null)
            {
                this.DeleteFormaBtn.IsEnabled = true;
                this.UpdateFormaBtn.IsEnabled = true;
            }
            else
            {
                this.DeleteFormaBtn.IsEnabled = false;
                this.UpdateFormaBtn.IsEnabled = false;
            }
        }

        #endregion Forma

        #region Procedure,Funkcije,Trigeri
        public void GetSPOJoin(int mbr)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@mbr", mbr)
            };

            List<Models.SPOJoin> lista = dBContext.Database.SqlQuery<Models.SPOJoin>("ServiserPopravkaOstecenjeJoin @mbr", param).ToList();
            PFTViews.SPOJoin spview = new PFTViews.SPOJoin(lista);
            spview.ShowDialog();

        }

        public List<int> GetSluzbeniciIDs()
        {
            List<int> items = dBContext.Database.SqlQuery<int>("GetSluzbeniciIDs").ToList();
            return items;
        }

        public List<int> GetServiseriIDs()
        {
            List<int> items = dBContext.Database.SqlQuery<int>("GetServiseriIDs").ToList();
            return items;
        }

        public void ProveriInventar(object sender, RoutedEventArgs e)
        {


            List<Models.Inventar> lista = dBContext.Database.SqlQuery <Models.Inventar>("ProveriInventar").ToList();
            PFTViews.Inventar spview = new PFTViews.Inventar(lista);
            spview.ShowDialog();
        }

        private void Add5TelDeoBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlParameter[] param = new SqlParameter[]
           {
                new SqlParameter("@count", 5)
           };

            try
            {
                dBContext.Database.ExecuteSqlCommand("DODAJTELEFONE @count", param);
            }
            catch { }
            LoadAllTelDeo();

        }

        private void RacunMusterijaBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.MusterijaDG.SelectedItem != null)
            {
                int selectedid1 = (this.MusterijaDG.SelectedItem as MUSTERIJA).MUS_ID;



                SqlParameter[] param = new SqlParameter[]
           {
                new SqlParameter("@musid", selectedid1)
           };

                try
                {
                    var query = dBContext.Database.SqlQuery<int>($"DECLARE @P_Racun int; BEGIN EXEC @P_Racun = OdrediRacun {selectedid1}; select @P_Racun END;", param).ToList();
                    this.RacunLB.Content = $"Racun iznosi {query[0]} rsd.";
                }
                catch(Exception n){
                    Console.WriteLine(n.Message);
                }
               
            }

        }

        private bool isLocked = false;
        private void LockRadnikBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isLocked)
                {
                    dBContext.Database.ExecuteSqlCommand("DISABLE TRIGGER [MaxRadnikTrigger] on [dbo].[RADNICI]");
                    isLocked = false;
                }
                else
                {
                    dBContext.Database.ExecuteSqlCommand("ENABLE TRIGGER[MaxRadnikTrigger] on[dbo].[RADNICI]");
                    isLocked = true;
                }

            }
            catch { }
            
           

        }

        # endregion Procedure,Funkcije,Trigeri
    }
}
