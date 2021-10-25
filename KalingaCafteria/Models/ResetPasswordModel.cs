using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KalingaCafteria.Models
{
    public class ResetPasswordModel
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}