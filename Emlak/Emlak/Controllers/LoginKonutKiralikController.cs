using Emlak.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emlak.Controllers
{
    public class LoginKonutKiralikController : Controller
    {
        // GET: KonutKiralik
        public ActionResult Index(int? page, string Pil, string Pilce, string Pmahalle, int konutTuru = 0, int PodaSayisi = 0, int minTL = 0, int maxTL = Int32.MaxValue)
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

                List<SelectListItem> konutTipi = (from i in db.EMLAKTURU.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = i.EMLAK_TURU,
                                                      Value = i.EMLAK_TURU_ID.ToString()
                                                  }).ToList();
                List<SelectListItem> OdaSayisi = (from i in db.ODA_SAYISI.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = i.ODA_SAYISI1,
                                                      Value = i.ODA_SAYISI_ID.ToString()
                                                  }).ToList();
                List<SelectListItem> IsitmaTipi = (from i in db.ISITMA_TIPI.ToList()
                                                   select new SelectListItem
                                                   {
                                                       Text = i.ISITMA_TIPI1,
                                                       Value = i.ISITMA_TIPI_ID.ToString()
                                                   }).ToList();

                konutTipi.Insert(0, (new SelectListItem { Text = "Tüm Konut Tipleri", Value = "0" }));
                OdaSayisi.Insert(0, (new SelectListItem { Text = "Tüm Oda Sayıları", Value = "0" }));
                IsitmaTipi.Insert(0, (new SelectListItem { Text = "Tüm Isıtma Tipleri", Value = "0" }));

                ViewBag.konutTipi = konutTipi;
                ViewBag.OdaSayisi = OdaSayisi;
                ViewBag.IsitmaTipi = IsitmaTipi;

                if (konutTuru == 0 && PodaSayisi == 0 && Pil == null && Pilce == null && Pmahalle == null && minTL == 0 && maxTL == Int32.MaxValue)
                {
                    return View(db.KONUT_ISYERI.Where(s => s.Isyeri_Konut == "Konut" && s.Kira_Satilik == "Kiralık"
                    && s.FiyatNet < maxTL && s.FiyatNet > minTL)
                   .ToList().ToPagedList(page ?? 1, 12));
                }

                if (konutTuru == 0 && PodaSayisi == 0)
                {
                    return View(db.KONUT_ISYERI.Where(s => s.Isyeri_Konut == "Konut" && s.Kira_Satilik == "Kiralık"
                    && (s.il == null || s.il.StartsWith(Pil)) && (s.ilce == null || s.ilce.StartsWith(Pilce)) && (s.Mahalle == null || s.Mahalle.StartsWith(Pmahalle))
                    && s.FiyatNet < maxTL && s.FiyatNet > minTL)
                   .ToList().ToPagedList(page ?? 1, 12));
                }
                else if (konutTuru != 0 && PodaSayisi == 0)
                {
                    return View(db.KONUT_ISYERI.Where(s => s.Isyeri_Konut == "Konut" && s.Kira_Satilik == "Kiralık" && s.EmlakTuru == konutTuru
                    && (s.il == null || s.il.StartsWith(Pil)) && (s.ilce == null || s.ilce.StartsWith(Pilce)) && (s.Mahalle == null || s.Mahalle.StartsWith(Pmahalle))
                    && s.FiyatNet < maxTL && s.FiyatNet > minTL)
                   .ToList().ToPagedList(page ?? 1, 12));
                }
                else if (konutTuru == 0 && PodaSayisi != 0)
                {
                    return View(db.KONUT_ISYERI.Where(s => s.Isyeri_Konut == "Konut" && s.Kira_Satilik == "Kiralık" && s.OdaSayisi == PodaSayisi
                    && (s.il == null || s.il.StartsWith(Pil)) && (s.ilce == null || s.ilce.StartsWith(Pilce)) && (s.Mahalle == null || s.Mahalle.StartsWith(Pmahalle))
                    && s.FiyatNet < maxTL && s.FiyatNet > minTL)
                   .ToList().ToPagedList(page ?? 1, 12));
                }
                else if (konutTuru != 0 && PodaSayisi != 0)
                {
                    return View(db.KONUT_ISYERI.Where(s => s.Isyeri_Konut == "Konut" && s.Kira_Satilik == "Kiralık" && s.EmlakTuru == konutTuru && s.OdaSayisi == PodaSayisi
                    && (s.il == null || s.il.StartsWith(Pil)) && (s.ilce == null || s.ilce.StartsWith(Pilce)) && (s.Mahalle == null || s.Mahalle.StartsWith(Pmahalle))
                    && s.FiyatNet < maxTL && s.FiyatNet > minTL)
                   .ToList().ToPagedList(page ?? 1, 12));
                }
                else
                {
                    return View(db.KONUT_ISYERI.Where(s => s.Isyeri_Konut == "Konut" && s.Kira_Satilik == "Kiralık"
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
                List<SelectListItem> konutTipi = (from i in db.EMLAKTURU.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = i.EMLAK_TURU,
                                                      Value = i.EMLAK_TURU_ID.ToString()
                                                  }).ToList();
                List<SelectListItem> OdaSayisi = (from i in db.ODA_SAYISI.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = i.ODA_SAYISI1,
                                                      Value = i.ODA_SAYISI_ID.ToString()
                                                  }).ToList();
                List<SelectListItem> IsitmaTipi = (from i in db.ISITMA_TIPI.ToList()
                                                   select new SelectListItem
                                                   {
                                                       Text = i.ISITMA_TIPI1,
                                                       Value = i.ISITMA_TIPI_ID.ToString()
                                                   }).ToList();

                ViewBag.konutTipi = konutTipi;
                ViewBag.OdaSayisi = OdaSayisi;
                ViewBag.IsitmaTipi = IsitmaTipi;
                return View(db.KONUT_ISYERI.Where(x => x.ID == id).FirstOrDefault<KONUT_ISYERI>());
            }
        }
    }
}