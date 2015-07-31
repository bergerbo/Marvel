using Marvel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Marvel.Controllers
{
    public class ComicController : Controller
    {
        // GET: Comic
        public ActionResult Index()
        {
            return View();
        }

        // GET: Comic/Details/5
        public ActionResult Details(int id)
        {
            ComicDataWrapper cdw =  MarvelApi.MarvelApiInterface.getComic(id);
            return View(cdw.data.results.First());
        }

        // GET: Comic/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comic/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Comic/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Comic/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Comic/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Comic/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
