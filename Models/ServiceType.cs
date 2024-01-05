using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Proiect_MDP_Mobile.Models
{
    public class ServiceType
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Description { get; set; }

        [OneToMany]
        public List<ListService> ListServices { get; set; }
    }
}
