
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
    public class SearchController : Controller
    {
        private Entities5 db = new Entities5();
        //public ActionResult SearchResult()
        //{

        //    return View();
        //}
        //public ActionResult Playhistory(int id)
        //{

        //    return View();
        //}
        public ActionResult Searchresult()
        {
            List<int> reID = new List<int>();
            List<string> reName = new List<string>();
            ViewBag.number =1;
            reName.Add("无记录");
            ViewBag.name = reName;
            return View();
        }


        //public void Searchresult(string keyword)

        //{
        //    // var MusicList = db.MUSIC.SqlQuery("select * from MUSIC where MUSICNAME like '%'" + keyword + "'%'").ToList();

        //    var result = from u in db.MUSIC
        //                 where u.MUSICNAME.Contains("A")
        //                 select u;
        //    //var MusicList = db.Database.SqlQuery<MUSIC>("select * from MUSIC where MUSICID=123").ToList();
        //    List<int> reID = new List<int>();
        //    List<string> reName = new List<string>();

        //    foreach (var res in result)
        //    {
        //        reID.Add((int)res.MUSICID);
        //        reName.Add(res.MUSICNAME);
        //    }
        //    ViewBag.number = result.Count();
        //    ViewBag.ID = reID;
        //    ViewBag.name = reName;
        //    //使用ViewBag传参数
        //    //ViewBag.MusicList = MusicList<>;



        //    //}



            //用于表单传递信息
        public class SearchSender
        {
            public string songname { get; set; }
        }

        [HttpPost]
        public ActionResult Searchresult(SearchSender msg)
        {
            var keyword = msg.songname;
            var result = from u in db.MUSIC
                         where u.MUSICNAME.Contains(keyword)
                         select u;
            //var MusicList = db.Database.SqlQuery<MUSIC>("select * from MUSIC where MUSICID=123").ToList();
            List<int> reID = new List<int>();
            List<string> reName = new List<string>();

            foreach (var res in result)
            {
                reID.Add((int)res.MUSICID);
                reName.Add(res.MUSICNAME);
            }
            ViewBag.number = result.Count();
            ViewBag.ID = reID;
            ViewBag.name = reName;
            return View();
        }


        //public ActionResult Searchresult(SearchSender msg)
        //{

        //    var name = Request.Form["songname"];
        //    ViewBag.name = name;
        //    return View();
        //}

    }
}