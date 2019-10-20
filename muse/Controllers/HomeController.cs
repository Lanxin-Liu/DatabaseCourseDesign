using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    public class HomeController : Controller
    {
        private Entities5 db = new Entities5();
        //public class JsonMessage
        //{
        //    //状态
        //    private bool _statues;

        //    public bool status
        //    {
        //        get { return _statues; }
        //        set { _statues = value; }
        //    }
        //    //消息
        //    private string _msg;

        //    public string msg
        //    {
        //        get { return _msg; }
        //        set { _msg = value; }
        //    }
        //    //数据
        //    private object _data;
        //    public object data
        //    {
        //        get { return _data; }
        //        set { _data = value; }
        //    }
        //}

        public ActionResult Index()
        {
           string[] href= { "1", "2", "3" };
            ViewBag.a = "/Home/Myinfo";


            object s = HttpContext.Session["account"];
            var recom = from rec in db.HISTORY
                        join mus in db.MUSIC
                            on rec.MUSICID equals mus.MUSICID
                        where rec.USERID == (int)s
                        orderby rec.PLAYEDTIMES
                        select mus;
            List<int> songID = new List<int>();
            List<string> songName = new List<string>();
            if (recom.Count() < 3)
            {
                var ran = from rec in db.MUSIC
                          orderby rec.PLAYTIMES ascending
                          select rec;
                foreach (var reccom in ran)
                {
                    songID.Add((int)reccom.MUSICID);
                    songName.Add(reccom.MUSICNAME);
                }
            }
            else
            {
                foreach (var reccom in recom)
                {
                    songID.Add((int)reccom.MUSICID);
                    songName.Add(reccom.MUSICNAME);
                }
            }

            List<int> boardID = new List<int>();
            List<string> boardName = new List<string>();

            var board = from mus in db.MUSIC
                        orderby mus.PLAYTIMES descending
                        select mus;
            foreach (var boa in board)
            {
                boardID.Add((int)boa.MUSICID);
                boardName.Add(boa.MUSICNAME);
            }
            ViewBag.ID = songID;
            ViewBag.Name = songName;
            List<string> Address = new List<string>();
            foreach (int id in songID)
            {
                Address.Add("/save_cover/" + id + ".jpg");
            }
            ViewBag.img = Address;


            ViewBag.SongID = boardID;
            ViewBag.SongName = boardName;
            return View();


        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        //[HttpGet]
        //public JsonResult getCount()
        //{
        //    String connection = "User Id=system;Password=tkh603;Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))" + "(CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = orcl)))";
        //    decimal customerCount, homestayCount, activityCount, reviewCount;
        //    using (OracleConnection conn = new OracleConnection(connection))
        //    {
        //        conn.Open();
        //        OracleCommand cmd = new OracleCommand("select count(*) from users", conn);//执行一条SQL语句
        //        customerCount = (decimal)cmd.ExecuteScalar();
        //        cmd = new OracleCommand("select count(*) from Homestay", conn);//执行一条SQL语句
        //        homestayCount = (decimal)cmd.ExecuteScalar();
        //        cmd = new OracleCommand("select count(*) from Activity", conn);//执行一条SQL语句
        //        activityCount = (decimal)cmd.ExecuteScalar();
        //        cmd = new OracleCommand("select count(*) from Comments", conn);//执行一条SQL语句
        //        reviewCount = (decimal)cmd.ExecuteScalar();
        //        conn.Close();
        //    }
        //    JsonMessage m = new JsonMessage();
        //    var jsonData = "{ \"customerCount\":\"" + customerCount.ToString() + "\", \"homestayCount\":\"" + homestayCount.ToString() + "\", \"activityCount\":\"" + activityCount.ToString() + "\", \"reviewCount\":\"" + reviewCount.ToString() + "\"}";
        //    m.status = true;
        //    m.msg = "return Success";
        //    m.data = jsonData;
        //    return Json(m, JsonRequestBehavior.AllowGet);
        //}


        //public /*ActionResult*/ void Index(FormCollection form)
        //{

        //    var data = new MUSER();
        //    //{
        //    //    USERID = form["form-username"],
        //    //    //head_icon = "../images/headicon/default.png",
        //    //    //first_name = form["form-first-name"],
        //    //    //last_name = form["form-last-name"],
        //    //    //pass_word = GetMD5(form["form-password"]),
        //    //    //gender = Convert.ToInt16(form["form-gender"]),
        //    //    //email_address = form["form-email"],
        //    //    //phone_number = form["form-phoneNumber"],
        //    //    //main_language = form["form-language"],
        //    //    //country = form["form-country"],
        //    //    //self_introduction = form["form-about-yourself"],
        //    //    //bonus_points = 0
        //    //};

        //    //int id = db.Database.SqlQuery<int>("select max(ID) from MUSER").FirstOrDefault();
        //    int id = 0;
        //    id++;//新的书的ID
        //    data.USERID = id;
        //    data.USERNAME = form["form-name"];
        //    data.USERSEX = form["form-sex"];
        //    data.USEREMAIL = form["form-email"];
        //    //data.USERBIRTHDAY = form["form-birthday"];
        //    data.USERPASSWORD = GetMD5(form["form-password"]);

        //    db.MUSER.Add(data);
        //    //db.SaveChanges();
        //    if (db.SaveChanges() == 1)
        //    {
        //        ViewBag.errorMessage = "注册成功,欢迎探索Adventure!";
        //        ViewBag.flag = 1;
        //        //return View();
        //    }
        //    else
        //    {
        //        ViewBag.errorMessage = "注册失败!";
        //        ViewBag.flag = 0;
        //        //return View();
        //    }
        //}


        //return Content(JsonConvert.SerializeObject(isSuccess, Formatting.Indented));

        //public static string GetMD5(string str)
        //{
        //    byte[] b = System.Text.Encoding.Default.GetBytes(str);
        //    b = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(b);
        //    string ret = " ";
        //    for (int i = 0; i < b.Length; i++)
        //    {
        //        ret += b[i].ToString("x").PadLeft(2, '0');
        //    }
        //    var a = 0;
        //    return ret;
        //}


        public bool loginin(int id, string password)
        {
            id = 3;
            password = "57684856";


            var isuser = db.Database.SqlQuery<int>("select USERID from MUSER where USERID=" + id).FirstOrDefault();
            var _password = db.Database.SqlQuery<string>("select USERPASSWORD from MUSER where USERID=" + id.ToString()).FirstOrDefault();

            ViewBag._password = _password;

            if (isuser == 0)
            {
                ViewBag.check = 0;
                return false;
            }
            else
            {
                ViewBag._password = _password;

                if (password.CompareTo(_password) == 0)
                {
                    ViewBag.check = 1;
                    return true;
                }
                else
                {
                    ViewBag.check = 0;
                    return false;
                }
            }
        }

        //    //连接数据库

        //    //using (OracleConnection conn = new OracleConnection(connection))
        //    //{
        //    //    conn.Open();
        //    //    var _password = db.Database.SqlQuery<string>("select USERPASSWORD  from MUSER where USERID=" + id).FirstOrDefault();
        //    //    //OracleCommand cmd = new OracleCommand("select USERPASSWORD from MUSER where USERID=", conn);//执行一条SQL语句
        //    //    customerCount = (decimal)cmd.ExecuteScalar();
        //    //    cmd = new OracleCommand("select count(*) from Homestay", conn);//执行一条SQL语句
        //    //    homestayCount = (decimal)cmd.ExecuteScalar();
        //    //    cmd = new OracleCommand("select count(*) from Activity", conn);//执行一条SQL语句
        //    //    activityCount = (decimal)cmd.ExecuteScalar();
        //    //    cmd = new OracleCommand("select count(*) from Comments", conn);//执行一条SQL语句
        //    //    reviewCount = (decimal)cmd.ExecuteScalar();
        //    //    conn.Close();
        //    //}

        //    //    JsonMessage m = new JsonMessage();
        //    //    var jsonData = "{ \"customerCount\":\"" + customerCount.ToString() + "\", \"homestayCount\":\"" + homestayCount.ToString() + "\", \"activityCount\":\"" + activityCount.ToString() + "\", \"reviewCount\":\"" + reviewCount.ToString() + "\"}";
        //    //    m.status = true;
        //    //    m.msg = "return Success";
        //    //    m.data = jsonData;
        //    //    return Json(m, JsonRequestBehavior.AllowGet);

        //    //    return true;
        //    //}
        //}

        //public bool signin(string name, string password, string sex, string email, string birthday)
        //{


        //    MUSER newmuser = new Models.MUSER();
        //    var maxid = db.Database.SqlQuery<int>("select max(USERID) from MUSER").FirstOrDefault();
        //    maxid++;//新的书的ID
        //    newmuser.USERID = maxid;
        //    newmuser.USERNAME = name;
        //    newmuser.USERPASSWORD = password;
        //    newmuser.USERSEX = sex;
        //    newmuser.USEREMAIL = email;
        //    newmuser.USERBIRTHDAY = birthday;


        //    db.MUSER.Add(newmuser);
        //    db.SaveChanges();

        //    return true;
        //}

        //public bool user_renew(int id, string name, string password, string sex, string email, string birthday)
        //{

        //    var isuser = db.Database.SqlQuery<int>("select USERID from MUSER where USERID=" + id).FirstOrDefault();
        //    if (isuser == null)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        var user = db.MUSER.Find(id);
        //        user.USERID = id;
        //        user.USERNAME = name;
        //        user.USERPASSWORD = password;
        //        user.USERSEX = sex;
        //        user.USEREMAIL = email;
        //        //newmuser.USERBIRTHDAY = birthday

        //        db.SaveChanges();
        //    }

        //    return true;
        //}

        //public void search()
        //{


        //}
        //public void rank()
        //{



        //}
        //public bool submitlyric(int id,string lyricpath)
        //{
        //    var music = db.MUSIC.Find(id);
        //    music.LYRICS= lyricpath;

        //    return true;
        //}


        //public void newsonglist(string SongListName,int CreatorID)//添加歌单
        //{
        //    SONGLIST  newsonglist = new Models.SONGLIST();
        //    var maxid = db.Database.SqlQuery<int>("select max(SONGLISTID) from SONGLIST").FirstOrDefault();
        //    maxid++;//新的书的ID
        //    newsonglist.SONGLISTID = maxid;
        //    newsonglist.SONGLISTNAME = SongListName;
        //    newsonglist.USERID = CreatorID;
        //    newsonglist.COLLECTIONTIMES = 0;
        //    db.SONGLIST.Add(newsonglist);
        //    db.SaveChanges();
        //}
        //public void deletesonglist(int songlistID)//删除歌单
        //{
        //    var songlist = db.SONGLIST.Find(songlistID);
        //    db.SONGLIST.Remove(songlist);
        //    db.SaveChanges();
        //}

        //public class SONGLISTMUSIC
        //{
        //    public int SongListID{ get; set; }
        //    public int MusicID { get; set; }
        //}
        //public void managesonglist(int SongListID, int MusicID)//管理歌单
        //{


        //    db.SONGLIST.Remove();
        //    db.SONGLISTMUSIC.Remove(SongListID, MusicID);
        //    db.SaveChanges();
        //}
        //public void addtosonglist(int SongListID, int MusicID)//添加歌曲
        //{
        //    SONGLISTMUSIC newsonglistmusic = new Models.SONGLISTMUSIC();

        //    db.SONGLISTMUSIC.Insert(SongListID, MusicID);
        //    db.SONGLISTMUSIC.Add(newsonglistmusic);
        //    db.SaveChanges();
        //}
        //public bool playmusic(int UserID,int MusicID)
        //{
        //    var isprice = db.Database.SqlQuery<int>("select PRICE from MUSIC where MUSICID="+MusicID).FirstOrDefault();
        //    if(isprice==0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        //var isbuy = db.Database.SqlQuery<int>("select MUSICID from BUYHISTORY where MUSICID="+ MusicID+"and USERID =" + UserID).FirstOrDefault();
        //        //if ()//???
        //        //{
        //        //    return true;
        //        //}
        //        //else return false;
        //        return false;
        //    }


        //}
        ////public bool addfriends()
        ////{


        ////}
        ///

        //搜索

        public void Search(string keyword)

        {
            // var MusicList = db.MUSIC.SqlQuery("select * from MUSIC where MUSICNAME like '%'" + keyword + "'%'").ToList();

            var result = from u in db.MUSIC
                         where u.MUSICNAME.Contains("A")
                         select u;
            //var MusicList = db.Database.SqlQuery<MUSIC>("select * from MUSIC where MUSICID=123").ToList();
            var MusicList = result.FirstOrDefault().MUSICNAME;
            var MusicID = result.FirstOrDefault().MUSICID;

            //使用ViewBag传参数
            ViewBag.MusicList = MusicList;



        }

        //排名

        public void All_Rank()

        {

            var All_Rank = db.Database.SqlQuery<MUSIC>("select MUSIC.* from MUSIC order by PLAYTIMES desc, LIKES desc").ToList();

            ViewBag.AllRank = All_Rank;

 

        }

        //public ActionResult Myinfo()
        //{
        //    var id = Session["account"];
        //    ViewBag.id = id;



        //    return View();
        //}

        public ActionResult Myinfo()
        {
            object s = HttpContext.Session["account"];
            var userinfo = from user in db.MUSER
                           where user.USERID == (int)s
                           select user;
            var expire = from vips in db.VIP
                         where vips.VIPID == (int)s
                         select vips.DAYS;

            var songplayed = from songs in db.HISTORY
                             where songs.USERID == (int)s
                             select songs;

            int songnum = songplayed.Count();
            if (songnum > 0)
            {
                ViewBag.red = "/assets/images/honor1.png";
            }
            else
            {
                ViewBag.red = "/assets/images/honor0.png";
            }


            if (expire.Count() == 0)
            {
                ViewBag.isVIP = "/assets/images/vip0.png";
                ViewBag.viptime = 0;
            }
            else
            {
                ViewBag.isVIP = "/assets/images/vip.png";
                foreach (var exp in expire)
                {
                    ViewBag.viptime = exp;

                }
            }
            ViewBag.blue = 0;
            ViewBag.yellow = 0;



            foreach (var info in userinfo)
            {
                ViewBag.id = info.USERID;
                ViewBag.name = info.USERNAME;
                ViewBag.sex = info.USERSEX;
                ViewBag.Birthday = info.USERBIRTHDAY;
                ViewBag.Email = info.USEREMAIL;
            }

            return View();
        }

        [HttpPost]
        public void BuyVip(int viptime)//VIP用户
        {
            var muserid = Session["account"];
            VIP newvip = new Models.VIP();
            var vip = from r in db.VIP
                      where r.VIPID == (int)muserid
                      select r;
            int number = vip.Count();
            if (number == 0)
            {
                newvip.VIPID = (int)muserid;
                newvip.EXPIRETIME = "2019/07/13";
                newvip.DAYS = 1;
                db.VIP.Add(newvip);
            }
            else
            {
                vip.First().DAYS = viptime + 10;
            }
            db.SaveChanges();
        }


        public ActionResult SongList()
        {
            object s = HttpContext.Session["account"];
            var songLists = from song in db.SONGLIST
                            where song.USERID == (int)s
                            select song;
            List<string> songLN = new List<string>();
            List<int> songLID = new List<int>();
            foreach (var i in songLists)
            {
                songLN.Add(i.SONGLISTNAME);
                songLID.Add((int)i.SONGLISTID);
            }
            int number = songLists.Count();
            ViewBag.number = number;
            ViewBag.name = songLN;
            ViewBag.ID = songLID;
            return View();
        }
        public ActionResult ShowList(int id)
        {
            var songs = from so in db.SONGLISTMUSIC
                        join mu in db.MUSIC
                            on so.MUSICID equals mu.MUSICID
                        where so.SONGLISTID == id
                        select mu;
            List<int> songid = new List<int>();
            List<string> songname = new List<string>();
            foreach (var i in songs)
            {
                songid.Add((int)i.MUSICID);
                songname.Add(i.MUSICNAME);
            }
            int number = songs.Count();
            ViewBag.number = number;
            ViewBag.id = songid;
            ViewBag.name = songname;
            return View();
        }
        public ActionResult Createproduct()
        {
            var id = Session["account"];
            ViewBag.id = id;
            return View();
        }
            public class MycreateSender
        {
            public string songname { get; set; }
            public string singername { get; set; }
            public string style { get; set; }
            public int duration { get; set; }
        }





        [HttpPost]
        public ActionResult Createproduct(MycreateSender msg)
        {
            int mid = 0;
            var file = Request.Files["file1"];
            //var hy = Request.Form[""];
            if (msg == null)
            {
                ViewBag.result = "上传失败";
            }
            else
            {
                var iss = from t in db.SINGER
                          where t.SINGERNAME == msg.singername
                          select t.SINGERID;
                var issinger = iss.FirstOrDefault();
                // var issinger = db.Database.SqlQuery<int>("select SINGERID from SINGER where SINGERNAME=" + msg.singername).FirstOrDefault();
                //var issinger = db.Database.SqlQuery<int>("select SINGERID from SINGER where SINGERID=1").FirstOrDefault();
                if (issinger == 0)
                {
                    //写入singer
                    var singer = new SINGER();
                    var maxid = db.Database.SqlQuery<int>("select max(SINGERID) from SINGER").FirstOrDefault();
                    maxid++;
                    singer.SINGERID = maxid;
                    singer.SINGERNAME = msg.singername;
                    db.SINGER.Add(singer);
                    db.SaveChanges();

                    //写入music
                    var music = new MUSIC();
                    var maxid1 = db.Database.SqlQuery<int>("select max(MUSICID) from MUSIC").FirstOrDefault();
                    maxid1++;//新的user的ID
                    mid = maxid1;
                    music.MUSICID = maxid1;
                    music.MUSICNAME = msg.songname;
                    music.SINGERID = maxid;
                    music.DURATION = msg.duration;
                    db.MUSIC.Add(music);
                    db.SaveChanges();

                    //写入MUISCTAG
                    var musictag = new MUSICTAG();
                    musictag.MUSICID = maxid1;
                    musictag.TAGNAME = msg.style;
                    db.MUSICTAG.Add(musictag);
                    db.SaveChanges();
                    ViewBag.result = "success";

                }
                else
                {
                    //写入music
                    var music = new MUSIC();
                    var maxid = db.Database.SqlQuery<int>("select max(MUSICID) from MUSIC").FirstOrDefault();
                    maxid++;//新的user的ID
                    mid = maxid;
                    music.MUSICID = maxid;
                    music.MUSICNAME = msg.songname;
                    music.SINGERID = issinger;
                    music.DURATION = msg.duration;
                    db.MUSIC.Add(music);

                    db.SaveChanges();

                    //写入MUISCTAG
                    var musictag = new MUSICTAG();
                    musictag.MUSICID = maxid;
                    musictag.TAGNAME = msg.style;
                    db.MUSICTAG.Add(musictag);
                    db.SaveChanges();
                    ViewBag.result = "success";


                }
            }

            var name = mid.ToString();

            //上传文件
            var file1 = Request.Files["file1"];
            if (file1 != null)
            {

                var fileName1 = name + ".mp3";
                //var filePath = "C:\\Users\\16214\\Desktop";
                var filePath1 = Server.MapPath("../save_audio");
                if (!System.IO.Directory.Exists(filePath1))
                    System.IO.Directory.CreateDirectory(filePath1);        //若没有路径就创建新的路径
                                                                           //  file.SaveAs(filePath+fileName);
                file1.SaveAs(System.IO.Path.Combine(filePath1, fileName1));
            }
            var file2 = Request.Files["file2"];
            if (file2 != null)
            {
                var fileName2 = name + ".txt";
                //var filePath = "C:\\Users\\16214\\Desktop";
                var filePath2 = Server.MapPath("../save_lyric");
                if (!System.IO.Directory.Exists(filePath2))
                    System.IO.Directory.CreateDirectory(filePath2);        //若没有路径就创建新的路径
                                                                           //  file.SaveAs(filePath+fileName);
                file2.SaveAs(System.IO.Path.Combine(filePath2, fileName2));
            }

            var file3 = Request.Files["file3"];
            if (file3 != null)
            {
                var fileName3 = name + ".jpg";
                //var filePath = "C:\\Users\\16214\\Desktop";
                var filePath3 = Server.MapPath("../save_cover");
                if (!System.IO.Directory.Exists(filePath3))
                    System.IO.Directory.CreateDirectory(filePath3);        //若没有路径就创建新的路径
                                                                           //  file.SaveAs(filePath+fileName);
                file3.SaveAs(System.IO.Path.Combine(filePath3, fileName3));
            }
            return View();

        }
        public ActionResult Playhistory()
        {

            return View();
        }
        public ActionResult News()
        {
            object s = HttpContext.Session["account"];
            List<string> Friend = new List<string>();
            var friends = from fri in db.FRIENDS
                          join name in db.MUSER
                            on fri.FRIENDID equals name.USERID
                          where fri.USERID == (int)s
                          select name.USERNAME;

            Friend.Add("Admin");
            foreach (string i in friends)
            {
                Friend.Add(i);
            }
            int number = Friend.Count();
            ViewBag.number = number;
            ViewBag.friends = Friend;
            return View();
        }
        public ActionResult Edit()
        {

            return View();
        }


        public class EditSender
        {
            public string username { get; set; }
            public string gender { get; set; }
            public string birthday { get; set; }
        }

      
        public ActionResult Event()
        {
            var id = (int)Session["account"];
            ViewBag.id = id;
            var email = db.Database.SqlQuery<string>("select USEREMAIL from MUSER where USERID=" + id).FirstOrDefault();
            ViewBag.email = email;
            return View();
        }


        [HttpPost]
        public ActionResult Event(EditSender msg)
        {
            var id = (int)Session["account"];
            ViewBag.id = id;
            var email = db.Database.SqlQuery<string>("select USEREMAIL from MUSER where USERID=" + id).FirstOrDefault();
            ViewBag.email = email;

            MUSER muser = db.MUSER.Find(id);

            muser.USERNAME = msg.username;
            muser.USERSEX = msg.gender;
            muser.USERBIRTHDAY = msg.birthday;
            db.Entry(muser).State = EntityState.Modified;
            db.SaveChanges();
            ViewBag.isedit = "success";
            return RedirectToAction("Myinfo", "Home");
        }



        //排行暂时未好

        //public List<MUSIC> Pop_Rank()

        //{

        //    var Pop_Rank = db.Database.SqlQuery<MUSIC>("select MUSIC.* from MUSIC, MUSICTAG where MUSIC.MUSICID = MUSICTAG.MUSICID and TAGNAME = 'popular' order by PLAYTIMES desc, LIKES desc").ToList();

        //    return Pop_Rank;

        //}

        //public List<MUSIC> Folk_Rank()

        //{

        //    var Folk_Rank = db.Database.SqlQuery<MUSIC>("select MUSIC.* from MUSIC, MUSICTAG where MUSIC.MUSICID = MUSICTAG.MUSICID and TAGNAME = 'Folk' order by PLAYTIMES desc, LIKES desc").ToList();

        //    return Folk_Rank;

        //}


        //public bool submitlyric(int id, string lyricpath)   
        //{
        //    var music = db.MUSIC.Find(id);
        //    music.LYRICS = lyricpath;

        //    return true;
        //}


        //public void newsonglist(string SongListName, int CreatorID)//添加歌单
        //{
        //    SONGLIST newsonglist = new Models.SONGLIST();
        //    var maxid = db.Database.SqlQuery<int>("select max(SONGLISTID) from SONGLIST").FirstOrDefault();
        //    maxid++;//新的歌单的ID
        //    newsonglist.SONGLISTID = maxid;
        //    newsonglist.SONGLISTNAME = SongListName;
        //    newsonglist.USERID = CreatorID;
        //    newsonglist.COLLECTIONTIMES = 0;
        //    db.SONGLIST.Add(newsonglist);
        //    db.SaveChanges();
        //}
        //public void deletesonglist(int songlistID)//删除歌单
        //{
        //    var songlist = db.SONGLIST.Find(songlistID);
        //    db.SONGLIST.Remove(songlist);
        //    db.SaveChanges();
        //}

        //public class SONGLISTMUSIC
        //{
        //    public int SongListID { get; set; }
        //    public int MusicID { get; set; }
        //}
        //public void managesonglist(int SongListID, int MusicID)//管理歌单
        //{

        //    db.SONGLISTMUSIC.Remove(SongListID, MusicID);
        //    db.SaveChanges();
        //}
        //public void addtosonglist(int SongListID, int MusicID)//添加歌曲
        //{
        //    SONGLISTMUSIC newsonglistmusic = new Models.SONGLISTMUSIC();

        //    db.SONGLISTMUSIC.Insert(SongListID, MusicID);
        //    db.SONGLISTMUSIC.Add(newsonglistmusic);
        //    db.SaveChanges();
        //}

        //public bool playmusic(int UserID, int MusicID)
        //{
        //    var isprice = db.Database.SqlQuery<int>("select PRICE from MUSIC where MUSICID=" + MusicID.ToString()).FirstOrDefault();
        //    if (isprice == 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        //var isbuy = db.Database.SqlQuery<int>("select MUSICID from BUYHISTORY where MUSICID="+ MusicID+"and USERID =" + UserID).FirstOrDefault();
        //        //if ()//???
        //        //{
        //        //    return true;
        //        //}
        //        //else return false;
        //        return false;
        //    }


        //}
        //public class AddFriends
        //{
        //    public string UserID { get; set; }
        //    public string FriendID { get; set; }
        //}
        //public bool addfriends(AddFriends msg)
        //{
        //    Muser temp = new Muser();
        //    if (msg == null)
        //    {
        //        ViewBag.Error = "Empty User";
        //        return View();
        //    }
        //    else
        //    {
        //        var friend = db.Database.SqlQuery<string>(
        //            "select FriendID " +
        //            "from Friends" +
        //            " where " + msg.FriendID + "=some(select FriendID " +
        //            "from Friends " +
        //            "where UserID =" + msg.UserID + ")");//判断是否已有该好友
        //        if (friend == null)
        //        {
        //            db.Database.ExecuteSqlCommand("insert into Friends values(" + msg.UserID + "," + msg.FriendID + "," + "null" + ")");
        //            return View();
        //        }
        //        else
        //        {
        //            ViewBag.Error = "Had this friend";
        //            return View();
        //        }
        //    }
        //}

        //public class ViewPlayHistory
        //{
        //    public string UserID;
        //    public string type;
        //}

        //public int[] PlayAchievement(int userID)
        //{
        //    int[] Ach = new int[9];
        //    int ach1 = db.Database.SqlQuery<int>("select sum(PLAYEDTIMES) from HISTORY natural join MUSER where USERID=" + userID.ToString()).FirstOrDefault();
        //    int ach2 = db.Database.SqlQuery<int>("select sum(PLAYEDTIMES) from HISTORY natural join MUSER join MUSICTAG using (MusicID) where TAGNAME = 'CLASSIC' and USERID = " + userID.ToString()).FirstOrDefault();
        //    int ach3 = db.Database.SqlQuery<int>("select sum(PLAYEDTIMES) from HISTORY natural join MUSER join MUSICTAG using (MusicID) where TAGNAME = 'ROCK' and USERID = " + userID.ToString()).FirstOrDefault();
        //    int ach4 = db.Database.SqlQuery<int>("select sum(PLAYEDTIMES) from HISTORY natural join MUSER join MUSICTAG using (MusicID) where TAGNAME = 'OLDWIND' and USERID = " + userID.ToString()).FirstOrDefault();
        //    int ach5 = db.Database.SqlQuery<int>("select sum(PLAYEDTIMES) from HISTORY natural join MUSER join MUSICTAG using (MusicID) where TAGNAME = 'RAP' and USERID = " + userID.ToString()).FirstOrDefault();
        //    int ach6 = db.Database.SqlQuery<int>("select sum(PLAYEDTIMES) from HISTORY natural join MUSER join MUSICTAG using (MusicID) where TAGNAME = 'POP' and USERID = " + userID.ToString()).FirstOrDefault();
        //    int ach7 = db.Database.SqlQuery<int>("select sum(PLAYEDTIMES) from HISTORY natural join MUSER join MUSICTAG using (MusicID) where TAGNAME = 'LIGHT' and USERID = " + userID.ToString()).FirstOrDefault();
        //    int ach8 = db.Database.SqlQuery<int>("select sum(PLAYEDTIMES) from HISTORY natural join MUSER join MUSICTAG using (MusicID) where TAGNAME = 'FOLK' and USERID = " + userID.ToString()).FirstOrDefault();
        //    int ach9 = db.Database.SqlQuery<int>("select sum(PLAYEDTIMES) from HISTORY natural join MUSER join MUSICTAG using (MusicID) where TAGNAME = 'MOVIE' and USERID = " + userID.ToString()).FirstOrDefault();
        //    if (ach1 >= 10)
        //    {
        //        Ach[0] = 1;
        //    }
        //    if (ach2 >= 5)
        //    {
        //        Ach[1] = 1;
        //    }
        //    if (ach3 >= 5)
        //    {
        //        Ach[2] = 1;
        //    }
        //    if (ach4 >= 5)
        //    {
        //        Ach[3] = 1;
        //    }
        //    if (ach5 >= 5)
        //    {
        //        Ach[4] = 1;
        //    }
        //    if (ach6 >= 5)
        //    {
        //        Ach[5] = 1;
        //    }
        //    if (ach7 >= 5)
        //    {
        //        Ach[6] = 1;
        //    }
        //    if (ach8 >= 5)
        //    {
        //        Ach[7] = 1;
        //    }
        //    if (ach9 >= 5)
        //    {
        //        Ach[8] = 1;
        //    }
        //    return Ach;
        //}
        ////歌单收藏
        //public bool SongListCollected(int userID, int songlistID)
        //{
        //    var requestedSL = db.Database.SqlQuery<SONGLIST>("select * from SONGLIST where SONGLISTID = " + songlistID.ToString()).FirstOrDefault();//对应歌单
        //    int IsNull = -1;
        //    IsNull = db.Database.SqlQuery<int>("select songlistID from usersonglist where " + songlistID.ToString() + " = some(select songlistID from uesrSONGLIST where userID =" + userID.Tostring()).FirstOrDefault;
        //    if (IsNull != -1) return 0;//已存在
        //    else
        //    {
        //        requestedSL.COLLECTIONTIMES = requestedSL.COLLECTIONTIMES + 1;
        //        db.Database.ExecuteSqlCommand("insert into COLLECTION values(" + userID.ToString() + "," + songlistID.ToString() + ")");
        //        db.SaveChanges();
        //        return true;
        //    }

        //}
        ////音乐收藏
        //public bool Collection(int userID, int musicID)
        //{
        //    var SLID = db.Database.SqlQuery<int>("select SONGLISTID from songlist where userid = " + userID.ToString()).FirstOrDefault();
        //    int IsNull = -1;
        //    IsNull = db.Database.SqlQuery<int>("select musicid from songlistmusic where " + musicID.ToString() + " = some(select MusicID from SONGLISTMusic where SONGLISTID =" + SLID.Tostring()).FirstOrDefault;
        //    if (IsNull != -1) return false;//已存在
        //    else
        //    {
        //        db.Database.ExecuteSqlCommand("insert into SONGLISTMUSIC values(" + musicID.ToString() + "," + SLID.Tostring() + ")");
        //        db.SaveChanges();
        //        return true;//添加成功
        //    }
        //}
        ////音乐人认证
        //public int MusicianVerified(int userID)
        //{
        //    int id = -1;
        //    id = db.Database.SqlQuery<int>("select USERID from MUSICIAN where USERID = " + userID.ToString()).FirstOrDefault();
        //    if (id != -1)
        //    {
        //        return id;
        //    }
        //    else return 0;//若用户不是音乐家，返回0
        //}
        ////音乐报错
        //public void ReportFault()
        //{
        //    //想不起来objectID是做什么的了
        //}
        ////账号注销
        //public void DeleteUser(int userID)//删除用户
        //{
        //    var user = db.MUSER.Find(userID);
        //    db.MUSER.Remove(user);
        //    db.SaveChanges();
        //}
        ////点赞歌曲评论
        //public int CommentLikedId(int CommentNum, int MusicID)
        //{
        //    var likedComment = db.Database.SqlQuery<COMMENTS>("select * from COMMENTS where MUSICID = " + MusicID.ToString() + "and COMMENTNUMBER = " + CommentNum.ToString()).FirstOrDefault();
        //    likedComment.LIKES++;
        //    int u_id = db.Database.SqlQuery<int>("select USERID from COMMENTS where MUSICID = " + MusicID.ToString() + "and COMMENTNUMBER = " + CommentNum.ToString()).FirstOrDefault();//存疑？
        //    return u_id;
        //}
        ////提交评论
        //public void UploadComment(int MusicID, int userID, string Content, string SubmitTime)
        //{
        //    int num = db.Database.SqlQuery<int>("select count max(commentnumber) from COMMENTS where MUSICID = " + MusicID.ToString()).FirstOrDefault();
        //    int commentnum = num + 1;
        //    db.Database.ExecuteSqlCommand("insert into COMMENTS values(" + MusicID.ToString() + "," + commentnum.ToString() + "," + userID.ToString() + "," + Content + "," + SubmitTime + "," + 0.ToString() + ")");
        //    db.SaveChanges();
        //}
    }

}
