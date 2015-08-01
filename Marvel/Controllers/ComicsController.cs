using Marvel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Marvel.Controllers
{
    public class ComicsController : Controller
    {
        public ActionResult Comic(int id)
        {
            DataWrapper<Comic> cdw = MarvelApi.MarvelApiInterface.getComic(id);
            return View(cdw.data.results.First());
        }


        public ActionResult Comics(string order, int limit, int offset)
        {
            DataWrapper<Comic> cdw = MarvelApi.MarvelApiInterface.getComics(order, limit, offset);

            return PartialView("_ComicListDisplay", cdw.data);
        }

        public ActionResult ComicsForCreator(int creatorId, string order, int limit, int offset)
        {
            DataWrapper<Comic> cdw = MarvelApi.MarvelApiInterface.getComicsForCreator(creatorId, order, limit, offset);

            return PartialView("_ComicListDisplay", cdw.data);
        }
    }
}
