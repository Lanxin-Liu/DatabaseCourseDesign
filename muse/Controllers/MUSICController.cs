using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using muse.Models;
using System.IO;
using static System.IO.Directory;

namespace muse.Controllers
{
    public class MUSICController : Controller
    {
        private Entities5 db = new Entities5();

        // GET: MUSICs
        public ActionResult Index()
        {
            var mUSIC = db.MUSIC.Include(m => m.ALBUM).Include(m => m.SINGER);

            string style = "folk";

            //var result = from u in db.MUSICTAG
            //             where u.TAGNAME==style
            //             select u;
            //var result2 = from t in db.HISTORY
            //              orderby t
            //              where t.MUSICID==result.F
            //             select t;
            //var pop_rank = db.database.sqlquery<music>("select music.* from music, musictag where music.musicid = musictag.musicid and tagname = 'popular' order by playtimes desc, likes desc").tolist();



            return View(mUSIC.ToList());
        }

        // GET: MUSICs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MUSIC mUSIC = db.MUSIC.Find(id);
            if (mUSIC == null)
            {
                return HttpNotFound();
            }
            return View(mUSIC);
        }

        // GET: MUSICs/Create
        public ActionResult Create()
        {
            string keyword = "A";

            ViewBag.ALBUMID = new SelectList(db.ALBUM, "ALBUMID", "ALBUMNAME");
            ViewBag.SINGERID = new SelectList(db.SINGER, "SINGERID", "SINGERNAME");


            //// var MusicList = db.MUSIC.SqlQuery("select * from MUSIC where MUSICNAME like '%'" + keyword + "'%'").ToList();

            //var result = from u in db.MUSIC
            //             where u.MUSICNAME.Contains("A")
            //             select u;
            ////var MusicList = db.Database.SqlQuery<MUSIC>("select * from MUSIC where MUSICID=123").ToList();
            //var MusicList = result.FirstOrDefault().MUSICNAME;
            //var MusicID= result.FirstOrDefault().MUSICID;

            //ViewBag.MusicList = MusicList;




            //var All_Rank = db.Database.SqlQuery<MUSIC>("select MUSIC.* from MUSIC order by PLAYTIMES desc, LIKES desc").ToList();

            //ViewBag.AllRank = All_Rank;



            var Pop_Rank = db.Database.SqlQuery<MUSIC>("select MUSIC.* from MUSIC, MUSICTAG where MUSIC.MUSICID = MUSICTAG.MUSICID and TAGNAME = 'popular' order by PLAYTIMES desc, LIKES desc").ToList();


            ViewBag.AllRank = Pop_Rank;



            return View();
        }

        public ActionResult SingleArtist(int id)//歌手id

        {

            var singer = from ar in db.SINGER

                         where ar.SINGERID == id

                         select ar;

            var singer_intro = singer.First().SINGERINTRO;

            var singer_name = singer.First().SINGERNAME;

            var singer_image = singer.First().SINGERIMAGE;

            ViewBag.intro = singer_intro;

            ViewBag.name = singer_name;

            ViewBag.image = singer_image;

            var album = from al in db.ALBUM//下方的专辑信息

                        where al.SINGERID == id

                        select al;

            List<int> albumlist = new List<int>();

            List<string> coverlist = new List<string>();

            int number = album.Count();

            foreach (var i in album)

            {

                albumlist.Add((int)i.ALBUMID);

                coverlist.Add("/save_album/" + i.ALBUMID + ".jpg");

            }

            if (number < 4)

            {

                int i = number;

                while (i < 4)

                {

                    albumlist.Add(1);

                    coverlist.Add("/save_album/1.jpg");

                }

            }



            ViewBag.albumlist = albumlist;

            ViewBag.coverlist = coverlist;

            return View();

        }

        // POST: MUSICs/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MUSICID,MUSICNAME,SINGERID,PUBLISHER,PUBLISHTIME,ALBUMID,DURATION,PRICE,LIKES,PLAYTIMES,MUSICCOVER,MUSICPATH,LYRICS")] MUSIC mUSIC)
        {
            if (ModelState.IsValid)
            {
                db.MUSIC.Add(mUSIC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ALBUMID = new SelectList(db.ALBUM, "ALBUMID", "ALBUMNAME", mUSIC.ALBUMID);
            ViewBag.SINGERID = new SelectList(db.SINGER, "SINGERID", "SINGERNAME", mUSIC.SINGERID);
            return View(mUSIC);
        }

        // GET: MUSICs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MUSIC mUSIC = db.MUSIC.Find(id);
            if (mUSIC == null)
            {
                return HttpNotFound();
            }
            ViewBag.ALBUMID = new SelectList(db.ALBUM, "ALBUMID", "ALBUMNAME", mUSIC.ALBUMID);
            ViewBag.SINGERID = new SelectList(db.SINGER, "SINGERID", "SINGERNAME", mUSIC.SINGERID);
            return View(mUSIC);
        }

        // POST: MUSICs/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MUSICID,MUSICNAME,SINGERID,PUBLISHER,PUBLISHTIME,ALBUMID,DURATION,PRICE,LIKES,PLAYTIMES,MUSICCOVER,MUSICPATH,LYRICS")] MUSIC mUSIC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mUSIC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ALBUMID = new SelectList(db.ALBUM, "ALBUMID", "ALBUMNAME", mUSIC.ALBUMID);
            ViewBag.SINGERID = new SelectList(db.SINGER, "SINGERID", "SINGERNAME", mUSIC.SINGERID);
            return View(mUSIC);
        }

        // GET: MUSICs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MUSIC mUSIC = db.MUSIC.Find(id);
            if (mUSIC == null)
            {
                return HttpNotFound();
            }
            return View(mUSIC);
        }

        // POST: MUSICs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            MUSIC mUSIC = db.MUSIC.Find(id);
            db.MUSIC.Remove(mUSIC);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //public ActionResult Index()
        //{ 
        ////    var Pop_Rank = db.Database.SqlQuery<MUSIC>("select MUSIC.* from MUSIC, MUSICTAG where MUSIC.MUSICID = MUSICTAG.MUSICID and TAGNAME = 'popular' order by PLAYTIMES desc, LIKES desc").ToList();

        ////    return Pop_Rank;
        //   }
        //[HttpPost]
        //public ActionResult Index(HttpPostedFileBase[] files)
        //{

        //    var fileName1 = files[0].FileName;
        //    //var filePath = "C:\\Users\\16214\\Desktop";
        //    var filePath1 = Server.MapPath("../save_image");
        //    if (!System.IO.Directory.Exists(filePath1))
        //        System.IO.Directory.CreateDirectory(filePath1);        //若没有路径就创建新的路径
        //    //  file.SaveAs(filePath+fileName);
        //    files[0].SaveAs(System.IO.Path.Combine(filePath1, fileName1));

        //    var fileName2 = files[1].FileName;
        //    //var filePath = "C:\\Users\\16214\\Desktop";
        //    var filePath2 = Server.MapPath("../save_video");
        //    if (!System.IO.Directory.Exists(filePath2))
        //        System.IO.Directory.CreateDirectory(filePath2);        //若没有路径就创建新的路径
        //    //  file.SaveAs(filePath+fileName);
        //    files[1].SaveAs(System.IO.Path.Combine(filePath2, fileName2));
        //    return View();
        //}
        public ActionResult UploadFile()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult UploadFile(HttpPostedFileBase file)
        //{
        //    var fileName = file.FileName;
        //    var filePath = Server.MapPath(string.Format("~/{0}", "File"));
        //    file.SaveAs(System.IO.Path.Combine(filePath, fileName));
        //    return View();
        //}
        //public ActionResult Index( files)
        //{


        //    string path = "O:\test";
        //    files.
        //    files.SaveAs()
        //    files.CopyTo(path);
        //    //var fileName1 = files[0].FileName;
        //    ////var filePath = "C:\\Users\\16214\\Desktop";
        //    //var filePath1 = Server.MapPath("../save_image");
        //    //if (!Directory.Exists(filePath1))
        //    //    Directory.CreateDirectory(filePath1);        //若没有路径就创建新的路径
        //    ////  file.SaveAs(filePath+fileName);
        //    //files[0].SaveAs(Path.Combine(filePath1, fileName1));

        //    //var fileName2 = files[1].FileName;
        //    ////var filePath = "C:\\Users\\16214\\Desktop";
        //    //var filePath2 = Server.MapPath("../save_video");
        //    //if (!Directory.Exists(filePath2))
        //    //    Directory.CreateDirectory(filePath2);        //若没有路径就创建新的路径
        //    ////  file.SaveAs(filePath+fileName);
        //    //files[1].SaveAs(Path.Combine(filePath2, fileName2));
        //    return View();
        //}

        //public ActionResult Music()
        //{
        //    return View();
        //}


        public ActionResult Music(int id)
        {
            var info = from mu in db.MUSIC
                       where mu.MUSICID == id
                       select mu;

            int singerID = (int)info.First().SINGERID;
            ViewBag.SingerID = singerID;
            var tag = from ta in db.MUSICTAG
                      where ta.MUSICID == id
                      select ta.TAGNAME;
            List<string> tags = new List<string>();
            foreach (var i in tag)
            {
                tags.Add(i);
            }
            ViewBag.img = "/save_cover/" + id + ".jpg";
            ViewBag.tag = tags;
            var singerna = from si in db.SINGER
                           where si.SINGERID == singerID
                           select si.SINGERNAME;
            foreach(var i in singerna)
            {
                ViewBag.singername = i;
            }

            int albumID = (int)info.First().ALBUMID;
            ViewBag.albumID = albumID;
            var albumna = from al in db.ALBUM
                          where al.ALBUMID == albumID
                          select al.ALBUMNAME;
            foreach(var i in albumna)
            {
                ViewBag.albumname = i;
            }


            string filename = Server.MapPath("../save_lyric/") + id + ".txt";
            StreamReader sr = new StreamReader(filename);
            string lyrics = sr.ReadToEnd();
            sr.Close();
            ViewBag.lyrics = lyrics;
            ViewBag.id = id;

            var comment = from co in db.COMMENTS
                          join us in db.MUSER
                            on co.USERID equals us.USERID
                          where co.MUSICID == id
                          select new
                          {
                              USERID = co.USERID,
                              USERNAME = us.USERNAME,
                              CONTENT = co.CONTENT
                          };
            List<int> otherID = new List<int>();
            List<string> otherName = new List<string>();
            List<string> othercontent = new List<string>();

            foreach (var i in comment)
            {
                otherID.Add((int)i.USERID);
                otherName.Add(i.USERNAME);
                othercontent.Add(i.CONTENT);
            }
            if (comment.Count() < 3)
            {
                otherID.Add(1);
                otherName.Add("MUSE");
                othercontent.Add("前排空缺");
                otherID.Add(1);
                otherName.Add("MUSE");
                othercontent.Add("前排空缺");
                otherID.Add(1);
                otherName.Add("MUSE");
                othercontent.Add("前排空缺");
            }
            ViewBag.otherID = otherID;
            ViewBag.otherName = otherName;
            ViewBag.othercontent = othercontent;
            return View();
        }


        public ActionResult Album(int id = 1)
        {
            var album = from al in db.ALBUM
                        where al.ALBUMID == id
                        select al;
            ViewBag.name = album.First().ALBUMNAME;
            ViewBag.intro = album.First().ALBUMINTRO;
            ViewBag.date = album.First().ALBUMPBTIME;
            int singerID = (int)album.First().SINGERID;
            var singername = from si in db.SINGER
                             where si.SINGERID == singerID
                             select si.SINGERNAME;

            ViewBag.singername = singername.First();

            ViewBag.img = "/save_album/" + id + ".jpg";

            var songs = from so in db.MUSIC
                        where so.ALBUMID == id
                        select so;

            List<int> songID = new List<int>();
            List<string> songName = new List<string>();
            foreach (var i in songs)
            {
                songID.Add((int)i.MUSICID);
                songName.Add(i.MUSICNAME);
            }
            ViewBag.number = songs.Count();
            ViewBag.songID = songID;

            ViewBag.songName = songName;

            /*
            string[] ms = { "1", "4", "2", "3" };
            string[] sl = { "1", "4", "2", "3" };


            ViewBag.songID = sl;
            ViewBag.music = ms;
            */
            return View();
        }
        //提交评论
        [HttpPost]
        public void Publishcomt(int musicid, string value)
        {
            var id = (int)Session["account"];
            var comments = new COMMENTS();
            comments.MUSICID = musicid;
            var maxid = db.Database.SqlQuery<int>("select max(COMMENTNUMBER) from COMMENTS").FirstOrDefault();
            maxid++;
            comments.COMMENTNUMBER = maxid;
            comments.USERID = id;
            comments.CONTENT = value;
            comments.LIKES = 0;
            db.COMMENTS.Add(comments);
            db.SaveChanges();
           // return View();
        }
    }
}
