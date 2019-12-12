using Emlak.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emlak.Controllers
{
    public class TarlaSatilikController : Controller
    {
        public ActionResult Index(int? page, string Pil, string Pilce, string Pmahalle, int minTL = 0, int maxTL = Int32.MaxValue)
        {
            using (Entities db = new Entities())
            {
                if (Pil == null && Pilce == null && Pmahalle == null && minTL == 0 && maxTL == Int32.MaxValue)
                {
                    return View(db.ARSA_TARLA.Where(s => s.Kira_Satilik == "Satılık" && s.Tarla_Arsa == "Tarla")
                   .ToList().ToPagedList(page ?? 1, 12));
                }
                else
                {
                    return View(db.ARSA_TARLA.Where(s => s.Kira_Satilik == "Satılık" && s.Tarla_Arsa == "Tarla"
                    && (s.il == null || s.il.StartsWith(Pil)) && (s.ilce == null || s.ilce.StartsWith(Pilce)) && (s.mahalle == null || s.mahalle.StartsWith(Pmahalle))
                    && s.FiyatNet < maxTL && s.FiyatNet > minTL)
                    .ToList().ToPagedList(page ?? 1, 12));
                }
            }
        }

        [HttpGet]
        public ActionResult Goruntule(int id)
        {
            return View();
        }
    }
}