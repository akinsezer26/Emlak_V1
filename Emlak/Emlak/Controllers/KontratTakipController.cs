using Emlak.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emlak.Controllers
{
    public class KontratTakipController : Controller
    {
        // GET: KontratTakip
        public ActionResult Index()
        {
            using (Entities db = new Entities())
            {
                var today = DateTime.Today;
                List<MAKBUZ_SOZLESME> makbuzSozlesmeList = db.MAKBUZ_SOZLESME.ToList<MAKBUZ_SOZLESME>();
                List<MAKBUZ_SOZLESME> result = new List<MAKBUZ_SOZLESME>();
                foreach (var item in makbuzSozlesmeList)
                {
                    if (today>item.OdemeBaslangicTarihi.Value.AddMonths(item.KacAy.Value-2) && today< item.OdemeBaslangicTarihi.Value.AddMonths(item.KacAy.Value))
                    {
                        result.Add(item);
                    }
                }

                using (var context = new Entities())
                {
                    context.Database.ExecuteSqlCommand("TRUNCATE TABLE [KontratTakip]");
                }

                MAKBUZ_SOZLESME[] makbuz = result.Cast<MAKBUZ_SOZLESME>().ToArray();

                for (int i = 0; i < makbuz.Length; i++)
                {
                    KontratTakip kt = new KontratTakip();
                    if (db.KontratTakip.Count() != 0)
                    {
                        kt.KontratTakipID = db.KontratTakip.Max(item => item.KontratTakipID) + 1;
                    }
                    else
                    {
                        kt.KontratTakipID = 1;
                    }

                    int toplamKalan=0;

                    if (makbuz[i].Ay1!=null && makbuz[i].KacAy.Value>1)
                    {
                        toplamKalan += makbuz[i].AylikOdemeTL.Value - makbuz[i].Ay1.Value;
                    }
                    else if(makbuz[i].KacAy.Value > 1)
                    {
                        toplamKalan += makbuz[i].AylikOdemeTL.Value;
                    }
                    if (makbuz[i].Ay2 != null && makbuz[i].KacAy.Value > 2)
                    {
                        toplamKalan += makbuz[i].AylikOdemeTL.Value - makbuz[i].Ay2.Value;
                    }
                    else if (makbuz[i].KacAy.Value > 2)
                    {
                        toplamKalan += makbuz[i].AylikOdemeTL.Value;
                    }
                    if (makbuz[i].Ay2 != null && makbuz[i].KacAy.Value > 2)
                    {
                        toplamKalan += makbuz[i].AylikOdemeTL.Value - makbuz[i].Ay2.Value;
                    }
                    else if (makbuz[i].KacAy.Value > 2)
                    {
                        toplamKalan += makbuz[i].AylikOdemeTL.Value;
                    }
                    if (makbuz[i].Ay3 != null && makbuz[i].KacAy.Value > 3)
                    {
                        toplamKalan += makbuz[i].AylikOdemeTL.Value - makbuz[i].Ay3.Value;
                    }
                    else if (makbuz[i].KacAy.Value > 3)
                    {
                        toplamKalan += makbuz[i].AylikOdemeTL.Value;
                    }
                    if (makbuz[i].Ay4 != null && makbuz[i].KacAy.Value > 4)
                    {
                        toplamKalan += makbuz[i].AylikOdemeTL.Value - makbuz[i].Ay4.Value;
                    }
                    else if (makbuz[i].KacAy.Value > 4)
                    {
                        toplamKalan += makbuz[i].AylikOdemeTL.Value;
                    }
                    if (makbuz[i].Ay5 != null && makbuz[i].KacAy.Value > 5)
                    {
                        toplamKalan += makbuz[i].AylikOdemeTL.Value - makbuz[i].Ay5.Value;
                    }
                    else if (makbuz[i].KacAy.Value > 5)
                    {
                        toplamKalan += makbuz[i].AylikOdemeTL.Value;
                    }
                    if (makbuz[i].Ay6 != null && makbuz[i].KacAy.Value > 6)
                    {
                        toplamKalan += makbuz[i].AylikOdemeTL.Value - makbuz[i].Ay6.Value;
                    }
                    else if (makbuz[i].KacAy.Value > 6)
                    {
                        toplamKalan += makbuz[i].AylikOdemeTL.Value;
                    }
                    if (makbuz[i].Ay7 != null && makbuz[i].KacAy.Value > 7)
                    {
                        toplamKalan += makbuz[i].AylikOdemeTL.Value - makbuz[i].Ay7.Value;
                    }
                    else if (makbuz[i].KacAy.Value > 7)
                    {
                        toplamKalan += makbuz[i].AylikOdemeTL.Value;
                    }
                    if (makbuz[i].Ay8 != null && makbuz[i].KacAy.Value > 8)
                    {
                        toplamKalan += makbuz[i].AylikOdemeTL.Value - makbuz[i].Ay8.Value;
                    }
                    else if (makbuz[i].KacAy.Value > 8)
                    {
                        toplamKalan += makbuz[i].AylikOdemeTL.Value;
                    }
                    if (makbuz[i].Ay9 != null && makbuz[i].KacAy.Value > 9)
                    {
                        toplamKalan += makbuz[i].AylikOdemeTL.Value - makbuz[i].Ay9.Value;
                    }
                    else if (makbuz[i].KacAy.Value > 9)
                    {
                        toplamKalan += makbuz[i].AylikOdemeTL.Value;
                    }
                    if (makbuz[i].Ay10 != null && makbuz[i].KacAy.Value > 10)
                    {
                        toplamKalan += makbuz[i].AylikOdemeTL.Value - makbuz[i].Ay10.Value;
                    }
                    else if (makbuz[i].KacAy.Value > 10)
                    {
                        toplamKalan += makbuz[i].AylikOdemeTL.Value;
                    }
                    if (makbuz[i].Ay11 != null && makbuz[i].KacAy.Value > 11)
                    {
                        toplamKalan += makbuz[i].AylikOdemeTL.Value - makbuz[i].Ay11.Value;
                    }
                    else if (makbuz[i].KacAy.Value > 11)
                    {
                        toplamKalan += makbuz[i].AylikOdemeTL.Value;
                    }
                    if (makbuz[i].Ay12 != null && makbuz[i].KacAy.Value > 12)
                    {
                        toplamKalan += makbuz[i].AylikOdemeTL.Value - makbuz[i].Ay12.Value;
                    }
                    else if (makbuz[i].KacAy.Value > 12)
                    {
                        toplamKalan += makbuz[i].AylikOdemeTL.Value;
                    }
                    kt.Ad = makbuz[i].KiraciAdi;
                    kt.SoyAd = makbuz[i].KiraciSoyAdi;
                    kt.TC = makbuz[i].KiraciTCKimlik;
                    kt.Cep = makbuz[i].Cep;
                    kt.OdemeTarihi = makbuz[i].OdemeBaslangicTarihi.Value.AddMonths(makbuz[i].KacAy.Value);
                    kt.KaçAy = makbuz[i].KacAy;
                    kt.Kalan = toplamKalan;
                    kt.Note = makbuz[i].Note;

                    db.KontratTakip.Add(kt);
                }

                db.SaveChanges();

                return View();
            }
        }
    }
}