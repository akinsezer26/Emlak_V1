using Emlak.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emlak.Controllers
{
    public class BorcluListesiController : Controller
    {
        // GET: BorcluListesi
        public ActionResult Index()
        {
            using (Entities db = new Entities())
            {
                var today = DateTime.Today;
                List<MAKBUZ_SOZLESME> makbuzSozlesmeList = db.MAKBUZ_SOZLESME.ToList<MAKBUZ_SOZLESME>();
                
                using (var context = new Entities())
                {
                    context.Database.ExecuteSqlCommand("TRUNCATE TABLE [BorcluTakip]");
                }

                foreach (var item in makbuzSozlesmeList)
                {
                    int zaman = today.Month - item.OdemeBaslangicTarihi.Value.Month;
                    if (zaman > item.KacAy.Value)
                    {
                        zaman = item.KacAy.Value;
                    }
                    switch (zaman)
                    {
                        case 0:
                            break;
                        case 1:
                            if ((item.Ay1 < item.AylikOdemeTL) || item.Ay1==null)
                            {
                                BorcluTakip bt = new BorcluTakip();
                                if (db.BorcluTakip.Count() != 0)
                                {
                                    bt.BorcluTakipID = db.BorcluTakip.Max(iter => iter.BorcluTakipID) + 1;
                                }
                                else
                                {
                                    bt.BorcluTakipID = 1;
                                }

                                bt.Ad = item.KiraciAdi;
                                bt.SoyAd = item.KiraciSoyAdi;
                                bt.Cep = item.Cep;
                                bt.OdemeToplamTL = item.KacAy * item.AylikOdemeTL;
                                bt.Odenen = (item.Ay1 ?? 0); //!!!!
                                bt.Kalan = item.KacAy * item.AylikOdemeTL - (item.Ay1 ?? 0); //!!!!!
                                bt.OdemeTarihi = item.OdemeBaslangicTarihi.Value.AddMonths(item.KacAy.Value);
                                bt.Note = item.Note;

                                db.BorcluTakip.Add(bt);
                            }
                            break;
                        case 2:
                            if (((item.Ay1 < item.AylikOdemeTL) || item.Ay1 == null)
                                || ((item.Ay2 < item.AylikOdemeTL) || item.Ay2 == null))
                            {
                                BorcluTakip bt = new BorcluTakip();
                                if (db.BorcluTakip.Count() != 0)
                                {
                                    bt.BorcluTakipID = db.BorcluTakip.Max(iter => iter.BorcluTakipID) + 1;
                                }
                                else
                                {
                                    bt.BorcluTakipID = 1;
                                }

                                bt.Ad = item.KiraciAdi;
                                bt.SoyAd = item.KiraciSoyAdi;
                                bt.Cep = item.Cep;
                                bt.OdemeToplamTL = item.KacAy * item.AylikOdemeTL;
                                bt.Odenen = (item.Ay1 ?? 0) + (item.Ay2 ?? 0); //!!!!
                                bt.Kalan = item.KacAy * item.AylikOdemeTL - (item.Ay1 ?? 0) - (item.Ay2 ?? 0); //!!!!!
                                bt.OdemeTarihi = item.OdemeBaslangicTarihi.Value.AddMonths(item.KacAy.Value);
                                bt.Note = item.Note;

                                db.BorcluTakip.Add(bt);
                            }
                            break;
                        case 3:
                            if (((item.Ay1 < item.AylikOdemeTL) || item.Ay1 == null)
                                || ((item.Ay2 < item.AylikOdemeTL) || item.Ay2 == null)
                                || ((item.Ay3 < item.AylikOdemeTL) || item.Ay3 == null))
                            {
                                BorcluTakip bt = new BorcluTakip();
                                if (db.BorcluTakip.Count() != 0)
                                {
                                    bt.BorcluTakipID = db.BorcluTakip.Max(iter => iter.BorcluTakipID) + 1;
                                }
                                else
                                {
                                    bt.BorcluTakipID = 1;
                                }

                                bt.Ad = item.KiraciAdi;
                                bt.SoyAd = item.KiraciSoyAdi;
                                bt.Cep = item.Cep;
                                bt.OdemeToplamTL = item.KacAy * item.AylikOdemeTL;
                                bt.Odenen = (item.Ay1 ?? 0) + (item.Ay2 ?? 0) + (item.Ay3 ?? 0); //!!!!
                                bt.Kalan = item.KacAy * item.AylikOdemeTL - (item.Ay1 ?? 0) - (item.Ay2 ?? 0) - (item.Ay3 ?? 0); //!!!!!
                                bt.OdemeTarihi = item.OdemeBaslangicTarihi.Value.AddMonths(item.KacAy.Value);
                                bt.Note = item.Note;

                                db.BorcluTakip.Add(bt);
                            }
                            break;
                        case 4:
                            if (((item.Ay1 < item.AylikOdemeTL) || item.Ay1 == null)
                                || ((item.Ay2 < item.AylikOdemeTL) || item.Ay2 == null)
                                || ((item.Ay3 < item.AylikOdemeTL) || item.Ay3 == null)
                                || ((item.Ay4 < item.AylikOdemeTL) || item.Ay4 == null))
                            {
                                BorcluTakip bt = new BorcluTakip();
                                if (db.BorcluTakip.Count() != 0)
                                {
                                    bt.BorcluTakipID = db.BorcluTakip.Max(iter => iter.BorcluTakipID) + 1;
                                }
                                else
                                {
                                    bt.BorcluTakipID = 1;
                                }

                                bt.Ad = item.KiraciAdi;
                                bt.SoyAd = item.KiraciSoyAdi;
                                bt.Cep = item.Cep;
                                bt.OdemeToplamTL = item.KacAy * item.AylikOdemeTL;
                                bt.Odenen = (item.Ay1 ?? 0) + (item.Ay2 ?? 0) + (item.Ay3 ?? 0) + (item.Ay4 ?? 0); //!!!!
                                bt.Kalan = item.KacAy * item.AylikOdemeTL - (item.Ay1 ?? 0) - (item.Ay2 ?? 0) - (item.Ay3 ?? 0) - (item.Ay4 ?? 0); //!!!!!
                                bt.OdemeTarihi = item.OdemeBaslangicTarihi.Value.AddMonths(item.KacAy.Value);
                                bt.Note = item.Note;

                                db.BorcluTakip.Add(bt);
                            }
                            break;
                        case 5:
                            if (((item.Ay1 < item.AylikOdemeTL) || item.Ay1 == null)
                                || ((item.Ay2 < item.AylikOdemeTL) || item.Ay2 == null)
                                || ((item.Ay3 < item.AylikOdemeTL) || item.Ay3 == null)
                                || ((item.Ay4 < item.AylikOdemeTL) || item.Ay4 == null)
                                || ((item.Ay5 < item.AylikOdemeTL) || item.Ay5 == null))
                            {
                                BorcluTakip bt = new BorcluTakip();
                                if (db.BorcluTakip.Count() != 0)
                                {
                                    bt.BorcluTakipID = db.BorcluTakip.Max(iter => iter.BorcluTakipID) + 1;
                                }
                                else
                                {
                                    bt.BorcluTakipID = 1;
                                }

                                bt.Ad = item.KiraciAdi;
                                bt.SoyAd = item.KiraciSoyAdi;
                                bt.Cep = item.Cep;
                                bt.OdemeToplamTL = item.KacAy * item.AylikOdemeTL;
                                bt.Odenen = (item.Ay1 ?? 0) + (item.Ay2 ?? 0) + (item.Ay3 ?? 0) + (item.Ay4 ?? 0) + (item.Ay5 ?? 0); //!!!!
                                bt.Kalan = item.KacAy * item.AylikOdemeTL - (item.Ay1 ?? 0) - (item.Ay2 ?? 0) - (item.Ay3 ?? 0) - (item.Ay4 ?? 0) - (item.Ay5 ?? 0); //!!!!!
                                bt.OdemeTarihi = item.OdemeBaslangicTarihi.Value.AddMonths(item.KacAy.Value);
                                bt.Note = item.Note;

                                db.BorcluTakip.Add(bt);
                            }
                            break;
                        case 6:
                            if (((item.Ay1 < item.AylikOdemeTL) || item.Ay1 == null)
                                || ((item.Ay2 < item.AylikOdemeTL) || item.Ay2 == null)
                                || ((item.Ay3 < item.AylikOdemeTL) || item.Ay3 == null)
                                || ((item.Ay4 < item.AylikOdemeTL) || item.Ay4 == null)
                                || ((item.Ay5 < item.AylikOdemeTL) || item.Ay5 == null)
                                || ((item.Ay6 < item.AylikOdemeTL) || item.Ay6 == null))
                            {
                                BorcluTakip bt = new BorcluTakip();
                                if (db.BorcluTakip.Count() != 0)
                                {
                                    bt.BorcluTakipID = db.BorcluTakip.Max(iter => iter.BorcluTakipID) + 1;
                                }
                                else
                                {
                                    bt.BorcluTakipID = 1;
                                }

                                bt.Ad = item.KiraciAdi;
                                bt.SoyAd = item.KiraciSoyAdi;
                                bt.Cep = item.Cep;
                                bt.OdemeToplamTL = item.KacAy * item.AylikOdemeTL;
                                bt.Odenen = (item.Ay1 ?? 0) + (item.Ay2 ?? 0) + (item.Ay3 ?? 0) + (item.Ay4 ?? 0) + (item.Ay5 ?? 0) + (item.Ay6 ?? 0); //!!!!
                                bt.Kalan = item.KacAy * item.AylikOdemeTL - (item.Ay1 ?? 0) - (item.Ay2 ?? 0) - (item.Ay3 ?? 0) - (item.Ay4 ?? 0) - (item.Ay5 ?? 0) - (item.Ay6 ?? 0); //!!!!!
                                bt.OdemeTarihi = item.OdemeBaslangicTarihi.Value.AddMonths(item.KacAy.Value);
                                bt.Note = item.Note;

                                db.BorcluTakip.Add(bt);
                            }
                            break;
                        case 7:
                            if (((item.Ay1 < item.AylikOdemeTL) || item.Ay1 == null)
                                || ((item.Ay2 < item.AylikOdemeTL) || item.Ay2 == null)
                                || ((item.Ay3 < item.AylikOdemeTL) || item.Ay3 == null)
                                || ((item.Ay4 < item.AylikOdemeTL) || item.Ay4 == null)
                                || ((item.Ay5 < item.AylikOdemeTL) || item.Ay5 == null)
                                || ((item.Ay6 < item.AylikOdemeTL) || item.Ay6 == null)
                                || ((item.Ay7 < item.AylikOdemeTL) || item.Ay7 == null))
                            {
                                BorcluTakip bt = new BorcluTakip();
                                if (db.BorcluTakip.Count() != 0)
                                {
                                    bt.BorcluTakipID = db.BorcluTakip.Max(iter => iter.BorcluTakipID) + 1;
                                }
                                else
                                {
                                    bt.BorcluTakipID = 1;
                                }

                                bt.Ad = item.KiraciAdi;
                                bt.SoyAd = item.KiraciSoyAdi;
                                bt.Cep = item.Cep;
                                bt.OdemeToplamTL = item.KacAy * item.AylikOdemeTL;
                                bt.Odenen = (item.Ay1 ?? 0) + (item.Ay2 ?? 0) + (item.Ay3 ?? 0) + (item.Ay4 ?? 0) + (item.Ay5 ?? 0) + (item.Ay6 ?? 0) + (item.Ay7 ?? 0); //!!!!
                                bt.Kalan = item.KacAy * item.AylikOdemeTL - (item.Ay1 ?? 0) - (item.Ay2 ?? 0) - (item.Ay3 ?? 0) - (item.Ay4 ?? 0) - (item.Ay5 ?? 0) - (item.Ay6 ?? 0) - (item.Ay7 ?? 0); //!!!!!
                                bt.OdemeTarihi = item.OdemeBaslangicTarihi.Value.AddMonths(item.KacAy.Value);
                                bt.Note = item.Note;

                                db.BorcluTakip.Add(bt);
                            }
                            break;
                        case 8:
                            if (((item.Ay1 < item.AylikOdemeTL) || item.Ay1 == null)
                                || ((item.Ay2 < item.AylikOdemeTL) || item.Ay2 == null)
                                || ((item.Ay3 < item.AylikOdemeTL) || item.Ay3 == null)
                                || ((item.Ay4 < item.AylikOdemeTL) || item.Ay4 == null)
                                || ((item.Ay5 < item.AylikOdemeTL) || item.Ay5 == null)
                                || ((item.Ay6 < item.AylikOdemeTL) || item.Ay6 == null)
                                || ((item.Ay7 < item.AylikOdemeTL) || item.Ay7 == null)
                                || ((item.Ay8 < item.AylikOdemeTL) || item.Ay8 == null))
                            {
                                BorcluTakip bt = new BorcluTakip();
                                if (db.BorcluTakip.Count() != 0)
                                {
                                    bt.BorcluTakipID = db.BorcluTakip.Max(iter => iter.BorcluTakipID) + 1;
                                }
                                else
                                {
                                    bt.BorcluTakipID = 1;
                                }

                                bt.Ad = item.KiraciAdi;
                                bt.SoyAd = item.KiraciSoyAdi;
                                bt.Cep = item.Cep;
                                bt.OdemeToplamTL = item.KacAy * item.AylikOdemeTL;
                                bt.Odenen = (item.Ay1 ?? 0) + (item.Ay2 ?? 0) + (item.Ay3 ?? 0) + (item.Ay4 ?? 0) + (item.Ay5 ?? 0) + (item.Ay6 ?? 0) + (item.Ay7 ?? 0) + (item.Ay8 ?? 0); //!!!!
                                bt.Kalan = item.KacAy * item.AylikOdemeTL - (item.Ay1 ?? 0) - (item.Ay2 ?? 0) - (item.Ay3 ?? 0) - (item.Ay4 ?? 0) - (item.Ay5 ?? 0) - (item.Ay6 ?? 0) - (item.Ay7 ?? 0) - (item.Ay8 ?? 0); //!!!!!
                                bt.OdemeTarihi = item.OdemeBaslangicTarihi.Value.AddMonths(item.KacAy.Value);
                                bt.Note = item.Note;

                                db.BorcluTakip.Add(bt);
                            }
                            break;
                        case 9:
                            if (((item.Ay1 < item.AylikOdemeTL) || item.Ay1 == null)
                                || ((item.Ay2 < item.AylikOdemeTL) || item.Ay2 == null)
                                || ((item.Ay3 < item.AylikOdemeTL) || item.Ay3 == null)
                                || ((item.Ay4 < item.AylikOdemeTL) || item.Ay4 == null)
                                || ((item.Ay5 < item.AylikOdemeTL) || item.Ay5 == null)
                                || ((item.Ay6 < item.AylikOdemeTL) || item.Ay6 == null)
                                || ((item.Ay7 < item.AylikOdemeTL) || item.Ay7 == null)
                                || ((item.Ay8 < item.AylikOdemeTL) || item.Ay8 == null)
                                || ((item.Ay9 < item.AylikOdemeTL) || item.Ay9 == null))
                            {
                                BorcluTakip bt = new BorcluTakip();
                                if (db.BorcluTakip.Count() != 0)
                                {
                                    bt.BorcluTakipID = db.BorcluTakip.Max(iter => iter.BorcluTakipID) + 1;
                                }
                                else
                                {
                                    bt.BorcluTakipID = 1;
                                }

                                bt.Ad = item.KiraciAdi;
                                bt.SoyAd = item.KiraciSoyAdi;
                                bt.Cep = item.Cep;
                                bt.OdemeToplamTL = item.KacAy * item.AylikOdemeTL;
                                bt.Odenen = (item.Ay1 ?? 0) + (item.Ay2 ?? 0) + (item.Ay3 ?? 0) + (item.Ay4 ?? 0) + (item.Ay5 ?? 0) + (item.Ay6 ?? 0) + (item.Ay7 ?? 0) + (item.Ay8 ?? 0) + (item.Ay9 ?? 0); //!!!!
                                bt.Kalan = item.KacAy * item.AylikOdemeTL - (item.Ay1 ?? 0) - (item.Ay2 ?? 0) - (item.Ay3 ?? 0) - (item.Ay4 ?? 0) - (item.Ay5 ?? 0) - (item.Ay6 ?? 0) - (item.Ay7 ?? 0) - (item.Ay8 ?? 0) - (item.Ay9 ?? 0); //!!!!!
                                bt.OdemeTarihi = item.OdemeBaslangicTarihi.Value.AddMonths(item.KacAy.Value);
                                bt.Note = item.Note;

                                db.BorcluTakip.Add(bt);
                            }
                            break;
                        case 10:
                            if (((item.Ay1 < item.AylikOdemeTL) || item.Ay1 == null)
                                || ((item.Ay2 < item.AylikOdemeTL) || item.Ay2 == null)
                                || ((item.Ay3 < item.AylikOdemeTL) || item.Ay3 == null)
                                || ((item.Ay4 < item.AylikOdemeTL) || item.Ay4 == null)
                                || ((item.Ay5 < item.AylikOdemeTL) || item.Ay5 == null)
                                || ((item.Ay6 < item.AylikOdemeTL) || item.Ay6 == null)
                                || ((item.Ay7 < item.AylikOdemeTL) || item.Ay7 == null)
                                || ((item.Ay8 < item.AylikOdemeTL) || item.Ay8 == null)
                                || ((item.Ay9 < item.AylikOdemeTL) || item.Ay9 == null)
                                || ((item.Ay10 < item.AylikOdemeTL) || item.Ay10 == null))
                            {
                                BorcluTakip bt = new BorcluTakip();
                                if (db.BorcluTakip.Count() != 0)
                                {
                                    bt.BorcluTakipID = db.BorcluTakip.Max(iter => iter.BorcluTakipID) + 1;
                                }
                                else
                                {
                                    bt.BorcluTakipID = 1;
                                }

                                bt.Ad = item.KiraciAdi;
                                bt.SoyAd = item.KiraciSoyAdi;
                                bt.Cep = item.Cep;
                                bt.OdemeToplamTL = item.KacAy * item.AylikOdemeTL;
                                bt.Odenen = (item.Ay1 ?? 0) + (item.Ay2 ?? 0) + (item.Ay3 ?? 0) + (item.Ay4 ?? 0) + (item.Ay5 ?? 0) + (item.Ay6 ?? 0) + (item.Ay7 ?? 0) + (item.Ay8 ?? 0) + (item.Ay9 ?? 0) + (item.Ay10 ?? 0); //!!!!
                                bt.Kalan = item.KacAy * item.AylikOdemeTL - (item.Ay1 ?? 0) - (item.Ay2 ?? 0) - (item.Ay3 ?? 0) - (item.Ay4 ?? 0) - (item.Ay5 ?? 0) - (item.Ay6 ?? 0) - (item.Ay7 ?? 0) - (item.Ay8 ?? 0) - (item.Ay9 ?? 0) - (item.Ay10 ?? 0); //!!!!!
                                bt.OdemeTarihi = item.OdemeBaslangicTarihi.Value.AddMonths(item.KacAy.Value);
                                bt.Note = item.Note;

                                db.BorcluTakip.Add(bt);
                            }
                            break;
                        case 11:
                            if (((item.Ay1 < item.AylikOdemeTL) || item.Ay1 == null)
                                || ((item.Ay2 < item.AylikOdemeTL) || item.Ay2 == null)
                                || ((item.Ay3 < item.AylikOdemeTL) || item.Ay3 == null)
                                || ((item.Ay4 < item.AylikOdemeTL) || item.Ay4 == null)
                                || ((item.Ay5 < item.AylikOdemeTL) || item.Ay5 == null)
                                || ((item.Ay6 < item.AylikOdemeTL) || item.Ay6 == null)
                                || ((item.Ay7 < item.AylikOdemeTL) || item.Ay7 == null)
                                || ((item.Ay8 < item.AylikOdemeTL) || item.Ay8 == null)
                                || ((item.Ay9 < item.AylikOdemeTL) || item.Ay9 == null)
                                || ((item.Ay10 < item.AylikOdemeTL) || item.Ay10 == null)
                                || ((item.Ay11 < item.AylikOdemeTL) || item.Ay11 == null))
                            {
                                BorcluTakip bt = new BorcluTakip();
                                if (db.BorcluTakip.Count() != 0)
                                {
                                    bt.BorcluTakipID = db.BorcluTakip.Max(iter => iter.BorcluTakipID) + 1;
                                }
                                else
                                {
                                    bt.BorcluTakipID = 1;
                                }

                                bt.Ad = item.KiraciAdi;
                                bt.SoyAd = item.KiraciSoyAdi;
                                bt.Cep = item.Cep;
                                bt.OdemeToplamTL = item.KacAy * item.AylikOdemeTL;
                                bt.Odenen = (item.Ay1 ?? 0) + (item.Ay2 ?? 0) + (item.Ay3 ?? 0) + (item.Ay4 ?? 0) + (item.Ay5 ?? 0) + (item.Ay6 ?? 0) + (item.Ay7 ?? 0) + (item.Ay8 ?? 0) + (item.Ay9 ?? 0) + (item.Ay10 ?? 0) + (item.Ay11 ?? 0); //!!!!
                                bt.Kalan = item.KacAy * item.AylikOdemeTL - (item.Ay1 ?? 0) - (item.Ay2 ?? 0) - (item.Ay3 ?? 0) - (item.Ay4 ?? 0) - (item.Ay5 ?? 0) - (item.Ay6 ?? 0) - (item.Ay7 ?? 0) - (item.Ay8 ?? 0) - (item.Ay9 ?? 0) - (item.Ay10 ?? 0) - (item.Ay11 ?? 0); //!!!!!
                                bt.OdemeTarihi = item.OdemeBaslangicTarihi.Value.AddMonths(item.KacAy.Value);
                                bt.Note = item.Note;

                                db.BorcluTakip.Add(bt);
                            }
                            break;
                        case 12:
                            if (((item.Ay1 < item.AylikOdemeTL) || item.Ay1 == null)
                                || ((item.Ay2 < item.AylikOdemeTL) || item.Ay2 == null)
                                || ((item.Ay3 < item.AylikOdemeTL) || item.Ay3 == null)
                                || ((item.Ay4 < item.AylikOdemeTL) || item.Ay4 == null)
                                || ((item.Ay5 < item.AylikOdemeTL) || item.Ay5 == null)
                                || ((item.Ay6 < item.AylikOdemeTL) || item.Ay6 == null)
                                || ((item.Ay7 < item.AylikOdemeTL) || item.Ay7 == null)
                                || ((item.Ay8 < item.AylikOdemeTL) || item.Ay8 == null)
                                || ((item.Ay9 < item.AylikOdemeTL) || item.Ay9 == null)
                                || ((item.Ay10 < item.AylikOdemeTL) || item.Ay10 == null)
                                || ((item.Ay11 < item.AylikOdemeTL) || item.Ay11 == null)
                                || ((item.Ay12 < item.AylikOdemeTL) || item.Ay12 == null))
                            {
                                BorcluTakip bt = new BorcluTakip();
                                if (db.BorcluTakip.Count() != 0)
                                {
                                    bt.BorcluTakipID = db.BorcluTakip.Max(iter => iter.BorcluTakipID) + 1;
                                }
                                else
                                {
                                    bt.BorcluTakipID = 1;
                                }

                                bt.Ad = item.KiraciAdi;
                                bt.SoyAd = item.KiraciSoyAdi;
                                bt.Cep = item.Cep;
                                bt.OdemeToplamTL = item.KacAy * item.AylikOdemeTL;
                                bt.Odenen = (item.Ay1 ?? 0) + (item.Ay2 ?? 0) + (item.Ay3 ?? 0) + (item.Ay4 ?? 0) + (item.Ay5 ?? 0) + (item.Ay6 ?? 0) + (item.Ay7 ?? 0) + (item.Ay8 ?? 0) + (item.Ay9 ?? 0) + (item.Ay10 ?? 0) + (item.Ay11 ?? 0) + (item.Ay12 ?? 0); //!!!!
                                bt.Kalan = item.KacAy * item.AylikOdemeTL - (item.Ay1 ?? 0) - (item.Ay2 ?? 0) - (item.Ay3 ?? 0) - (item.Ay4 ?? 0) - (item.Ay5 ?? 0) - (item.Ay6 ?? 0) - (item.Ay7 ?? 0) - (item.Ay8 ?? 0) - (item.Ay9 ?? 0) - (item.Ay10 ?? 0) - (item.Ay11 ?? 0) - (item.Ay12 ?? 0); //!!!!!
                                bt.OdemeTarihi = item.OdemeBaslangicTarihi.Value.AddMonths(item.KacAy.Value);
                                bt.Note = item.Note;

                                db.BorcluTakip.Add(bt);
                            }
                            break;
                        default:
                            break;
                    }
                }

                db.SaveChanges();

                return View();
            }
        }
    }
}