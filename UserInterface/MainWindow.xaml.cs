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


    }
}
