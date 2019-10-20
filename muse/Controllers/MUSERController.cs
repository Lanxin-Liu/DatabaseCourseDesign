using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using muse.Models;

namespace muse.Controllers
{
    public class MUSERController : Controller
    {
        private Entities5 db = new Entities5();

        // GET: MUSERs
        public ActionResult Index()
        {
            var mUSER = db.MUSER.Include(m => m.VIP).Include(m => m.MUSICIAN);
            return View(mUSER.ToList());
        }

        // GET: MUSERs/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MUSER mUSER = db.MUSER.Find(id);
            if (mUSER == null)
            {
                return HttpNotFound();
            }
            return View(mUSER);
        }

        // GET: MUSERs/Create
        public ActionResult Create()
        {
            ViewBag.USERID = new SelectList(db.VIP, "VIPID", "EXPIRETIME");
            ViewBag.USERID = new SelectList(db.MUSICIAN, "USERID", "MUSICIANINTRO");
            return View();
        }

        // POST: MUSERs/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "USERID,USERNAME,USERPASSWORD,USERIMAGE,USERSEX,USERBIRTHDAY,USEREMAIL")] MUSER mUSER)
        {
            if (ModelState.IsValid)
            {
                db.MUSER.Add(mUSER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.USERID = new SelectList(db.VIP, "VIPID", "EXPIRETIME", mUSER.USERID);
            ViewBag.USERID = new SelectList(db.MUSICIAN, "USERID", "MUSICIANINTRO", mUSER.USERID);
            return View(mUSER);
        }

        // GET: MUSERs/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MUSER mUSER = db.MUSER.Find(id);
            if (mUSER == null)
            {
                return HttpNotFound();
            }
            ViewBag.USERID = new SelectList(db.VIP, "VIPID", "EXPIRETIME", mUSER.USERID);
            ViewBag.USERID = new SelectList(db.MUSICIAN, "USERID", "MUSICIANINTRO", mUSER.USERID);
            return View(mUSER);
        }

        // POST: MUSERs/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "USERID,USERNAME,USERPASSWORD,USERIMAGE,USERSEX,USERBIRTHDAY,USEREMAIL")] MUSER mUSER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mUSER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.USERID = new SelectList(db.VIP, "VIPID", "EXPIRETIME", mUSER.USERID);
            ViewBag.USERID = new SelectList(db.MUSICIAN, "USERID", "MUSICIANINTRO", mUSER.USERID);
            return View(mUSER);
        }

        // GET: MUSERs/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MUSER mUSER = db.MUSER.Find(id);
            if (mUSER == null)
            {
                return HttpNotFound();
            }
            return View(mUSER);
        }

        // POST: MUSERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            MUSER mUSER = db.MUSER.Find(id);
            db.MUSER.Remove(mUSER);
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
    }
}
