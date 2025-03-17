using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_jeux
{
    internal class User
    {
        public int id { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string Mot_de_passe { get; set; }
        public string email { get; set; }
        public int role_id { get; set; }
        public int date_created { get; set; }

    }
}
