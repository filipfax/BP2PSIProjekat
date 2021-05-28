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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserInterface.EntityCreateUpdaters;

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ProjekatModelDBContext dBContext;

        public MainWindow()
        {
            InitializeComponent();
            dBContext = new ProjekatModelDBContext();
            LoadAllServis();
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
                Console.WriteLine($"Greska pri dodavanju entiteta: {e.Message}");
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
                LoadAllRadnik();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Greska pri izmeni entiteta: {e.Message}");
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

        private void RadnikDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.RadnikDG.SelectedItem != null)
            {
                this.DeleteRadnikBtn.IsEnabled = true;
                this.UpdateRadnikBtn.IsEnabled = true;
            }
            else
            {
                this.DeleteRadnikBtn.IsEnabled = false;
                this.UpdateRadnikBtn.IsEnabled = false;
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
            }
            else
            {
                this.DeleteMusterijaBtn.IsEnabled = false;
                this.UpdateMusterijaBtn.IsEnabled = false;
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


    }
}
