using Emlak.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emlak.Controllers
{
    public class LoginTarlaSatilikController : Controller
    {
        public ActionResult Index(int? page, string Pil, string Pilce, string Pmahalle, int minTL = 0, int maxTL = Int32.MaxValue)
        {
            using (Entities db = new Entities())
            {
                ViewBag.KSCount = db.KONUT_ISYERI.Count(x => x.Isyeri_Konut == "Konut" && x.Kira_Satilik == "Satılık");
                ViewBag.KKCount = db.KONUT_ISYERI.Count(x => x.Isyeri_Konut == "Konut" && x.Kira_Satilik == "Kiralık");
                ViewBag.ISCount = db.KONUT_ISYERI.Count(x => x.Isyeri_Konut == "Isyeri" && x.Kira_Satilik == "Satılık" && x.DevrenSatilik == false && x.DevrenKiralik == false);
                ViewBag.IKCount = db.KONUT_ISYERI.Count(x => x.Isyeri_Konut == "Isyeri" && x.Kira_Satilik == "Kiralık" && x.DevrenSatilik == false && x.DevrenKiralik == false);
                ViewBag.IDSCount = db.KONUT_ISYERI.Count(x => x.Isyeri_Konut == "Isyeri" && x.Kira_Satilik == "Satılık" && x.DevrenSatilik == true && x.DevrenKiralik == false);
                ViewBag.IDKCount = db.KONUT_ISYERI.Count(x => x.Isyeri_Konut == "Isyeri" && x.Kira_Satilik == "Kiralık" && x.DevrenSatilik == false && x.DevrenKiralik == true);
                ViewBag.ASCount = db.ARSA_TARLA.Count(x => x.Tarla_Arsa == "Arsa" && x.Kira_Satilik == "Satılık");
                ViewBag.AKCount = db.ARSA_TARLA.Count(x => x.Tarla_Arsa == "Arsa" && x.Kira_Satilik == "Kiralık");
                ViewBag.TSCount = db.ARSA_TARLA.Count(x => x.Tarla_Arsa == "Tarla" && x.Kira_Satilik == "Satılık");
                ViewBag.TKCount = db.ARSA_TARLA.Count(x => x.Tarla_Arsa == "Tarla" && x.Kira_Satilik == "Kiralık");
                ViewBag.BSCount = db.BINA.Count(x => x.Kira_Satilik == "Satılık");
                ViewBag.BKCount = db.BINA.Count(x => x.Kira_Satilik == "Kiralık");

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