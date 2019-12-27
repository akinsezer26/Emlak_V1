using Emlak.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emlak.Controllers
{
    public class KiraTakipController : Controller
    {
        // GET: KiraTakip
        public ActionResult Index()
        {
            using (Entities db = new Entities())
            {
                var today = DateTime.Today;
                List<MAKBUZ_SOZLESME> makbuzSozlesmeList = db.MAKBUZ_SOZLESME.ToList<MAKBUZ_SOZLESME>();
                List<MAKBUZ_SOZLESME> result = new List<MAKBUZ_SOZLESME>();
                foreach (var item in makbuzSozlesmeList)
                {
                    if (item.Ay1 == null && (today > item.OdemeBaslangicTarihi.Value.AddDays(7)))
                    {
                        result.Add(item);
                    }
                    else if (item.Ay2 == null && (today > item.OdemeBaslangicTarihi.Value.AddMonths(1).AddDays(7)))
                    {
                        result.Add(item);
                    }
                    else if (item.Ay3 == null && (today > item.OdemeBaslangicTarihi.Value.AddMonths(2).AddDays(7)))
                    {
                        result.Add(item);
                    }
                    else if (item.Ay4 == null && (today > item.OdemeBaslangicTarihi.Value.AddMonths(3).AddDays(7)))
                    {
                        result.Add(item);
                    }
                    else if (item.Ay5 == null && (today > item.OdemeBaslangicTarihi.Value.AddMonths(4).AddDays(7)))
                    {
                        result.Add(item);
                    }
                    else if (item.Ay6 == null && (today > item.OdemeBaslangicTarihi.Value.AddMonths(5).AddDays(7)))
                    {
                        result.Add(item);
                    }
                    else if (item.Ay7 == null && (today > item.OdemeBaslangicTarihi.Value.AddMonths(6).AddDays(7)))
                    {
                        result.Add(item);
                    }
                    else if (item.Ay8 == null && (today > item.OdemeBaslangicTarihi.Value.AddMonths(7).AddDays(7)))
                    {
                        result.Add(item);
                    }
                    else if (item.Ay9 == null && (today > item.OdemeBaslangicTarihi.Value.AddMonths(8).AddDays(7)))
                    {
                        result.Add(item);
                    }
                    else if (item.Ay10 == null && (today > item.OdemeBaslangicTarihi.Value.AddMonths(9).AddDays(7)))
                    {
                        result.Add(item);
                    }
                    else if (item.Ay11 == null && (today > item.OdemeBaslangicTarihi.Value.AddMonths(10).AddDays(7)))
                    {
                        result.Add(item);
                    }
                    else if (item.Ay12 == null && (today > item.OdemeBaslangicTarihi.Value.AddMonths(11).AddDays(7)))
                    {
                        result.Add(item);
                    }
                }

                using (var context = new Entities())
                {
                    context.Database.ExecuteSqlCommand("TRUNCATE TABLE [KiraTakip]");
                }

                MAKBUZ_SOZLESME[] makbuz = result.Cast<MAKBUZ_SOZLESME>().ToArray();

                for (int i = 0; i < makbuz.Length; i++)
                {
                    KiraTakip kt = new KiraTakip();
                    if (db.KiraTakip.Count() != 0)
                    {
                        kt.KiraTakipID = db.KiraTakip.Max(item => item.KiraTakipID) + 1;
                    }
                    else
                    {
                        kt.KiraTakipID = 1;
                    }
                    kt.Ad = makbuz[i].KiraciAdi;
                    kt.SoyAd = makbuz[i].KiraciSoyAdi;
                    kt.Cep = makbuz[i].Cep;
                    kt.OdemeTarihi = makbuz[i].OdemeBaslangicTarihi;
                    kt.Note = makbuz[i].Note;

                    db.KiraTakip.Add(kt);
                }

                db.SaveChanges();

                return View();
            }
        }
    }
}