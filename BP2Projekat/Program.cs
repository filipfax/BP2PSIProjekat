using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2Projekat
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new ProjekatModelDBContext())
            {
                /* public int SERV_ID { get; set; }
                public string WEB_STR { get; set; }
                public int BR_ZAP { get; set; }
                public int TELBROJ { get; set; }
                public string ADRESA { get; set; }
                public string RAD_VRM { get; set; }*/

                var servis = new SERVIS
                {
                    SERV_ID = 1,
                    WEB_STR = "www.webpage.com",
                    BR_ZAP = 10,
                    TELBROJ = 0605456,
                    ADRESA = "uluca ulicic 87",
                    RAD_VRM = "24/7"

                };

                db.SERVIS1.Add(servis);
                db.SaveChanges();

                var query = from b in db.SERVIS1
                            orderby b.SERV_ID
                            select b;

                foreach( var item in query)
                {
                    Console.WriteLine(item.SERV_ID);
                    Console.WriteLine(item.WEB_STR);
                    Console.WriteLine(item.BR_ZAP);
                    Console.WriteLine(item.TELBROJ);
                    Console.WriteLine(item.ADRESA);
                    Console.WriteLine(item.RAD_VRM);
                }
                Console.ReadLine();
            }

        }
    }
}
