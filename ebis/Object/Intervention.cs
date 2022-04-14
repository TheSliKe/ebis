using System;

namespace Ebis.Object
{
    class Intervention
    {
        public string numeroInter { get; set; }
        public string typeInter { get; set; }
        public DateTime dateDebut { get; set; }
        public DateTime dateFin { get; set; }
        public string detailInter { get; set; }
        public string secteur { get; set; }
        public string technicien { get; set; }
        public string borne { get; set; }
    }
}
