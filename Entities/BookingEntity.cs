using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
   public class BookingEntity
    {
        [Key]
        public int BookingId { get; set; }
        [ForeignKey("CounterDetailsEntity")]
        public int CID { get; set; }
        public CounterDetailsEntity CounterDetailsEntity { get; set; }

        [ForeignKey("VendorDetailsEntity")]
        public int VId { get; set; }
        public VendorDetailsEntity VendorDetailsEntity { get; set; }

    }
}
