using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalingaCafteria.Models
{
   public class BookingModel
    {
        [Key]
        public int BookingId { get; set; }
        [ForeignKey("CounterDetailsEntity")]
        public int CID { get; set; }
        public CounterDetailsModel CounterDetails { get; set; }

        [ForeignKey("VendorDetailsEntity")]
        public int VId { get; set; }
        public VendorDetailsModel VendorDetails { get; set; }

    }
}
