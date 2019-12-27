using Emlak.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emlak.Controllers
{
    public class KonutKiralikController : Controller
    {
        // GET: KonutKiralik
        public ActionResult Index(int? page, string Pil, string Pilce, string Pmahalle, int konutTuru = 0, int PodaSayisi = 0, int minTL = 0, int maxTL = Int32.MaxValue)
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
                List<SelectListItem> BinaYasi = (from i in db.BINA_YASI.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = i.BINA_YASI1,
                                                     Value = i.BINA_YASI_ID.ToString()
                                                 }).ToList();
                List<SelectListItem> BulunduguKat = (from i in db.BULUNDUGU_KAT.ToList()
                                                     select new SelectListItem
                                                     {
                                                         Text = i.BULUNDUGU_KAT1,
                                                         Value = i.BULUNDUGU_KAT_ID.ToString()
                                                     }).ToList();
                List<SelectListItem> KatSayisi = (from i in db.KAT_SAYISI.ToList()
                                                  select new SelectListItem
                                                  {
                                                      Text = i.KAT_SAYISI1,
                                                      Value = i.KAT_SAYISI_ID.ToString()
                                                  }).ToList();
                List<SelectListItem> BanyoSayisi = (from i in db.BANYO_SAYISI.ToList()
                                                    select new SelectListItem
                                                    {
                                                        Text = i.BANYO_SAYISI1,
                                                        Value = i.BANYO_SAYISI_ID.ToString()
                                                    }).ToList();

                List<SelectListItem> KullanimDurumu = (from i in db.KULLANIM_DURUMU.ToList()
                                                       select new SelectListItem
                                                       {
                                                           Text = i.KULLANIM_DURUMU1,
                                                           Value = i.KULLANIM_DURUMU_ID.ToString()
                                                       }).ToList();

                ViewBag.konutTipi = konutTipi;
                ViewBag.OdaSayisi = OdaSayisi;
                ViewBag.IsitmaTipi = IsitmaTipi;
                return View(db.KONUT_ISYERI.Where(x => x.ID == id).FirstOrDefault<KONUT_ISYERI>());
            }
        }
    }
}