using Emlak.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emlak.Controllers
{
    public class BinaKiralikController : Controller
    {
        public ActionResult Index(int? page, string Pil, string Pilce, string Pmahalle, int kSayisi = 0, int minTL = 0, int maxTL = Int32.MaxValue)
        {
            using (Entities db = new Entities())
            {
                List<SelectListItem> KatSayisi = (from i in db.KAT_SAYISI.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = i.KAT_SAYISI1,
                                                      Value = i.KAT_SAYISI_ID.ToString()
                                                  }).ToList();
                KatSayisi.Insert(0, (new SelectListItem { Text = "Tüm Kat Sayıları", Value = "0" }));
                ViewBag.KatSayisi = KatSayisi;

                if (kSayisi == 0 && Pil == null && Pilce == null && Pmahalle == null && minTL == 0 && maxTL == Int32.MaxValue)
                {
                    return View(db.BINA.Where(s => s.Kira_Satilik == "Kiralık")
                   .ToList().ToPagedList(page ?? 1, 12));
                }
                if (kSayisi == 0)
                {
                    return View(db.BINA.Where(s => s.Kira_Satilik == "Kiralık"
                    && (s.il == null || s.il.StartsWith(Pil)) && (s.ilce == null || s.ilce.StartsWith(Pilce)) && (s.mahalle == null || s.mahalle.StartsWith(Pmahalle))
                    && s.FiyatNet < maxTL && s.FiyatNet > minTL)
                   .ToList().ToPagedList(page ?? 1, 12));
                }
                else
                {
                    return View(db.BINA.Where(s => s.KatSayisi == kSayisi && s.Kira_Satilik == "Kiralık"
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