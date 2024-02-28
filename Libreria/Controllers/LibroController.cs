using Libreria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace Libreria.Controllers
{
    public class LibroController : Controller
    {
        // GET: Libro
        public ActionResult Index()
        {
            using (DbModels context = new DbModels())
            {
                return View(context.libros.ToList());
            }
            
        }

        // GET: Libro/Details/5
        public ActionResult Details(int id)
        {
            using (DbModels context = new DbModels())
            {
                return View(context.libros.Where(x => x.id == id).FirstOrDefault());
            }
        }

        // GET: Libro/Create
        public ActionResult Create()
        {
                return View();
        }

        // POST: Libro/Create
        [HttpPost]
        public ActionResult Create(libros libros)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    context.libros.Add(libros);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Libro/Edit/5
        public ActionResult Edit(int id)
        {
            using (DbModels context = new DbModels())
            {
                return View(context.libros.Where(x => x.id == id).FirstOrDefault());
            }
        }

        // POST: Libro/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, libros libros)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    context.Entry(libros).State = EntityState.Modified;
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Libro/Delete/5
        public ActionResult Delete(int id)
        {
            using (DbModels context = new DbModels())
            {
                return View(context.libros.Where(x => x.id == id).FirstOrDefault());
            }
        }

        // POST: Libro/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using(DbModels context = new DbModels())
                {
                    libros libros = context.libros.Where(x => x.id == id).FirstOrDefault();
                    context.libros.Remove(libros);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
