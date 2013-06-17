using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataDrivenViewEngine.Models.Core;
using DataDrivenViewEngine.Models;

namespace DataDrivenViewEngine.Controllers.Core
{
    public static class Enums
    {
        public static IList<dynamic> ListFrom<T>()
        {
            var list = new List<dynamic>();
            var enumType = typeof(T);

            foreach (var o in Enum.GetValues(enumType))
            {
                list.Add(new
                {
                    Name = Enum.GetName(enumType, o),
                    Value = (int)o
                });
            }

            return list;
        }
    }


    public class DataFieldController : Controller
    {
        private DataDrivenViewEngineContext db = new DataDrivenViewEngineContext();

        //
        // GET: /DataField/

        public ActionResult Index()
        {
            var datafields = db.DataFields.Include(d => d.ParentForm);
            return View(datafields.ToList());
        }

        //
        // GET: /DataField/Details/5

        public ActionResult Details(int id = 0)
        {
            DataField datafield = db.DataFields.Find(id);
            if (datafield == null)
            {
                return HttpNotFound();
            }
            return View(datafield);
        }

        //
        // GET: /DataField/Create

        public ActionResult Create()
        {
            ViewBag.FormId = new SelectList(db.DataForms, "Id", "Name");
            ViewBag.FieldTypes = new SelectList(Enums.ListFrom<FieldType>(), "Value", "Name", string.Empty);
            return View();
        }

        //
        // POST: /DataField/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DataField datafield)
        {
            if (ModelState.IsValid)
            {
                db.DataFields.Add(datafield);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FormId = new SelectList(db.DataForms, "Id", "Name", datafield.FormId);
            ViewBag.FieldTypes = new SelectList(Enums.ListFrom<FieldType>(), "Value", "Name", string.Empty);
            return View(datafield);
        }

        //
        // GET: /DataField/Edit/5

        public ActionResult Edit(int id = 0)
        {
            DataField datafield = db.DataFields.Find(id);
            if (datafield == null)
            {
                return HttpNotFound();
            }
            ViewBag.FormId = new SelectList(db.DataForms, "Id", "Name", datafield.FormId);
            ViewBag.FieldTypes = new SelectList(Enums.ListFrom<FieldType>(), "Value", "Name", string.Empty);
            return View(datafield);
        }

        //
        // POST: /DataField/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DataField datafield)
        {
            if (ModelState.IsValid)
            {
                db.Entry(datafield).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FormId = new SelectList(db.DataForms, "Id", "Name", datafield.FormId);
            ViewBag.FieldTypes = new SelectList(Enums.ListFrom<FieldType>(), "Value", "Name", string.Empty);
            return View(datafield);
        }

        //
        // GET: /DataField/Delete/5

        public ActionResult Delete(int id = 0)
        {
            DataField datafield = db.DataFields.Find(id);
            if (datafield == null)
            {
                return HttpNotFound();
            }
            return View(datafield);
        }

        //
        // POST: /DataField/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DataField datafield = db.DataFields.Find(id);
            db.DataFields.Remove(datafield);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}