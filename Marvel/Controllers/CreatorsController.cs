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


        public ActionResult Creators(string order, int limit, int offset)
        {
            DataWrapper<Creator> cdw = MarvelApi.MarvelApiInterface.getCreators(order, limit, offset);

            return PartialView("_CreatorListDisplay", cdw.data);
        }
    }
}
