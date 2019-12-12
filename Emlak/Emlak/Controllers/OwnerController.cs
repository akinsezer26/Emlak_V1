using Emlak.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emlak.Controllers
{
    public class OwnerController : Controller
    {
        Entities ent = new Entities();
        public ActionResult Index()
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
                return View();
            }
        }
    }
}