using Cafeteria.Models;
using DotNetOpenAuth.GoogleOAuth2;
using ExceptionLayer;
using KalingaCafteria.Models;
using Microsoft.AspNet.Membership.OpenAuth;
using RepositoryLayer;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace KalingaCafteria.Controllers
{
    public class CafeteriaController : Controller
    {

        public ActionResult HomePage()
        {
            FormsAuthentication.SignOut();
            return View();
        }
        public ActionResult CounterDisplay()
        {
            ManagerModel manObj = new ManagerModel();
            List<string> list = manObj.FetchCounter();
            return View(list);
        }

        public ActionResult MenuList(string cname)
        {
            ManagerModel manObj = new ManagerModel();
            List<MenuModel> list = manObj.FetchMenuUser(cname);
            return View(list);
        }
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult VendorRegister(CounterDetailsModel counter)
        {
            ManagerModel manObj = new ManagerModel();
            List<CounterDetailsModel> ob = manObj.FetchData();
            List<CounterDetailsModel> dt2 = new List<CounterDetailsModel>();

            return View(ob);
        }
        public ActionResult VendorSignUp(int counterId)
        {
            int cid = counterId;
            Session["CurrentCounterId"] = cid;
            return View();
        }
        [HttpPost]
        public ActionResult VendorSignupSuccess(VendorDetailsModel vendor)
        {
            string cid1 = Session["CurrentCounterId"].ToString();
            int cid = int.Parse(cid1);
            ManagerModel model = new ManagerModel();

            try
            {
                model.InsertVendor(vendor, cid);
                ViewBag.Message2 = "Successfully Registered";
            }
            catch (UserExistsException ue)
            {
                ViewBag.Exception = ue.Message;
                return View("VendorSignUp");
            }


            return View("VendorSignUp");
        }
        public ActionResult VendorLogin()
        {

            return View();
        }

        [HttpPost]
        public ActionResult VendorLogin(VendorDetailsModel model)
        {
            Session["CurrentEmail"] = model.Email;
            ManagerModel manager = new ManagerModel();

            bool isValid = manager.Validate(model.Email, model.Pswd);
            bool isValidUser = manager.ValidateUser(model.Email, model.Pswd);

            if (isValid)
            {
                FormsAuthentication.SetAuthCookie(model.Email, true);
                return RedirectToAction("MenuListVendor", "Cafeteria");
            }
            if (isValidUser)
            {
                FormsAuthentication.SetAuthCookie(model.Email, true);
                return RedirectToAction("CounterDisplayUser", "Cafeteria");
            }
            else
            {
                ModelState.AddModelError("", "Invalid username and password");
                ViewBag.Message = "Invalid Login attempt";
                return View();
            }
        }
        [Authorize]
        public ActionResult CounterDisplayUser()
        {
            ManagerModel manObj = new ManagerModel();
            List<string> list = manObj.FetchCounter();
            return View(list);
        }
        [Authorize]
        public ActionResult MenuListUser(string cname)
        {
            //string cname = Session["CounterName"].ToString();
            ManagerModel manObj = new ManagerModel();
            List<MenuModel> list = manObj.FetchMenuUser(cname);
            if (list.Count == 0)
            {
                ViewBag.Message = "No item in this Counter";
                return View();
            }
            return View(list);
        }
        [Authorize]
        public ActionResult ItemPurchase(int fid, int price)
        {
            Session["Currentfid"] = fid;
            Session["Price"] = price;

            //ManagerModel model = new ManagerModel();

            return View("FoodItemQuantity");
        }
        [HttpPost]
        [Authorize]
        public ActionResult FoodItemQuantity(FormCollection form)
        {

            string qty = form["Quantity"];

            int quantity = int.Parse(qty);
            Session["Currentquant"] = quantity;

            return RedirectToAction("GetFoodBill");
        }
        [Authorize]
        public ActionResult GetFoodBill()
        {
            ManagerModel manObj = new ManagerModel();

            string q = Session["Currentquant"].ToString();
            int quantity = int.Parse(q);
            ViewBag.Name = quantity;
            string fid1 = Session["Currentfid"].ToString();
            int fid = int.Parse(fid1);

            string p = Session["Price"].ToString();
            int price = int.Parse(p);

            List<MenuModel> list = manObj.FetchOrderData(fid);

            //To store the History of orders of customer
            string path = @"D:\ProjectBill.txt";

            FileStream fs1 = new FileStream(path, FileMode.Append, FileAccess.Write);
            using (StreamWriter sw = new StreamWriter(fs1))
            {
                foreach (MenuModel item in list)
                {
                    sw.WriteLine("{0} \t {1} \t {2}", item.ItemName, item.Price, q);
                }
            }
            int total = manObj.Calculate(price, quantity);
            Session["TotalCost"] = total;
            fs1.Close();
            return View();
        }
        [Authorize]

        public ActionResult PrintFoodBill()
        {
            ManagerModel ob = new ManagerModel();
            string tot = Session["TotalCost"].ToString();
            int bill = int.Parse(tot);
            ViewBag.Message = bill;
            string path = @"D:\ProjectBill.txt";
            ArrayList lines = ob.ReadFile(path);
            ViewBag.Name = lines;
            return View();
        }

        public ActionResult Logout()
        {
            ManagerModel ob = new ManagerModel();
            string path = @"D:\ProjectBill.txt";
            ob.DeleteFile(path);
            FormsAuthentication.SignOut();
            return RedirectToAction("VendorLogin");
        }
        //Todays code
        [Authorize]
        public ActionResult MenuListVendor()
        {
            string email = Session["CurrentEmail"].ToString();
            ManagerModel ob = new ManagerModel();
            List<MenuModel> menObj = ob.FetchMenu(email);
            if (menObj.Count == 0)
                return View();
            return View(menObj);
        }
        //add menu
        [Authorize]
        public ActionResult VendorMenuList()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult MenuListSuccess(MenuModel menu)
        {
            //save menu to data base

            ManagerModel manager = new ManagerModel();
            manager.Menu(menu);
            ViewBag.Message = "Successfully added";
            return View("VendorMenuList");
        }

        
        [Authorize]
        public ActionResult DeleteMenuDet()
        {
            return View();
        }
        [Authorize]
        public ActionResult UpdateMenuDet()
        {
            return View();
        }


        [Authorize]
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(FormCollection form)
        {
            ManagerModel manager = new ManagerModel();
            string id1 = form["ItemId"];

            int id = int.Parse(id1);
            manager.Delete(id);
            return RedirectToAction("MenuListVendor");
        }


        //User part

        public ActionResult UserSignUp()
        {
            return View();
        }
        [HttpPost]

        public ActionResult UserSignupSuccess(UsersModel user)
        {
            ManagerModel manObj = new ManagerModel();
            try
            {
                manObj.AddUser(user);
                ViewBag.Message3 = "User Successfully Registered";
            }
            catch (UserExistsException ue)
            {
                ViewBag.Exception1 = ue.Message;
                return View();
            }

            return View("UserSignUp");
        }
        public ActionResult AboutCafeteria()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult PhotoGallery()
        {

            return View();
        }
        public ActionResult PhotoGalleryPic()
        {
            return View();
        }

        //Google Login
        public ActionResult RedirectToGoogle()
        {
            string provider = "google";
            string returnUrl = "";
            return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
        }
        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OpenAuth.RequestAuthentication(Provider, ReturnUrl);
            }
        }
        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            string ProviderName = OpenAuth.GetProviderNameFromCurrentRequest();

            if (ProviderName == null || ProviderName == "")
            {
                NameValueCollection nvs = Request.QueryString;
                if (nvs.Count > 0)
                {
                    if (nvs["state"] != null)
                    {
                        NameValueCollection provideritem = HttpUtility.ParseQueryString(nvs["state"]);
                        if (provideritem["__provider__"] != null)
                        {
                            ProviderName = provideritem["__provider__"];
                        }
                    }
                }
            }

            GoogleOAuth2Client.RewriteRequest();

            var redirectUrl = Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl });
            var retUrl = returnUrl;
            var authResult = OpenAuth.VerifyAuthentication(redirectUrl);

           

            
           

            //Get provider user details
            string ProviderUserId = authResult.ProviderUserId;
            string ProviderUserName = authResult.UserName;

            string Email = null;
            if (Email == null && authResult.ExtraData.ContainsKey("email"))
            {
                Email = authResult.ExtraData["email"];
            }
            FormsAuthentication.SetAuthCookie(Email, false);
            return RedirectToAction("CounterDisplayUser");

        }
        //Forgot password
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string EmailID)
        {
            ManagerModel manObj = new ManagerModel();
            string message = "";
            TempData["Email"] = EmailID;

            using (RepoContext dc = new RepoContext())
            {
                var account = manObj.FetchUserEmail(EmailID);
                if (account != null)
                {

                    SendVerificationLinkEmail(account, "ResetPassword");


                    dc.SaveChanges();
                    message = "Reset password link has been sent to your email id.";
                }
                else
                {
                    message = "Account not found";
                }
            }
            ViewBag.Message = message;
            return View();
        }
        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string emailFor)
        {


            var link = "https://localhost:44397/Cafeteria/ResetPassword";
            var fromEmail = new MailAddress("vinuthademo@gmail.com");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "vinuthademo123"; // Replace with actual password

            string subject = "";
            string body = "";

            if (emailFor == "ResetPassword")
            {
                subject = "Reset Password";
                body = "Hi,We got request for reset your account password. Please click on the below link to reset your password" +
                    "<a href=" + link + ">Reset Password link</a>";
            }


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            ManagerModel manObj = new ManagerModel();
            var EmailId = (string)TempData["Email"];
            manObj.ResetPass(model, EmailId);
            ViewBag.Message = "New password updated successfully";



            return View();
            

        }

    }
}