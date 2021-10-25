using Entities;
using KalingaCafteria.Models;
using Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Cafeteria.Models
{
    public class ManagerModel
    {
        Manage mangerObj = new Manage();

        public void InsertVendor(VendorDetailsModel venObj, int cid)
        {

            VendorDetailsEntity venObj1 = new VendorDetailsEntity();
            venObj1.VendorName = venObj.VendorName;
            venObj1.CounterName = venObj.CounterName; ;
            venObj1.City = venObj.City;
            venObj1.ContactNo = venObj.ContactNo;
            venObj1.Email = venObj.Email;
            venObj1.Pswd = venObj.Pswd;
            mangerObj.InsertVendor(venObj1, cid);

        }
        public List<MenuModel> FetchMenu(string Email)
        {
            Manage managerObj = new Manage();
            List<MenuEntity> res = managerObj.FetchMenu(Email);
            List<MenuModel> res1 = res.Select(s => new MenuModel
            {
                ItemId = s.ItemId,
                ItemName = s.ItemName,
                CounterID = s.CounterID,
                Itemdescription = s.Itemdescription,
                Price = s.Price,



            }).ToList();

            return res1;
        }
        public List<MenuModel> FetchMenuUser(string cname)
        {
            Manage managerObj = new Manage();
            List<MenuEntity> res = managerObj.FetchMenuUser(cname);
            List<MenuModel> res1 = res.Select(s => new MenuModel
            {
                ItemId = s.ItemId,
                ItemName = s.ItemName,
                CounterID = s.CounterID,
                Itemdescription = s.Itemdescription,
                Price = s.Price,



            }).ToList();

            return res1;
        }
        public void DeleteFile(string path)
        {
            File.Delete(path);
        }

        public void Delete(int id)
        {
            Manage manager = new Manage();
            manager.Delete(id);
        }

        public ArrayList ReadFile(string path)
        {
            ArrayList arryList = new ArrayList();
            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines)
            {
                arryList.Add(line);
            }
            return arryList;


        }
        public List<MenuModel> FetchOrderData(int fid)
        {
            Manage managerObj = new Manage();
            List<MenuEntity> res = managerObj.FetchOrderData(fid);
            List<MenuModel> res1 = res.Select(s => new MenuModel
            {
                ItemId = s.ItemId,
                ItemName = s.ItemName,
                CounterID = s.CounterID,
                Itemdescription = s.Itemdescription,
                Price = s.Price,



            }).ToList();

            return res1;
        }


        public bool Validate(string email, string pswd)
        {
            bool res = mangerObj.Validate(email, pswd);
            return res;
        }
        public void Menu(MenuModel menuObj)
        {
            MenuEntity ob = new MenuEntity();
            ob.CounterID = menuObj.CounterID;
            ob.ItemName = menuObj.ItemName;
            ob.Itemdescription = menuObj.Itemdescription;
            ob.Price = menuObj.Price;
            mangerObj.InsertMenu(ob);
        }
        public List<CounterDetailsModel> FetchData()
        {
            List<CounterDetailsEntity> res = mangerObj.FetchData();
            List<CounterDetailsModel> res1 = res.Select(s => new CounterDetailsModel
            {
                CounterID = s.CounterID,
                Availability = s.Availability,


            }).ToList();

            return res1;
        }
        public void AddUser(UsersModel user)
        {
            Manage manObj = new Manage();
            UsersEntity ob = new UsersEntity();
            ob.UserId = user.UserId;
            ob.UserName = user.UserName;
            ob.ContactNo = user.ContactNo;
            ob.ContactNo = user.ContactNo;
            ob.Email = user.Email;
            ob.Pswd = user.Pswd;
            manObj.AddUser(ob);


        }

        public bool ValidateUser(string email, string password)
        {
            Manage manObj = new Manage();
            bool valid = manObj.ValidateUser(email, password);
            return valid;
        }

        public List<string> FetchCounter()
        {
            List<string> res = mangerObj.FetchCounter();

            return res;
        }

        public int Calculate(int p, int q)
        {
            Manage manObj = new Manage();
            int res = manObj.Calculate(p, q);
            return res;
        }
        public string FetchUserEmail(string Email)
        {
            Manage manObj = new Manage();
            string email = mangerObj.FetchUserEmail(Email);
            return email;
        }
        public void ResetPass(ResetPasswordModel model,string Email)
        {
            Manage manObj = new Manage();
            manObj.ResetPass(model.Password, Email);

        }
    }
}