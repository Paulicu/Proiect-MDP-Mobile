using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Proiect_MDP_Mobile.Models
{
    public class ListService
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [ForeignKey(typeof(ServiceList))]
        public int ServiceListID { get; set; }
        public int ServiceTypeID { get; set; }

    }
}
