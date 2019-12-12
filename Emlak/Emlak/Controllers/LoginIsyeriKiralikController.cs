using Emlak.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emlak.Controllers
{
    public class LoginIsyeriKiralikController : Controller
    {
        // GET: IsyeriKiralik
        public ActionResult Index(int? page, string Pil, string Pilce, string Pmahalle, int isyeriTuru = 0, int minTL = 0, int maxTL = Int32.MaxValue)
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

                List<SelectListItem> isyeriTipi = (from i in db.ISYERITURU.ToList()
                                                   select new SelectListItem
                                                   {
                                                       Text = i.ISYERI_TURU,
                                                       Value = i.ISYERI_TURU_ID.ToString()
                                                   }).ToList();

                isyeriTipi.Insert(0, (new SelectListItem { Text = "Tüm İşyeri Tipleri", Value = "0" }));

                ViewBag.isyeriTipi = isyeriTipi;

                if (isyeriTuru == 0 && Pil == null && Pilce == null && Pmahalle == null && minTL == 0 && maxTL == Int32.MaxValue)
                {
                    return View(db.KONUT_ISYERI.Where(s => s.Isyeri_Konut == "Isyeri" && s.Kira_Satilik == "Kiralık" && s.DevrenSatilik == false && s.DevrenKiralik == false
                    && s.FiyatNet < maxTL && s.FiyatNet > minTL)
                   .ToList().ToPagedList(page ?? 1, 12));
                }

                if (isyeriTuru == 0)
                {
                    return View(db.KONUT_ISYERI.Where(s => s.Isyeri_Konut == "Isyeri" && s.Kira_Satilik == "Kiralık" && s.DevrenSatilik == false && s.DevrenKiralik == false
                    && (s.il == null || s.il.StartsWith(Pil)) && (s.ilce == null || s.ilce.StartsWith(Pilce)) && (s.Mahalle == null || s.Mahalle.StartsWith(Pmahalle))
                    && s.FiyatNet < maxTL && s.FiyatNet > minTL)
                   .ToList().ToPagedList(page ?? 1, 12));
                }
                else if (isyeriTuru != 0)
                {
                    return View(db.KONUT_ISYERI.Where(s => s.Isyeri_Konut == "Isyeri" && s.Kira_Satilik == "Kiralık" && s.IsyeriTuru == isyeriTuru && s.DevrenSatilik == false && s.DevrenKiralik == false
                    && (s.il == null || s.il.StartsWith(Pil)) && (s.ilce == null || s.ilce.StartsWith(Pilce)) && (s.Mahalle == null || s.Mahalle.StartsWith(Pmahalle))
                    && s.FiyatNet < maxTL && s.FiyatNet > minTL)
                   .ToList().ToPagedList(page ?? 1, 12));
                }
                else if (isyeriTuru == 0)
                {
                    return View(db.KONUT_ISYERI.Where(s => s.Isyeri_Konut == "Isyeri" && s.Kira_Satilik == "Kiralık" && s.DevrenSatilik == false && s.DevrenKiralik == false
                    && (s.il == null || s.il.StartsWith(Pil)) && (s.ilce == null || s.ilce.StartsWith(Pilce)) && (s.Mahalle == null || s.Mahalle.StartsWith(Pmahalle))
                    && s.FiyatNet < maxTL && s.FiyatNet > minTL)
                   .ToList().ToPagedList(page ?? 1, 12));
                }
                else if (isyeriTuru != 0)
                {
                    return View(db.KONUT_ISYERI.Where(s => s.Isyeri_Konut == "Isyeri" && s.Kira_Satilik == "Kiralık" && s.IsyeriTuru == isyeriTuru && s.DevrenSatilik == false && s.DevrenKiralik == false
                    && (s.il == null || s.il.StartsWith(Pil)) && (s.ilce == null || s.ilce.StartsWith(Pilce)) && (s.Mahalle == null || s.Mahalle.StartsWith(Pmahalle))
                    && s.FiyatNet < maxTL && s.FiyatNet > minTL)
                   .ToList().ToPagedList(page ?? 1, 12));
                }
                else
                {
                    return View(db.KONUT_ISYERI.Where(s => s.Isyeri_Konut == "Isyeri" && s.Kira_Satilik == "Kiralık" && s.DevrenSatilik == false && s.DevrenKiralik == false
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