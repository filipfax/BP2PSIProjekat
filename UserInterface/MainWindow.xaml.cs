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

        public void CreateRadnik(RADNIK r)
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
        }
        #endregion Radnik

        
    }
}
