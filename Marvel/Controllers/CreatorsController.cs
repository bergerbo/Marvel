using Marvel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Marvel.Controllers
{
    public class CreatorsController : Controller
    {
        public ActionResult Creator(int id)
        {

            DataWrapper<Creator> dataWrapper = MarvelApi.MarvelApiInterface.getCreator(id);
            return View(dataWrapper.data.results.First());
        }


        public ActionResult Creators(string orderBy, int limit, int offset)
        {
            DataWrapper<Creator> cdw = MarvelApi.MarvelApiInterface.getCreators(new { orderBy = orderBy, limit = limit, offset = offset });

            return PartialView("_CreatorsList", cdw.data);
        }

        public ActionResult CreatorsRow(string nameStartsWith, string orderBy, int limit, int offset)
        {
            DataWrapper<Creator> cdw = MarvelApi.MarvelApiInterface.getCreators(new { nameStartsWith = nameStartsWith, orderBy = orderBy, limit = limit, offset = offset });

            return PartialView("_CreatorsRow", cdw.data);
        }

        public ActionResult Search(string searchCreators)
        {
            DataWrapper<Creator> cdw = MarvelApi.MarvelApiInterface.getCreators(new { nameStartsWith = searchCreators, orderBy = "-modified", limit = 4, offset = 0 });

            return PartialView("_CreatorsList", cdw.data);
        }
    }
}
