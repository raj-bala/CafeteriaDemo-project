using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalingaCafteria.Models
{
   public class UsersModel
    {
        [Key]
        public int UserId { get; set; }
        [StringLength(20)]
        public string UserName { get; set; }
        [StringLength(10)]
        public string ContactNo { get; set; }
        [StringLength(20)]
        public string Email { get; set; }

        public string Pswd { get; set; }
    }
}
