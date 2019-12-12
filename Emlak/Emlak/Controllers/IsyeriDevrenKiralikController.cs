using Emlak.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emlak.Controllers
{
    public class IsyeriDevrenKiralikController : Controller
    {
        // GET: IsyeriDevrenKiralik
        public ActionResult Index(int? page, string Pil, string Pilce, string Pmahalle, int isyeriTuru = 0, int minTL = 0, int maxTL = Int32.MaxValue)
        {
            using (Entities db = new Entities())
            {
                List<SelectListItem> isyeriTipi = (from i in db.ISYERITURU.ToList()
                                                   select new SelectListItem
                                                   {
                                                       Text = i.ISYERI_TURU,
                                                       Value = i.ISYERI_TURU_ID.ToString()
                                                   }).ToList();
                List<SelectListItem> IsitmaTipi = (from i in db.ISITMA_TIPI.ToList()
                                                   select new SelectListItem
                                                   {
                                                       Text = i.ISITMA_TIPI1,
                                                       Value = i.ISITMA_TIPI_ID.ToString()
                                                   }).ToList();

                isyeriTipi.Insert(0, (new SelectListItem { Text = "Tüm İşyeri Tipleri", Value = "0" }));
                IsitmaTipi.Insert(0, (new SelectListItem { Text = "Tüm Isıtma Tipleri", Value = "0" }));

                ViewBag.isyeriTipi = isyeriTipi;
                ViewBag.IsitmaTipi = IsitmaTipi;

                if (isyeriTuru == 0 && Pil == null && Pilce == null && Pmahalle == null && minTL == 0 && maxTL == Int32.MaxValue)
                {
                    return View(db.KONUT_ISYERI.Where(s => s.Isyeri_Konut == "Isyeri" && s.Kira_Satilik == "Kiralık" && s.DevrenKiralik == true
                    && s.FiyatNet < maxTL && s.FiyatNet > minTL)
                   .ToList().ToPagedList(page ?? 1, 12));
                }

                if (isyeriTuru == 0)
                {
                    return View(db.KONUT_ISYERI.Where(s => s.Isyeri_Konut == "Isyeri" && s.Kira_Satilik == "Kiralık" && s.DevrenKiralik == true
                    && (s.il == null || s.il.StartsWith(Pil)) && (s.ilce == null || s.ilce.StartsWith(Pilce)) && (s.Mahalle == null || s.Mahalle.StartsWith(Pmahalle))
                    && s.FiyatNet < maxTL && s.FiyatNet > minTL)
                   .ToList().ToPagedList(page ?? 1, 12));
                }
                else if (isyeriTuru != 0)
                {
                    return View(db.KONUT_ISYERI.Where(s => s.Isyeri_Konut == "Isyeri" && s.Kira_Satilik == "Kiralık" && s.IsyeriTuru == isyeriTuru && s.DevrenKiralik == true
                    && (s.il == null || s.il.StartsWith(Pil)) && (s.ilce == null || s.ilce.StartsWith(Pilce)) && (s.Mahalle == null || s.Mahalle.StartsWith(Pmahalle))
                    && s.FiyatNet < maxTL && s.FiyatNet > minTL)
                   .ToList().ToPagedList(page ?? 1, 12));
                }
                else if (isyeriTuru == 0)
                {
                    return View(db.KONUT_ISYERI.Where(s => s.Isyeri_Konut == "Isyeri" && s.Kira_Satilik == "Kiralık" && s.DevrenKiralik == true
                    && (s.il == null || s.il.StartsWith(Pil)) && (s.ilce == null || s.ilce.StartsWith(Pilce)) && (s.Mahalle == null || s.Mahalle.StartsWith(Pmahalle))
                    && s.FiyatNet < maxTL && s.FiyatNet > minTL)
                   .ToList().ToPagedList(page ?? 1, 12));
                }
                else if (isyeriTuru != 0)
                {
                    return View(db.KONUT_ISYERI.Where(s => s.Isyeri_Konut == "Isyeri" && s.Kira_Satilik == "Kiralık" && s.IsyeriTuru == isyeriTuru && s.DevrenKiralik == true
                    && (s.il == null || s.il.StartsWith(Pil)) && (s.ilce == null || s.ilce.StartsWith(Pilce)) && (s.Mahalle == null || s.Mahalle.StartsWith(Pmahalle))
                    && s.FiyatNet < maxTL && s.FiyatNet > minTL)
                   .ToList().ToPagedList(page ?? 1, 12));
                }
                else
                {
                    return View(db.KONUT_ISYERI.Where(s => s.Isyeri_Konut == "Isyeri" && s.Kira_Satilik == "Kiralık" && s.DevrenKiralik == true
                    && s.FiyatNet < maxTL && s.FiyatNet > minTL)
                   .ToList().ToPagedList(page ?? 1, 12));
                }
            }
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