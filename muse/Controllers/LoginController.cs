using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Text;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Newtonsoft.Json;

using System.Data;
using System.Data.Entity;

using System.Net;

using muse.Models;

using Newtonsoft.Json.Linq;
namespace muse.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            Session["account"] = 0;
            ViewBag.test = Session["account"];
            return View();
        }


        private Entities5 db = new Entities5();

        //[HttpPost]
        //public ActionResult Index(int id, string password)
        //{
        //    id = 3;
        //    password = "57684856";


        //    var isuser = db.Database.SqlQuery<int>("select USERID from MUSER where USERID=" + id).FirstOrDefault();
        //    var _password = db.Database.SqlQuery<string>("select USERPASSWORD from MUSER where USERID=" + id.ToString()).FirstOrDefault();

        //    ViewBag._password = _password;

        //    if (isuser == 0)
        //    {
        //        ViewBag.check = 0;
        //        return View();
        //    }
        //    else
        //    {
        //        ViewBag._password = _password;

        //        if (password.CompareTo(_password) == 0)
        //        {
        //            ViewBag.check = 1;
        //            return View();
        //        }
        //        else
        //        {
        //            ViewBag.check = 0;
        //            return View();
        //        }
        //    }
        //}
        public class LoginSender
        {
            public string account { get; set; }
            public string password { get; set; }
        }

        public static string GetMD5(string str)
        {
            byte[] b = System.Text.Encoding.Default.GetBytes(str);
            b = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(b);
            string ret = " ";
            for (int i = 0; i < b.Length; i++)
            {
                ret += b[i].ToString("x").PadLeft(2, '0');
            }
            return ret;
        }

        [HttpPost]
        public ActionResult Index(LoginSender msg)
        {
    
            MUSER temp = new MUSER();
            if (msg == null)
            {
                ViewBag.Error = "Empty account";
            }
            else
            {
                var isuser = db.Database.SqlQuery<int>("select USERID from MUSER where USERID=" +msg.account).FirstOrDefault();
                var _password = db.Database.SqlQuery<string>("select USERPASSWORD from MUSER where USERID=" + msg.account).FirstOrDefault();

                ViewBag._password = _password;

                if (isuser == 0)
                {
                    ViewBag.Error = "Account wrong";
                    return View();
                }
                else
                {
                    ViewBag._password = _password;

                    if (msg.password.CompareTo(_password) == 0)
                    {
                        // Store userID into session
                        //HttpContext.Session["account"] = msg.account;
                        HttpContext.Session.Add("account", isuser);
                        //Object s = HttpContext.Session["account"];
                        ViewBag.check = 1;
                      
                        return Redirect("~/Home/Index");
                    }
                    else
                    {
                        ViewBag.Error = "Account or password wrong!";
                        return View();
                    }
                }
            }

            return Redirect("~/Home/Index");
        }

        public class CreateSender
        {
            public string username { get; set; }
            public string password { get; set; }
            public string _password { get; set; }
            public string email { get; set; }
            public string birthday { get; set; }
            public string sex { get; set; }
        }

        public ActionResult Create()
        {
            ViewBag.USERID = new SelectList(db.VIP, "VIPID", "EXPIRETIME");
            ViewBag.USERID = new SelectList(db.MUSICIAN, "USERID", "MUSICIANINTRO");
            return View();
        }

        //[HttpPost]
        //public ActionResult Create(CreateSender msg)
        //{

        //    var a = String.Compare(msg.password, msg._password);
        //    if (a!=0)
        //    {
        //        ViewBag.createresult = "ISNOTPASSWORD";
        //        return View();
        //    }
        //    else
        //    { 
        //    var muser = new MUSER();
        //        db.MUSER.Add(muser);
        //        var maxid = db.Database.SqlQuery<int>("select max(USERID) from MUSER").FirstOrDefault();
        //    maxid++;//新的user的ID
        //    muser.USERID = maxid;
        //    muser.USERNAME = msg.username;
        //    muser.USERPASSWORD = msg.password;
        //    muser.USEREMAIL = msg.email;
        //    muser.USERSEX = msg.sex;
        //    muser.USERBIRTHDAY = msg.birthday;
        //    muser.USERIMAGE = "";
        //    //db.MUSER.Add(muser);
        //    ViewBag.id = maxid;
        //    db.SaveChanges();
        //    ViewBag.createresult = "SUCCESS";
        //    return RedirectToAction("/Home/Myinfo");
        //    }
        //}

        // POST: MUSERs/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "USERID,USERNAME,USERPASSWORD,USERIMAGE,USERSEX,USERBIRTHDAY,USEREMAIL")] MUSER mUSER)
        {


            if (mUSER.USERPASSWORD.Length<17&& mUSER.USERPASSWORD.Length>7)
            {
                if (ModelState.IsValid)
                {
                    var maxid = db.Database.SqlQuery<int>("select max(USERID) from MUSER").FirstOrDefault();
                    maxid++;//新的user的ID
                    mUSER.USERID = maxid;
                    db.MUSER.Add(mUSER);
                    ViewBag.id = maxid;
                    db.SaveChanges();
                    Session["account"] = maxid;
                    ViewBag.createresult = "SUCCESS";

                    var songlist = new SONGLIST();
                    var max = db.Database.SqlQuery<int>("select max(SONGLISTID) from SONGLIST").FirstOrDefault();
                    max++;//新的user的ID
                    songlist.SONGLISTID = max;
                    songlist.SONGLISTNAME = "我喜欢";
                    songlist.USERID = mUSER.USERID;
                    songlist.COLLECTIONTIMES = 0;
                    db.SONGLIST.Add(songlist);
                    db.SaveChanges();

                    var usersonglist = new USERSONGLIST();
                    usersonglist.USERID = mUSER.USERID;
                    usersonglist.SONGLISTID = max;
                    db.USERSONGLIST.Add(usersonglist);
                    db.SaveChanges();


                    return RedirectToAction("Myinfo","Home");
                   
                }

                ViewBag.USERID = new SelectList(db.VIP, "VIPID", "EXPIRETIME", mUSER.USERID);
                ViewBag.USERID = new SelectList(db.MUSICIAN, "USERID", "MUSICIANINTRO", mUSER.USERID);

               
                return View(mUSER);
            }
            else
            {
                ViewBag.createresult = "ISNOTPASSWORD";
                return View();

            }


        }


        public ActionResult Details()
        {
           //var id = Session["accout"].ToString();

            //var accout = int.Parse(id);
            //ViewBag.id = accout;

            //var name = from t in db.MUSER
            //           where t.USERID == accout
            //           select t.USERNAME;

            //ViewBag.name = name;

            return View();
        }
    }
}