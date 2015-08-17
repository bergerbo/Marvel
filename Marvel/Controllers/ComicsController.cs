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

        public ActionResult Search(string searchComics)
        {
            DataWrapper<Comic> cdw = MarvelApi.MarvelApiInterface.getComics(new { titleStartsWith=searchComics, orderBy = "-focDate", limit = 4, offset = 0 });

            return PartialView("_ComicsList", cdw.data);
        }

        public ActionResult Comics(string orderBy, int limit, int offset)
        {
            DataWrapper<Comic> cdw = MarvelApi.MarvelApiInterface.getComics(new { orderBy, limit, offset });

            return PartialView("_ComicsList", cdw.data);
        }

        public ActionResult ComicsRow(string titleStartsWith, string orderBy, int limit, int offset)
        {
            DataWrapper<Comic> cdw = MarvelApi.MarvelApiInterface.getComics(new { titleStartsWith, orderBy, limit, offset });

            return PartialView("_ComicsRow", cdw.data);
        }

        public ActionResult ComicsRowForCreator(int creatorId, string titleStartsWith, string orderBy, int limit, int offset)
        {
            DataWrapper<Comic> cdw = MarvelApi.MarvelApiInterface.getComicsForCreator(creatorId, new { titleStartsWith, orderBy, limit, offset });

            return PartialView("_ComicsRow", cdw.data);
        }

        public ActionResult ComicsForCreator(int creatorId, string orderBy, int limit, int offset)
        {
            DataWrapper<Comic> cdw = MarvelApi.MarvelApiInterface.getComicsForCreator(creatorId, new { orderBy, limit, offset });

            return PartialView("_ComicsList", cdw.data);
        }

        public ActionResult SearchForCreator(int creatorId, string titleStartsWith)
        {
            DataWrapper<Comic> cdw = MarvelApi.MarvelApiInterface.getComicsForCreator(creatorId, new { titleStartsWith = titleStartsWith, orderBy = "-focDate", limit = 4, offset = 0 });

            return PartialView("_ComicsList", cdw.data);
        }
    }
}
