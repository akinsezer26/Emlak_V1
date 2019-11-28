using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Emlak.Models;
using Newtonsoft.Json;

namespace Emlak.Controllers
{
    public class DBController : Controller
    {
        // GET: DB
        public ActionResult Index()
        {
            return View();
        }
      
        public bool setViewBags()
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
                                                      Text = i.ISITMA_TIPI,
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
                return true;
            }
        }

        public ActionResult getAjanda()
        {
            using (Entities db = new Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<AJANDA> ajandaList = db.AJANDA.ToList<AJANDA>();
                return Json(new { data = ajandaList }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AddOrEditAjanda(int id = 0)
        {
            if (id == 0)
            {
                return View(new AJANDA());
            }
            else
            {
                using (Entities db = new Entities())
                {
                    return View(db.AJANDA.Where(x => x.AjandaID == id).FirstOrDefault<AJANDA>());
                }
            }
        }
        [HttpPost]
        public ActionResult AddOrEditAjanda(AJANDA ajanda)
        {
            using (Entities db = new Entities())
            {
                if (ajanda.AjandaID == 0)
                {
                    ajanda.UserID = Int16.Parse(Session["userID"].ToString());
                    db.AJANDA.Add(ajanda);

                    db.SaveChanges();

                    return Json(new { success = true, message = "Başarıyla Kaydedildi" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ajanda.UserID = Int16.Parse(Session["userID"].ToString());
                    db.Entry(ajanda).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Başarıyla Güncellendi" }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        [HttpPost]
        public ActionResult DeleteAjanda(int id)
        {
            using (Entities db = new Entities())
            {
                AJANDA ajanda = db.AJANDA.Where(x => x.AjandaID == id).FirstOrDefault<AJANDA>();
                db.AJANDA.Remove(ajanda);
                db.SaveChanges();
                return Json(new { success = true, message = "Başarıyla Silindi" }, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult getAlacaklarim()
        {
            using (Entities db = new Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<ALACAKLARIM> alacaklarimlist = db.ALACAKLARIM.ToList<ALACAKLARIM>();
                return Json(new { data = alacaklarimlist }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AddOrEditAlacaklarim(int id = 0)
        {
            if (id == 0)
            {
                return View(new ALACAKLARIM());
            }
            else
            {
                using (Entities db = new Entities())
                {
                    return View(db.ALACAKLARIM.Where(x => x.AlacakID == id).FirstOrDefault<ALACAKLARIM>());
                }
            }
        }
        [HttpPost]
        public ActionResult AddOrEditAlacaklarim(ALACAKLARIM alacaklarim)
        {
            using (Entities db = new Entities())
            {
                if (alacaklarim.AlacakID == 0)
                {
                    alacaklarim.UserID = Int16.Parse(Session["userID"].ToString());
                    alacaklarim.tarih = DateTime.Now;
                    db.ALACAKLARIM.Add(alacaklarim);

                    db.SaveChanges();

                    return Json(new { success = true, message = "Başarıyla Kaydedildi" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    alacaklarim.UserID = Int16.Parse(Session["userID"].ToString());
                    alacaklarim.tarih = DateTime.Now;
                    db.Entry(alacaklarim).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Başarıyla Güncellendi" }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        [HttpPost]
        public ActionResult DeleteAlacaklarim(int id)
        {
            using (Entities db = new Entities())
            {
                ALACAKLARIM alacaklarim = db.ALACAKLARIM.Where(x => x.AlacakID == id).FirstOrDefault<ALACAKLARIM>();
                db.ALACAKLARIM.Remove(alacaklarim);
                db.SaveChanges();
                return Json(new { success = true, message = "Başarıyla Silindi" }, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult getBorclarim()
        {
            using (Entities db = new Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<BORCLARIM> borclarimlist = db.BORCLARIM.ToList<BORCLARIM>();
                return Json(new { data = borclarimlist }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AddOrEditBorclarim(int id = 0)
        {
            if (id == 0)
            {
                return View(new BORCLARIM());
            }
            else
            {
                using (Entities db = new Entities())
                {
                    return View(db.BORCLARIM.Where(x => x.BorcID == id).FirstOrDefault<BORCLARIM>());
                }
            }
        }
        [HttpPost]
        public ActionResult AddOrEditBorclarim(BORCLARIM borclarim)
        {
            using (Entities db = new Entities())
            {
                if (borclarim.BorcID == 0)
                {
                    borclarim.UserID = Int16.Parse(Session["userID"].ToString());
                    borclarim.Tarih = DateTime.Now;
                    db.BORCLARIM.Add(borclarim);

                    db.SaveChanges();

                    return Json(new { success = true, message = "Başarıyla Kaydedildi" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    borclarim.UserID = Int16.Parse(Session["userID"].ToString());
                    borclarim.Tarih = DateTime.Now;
                    db.Entry(borclarim).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Başarıyla Güncellendi" }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        [HttpPost]
        public ActionResult DeleteBorclarim(int id)
        {
            using (Entities db = new Entities())
            {
                BORCLARIM borclarim = db.BORCLARIM.Where(x => x.BorcID == id).FirstOrDefault<BORCLARIM>();
                db.BORCLARIM.Remove(borclarim);
                db.SaveChanges();
                return Json(new { success = true, message = "Başarıyla Silindi" }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult getEmlakArayanlar()
        {
            using (Entities db = new Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<EmlakArayanlar> EmlakArayanlarlist = db.EmlakArayanlar.ToList<EmlakArayanlar>();
                return Json(new { data = EmlakArayanlarlist }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AddOrEditEmlakArayanlar(int id = 0)
        {
            setViewBags();
            if (id == 0)
            {
                return View(new EmlakArayanlar());
            }
            else
            {
                using (Entities db = new Entities())
                {
                    return View(db.EmlakArayanlar.Where(x => x.ArayanlarID == id).FirstOrDefault<EmlakArayanlar>());
                }
            }
        }
        [HttpPost]
        public ActionResult AddOrEditEmlakArayanlar(EmlakArayanlar arayanlar)
        {
            using (Entities db = new Entities())
            {
                if (arayanlar.ArayanlarID == 0)
                {
                    arayanlar.UserID = Int16.Parse(Session["userID"].ToString());
                    //alacaklarim.tarih = DateTime.Now;
                    db.EmlakArayanlar.Add(arayanlar);

                    db.SaveChanges();

                    return Json(new { success = true, message = "Başarıyla Kaydedildi" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    arayanlar.UserID = Int16.Parse(Session["userID"].ToString());
                    db.Entry(arayanlar).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Başarıyla Güncellendi" }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        [HttpPost]
        public ActionResult DeleteEmlakArayanlar(int id)
        {
            using (Entities db = new Entities())
            {
                EmlakArayanlar arayanlar = db.EmlakArayanlar.Where(x => x.ArayanlarID == id).FirstOrDefault<EmlakArayanlar>();
                db.EmlakArayanlar.Remove(arayanlar);
                db.SaveChanges();
                return Json(new { success = true, message = "Başarıyla Silindi" }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult getKonutSatilik()
        {
            //var draw = Request.Form.GetValues("draw").FirstOrDefault();
            //var start = Request.Form.GetValues("start").FirstOrDefault();
            //var length = Request.Form.GetValues("length").FirstOrDefault();
            //var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            //var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

            //var il = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();

            //int pageSize = length != null ? Convert.ToInt32(length) : 0;
            //int skip = start != null ? Convert.ToInt16(start) : 0;
            //int records = 0;

            using (Entities db = new Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<KONUT_ISYERI> KonutList = db.KONUT_ISYERI.Where(s => s.Isyeri_Konut == "Konut" && s.Kira_Satilik=="Satılık")
                    .ToList<KONUT_ISYERI>();


                return Json(new { data = KonutList.Select((x, idx) => new { x, idx })
                    .GroupBy(x => x.idx / 4)
                    .Select(g => g.Select(a => a.x)) }
                , JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AddOrEditKonutSatilik(int id = 0)
        {
            setViewBags();
            if (id == 0)
            {
                return View(new KONUT_ISYERI());
            }
            else
            {
                using (Entities db = new Entities())
                {
                    return View(db.KONUT_ISYERI.Where(x => x.ID == id).FirstOrDefault<KONUT_ISYERI>());
                }
            }
        }
        [HttpPost]
        public ActionResult AddOrEditKonutSatilik(KONUT_ISYERI konutisyeri)
        {
            using (Entities db = new Entities())
            {
                try
                {
                    Debug.WriteLine(Request.Files.Count.ToString());
                    if (Request.Files.Count < 25 && Path.GetFileName(Request.Files[0].FileName)!="")
                    {
                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            string imgname = Path.GetFileName(Request.Files[i].FileName);
                            string ext = Path.GetExtension(imgname);
                            if (ext == ".jpg" || ext==".JPG" || ext == ".jpeg")
                            {
                                //success
                            }
                            else { return Json(new { status = "error", message = "Lütfen jpg veya png uzantılı dosyalar yükleyin!" }); }
                        }

                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            string imgname = Path.GetFileName(Request.Files[i].FileName);
                            string ext = Path.GetExtension(imgname);

                            string imgpath = Path.Combine(Server.MapPath("~/EmlakImages"), "Kisyeri_" + (db.KONUT_ISYERI.Max(item => item.ID) + 1).ToString() + "_" + (i + 1).ToString() + ext);
                            Request.Files[i].SaveAs(imgpath);
                            switch (i)
                            {
                                case 0:
                                    konutisyeri.picture1 = imgpath;
                                    break;
                                case 1:
                                    konutisyeri.picture2 = imgpath;
                                    break;
                                case 2:
                                    konutisyeri.picture3 = imgpath;
                                    break;
                                case 3:
                                    konutisyeri.picture4 = imgpath;
                                    break;
                                case 4:
                                    konutisyeri.picture5 = imgpath;
                                    break;
                                case 5:
                                    konutisyeri.picture6 = imgpath;
                                    break;
                                case 6:
                                    konutisyeri.picture7 = imgpath;
                                    break;
                                case 7:
                                    konutisyeri.picture8 = imgpath;
                                    break;
                                case 9:
                                    konutisyeri.picture10 = imgpath;
                                    break;
                                case 10:
                                    konutisyeri.picture11 = imgpath;
                                    break;
                                case 11:
                                    konutisyeri.picture12 = imgpath;
                                    break;
                                case 12:
                                    konutisyeri.picture13 = imgpath;
                                    break;
                                case 13:
                                    konutisyeri.picture14 = imgpath;
                                    break;
                                case 14:
                                    konutisyeri.picture15 = imgpath;
                                    break;
                                case 15:
                                    konutisyeri.picture16 = imgpath;
                                    break;
                                case 16:
                                    konutisyeri.picture17 = imgpath;
                                    break;
                                case 17:
                                    konutisyeri.picture18 = imgpath;
                                    break;
                                case 18:
                                    konutisyeri.picture19 = imgpath;
                                    break;
                                case 19:
                                    konutisyeri.picture20 = imgpath;
                                    break;
                                case 20:
                                    konutisyeri.picture21 = imgpath;
                                    break;
                                case 21:
                                    konutisyeri.picture22 = imgpath;
                                    break;
                                case 22:
                                    konutisyeri.picture23 = imgpath;
                                    break;
                                case 23:
                                    konutisyeri.picture24 = imgpath;
                                    break;
                                case 24:
                                    konutisyeri.picture25 = imgpath;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else if(Request.Files.Count>25 && Path.GetFileName(Request.Files[0].FileName) != "") {return Json(new { status = "error", message = "25'den fazla resim giremezsiniz!" }); }
                    if (konutisyeri.ID == 0)
                    {
                        konutisyeri.UserID = Int16.Parse(Session["userID"].ToString());
                        konutisyeri.Isyeri_Konut = "Konut";
                        konutisyeri.Tarih = DateTime.Now;
                        konutisyeri.Kira_Satilik = "Satılık";
                        db.KONUT_ISYERI.Add(konutisyeri);
 
                        db.SaveChanges();

                        return Json(new { success = true, message = "Başarıyla Kaydedildi" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        konutisyeri.UserID = Int16.Parse(Session["userID"].ToString());
                        db.Entry(konutisyeri).State = EntityState.Modified;
                        db.SaveChanges();
                        return Json(new { success = true, message = "Başarıyla Güncellendi" }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    throw;
                }
            }
        }



        public ActionResult getKonutKiralik() {
            using (Entities db = new Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<KONUT_ISYERI> KonutList = db.KONUT_ISYERI.Where(s => s.Isyeri_Konut == "Konut" && s.Kira_Satilik == "Kiralik")
                    .ToList<KONUT_ISYERI>();


                return Json(new
                {
                    data = KonutList.Select((x, idx) => new { x, idx })
                    .GroupBy(x => x.idx / 4)
                    .Select(g => g.Select(a => a.x))
                }
                , JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AddOrEditKonutKiralik(int id = 0)
        {
            setViewBags();
            if (id == 0)
            {
                return View(new KONUT_ISYERI());
            }
            else
            {
                using (Entities db = new Entities())
                {
                    return View(db.KONUT_ISYERI.Where(x => x.ID == id).FirstOrDefault<KONUT_ISYERI>());
                }
            }
        }
        [HttpPost]
        public ActionResult AddOrEditKonutKiralik(KONUT_ISYERI konutisyeri)
        {
            using (Entities db = new Entities())
            {
                try
                {
                    Debug.WriteLine(Request.Files.Count.ToString());
                    if (Request.Files.Count < 25 && Path.GetFileName(Request.Files[0].FileName) != "")
                    {
                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            string imgname = Path.GetFileName(Request.Files[i].FileName);
                            string ext = Path.GetExtension(imgname);
                            if (ext == ".jpg" || ext == ".JPG" || ext == ".jpeg")
                            {
                                //success
                            }
                            else { return Json(new { status = "error", message = "Lütfen jpg veya png uzantılı dosyalar yükleyin!" }); }
                        }

                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            string imgname = Path.GetFileName(Request.Files[i].FileName);
                            string ext = Path.GetExtension(imgname);

                            string imgpath = Path.Combine(Server.MapPath("~/EmlakImages"), "Kisyeri_" + (db.KONUT_ISYERI.Max(item => item.ID) + 1).ToString() + "_" + (i + 1).ToString() + ext);
                            Request.Files[i].SaveAs(imgpath);
                            switch (i)
                            {
                                case 0:
                                    konutisyeri.picture1 = imgpath;
                                    break;
                                case 1:
                                    konutisyeri.picture2 = imgpath;
                                    break;
                                case 2:
                                    konutisyeri.picture3 = imgpath;
                                    break;
                                case 3:
                                    konutisyeri.picture4 = imgpath;
                                    break;
                                case 4:
                                    konutisyeri.picture5 = imgpath;
                                    break;
                                case 5:
                                    konutisyeri.picture6 = imgpath;
                                    break;
                                case 6:
                                    konutisyeri.picture7 = imgpath;
                                    break;
                                case 7:
                                    konutisyeri.picture8 = imgpath;
                                    break;
                                case 9:
                                    konutisyeri.picture10 = imgpath;
                                    break;
                                case 10:
                                    konutisyeri.picture11 = imgpath;
                                    break;
                                case 11:
                                    konutisyeri.picture12 = imgpath;
                                    break;
                                case 12:
                                    konutisyeri.picture13 = imgpath;
                                    break;
                                case 13:
                                    konutisyeri.picture14 = imgpath;
                                    break;
                                case 14:
                                    konutisyeri.picture15 = imgpath;
                                    break;
                                case 15:
                                    konutisyeri.picture16 = imgpath;
                                    break;
                                case 16:
                                    konutisyeri.picture17 = imgpath;
                                    break;
                                case 17:
                                    konutisyeri.picture18 = imgpath;
                                    break;
                                case 18:
                                    konutisyeri.picture19 = imgpath;
                                    break;
                                case 19:
                                    konutisyeri.picture20 = imgpath;
                                    break;
                                case 20:
                                    konutisyeri.picture21 = imgpath;
                                    break;
                                case 21:
                                    konutisyeri.picture22 = imgpath;
                                    break;
                                case 22:
                                    konutisyeri.picture23 = imgpath;
                                    break;
                                case 23:
                                    konutisyeri.picture24 = imgpath;
                                    break;
                                case 24:
                                    konutisyeri.picture25 = imgpath;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else if (Request.Files.Count > 25 && Path.GetFileName(Request.Files[0].FileName) != "") { return Json(new { status = "error", message = "25'den fazla resim giremezsiniz!" }); }
                    if (konutisyeri.ID == 0)
                    {
                        konutisyeri.UserID = Int16.Parse(Session["userID"].ToString());
                        konutisyeri.Isyeri_Konut = "Konut";
                        konutisyeri.Tarih = DateTime.Now;
                        konutisyeri.Kira_Satilik = "Kiralik";
                        db.KONUT_ISYERI.Add(konutisyeri);

                        db.SaveChanges();

                        return Json(new { success = true, message = "Başarıyla Kaydedildi" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        konutisyeri.UserID = Int16.Parse(Session["userID"].ToString());
                        db.Entry(konutisyeri).State = EntityState.Modified;
                        db.SaveChanges();
                        return Json(new { success = true, message = "Başarıyla Güncellendi" }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    throw;
                }
            }
        }



        public ActionResult getIsyeriSatilik()
        {
            using (Entities db = new Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<KONUT_ISYERI> KonutList = db.KONUT_ISYERI.Where(s => s.Isyeri_Konut == "Isyeri" && s.Kira_Satilik == "Satilik" && s.DevrenSatilik == false && s.DevrenKiralik == false)
                    .ToList<KONUT_ISYERI>();


                return Json(new
                {
                    data = KonutList.Select((x, idx) => new { x, idx })
                    .GroupBy(x => x.idx / 4)
                    .Select(g => g.Select(a => a.x))
                }
                , JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AddOrEditIsyeriSatilik(int id = 0)
        {
            setViewBags();
            if (id == 0)
            {
                return View(new KONUT_ISYERI());
            }
            else
            {
                using (Entities db = new Entities())
                {
                    return View(db.KONUT_ISYERI.Where(x => x.ID == id).FirstOrDefault<KONUT_ISYERI>());
                }
            }
        }
        [HttpPost]
        public ActionResult AddOrEditIsyeriSatilik(KONUT_ISYERI konutisyeri)
        {
            using (Entities db = new Entities())
            {
                try
                {
                    Debug.WriteLine(Request.Files.Count.ToString());
                    if (Request.Files.Count < 25 && Path.GetFileName(Request.Files[0].FileName) != "")
                    {
                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            string imgname = Path.GetFileName(Request.Files[i].FileName);
                            string ext = Path.GetExtension(imgname);
                            if (ext == ".jpg" || ext == ".JPG" || ext == ".jpeg")
                            {
                                //success
                            }
                            else { return Json(new { status = "error", message = "Lütfen jpg veya png uzantılı dosyalar yükleyin!" }); }
                        }

                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            string imgname = Path.GetFileName(Request.Files[i].FileName);
                            string ext = Path.GetExtension(imgname);

                            string imgpath = Path.Combine(Server.MapPath("~/EmlakImages"), "Kisyeri_" + (db.KONUT_ISYERI.Max(item => item.ID) + 1).ToString() + "_" + (i + 1).ToString() + ext);
                            Request.Files[i].SaveAs(imgpath);
                            switch (i)
                            {
                                case 0:
                                    konutisyeri.picture1 = imgpath;
                                    break;
                                case 1:
                                    konutisyeri.picture2 = imgpath;
                                    break;
                                case 2:
                                    konutisyeri.picture3 = imgpath;
                                    break;
                                case 3:
                                    konutisyeri.picture4 = imgpath;
                                    break;
                                case 4:
                                    konutisyeri.picture5 = imgpath;
                                    break;
                                case 5:
                                    konutisyeri.picture6 = imgpath;
                                    break;
                                case 6:
                                    konutisyeri.picture7 = imgpath;
                                    break;
                                case 7:
                                    konutisyeri.picture8 = imgpath;
                                    break;
                                case 9:
                                    konutisyeri.picture10 = imgpath;
                                    break;
                                case 10:
                                    konutisyeri.picture11 = imgpath;
                                    break;
                                case 11:
                                    konutisyeri.picture12 = imgpath;
                                    break;
                                case 12:
                                    konutisyeri.picture13 = imgpath;
                                    break;
                                case 13:
                                    konutisyeri.picture14 = imgpath;
                                    break;
                                case 14:
                                    konutisyeri.picture15 = imgpath;
                                    break;
                                case 15:
                                    konutisyeri.picture16 = imgpath;
                                    break;
                                case 16:
                                    konutisyeri.picture17 = imgpath;
                                    break;
                                case 17:
                                    konutisyeri.picture18 = imgpath;
                                    break;
                                case 18:
                                    konutisyeri.picture19 = imgpath;
                                    break;
                                case 19:
                                    konutisyeri.picture20 = imgpath;
                                    break;
                                case 20:
                                    konutisyeri.picture21 = imgpath;
                                    break;
                                case 21:
                                    konutisyeri.picture22 = imgpath;
                                    break;
                                case 22:
                                    konutisyeri.picture23 = imgpath;
                                    break;
                                case 23:
                                    konutisyeri.picture24 = imgpath;
                                    break;
                                case 24:
                                    konutisyeri.picture25 = imgpath;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else if (Request.Files.Count > 25 && Path.GetFileName(Request.Files[0].FileName) != "") { return Json(new { status = "error", message = "25'den fazla resim giremezsiniz!" }); }
                    if (konutisyeri.ID == 0)
                    {
                        konutisyeri.UserID = Int16.Parse(Session["userID"].ToString());
                        konutisyeri.Isyeri_Konut = "Isyeri";
                        konutisyeri.Tarih = DateTime.Now;
                        konutisyeri.Kira_Satilik = "Satilik";
                        konutisyeri.DevrenKiralik = false;
                        konutisyeri.DevrenSatilik = false;
                        db.KONUT_ISYERI.Add(konutisyeri);

                        db.SaveChanges();

                        return Json(new { success = true, message = "Başarıyla Kaydedildi" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        konutisyeri.UserID = Int16.Parse(Session["userID"].ToString());
                        db.Entry(konutisyeri).State = EntityState.Modified;
                        db.SaveChanges();
                        return Json(new { success = true, message = "Başarıyla Güncellendi" }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    throw;
                }
            }
        }




        public ActionResult getIsyeriKiralik()
        {
            using (Entities db = new Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<KONUT_ISYERI> KonutList = db.KONUT_ISYERI.Where(s => s.Isyeri_Konut == "Isyeri" && s.Kira_Satilik == "Kiralik" && s.DevrenSatilik==false && s.DevrenKiralik==false)
                    .ToList<KONUT_ISYERI>();


                return Json(new
                {
                    data = KonutList.Select((x, idx) => new { x, idx })
                    .GroupBy(x => x.idx / 4)
                    .Select(g => g.Select(a => a.x))
                }
                , JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AddOrEditIsyeriKiralik(int id = 0)
        {
            setViewBags();
            if (id == 0)
            {
                return View(new KONUT_ISYERI());
            }
            else
            {
                using (Entities db = new Entities())
                {
                    return View(db.KONUT_ISYERI.Where(x => x.ID == id).FirstOrDefault<KONUT_ISYERI>());
                }
            }
        }
        [HttpPost]
        public ActionResult AddOrEditIsyeriKiralik(KONUT_ISYERI konutisyeri)
        {
            using (Entities db = new Entities())
            {
                try
                {
                    Debug.WriteLine(Request.Files.Count.ToString());
                    if (Request.Files.Count < 25 && Path.GetFileName(Request.Files[0].FileName) != "")
                    {
                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            string imgname = Path.GetFileName(Request.Files[i].FileName);
                            string ext = Path.GetExtension(imgname);
                            if (ext == ".jpg" || ext == ".JPG" || ext == ".jpeg")
                            {
                                //success
                            }
                            else { return Json(new { status = "error", message = "Lütfen jpg veya png uzantılı dosyalar yükleyin!" }); }
                        }

                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            string imgname = Path.GetFileName(Request.Files[i].FileName);
                            string ext = Path.GetExtension(imgname);

                            string imgpath = Path.Combine(Server.MapPath("~/EmlakImages"), "Kisyeri_" + (db.KONUT_ISYERI.Max(item => item.ID) + 1).ToString() + "_" + (i + 1).ToString() + ext);
                            Request.Files[i].SaveAs(imgpath);
                            switch (i)
                            {
                                case 0:
                                    konutisyeri.picture1 = imgpath;
                                    break;
                                case 1:
                                    konutisyeri.picture2 = imgpath;
                                    break;
                                case 2:
                                    konutisyeri.picture3 = imgpath;
                                    break;
                                case 3:
                                    konutisyeri.picture4 = imgpath;
                                    break;
                                case 4:
                                    konutisyeri.picture5 = imgpath;
                                    break;
                                case 5:
                                    konutisyeri.picture6 = imgpath;
                                    break;
                                case 6:
                                    konutisyeri.picture7 = imgpath;
                                    break;
                                case 7:
                                    konutisyeri.picture8 = imgpath;
                                    break;
                                case 9:
                                    konutisyeri.picture10 = imgpath;
                                    break;
                                case 10:
                                    konutisyeri.picture11 = imgpath;
                                    break;
                                case 11:
                                    konutisyeri.picture12 = imgpath;
                                    break;
                                case 12:
                                    konutisyeri.picture13 = imgpath;
                                    break;
                                case 13:
                                    konutisyeri.picture14 = imgpath;
                                    break;
                                case 14:
                                    konutisyeri.picture15 = imgpath;
                                    break;
                                case 15:
                                    konutisyeri.picture16 = imgpath;
                                    break;
                                case 16:
                                    konutisyeri.picture17 = imgpath;
                                    break;
                                case 17:
                                    konutisyeri.picture18 = imgpath;
                                    break;
                                case 18:
                                    konutisyeri.picture19 = imgpath;
                                    break;
                                case 19:
                                    konutisyeri.picture20 = imgpath;
                                    break;
                                case 20:
                                    konutisyeri.picture21 = imgpath;
                                    break;
                                case 21:
                                    konutisyeri.picture22 = imgpath;
                                    break;
                                case 22:
                                    konutisyeri.picture23 = imgpath;
                                    break;
                                case 23:
                                    konutisyeri.picture24 = imgpath;
                                    break;
                                case 24:
                                    konutisyeri.picture25 = imgpath;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else if (Request.Files.Count > 25 && Path.GetFileName(Request.Files[0].FileName) != "") { return Json(new { status = "error", message = "25'den fazla resim giremezsiniz!" }); }
                    if (konutisyeri.ID == 0)
                    {
                        konutisyeri.UserID = Int16.Parse(Session["userID"].ToString());
                        konutisyeri.Isyeri_Konut = "Isyeri";
                        konutisyeri.Tarih = DateTime.Now;
                        konutisyeri.Kira_Satilik = "Kiralik";
                        konutisyeri.DevrenKiralik = false;
                        konutisyeri.DevrenSatilik = false;
                        db.KONUT_ISYERI.Add(konutisyeri);

                        db.SaveChanges();

                        return Json(new { success = true, message = "Başarıyla Kaydedildi" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        konutisyeri.UserID = Int16.Parse(Session["userID"].ToString());
                        db.Entry(konutisyeri).State = EntityState.Modified;
                        db.SaveChanges();
                        return Json(new { success = true, message = "Başarıyla Güncellendi" }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    throw;
                }
            }
        }




        public ActionResult getIsyeriDevrenSatilik()
        {
            using (Entities db = new Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<KONUT_ISYERI> KonutList = db.KONUT_ISYERI.Where(s => s.Isyeri_Konut == "Isyeri" && s.Kira_Satilik == "Satilik" && s.DevrenSatilik == true && s.DevrenKiralik == false)
                    .ToList<KONUT_ISYERI>();


                return Json(new
                {
                    data = KonutList.Select((x, idx) => new { x, idx })
                    .GroupBy(x => x.idx / 4)
                    .Select(g => g.Select(a => a.x))
                }
                , JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AddOrEditIsyeriDevrenSatilik(int id = 0)
        {
            setViewBags();
            if (id == 0)
            {
                return View(new KONUT_ISYERI());
            }
            else
            {
                using (Entities db = new Entities())
                {
                    return View(db.KONUT_ISYERI.Where(x => x.ID == id).FirstOrDefault<KONUT_ISYERI>());
                }
            }
        }
        [HttpPost]
        public ActionResult AddOrEditIsyeriDevrenSatilik(KONUT_ISYERI konutisyeri)
        {
            using (Entities db = new Entities())
            {
                try
                {
                    Debug.WriteLine(Request.Files.Count.ToString());
                    if (Request.Files.Count < 25 && Path.GetFileName(Request.Files[0].FileName) != "")
                    {
                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            string imgname = Path.GetFileName(Request.Files[i].FileName);
                            string ext = Path.GetExtension(imgname);
                            if (ext == ".jpg" || ext == ".JPG" || ext == ".jpeg")
                            {
                                //success
                            }
                            else { return Json(new { status = "error", message = "Lütfen jpg veya png uzantılı dosyalar yükleyin!" }); }
                        }

                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            string imgname = Path.GetFileName(Request.Files[i].FileName);
                            string ext = Path.GetExtension(imgname);

                            string imgpath = Path.Combine(Server.MapPath("~/EmlakImages"), "Kisyeri_" + (db.KONUT_ISYERI.Max(item => item.ID) + 1).ToString() + "_" + (i + 1).ToString() + ext);
                            Request.Files[i].SaveAs(imgpath);
                            switch (i)
                            {
                                case 0:
                                    konutisyeri.picture1 = imgpath;
                                    break;
                                case 1:
                                    konutisyeri.picture2 = imgpath;
                                    break;
                                case 2:
                                    konutisyeri.picture3 = imgpath;
                                    break;
                                case 3:
                                    konutisyeri.picture4 = imgpath;
                                    break;
                                case 4:
                                    konutisyeri.picture5 = imgpath;
                                    break;
                                case 5:
                                    konutisyeri.picture6 = imgpath;
                                    break;
                                case 6:
                                    konutisyeri.picture7 = imgpath;
                                    break;
                                case 7:
                                    konutisyeri.picture8 = imgpath;
                                    break;
                                case 9:
                                    konutisyeri.picture10 = imgpath;
                                    break;
                                case 10:
                                    konutisyeri.picture11 = imgpath;
                                    break;
                                case 11:
                                    konutisyeri.picture12 = imgpath;
                                    break;
                                case 12:
                                    konutisyeri.picture13 = imgpath;
                                    break;
                                case 13:
                                    konutisyeri.picture14 = imgpath;
                                    break;
                                case 14:
                                    konutisyeri.picture15 = imgpath;
                                    break;
                                case 15:
                                    konutisyeri.picture16 = imgpath;
                                    break;
                                case 16:
                                    konutisyeri.picture17 = imgpath;
                                    break;
                                case 17:
                                    konutisyeri.picture18 = imgpath;
                                    break;
                                case 18:
                                    konutisyeri.picture19 = imgpath;
                                    break;
                                case 19:
                                    konutisyeri.picture20 = imgpath;
                                    break;
                                case 20:
                                    konutisyeri.picture21 = imgpath;
                                    break;
                                case 21:
                                    konutisyeri.picture22 = imgpath;
                                    break;
                                case 22:
                                    konutisyeri.picture23 = imgpath;
                                    break;
                                case 23:
                                    konutisyeri.picture24 = imgpath;
                                    break;
                                case 24:
                                    konutisyeri.picture25 = imgpath;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else if (Request.Files.Count > 25 && Path.GetFileName(Request.Files[0].FileName) != "") { return Json(new { status = "error", message = "25'den fazla resim giremezsiniz!" }); }
                    if (konutisyeri.ID == 0)
                    {
                        konutisyeri.UserID = Int16.Parse(Session["userID"].ToString());
                        konutisyeri.Isyeri_Konut = "Isyeri";
                        konutisyeri.Tarih = DateTime.Now;
                        konutisyeri.Kira_Satilik = "Satilik";
                        konutisyeri.DevrenKiralik = false;
                        konutisyeri.DevrenSatilik = true;
                        db.KONUT_ISYERI.Add(konutisyeri);

                        db.SaveChanges();

                        return Json(new { success = true, message = "Başarıyla Kaydedildi" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        konutisyeri.UserID = Int16.Parse(Session["userID"].ToString());
                        db.Entry(konutisyeri).State = EntityState.Modified;
                        db.SaveChanges();
                        return Json(new { success = true, message = "Başarıyla Güncellendi" }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    throw;
                }
            }
        }



        public ActionResult getIsyeriDevrenKiralik()
        {
            using (Entities db = new Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<KONUT_ISYERI> KonutList = db.KONUT_ISYERI.Where(s => s.Isyeri_Konut == "Isyeri" && s.Kira_Satilik == "Kiralik" && s.DevrenSatilik == false && s.DevrenKiralik == true)
                    .ToList<KONUT_ISYERI>();


                return Json(new
                {
                    data = KonutList.Select((x, idx) => new { x, idx })
                    .GroupBy(x => x.idx / 4)
                    .Select(g => g.Select(a => a.x))
                }
                , JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AddOrEditIsyeriDevrenKiralik(int id = 0)
        {
            setViewBags();
            if (id == 0)
            {
                return View(new KONUT_ISYERI());
            }
            else
            {
                using (Entities db = new Entities())
                {
                    return View(db.KONUT_ISYERI.Where(x => x.ID == id).FirstOrDefault<KONUT_ISYERI>());
                }
            }
        }
        [HttpPost]
        public ActionResult AddOrEditIsyeriDevrenKiralik(KONUT_ISYERI konutisyeri)
        {
            using (Entities db = new Entities())
            {
                try
                {
                    Debug.WriteLine(Request.Files.Count.ToString());
                    if (Request.Files.Count < 25 && Path.GetFileName(Request.Files[0].FileName) != "")
                    {
                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            string imgname = Path.GetFileName(Request.Files[i].FileName);
                            string ext = Path.GetExtension(imgname);
                            if (ext == ".jpg" || ext == ".JPG" || ext == ".jpeg")
                            {
                                //success
                            }
                            else { return Json(new { status = "error", message = "Lütfen jpg veya png uzantılı dosyalar yükleyin!" }); }
                        }

                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            string imgname = Path.GetFileName(Request.Files[i].FileName);
                            string ext = Path.GetExtension(imgname);

                            string imgpath = Path.Combine(Server.MapPath("~/EmlakImages"), "Kisyeri_" + (db.KONUT_ISYERI.Max(item => item.ID) + 1).ToString() + "_" + (i + 1).ToString() + ext);
                            Request.Files[i].SaveAs(imgpath);
                            switch (i)
                            {
                                case 0:
                                    konutisyeri.picture1 = imgpath;
                                    break;
                                case 1:
                                    konutisyeri.picture2 = imgpath;
                                    break;
                                case 2:
                                    konutisyeri.picture3 = imgpath;
                                    break;
                                case 3:
                                    konutisyeri.picture4 = imgpath;
                                    break;
                                case 4:
                                    konutisyeri.picture5 = imgpath;
                                    break;
                                case 5:
                                    konutisyeri.picture6 = imgpath;
                                    break;
                                case 6:
                                    konutisyeri.picture7 = imgpath;
                                    break;
                                case 7:
                                    konutisyeri.picture8 = imgpath;
                                    break;
                                case 9:
                                    konutisyeri.picture10 = imgpath;
                                    break;
                                case 10:
                                    konutisyeri.picture11 = imgpath;
                                    break;
                                case 11:
                                    konutisyeri.picture12 = imgpath;
                                    break;
                                case 12:
                                    konutisyeri.picture13 = imgpath;
                                    break;
                                case 13:
                                    konutisyeri.picture14 = imgpath;
                                    break;
                                case 14:
                                    konutisyeri.picture15 = imgpath;
                                    break;
                                case 15:
                                    konutisyeri.picture16 = imgpath;
                                    break;
                                case 16:
                                    konutisyeri.picture17 = imgpath;
                                    break;
                                case 17:
                                    konutisyeri.picture18 = imgpath;
                                    break;
                                case 18:
                                    konutisyeri.picture19 = imgpath;
                                    break;
                                case 19:
                                    konutisyeri.picture20 = imgpath;
                                    break;
                                case 20:
                                    konutisyeri.picture21 = imgpath;
                                    break;
                                case 21:
                                    konutisyeri.picture22 = imgpath;
                                    break;
                                case 22:
                                    konutisyeri.picture23 = imgpath;
                                    break;
                                case 23:
                                    konutisyeri.picture24 = imgpath;
                                    break;
                                case 24:
                                    konutisyeri.picture25 = imgpath;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else if (Request.Files.Count > 25 && Path.GetFileName(Request.Files[0].FileName) != "") { return Json(new { status = "error", message = "25'den fazla resim giremezsiniz!" }); }
                    if (konutisyeri.ID == 0)
                    {
                        konutisyeri.UserID = Int16.Parse(Session["userID"].ToString());
                        konutisyeri.Isyeri_Konut = "Isyeri";
                        konutisyeri.Tarih = DateTime.Now;
                        konutisyeri.Kira_Satilik = "Kiralik";
                        konutisyeri.DevrenKiralik = true;
                        konutisyeri.DevrenSatilik = false;
                        db.KONUT_ISYERI.Add(konutisyeri);

                        db.SaveChanges();

                        return Json(new { success = true, message = "Başarıyla Kaydedildi" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        konutisyeri.UserID = Int16.Parse(Session["userID"].ToString());
                        db.Entry(konutisyeri).State = EntityState.Modified;
                        db.SaveChanges();
                        return Json(new { success = true, message = "Başarıyla Güncellendi" }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    throw;
                }
            }
        }
    }
}