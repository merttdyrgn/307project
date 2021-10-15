using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Libraryproject1.Models;

namespace Libraryproject1.Controllers
{
    public class booksController : Controller
    {
        private libraryEntities db = new libraryEntities();

        // GET: books
        public ActionResult Index()
        {
            var book = db.book.Include(b => b.author).Include(b => b.category).Include(b => b.publisher);
            return View(book.ToList());
        }

        // GET: books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = db.book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: books/Create
        public ActionResult Create()
        {
            ViewBag.a_id = new SelectList(db.author, "id", "name");
            ViewBag.cat_id = new SelectList(db.category, "id", "catname");
            ViewBag.p_id = new SelectList(db.publisher, "id", "name");
            return View();
        }

        // POST: books/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,cat_id,a_id,p_id,contents,pages,edition")] book book)
        {
            if (ModelState.IsValid)
            {
                db.book.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.a_id = new SelectList(db.author, "id", "name", book.a_id);
            ViewBag.cat_id = new SelectList(db.category, "id", "catname", book.cat_id);
            ViewBag.p_id = new SelectList(db.publisher, "id", "name", book.p_id);
            return View(book);
        }

        // GET: books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = db.book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.a_id = new SelectList(db.author, "id", "name", book.a_id);
            ViewBag.cat_id = new SelectList(db.category, "id", "catname", book.cat_id);
            ViewBag.p_id = new SelectList(db.publisher, "id", "name", book.p_id);
            return View(book);
        }

        // POST: books/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,cat_id,a_id,p_id,contents,pages,edition")] book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.a_id = new SelectList(db.author, "id", "name", book.a_id);
            ViewBag.cat_id = new SelectList(db.category, "id", "catname", book.cat_id);
            ViewBag.p_id = new SelectList(db.publisher, "id", "name", book.p_id);
            return View(book);
        }

        // GET: books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = db.book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            book book = db.book.Find(id);
            db.book.Remove(book);
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
