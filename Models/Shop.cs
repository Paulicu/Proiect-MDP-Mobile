using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_MDP_Mobile.Models
{
    public class Shop
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }
    }
}
