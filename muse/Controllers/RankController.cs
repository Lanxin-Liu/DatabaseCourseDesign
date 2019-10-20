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
    public class RankController:Controller
    {
        private Entities5 db = new Entities5();
        public ActionResult Rank()
        {
            return View();
        }


    }
}