using Emlak.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emlak.Controllers
{
    public class ArsaKiralikController : Controller
    {
        // GET: ArsaKiralik
        public ActionResult Index(int? page, string Pil, string Pilce, string Pmahalle, int imarTipi = 0, int minTL = 0, int maxTL = Int32.MaxValue)
        {
            using (Entities db = new Entities())
            {
                List<SelectListItem> ImarDurumu = (from i in db.IMAR_DURUMU.ToList()
                                                   select new SelectListItem
                                                   {
                                                       Text = i.IMAR_DURUMU1,
                                                       Value = i.IMAR_DURUMU_ID.ToString()
                                                   }).ToList();
                ImarDurumu.Insert(0, (new SelectListItem { Text = "Tüm İmar Tipleri", Value = "0" }));
                ViewBag.ImarDurumu = ImarDurumu;
                
                if (imarTipi == 0 && Pil == null && Pilce == null && Pmahalle == null && minTL == 0 && maxTL == Int32.MaxValue)
                {
                    return View(db.ARSA_TARLA.Where(s => s.Kira_Satilik == "Kiralık" && s.Tarla_Arsa == "Arsa")
                   .ToList().ToPagedList(page ?? 1, 12));
                }
                if (imarTipi == 0)
                {
                    return View(db.ARSA_TARLA.Where(s => s.Kira_Satilik == "Kiralık" && s.Tarla_Arsa == "Arsa"
                    && (s.il == null || s.il.StartsWith(Pil)) && (s.ilce == null || s.ilce.StartsWith(Pilce)) && (s.mahalle == null || s.mahalle.StartsWith(Pmahalle))
                    && s.FiyatNet < maxTL && s.FiyatNet > minTL)
                   .ToList().ToPagedList(page ?? 1, 12));
                }

                else
                {
                    return View(db.ARSA_TARLA.Where(s => s.ImarDurumu == imarTipi && s.Kira_Satilik == "Kiralık" && s.Tarla_Arsa == "Arsa"
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