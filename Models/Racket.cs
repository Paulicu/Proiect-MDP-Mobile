using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_MDP_Mobile.Models
{
    public class Racket
    {
        [PrimaryKey, AutoIncrement]
        public int ID {  get; set; }

        [MaxLength(150), Unique]
        public string Name { get; set; }

        [MaxLength(50), Unique]
        public string Material { get; set; }

        [MaxLength(50), Unique]
        public string Technology { get; set; }

        public decimal Weight { get; set; }

        public DateTime Edition { get; set; }
    }
}
