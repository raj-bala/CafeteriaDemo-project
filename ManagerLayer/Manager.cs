using Entities;
using ExceptionLayer;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class Manage
    {
        Repo repObj = new Repo();
        public void InsertVendor(VendorDetailsEntity venObj, int cid)
        {
            try
            {
                repObj.InsertVendor(venObj, cid);
            }
            catch (UserExistsException)
            {
                throw;
            }
        }
        public bool Validate(string email, string pswd)
        {
            bool res = repObj.Validate(email, pswd);
            return res;
        }
        public void InsertMenu(MenuEntity menuObj)
        {
            repObj.InsertMenu(menuObj);

        }
        public List<CounterDetailsEntity> FetchData()
        {
            List<CounterDetailsEntity> res = repObj.FetchData();
            return res;
        }
        public List<MenuEntity> FetchMenu(string Email)
        {
            List<MenuEntity> res = repObj.FetchMenu(Email);
            return res;
        }
        public List<MenuEntity> FetchMenuUser(string cname)
        {
            List<MenuEntity> res = repObj.FetchMenuUser(cname);
            return res;
        }
        public List<MenuEntity> FetchOrderData(int fid)
        {
            List<MenuEntity> res = repObj.FetchOderData(fid);
            return res;
        }

        public void Delete(int id)
        {
            Repo repository = new Repo();
            repository.Delete(id);
        }

        public void AddUser(UsersEntity user)
        {
            try
            {
                repObj.AddUser(user);
            }
            catch (UserExistsException)
            {
                throw;
            }

        }
        public bool ValidateUser(string email, string password)
        {
            bool valid = repObj.ValidateUser(email, password);
            return valid;
        }
        public List<string> FetchCounter()
        {
            List<string> res = repObj.FetchCounter();
            return res;
        }
        static int total = 0;
        public int Calculate(int p, int q)
        {

            total = total + p * q;
            return total;
        }
        public string FetchUserEmail(string Email)
        {
            string email = repObj.FetchUserEmail(Email);
            return email;

        }
        public void ResetPass(string password,string Email)
        {
            repObj.ResetPass(password, Email);

        }
    }
}