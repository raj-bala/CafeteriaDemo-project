using Entities;
using ExceptionLayer;
using Moq;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Repo
    {
        RepoContext db = new RepoContext();
        public List<CounterDetailsEntity> FetchData()
        {
            var a = (from o in db.CounterDetails

                     select o).ToList();


            return a;
        }

        public void Delete(int id)
        {
            RepoContext context = new RepoContext();
            MenuEntity menu = context.Menu.Find(id);
            context.Menu.Remove(menu);
            context.SaveChanges();
        }
        public void InsertVendor(VendorDetailsEntity venObj, int cid)
        {
            var vendorCheck = db.VendorDetails.Where(v => v.Email == venObj.Email).FirstOrDefault();
            var vendorCheck1 = db.Users.Where(v => v.Email == venObj.Email).FirstOrDefault();
            if (vendorCheck != null || vendorCheck1 != null)
            {
                throw new UserExistsException("That Email Already got registered with the cafeteria");
            }

            db.VendorDetails.Add(venObj);
            db.SaveChanges();
            var vid = (from o in db.VendorDetails
                       where o.VendorId == venObj.VendorId
                       select o.VendorId).FirstOrDefault();
            var CId = (from p in db.CounterDetails
                       where p.CounterID == cid
                       select p.CounterID).FirstOrDefault();

            BookingEntity booObj = new BookingEntity();
            booObj.CID = CId;
            booObj.VId = vid;
            db.Booking.Add(booObj);


            db.SaveChanges();
            var x = db.CounterDetails.Where(a => a.CounterID == cid).FirstOrDefault();
            x.Availability = 0;
            db.SaveChanges();
        }
        public List<MenuEntity> FetchMenu(string Email)
        {
            var a = (from o in db.Menu
                     join p in db.Booking
                     on o.CounterID equals p.CID
                     join q in db.VendorDetails
                     on p.VId equals q.VendorId
                     where q.Email == Email

                     select o).ToList();


            return a;
        }
        public bool ValidateMenu(string Email)
        {
            var a = (from o in db.Menu
                     join p in db.Booking
                     on o.CounterID equals p.CID
                     join q in db.VendorDetails
                     on p.VId equals q.VendorId
                     where q.Email == Email

                     select o).Count();
            int b = (int)a;
            if (b>0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        public List<MenuEntity> FetchMenuUser(string cname)
        {
            var a = (from o in db.Menu
                     join p in db.Booking
                     on o.CounterID equals p.CID
                     join q in db.VendorDetails
                     on p.VId equals q.VendorId
                     where q.CounterName == cname

                     select o).ToList();


            return a;
        }
        public List<MenuEntity> FetchOderData(int fid)
        {
            var a = (from o in db.Menu
                     join p in db.Booking
                     on o.CounterID equals p.CID
                     join q in db.VendorDetails
                     on p.VId equals q.VendorId
                     where o.ItemId == fid

                     select o).ToList();


            return a;
        }

        public bool Validate(string Email, string Pswd)
        {
            bool var = db.VendorDetails.Any(x => x.Email == Email && x.Pswd == Pswd);
            return var;
        }
        public bool Validate(string Email, string Pswd,Mock<RepoContext> mock)
        {
            bool var = mock.Object.VendorDetails.Any(x => x.Email == Email && x.Pswd == Pswd);
            return var;
        }


        public void InsertMenu(MenuEntity menuObj)
        {
            db.Menu.Add(menuObj);
            db.SaveChanges();
        }

        public void AddUser(UsersEntity user)
        {
            var userCheck = db.Users.Where(x => x.Email == user.Email).FirstOrDefault();
            var userCheck1 = db.VendorDetails.Where(x => x.Email == user.Email).FirstOrDefault();
            if (userCheck != null || userCheck1 != null)
            {
                throw new UserExistsException("That Email Already got registered with the cafeteria");
            }
            db.Users.Add(user);
            db.SaveChanges();
        }
        public bool ValidateUser(string email, string password)
        {
            bool isValid = db.Users.Any(x => x.Email == email && x.Pswd == password);
            return isValid;
        }
        public List<string> FetchCounter()
        {
            var a = (from o in db.VendorDetails


                     select o.CounterName).ToList();


            return a;
        }
        public string FetchUserEmail(string Email)
        {
            var account = db.Users.Where(x => x.Email == Email).FirstOrDefault();
            string email = account.Email;

            return email;
        }
        public void ResetPass(string password,string Email)
        {
            var account = db.Users.Where(x => x.Email == Email).FirstOrDefault();
            account.Pswd = password;
            db.SaveChanges();
           
        }
    }
}
