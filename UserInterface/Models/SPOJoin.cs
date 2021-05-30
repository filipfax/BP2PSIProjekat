using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Models
{
    public class SPOJoin
    {

        public int MBR { get; set; }
        public int PLT { get; set; }
        public string IME { get; set; }
        public string PRZ { get; set; }
        public Nullable<int> SERVISSERV_ID { get; set; }
        public Nullable<int> RADNIKMBR { get; set; }

        public int CENA { get; set; }
        public int OSTECENJEOST_ID { get; set; }
        public int OSTECENJEMOBILNI_TELEFONMOB_ID { get; set; }
        public int SERVISERMBR { get; set; }

        public int OST_ID { get; set; }
        public string OPIS_OST { get; set; }
        public string TIP_OST { get; set; }
        public int MOBILNI_TELEFONMOB_ID { get; set; }

    }
}
