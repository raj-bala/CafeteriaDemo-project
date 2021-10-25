using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class MenuEntity
    {
        [Key]
        public int ItemId { get; set; }
        [ForeignKey("CounterDetailsEntity")]
        public int CounterID { get; set; }public CounterDetailsEntity CounterDetailsEntity { get; set; }

        public string ItemName { get; set; }
        [StringLength(30)]
        public string Itemdescription { get; set; }
        public int Price { get; set; }

    }
}
