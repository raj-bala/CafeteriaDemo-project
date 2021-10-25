using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class VendorDetailsEntity
    {
        [Key]
        public int VendorId { get; set; }
        [StringLength(20)]
        public string VendorName { get; set; }
        [StringLength(20)]
        public string CounterName { get; set; }
        [StringLength(10)]
        public string City { get; set; }

        [StringLength(20)]
        public string Email { get; set; }

        [StringLength(10)]
        public string ContactNo { get; set; }
        [StringLength(15)]
        public string Pswd { get; set; }

    }
}
