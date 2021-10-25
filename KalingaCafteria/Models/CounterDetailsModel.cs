using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalingaCafteria.Models
{
    public class CounterDetailsModel
    {
        [Key]
        public int CounterID { get; set; }
        public int Availability { get; set; }
    }
}
