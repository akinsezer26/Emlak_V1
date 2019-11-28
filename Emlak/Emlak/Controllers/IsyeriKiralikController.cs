using Emlak.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emlak.Controllers
{
    public class IsyeriKiralikController : Controller
    {
        // GET: IsyeriKiralik
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Goruntule(int id)
        {
            using (Entities db = new Entities())
            {
                return View(db.KONUT_ISYERI.Where(x => x.ID == id).FirstOrDefault<KONUT_ISYERI>());
            }
        }
    }
}