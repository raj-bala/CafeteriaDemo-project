using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalingaCafteria.Models
{
    public class MenuModel
    {
        [Key]
        public int ItemId { get; set; }
       // [ForeignKey("CounterDetails")]
        public int CounterID { get; set; }
        //public CounterDetails CounterDetails { get; set; }

        public string ItemName { get; set; }
        [StringLength(30)]
        public string Itemdescription { get; set; }
        public int Price { get; set; }

    }
}
