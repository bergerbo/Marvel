using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using Marvel.MarvelApi;
using Marvel.Models;

namespace Marvel.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ComicDataWrapper cdw = MarvelApiInterface.getComics(4,0,"-focDate");
            Index model = new Index { comicDataContainer = cdw.data } ;
            return View(model);
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
    }
}