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
    public class HistoryController:Controller
    {

        private Entities5 db = new Entities5();

        public ActionResult Buyhistory()
        {
            var s = HttpContext.Session["account"];
            var musicinfo = from mus in db.MUSIC
                            join buy in db.BUYHISTORY
                                on mus.MUSICID equals buy.MUSICID
                            where buy.USERID == (int)s
                            select new
                            {
                                musicID = mus.MUSICID,
                                musicName = mus.MUSICNAME
                            };
            int number = musicinfo.Count();
            ViewBag.number = number;
            List<int> MUSICID = new List<int>();
            List<string> MUSICNAME = new List<string>();
            foreach (var mu in musicinfo)
            {
                MUSICID.Add((int)mu.musicID);
                MUSICNAME.Add(mu.musicName);
            }
            ViewBag.musicID = MUSICID;
            ViewBag.musicName = MUSICNAME;
            return View();
        }

        public ActionResult Playhistory()
        {
            var s = HttpContext.Session["account"];
            var musicinfo = from mus in db.MUSIC
                            join his in db.HISTORY
                               on mus.MUSICID equals his.MUSICID
                            where his.USERID == (int)s
                            select new
                            {
                                musicID = mus.MUSICID,
                                musicName = mus.MUSICNAME
                            };

            int number = musicinfo.Count();
            ViewBag.number = number;

            List<int> MUSICID = new List<int>();
            List<string> MUSICNAME = new List<string>();
            foreach (var mu in musicinfo)
            {
                MUSICID.Add((int)mu.musicID);
                MUSICNAME.Add(mu.musicName);
            }
            ViewBag.musicID = MUSICID;
            ViewBag.musicName = MUSICNAME;


            return View();
        }

        public ActionResult PlayedTimes()
        {
            var s = HttpContext.Session["account"];
            var musicinfo = from mus in db.MUSIC
                            join his in db.HISTORY
                               on mus.MUSICID equals his.MUSICID
                            where his.USERID == (int)s
                            orderby his.PLAYEDTIMES descending
                            select new
                            {
                                musicID = mus.MUSICID,
                                musicName = mus.MUSICNAME
                            };

            int number = musicinfo.Count();
            ViewBag.number = number;

            List<int> MUSICID = new List<int>();
            List<string> MUSICNAME = new List<string>();
            foreach (var mu in musicinfo)
            {
                MUSICID.Add((int)mu.musicID);
                MUSICNAME.Add(mu.musicName);
            }
            ViewBag.musicID = MUSICID;
            ViewBag.musicName = MUSICNAME;


            return View();
        }


    }
}