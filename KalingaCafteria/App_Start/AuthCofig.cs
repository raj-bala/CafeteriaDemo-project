using DotNetOpenAuth.GoogleOAuth2;
using Microsoft.AspNet.Membership.OpenAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KalingaCafteria.App_Start
{
    public class AuthCofig
    {
        public static void RegisterAuth()
        {
            GoogleOAuth2Client clientGoog = new GoogleOAuth2Client("1042836808184-cmebgdffkoue0p4iqr3ucgjebpaq9f9v.apps.googleusercontent.com", "Tehh2ZqgyWHCSumcTZ-Y5dXO");
            IDictionary<string, string> extraData = new Dictionary<string, string>();
            OpenAuth.AuthenticationClients.Add("google", () => clientGoog, extraData);
        }
    }
}