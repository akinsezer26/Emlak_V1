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
                List<SelectListItem> IsyeriTuru = (from i in db.ISYERITURU.ToList()
                                                   select new SelectListItem
                                                   {
                                                       Text = i.ISYERI_TURU,
                                                       Value = i.ISYERI_TURU_ID.ToString()
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
                List<SelectListItem> ImarDurumu = (from i in db.IMAR_DURUMU.ToList()
                                                   select new SelectListItem
                                                   {
                                                       Text = i.IMAR_DURUMU1,
                                                       Value = i.IMAR_DURUMU_ID.ToString()
                                                   }).ToList();
                List<SelectListItem> Kaks = (from i in db.KAKS.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KAKS1,
                                                 Value = i.KAKS_ID.ToString()
                                             }).ToList();
                List<SelectListItem> Gabari = (from i in db.GABARI.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = i.GABARI1,
                                                   Value = i.GABARI_ID.ToString()
                                               }).ToList();
                List<SelectListItem> TapuDurumu = (from i in db.TAPU_DURUMU.ToList()
                                                   select new SelectListItem
                                                   {
                                                       Text = i.TAPU_DURUMU1,
                                                       Value = i.TAPU_DURUMU_ID.ToString()
                                                   }).ToList();
                ViewBag.konutTipi = konutTipi;
                ViewBag.OdaSayisi = OdaSayisi;
                ViewBag.IsitmaTipi = IsitmaTipi;
                ViewBag.BinaYasi = BinaYasi;
                ViewBag.IsyeriTuru = IsyeriTuru;
                ViewBag.BulunduguKat = BulunduguKat;
                ViewBag.KatSayisi = KatSayisi;
                ViewBag.BanyoSayisi = BanyoSayisi;
                ViewBag.KullanimDurumu = KullanimDurumu;
                ViewBag.ImarDurumu = ImarDurumu;
                ViewBag.Kaks = Kaks;
                ViewBag.Gabari = Gabari;
                ViewBag.TapuDurumu = TapuDurumu;
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
                    if (db.AJANDA.Count() != 0)
                    {
                        ajanda.AjandaID = db.AJANDA.Max(item => item.AjandaID) + 1;
                    }
                    else
                    {
                        ajanda.AjandaID = 1;
                    }
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
                    if (db.ALACAKLARIM.Count() != 0)
                    {
                        alacaklarim.AlacakID = db.ALACAKLARIM.Max(item => item.AlacakID) + 1;
                    }
                    else
                    {
                        alacaklarim.AlacakID = 1;
                    }

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
                    if (db.BORCLARIM.Count() != 0)
                    {
                        borclarim.BorcID = db.BORCLARIM.Max(item => item.BorcID) + 1;
                    }
                    else
                    {
                        borclarim.BorcID = 1;
                    }

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
                    if (db.EmlakArayanlar.Count() != 0)
                    {
                        arayanlar.ArayanlarID = db.EmlakArayanlar.Max(item => item.ArayanlarID) + 1;
                    }
                    else
                    {
                        arayanlar.ArayanlarID = 1;
                    }

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

        public ActionResult getKiraTakip()
        {
            using (Entities db = new Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<KiraTakip> kiraList = db.KiraTakip.ToList<KiraTakip>();
                return Json(new { data = kiraList }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult getSozlesmeKontrat()
        {
            using (Entities db = new Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<MAKBUZ_SOZLESME> makbuzSozlesmeList = db.MAKBUZ_SOZLESME.ToList<MAKBUZ_SOZLESME>();
                return Json(new { data = makbuzSozlesmeList }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult getKontratTakip()
        {
            using (Entities db = new Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<KontratTakip> kontratList = db.KontratTakip.ToList<KontratTakip>();
                return Json(new { data = kontratList }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult getBorcluTakip()
        {
            using (Entities db = new Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<BorcluTakip> borcluList = db.BorcluTakip.ToList<BorcluTakip>();
                return Json(new { data = borcluList }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult getAdminAta()
        {
            using (Entities db = new Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<USERS> user = db.USERS.Where(x => x.UserType == 2).ToList<USERS>();
                return Json(new { data = user }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AddOrEditAdminAta(int id = 0)
        {
            setViewBags();
            if (id == 0)
            {
                return View(new USERS());
            }
            else
            {
                using (Entities db = new Entities())
                {
                    return View(db.USERS.Where(x => x.UserID == id).FirstOrDefault<USERS>());
                }
            }
        }
        [HttpPost]
        public ActionResult AddOrEditAdminAta(USERS usrs)
        {
            using (Entities db = new Entities())
            {
                if (usrs.UserID == 0)
                {
                    if (db.USERS.Count() != 0)
                    {
                        usrs.UserID = db.USERS.Max(item => item.UserID) + 1;
                    }
                    else
                    {
                        usrs.UserID = 1;
                    }

                    usrs.UserType = 2;
                    usrs.Unvan = "Şirket Yetkilisi";
                    db.USERS.Add(usrs);

                    db.SaveChanges();

                    return Json(new { success = true, message = "Başarıyla Kaydedildi" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    usrs.UserType = 2;
                    usrs.Unvan = "Şirket Yetkilisi";
                    db.Entry(usrs).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Başarıyla Güncellendi" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult getKullaniciAta()
        {
            using (Entities db = new Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<USERS> user = db.USERS.Where(x => x.UserType == 3).ToList<USERS>();
                return Json(new { data = user }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AddOrEditKullaniciAta(int id = 0)
        {
            setViewBags();
            if (id == 0)
            {
                return View(new USERS());
            }
            else
            {
                using (Entities db = new Entities())
                {
                    return View(db.USERS.Where(x => x.UserID == id).FirstOrDefault<USERS>());
                }
            }
        }
        [HttpPost]
        public ActionResult AddOrEditKullaniciAta(USERS usrs)
        {
            using (Entities db = new Entities())
            {
                if (usrs.UserID == 0)
                {
                    if (db.USERS.Count() != 0)
                    {
                        usrs.UserID = db.USERS.Max(item => item.UserID) + 1;
                    }
                    else
                    {
                        usrs.UserID = 1;
                    }

                    usrs.UserType = 3;
                    usrs.Unvan = "Gayrimenkul Danışmanı";
                    db.USERS.Add(usrs);

                    db.SaveChanges();

                    return Json(new { success = true, message = "Başarıyla Kaydedildi" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    usrs.UserType = 3;
                    usrs.Unvan = "Gayrimenkul Danışmanı";
                    db.Entry(usrs).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Başarıyla Güncellendi" }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult adminBlokla(int id) {
            using (Entities db = new Entities())
            {
                USERS usr = db.USERS.Where(x => x.UserID == id).FirstOrDefault<USERS>();
                usr.isBlocked = true;
                db.Entry(usr).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, message = "Bloklandı!" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult adminBlokKaldir(int id) {
            using (Entities db = new Entities())
            {
                USERS usr = db.USERS.Where(x => x.UserID == id).FirstOrDefault<USERS>();
                usr.isBlocked = false;
                db.Entry(usr).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, message = "Blok Kaldırıldı!" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AddOrEditSozlesmeKontrat(int id = 0)
        {
            setViewBags();
            if (id == 0)
            {
                return View(new MAKBUZ_SOZLESME());
            }
            else
            {
                using (Entities db = new Entities())
                {
                    return View(db.MAKBUZ_SOZLESME.Where(x => x.MakbuzSozlesmeID == id).FirstOrDefault<MAKBUZ_SOZLESME>());
                }
            }
        }
        [HttpPost]
        public ActionResult AddOrEditSozlesmeKontrat(MAKBUZ_SOZLESME makbuz)
        {
            using (Entities db = new Entities())
            {
                if (makbuz.MakbuzSozlesmeID == 0)
                {
                    if (db.MAKBUZ_SOZLESME.Count() != 0)
                    {
                        makbuz.MakbuzSozlesmeID = db.MAKBUZ_SOZLESME.Max(item => item.MakbuzSozlesmeID) + 1;
                    }
                    else
                    {
                        makbuz.MakbuzSozlesmeID = 1;
                    }

                    makbuz.userID = Int16.Parse(Session["userID"].ToString());
                    db.MAKBUZ_SOZLESME.Add(makbuz);

                    db.SaveChanges();

                    return Json(new { success = true, message = "Başarıyla Kaydedildi" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    makbuz.userID = Int16.Parse(Session["userID"].ToString());
                    db.Entry(makbuz).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Başarıyla Güncellendi" }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult DeleteSozlesmeKontrat(int id)
        {
            using (Entities db = new Entities())
            {
                MAKBUZ_SOZLESME makbuz = db.MAKBUZ_SOZLESME.Where(x => x.MakbuzSozlesmeID == id).FirstOrDefault<MAKBUZ_SOZLESME>();

                //might edit here
                KONUT_ISYERI konut = db.KONUT_ISYERI.Where(x => x.ID == makbuz.KonutID).FirstOrDefault<KONUT_ISYERI>();
                konut.KiraTakip = false;
                db.Entry(konut).State = EntityState.Modified;
                //

                db.MAKBUZ_SOZLESME.Remove(makbuz);
                db.SaveChanges();
                return Json(new { success = true, message = "Başarıyla Silindi" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult EditKullaniciBilgileri(int id = 0)
        {
            setViewBags();
            if (id == 0)
            {
                return View(new USERS());
            }
            else
            {
                using (Entities db = new Entities())
                {
                    return View(db.USERS.Where(x => x.UserID == id).FirstOrDefault<USERS>());
                }
            }
        }
        [HttpPost]
        public ActionResult EditKullaniciBilgileri(USERS kullanici)
        {
                using (Entities db = new Entities())
                {
                    string imgname = Path.GetFileName(Request.Files[0].FileName);
                    string ext = Path.GetExtension(imgname);
                    string imgpath = Path.Combine(Server.MapPath("~/Image"), "User_" + kullanici.UserID.ToString() + ext);
                    Request.Files[0].SaveAs(imgpath);
                    kullanici.UserID = Int16.Parse(Session["userID"].ToString());
                    kullanici.UserType = int.Parse(Session["UserTip"].ToString());
                    kullanici.Unvan = Session["Unvan"].ToString();
                    kullanici.ppicture = imgpath;
                    Session["Userpp"] = imgpath;
                    db.Entry(kullanici).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Başarıyla Güncellendi" }, JsonRequestBehavior.AllowGet);
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
                if (konutisyeri.ID == 0)
                {
                    if (Request.Files.Count < 35 && Path.GetFileName(Request.Files[0].FileName)!="")
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


                        long tKIsyeri = 0, tArsaTarla = 0, tBina = 0;
                        if (db.KONUT_ISYERI.Count() != 0) { tKIsyeri = db.KONUT_ISYERI.Max(item => item.ID); }
                        if (db.ARSA_TARLA.Count() != 0) { tArsaTarla = db.ARSA_TARLA.Max(item => item.ID); }
                        if (db.BINA.Count() != 0) { tBina = db.BINA.Max(item => item.ID); }
                        if (tKIsyeri > tArsaTarla && tKIsyeri > tBina) { konutisyeri.ID = tKIsyeri + 1; }
                        else if (tArsaTarla > tKIsyeri && tArsaTarla > tBina) { konutisyeri.ID = tArsaTarla + 1; }
                        else if (tBina > tKIsyeri && tBina > tArsaTarla) { konutisyeri.ID = tBina + 1; }
                        else { konutisyeri.ID = 1; }


                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            string imgname = Path.GetFileName(Request.Files[i].FileName);
                            string ext = Path.GetExtension(imgname);

                            string imgpath = Path.Combine(Server.MapPath("~/EmlakImages"), "Kisyeri_" + konutisyeri.ID.ToString() + "_" + (i + 1).ToString() + ext);
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
                                case 8:
                                    konutisyeri.picture9 = imgpath;
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
                                case 25:
                                    konutisyeri.picture26 = imgpath;
                                    break;
                                case 26:
                                    konutisyeri.picture27 = imgpath;
                                    break;
                                case 27:
                                    konutisyeri.picture28 = imgpath;
                                    break;
                                case 28:
                                    konutisyeri.picture29 = imgpath;
                                    break;
                                case 29:
                                    konutisyeri.picture30 = imgpath;
                                    break;
                                case 30:
                                    konutisyeri.picture31 = imgpath;
                                    break;
                                case 31:
                                    konutisyeri.picture32 = imgpath;
                                    break;
                                case 32:
                                    konutisyeri.picture33 = imgpath;
                                    break;
                                case 33:
                                    konutisyeri.picture34 = imgpath;
                                    break;
                                case 34:
                                    konutisyeri.picture35 = imgpath;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else if(Request.Files.Count>35 && Path.GetFileName(Request.Files[0].FileName) != "") {return Json(new { status = "error", message = "25'den fazla resim giremezsiniz!" }); }

                    if (Path.GetFileName(Request.Files[0].FileName) == "")
                    {
                        long tKIsyeri = 0, tArsaTarla = 0, tBina = 0;
                        if (db.KONUT_ISYERI.Count() != 0) { tKIsyeri = db.KONUT_ISYERI.Max(item => item.ID); }
                        if (db.ARSA_TARLA.Count() != 0) { tArsaTarla = db.ARSA_TARLA.Max(item => item.ID); }
                        if (db.BINA.Count() != 0) { tBina = db.BINA.Max(item => item.ID); }
                        if (tKIsyeri > tArsaTarla && tKIsyeri > tBina) { konutisyeri.ID = tKIsyeri + 1; }
                        else if (tArsaTarla > tKIsyeri && tArsaTarla > tBina) { konutisyeri.ID = tArsaTarla + 1; }
                        else if (tBina > tKIsyeri && tBina > tArsaTarla) { konutisyeri.ID = tBina + 1; }
                        else { konutisyeri.ID = 1; }
                    }
                        
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
                    konutisyeri.Isyeri_Konut = "Konut";
                    konutisyeri.Tarih = DateTime.Now;
                    konutisyeri.Kira_Satilik = "Satılık";

                    if (Request.Files.Count < 35 && Path.GetFileName(Request.Files[0].FileName) != "")
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

                            string imgpath = Path.Combine(Server.MapPath("~/EmlakImages"), "Kisyeri_" + konutisyeri.ID.ToString() + "_" + (i + 1).ToString() + ext);
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
                                case 8:
                                    konutisyeri.picture9 = imgpath;
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
                                case 25:
                                    konutisyeri.picture26 = imgpath;
                                    break;
                                case 26:
                                    konutisyeri.picture27 = imgpath;
                                    break;
                                case 27:
                                    konutisyeri.picture28 = imgpath;
                                    break;
                                case 28:
                                    konutisyeri.picture29 = imgpath;
                                    break;
                                case 29:
                                    konutisyeri.picture30 = imgpath;
                                    break;
                                case 30:
                                    konutisyeri.picture31 = imgpath;
                                    break;
                                case 31:
                                    konutisyeri.picture32 = imgpath;
                                    break;
                                case 32:
                                    konutisyeri.picture33 = imgpath;
                                    break;
                                case 33:
                                    konutisyeri.picture34 = imgpath;
                                    break;
                                case 34:
                                    konutisyeri.picture35 = imgpath;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else if (Request.Files.Count > 35 && Path.GetFileName(Request.Files[0].FileName) != "") { return Json(new { status = "error", message = "25'den fazla resim giremezsiniz!" }); }

                    //if(Request.Files.Count < 35 && Path.GetFileName(Request.Files[0].FileName) != "")
                    //{
                    //    db.Entry(model).Property(x => x.Token).IsModified = false;
                    //}
                    db.Entry(konutisyeri).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Başarıyla Güncellendi" }, JsonRequestBehavior.AllowGet);
                }
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
                if (konutisyeri.ID == 0)
                {
                    if (Request.Files.Count < 35 && Path.GetFileName(Request.Files[0].FileName) != "")
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


                        long tKIsyeri = 0, tArsaTarla = 0, tBina = 0;
                        if (db.KONUT_ISYERI.Count() != 0) { tKIsyeri = db.KONUT_ISYERI.Max(item => item.ID); }
                        if (db.ARSA_TARLA.Count() != 0) { tArsaTarla = db.ARSA_TARLA.Max(item => item.ID); }
                        if (db.BINA.Count() != 0) { tBina = db.BINA.Max(item => item.ID); }
                        if (tKIsyeri > tArsaTarla && tKIsyeri > tBina) { konutisyeri.ID = tKIsyeri + 1; }
                        else if (tArsaTarla > tKIsyeri && tArsaTarla > tBina) { konutisyeri.ID = tArsaTarla + 1; }
                        else if (tBina > tKIsyeri && tBina > tArsaTarla) { konutisyeri.ID = tBina + 1; }
                        else { konutisyeri.ID = 1; }


                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            string imgname = Path.GetFileName(Request.Files[i].FileName);
                            string ext = Path.GetExtension(imgname);

                            string imgpath = Path.Combine(Server.MapPath("~/EmlakImages"), "Kisyeri_" + konutisyeri.ID.ToString() + "_" + (i + 1).ToString() + ext);
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
                                case 8:
                                    konutisyeri.picture9 = imgpath;
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
                                case 25:
                                    konutisyeri.picture26 = imgpath;
                                    break;
                                case 26:
                                    konutisyeri.picture27 = imgpath;
                                    break;
                                case 27:
                                    konutisyeri.picture28 = imgpath;
                                    break;
                                case 28:
                                    konutisyeri.picture29 = imgpath;
                                    break;
                                case 29:
                                    konutisyeri.picture30 = imgpath;
                                    break;
                                case 30:
                                    konutisyeri.picture31 = imgpath;
                                    break;
                                case 31:
                                    konutisyeri.picture32 = imgpath;
                                    break;
                                case 32:
                                    konutisyeri.picture33 = imgpath;
                                    break;
                                case 33:
                                    konutisyeri.picture34 = imgpath;
                                    break;
                                case 34:
                                    konutisyeri.picture35 = imgpath;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else if (Request.Files.Count > 35 && Path.GetFileName(Request.Files[0].FileName) != "") { return Json(new { status = "error", message = "25'den fazla resim giremezsiniz!" }); }

                    if (Path.GetFileName(Request.Files[0].FileName) == "")
                    {
                        long tKIsyeri = 0, tArsaTarla = 0, tBina = 0;
                        if (db.KONUT_ISYERI.Count() != 0) { tKIsyeri = db.KONUT_ISYERI.Max(item => item.ID); }
                        if (db.ARSA_TARLA.Count() != 0) { tArsaTarla = db.ARSA_TARLA.Max(item => item.ID); }
                        if (db.BINA.Count() != 0) { tBina = db.BINA.Max(item => item.ID); }
                        if (tKIsyeri > tArsaTarla && tKIsyeri > tBina) { konutisyeri.ID = tKIsyeri + 1; }
                        else if (tArsaTarla > tKIsyeri && tArsaTarla > tBina) { konutisyeri.ID = tArsaTarla + 1; }
                        else if (tBina > tKIsyeri && tBina > tArsaTarla) { konutisyeri.ID = tBina + 1; }
                        else { konutisyeri.ID = 1; }
                    }

                    konutisyeri.UserID = Int16.Parse(Session["userID"].ToString());
                    konutisyeri.Isyeri_Konut = "Konut";
                    konutisyeri.Tarih = DateTime.Now;
                    konutisyeri.Kira_Satilik = "Kiralık";
                    db.KONUT_ISYERI.Add(konutisyeri);

                    db.SaveChanges();

                    return Json(new { success = true, message = "Başarıyla Kaydedildi" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    konutisyeri.UserID = Int16.Parse(Session["userID"].ToString());
                    konutisyeri.Isyeri_Konut = "Konut";
                    konutisyeri.Tarih = DateTime.Now;
                    konutisyeri.Kira_Satilik = "Kiralık";

                    if (Request.Files.Count < 35 && Path.GetFileName(Request.Files[0].FileName) != "")
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

                            string imgpath = Path.Combine(Server.MapPath("~/EmlakImages"), "Kisyeri_" + konutisyeri.ID.ToString() + "_" + (i + 1).ToString() + ext);
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
                                case 8:
                                    konutisyeri.picture9 = imgpath;
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
                                case 25:
                                    konutisyeri.picture26 = imgpath;
                                    break;
                                case 26:
                                    konutisyeri.picture27 = imgpath;
                                    break;
                                case 27:
                                    konutisyeri.picture28 = imgpath;
                                    break;
                                case 28:
                                    konutisyeri.picture29 = imgpath;
                                    break;
                                case 29:
                                    konutisyeri.picture30 = imgpath;
                                    break;
                                case 30:
                                    konutisyeri.picture31 = imgpath;
                                    break;
                                case 31:
                                    konutisyeri.picture32 = imgpath;
                                    break;
                                case 32:
                                    konutisyeri.picture33 = imgpath;
                                    break;
                                case 33:
                                    konutisyeri.picture34 = imgpath;
                                    break;
                                case 34:
                                    konutisyeri.picture35 = imgpath;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else if (Request.Files.Count > 35 && Path.GetFileName(Request.Files[0].FileName) != "") { return Json(new { status = "error", message = "25'den fazla resim giremezsiniz!" }); }

                    //if(Request.Files.Count < 35 && Path.GetFileName(Request.Files[0].FileName) != "")
                    //{
                    //    db.Entry(model).Property(x => x.Token).IsModified = false;
                    //}
                    db.Entry(konutisyeri).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Başarıyla Güncellendi" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpGet]
        public ActionResult AddOrEditKiraTakipKonut(int id = 0)
        {
            setViewBags();
            using (Entities db = new Entities())
            {
                List<MAKBUZ_SOZLESME> tmp2 = db.MAKBUZ_SOZLESME.Where(x => x.KonutID == id).ToList();
                if (tmp2.Any())
                {
                    List<KONUT_ISYERI> tmp = db.KONUT_ISYERI.Where(x => x.ID == id).ToList();
                    KONUT_ISYERI[] kiraTakip = tmp.Cast<KONUT_ISYERI>().ToArray();
                    ViewBag.DanismanAdi = Session["UserName"].ToString() + " " + Session["UserSurName"];
                    ViewBag.EvSahibiAdi = kiraTakip[0].Adi;
                    ViewBag.EvSahibiSoyAdi = kiraTakip[0].Soyadi;
                    ViewBag.EvSahibiTC = kiraTakip[0].TC;
                    ViewBag.EvSahibiIban = kiraTakip[0].Iban;
                    ViewBag.EvSahibiCep = kiraTakip[0].CepTelefonu;
                    ViewBag.EvSahibiIl = kiraTakip[0].il;
                    ViewBag.EvSahibiIlce = kiraTakip[0].ilce;
                    ViewBag.EvSahibiMahalle = kiraTakip[0].Mahalle;
                    ViewBag.EvSahibiSiteIcerisinde = kiraTakip[0].SiteIcerisinde;
                    ViewBag.EvSahibiSiteAdi = kiraTakip[0].SiteAdi;
                    ViewBag.EvSahibiCadde = kiraTakip[0].Cadde;
                    ViewBag.EvSahibiSokak = kiraTakip[0].Sokak;
                    ViewBag.EvSahibiApartmanAdi = kiraTakip[0].AptAdi;
                    ViewBag.EvSahibiApartmanNo = kiraTakip[0].AptNo;
                    ViewBag.EvSahibiEmlakTuru = kiraTakip[0].EMLAKTURU1.EMLAK_TURU;
                    ViewBag.EvSahibiAdres = kiraTakip[0].EmlakSahibiAdres;
                    ViewBag.KonutID = kiraTakip[0].ID;
                    MAKBUZ_SOZLESME[] makbuzSozlesme = tmp2.Cast<MAKBUZ_SOZLESME>().ToArray();

                    return View(makbuzSozlesme[0]);
                }
                else
                {
                    List<KONUT_ISYERI> tmp = db.KONUT_ISYERI.Where(x => x.ID == id).ToList();
                    KONUT_ISYERI[] kiraTakip = tmp.Cast<KONUT_ISYERI>().ToArray();
                    ViewBag.DanismanAdi = Session["UserName"].ToString() + " " + Session["UserSurName"];
                    ViewBag.EvSahibiAdi = kiraTakip[0].Adi;
                    ViewBag.EvSahibiSoyAdi = kiraTakip[0].Soyadi;
                    ViewBag.EvSahibiTC = kiraTakip[0].TC;
                    ViewBag.EvSahibiIban = kiraTakip[0].Iban;
                    ViewBag.EvSahibiCep = kiraTakip[0].CepTelefonu;
                    ViewBag.EvSahibiIl = kiraTakip[0].il;
                    ViewBag.EvSahibiIlce = kiraTakip[0].ilce;
                    ViewBag.EvSahibiMahalle = kiraTakip[0].Mahalle;
                    ViewBag.EvSahibiSiteIcerisinde = kiraTakip[0].SiteIcerisinde;
                    ViewBag.EvSahibiSiteAdi = kiraTakip[0].SiteAdi;
                    ViewBag.EvSahibiCadde = kiraTakip[0].Cadde;
                    ViewBag.EvSahibiSokak = kiraTakip[0].Sokak;
                    ViewBag.EvSahibiApartmanAdi = kiraTakip[0].AptAdi;
                    ViewBag.EvSahibiApartmanNo = kiraTakip[0].AptNo;
                    ViewBag.EvSahibiEmlakTuru = kiraTakip[0].EMLAKTURU1.EMLAK_TURU;
                    ViewBag.EvSahibiAdres = kiraTakip[0].EmlakSahibiAdres;
                    ViewBag.KonutID = kiraTakip[0].ID;

                    kiraTakip[0].KiraTakip = true;
                    db.Entry(kiraTakip[0]).State = EntityState.Modified;
                    db.SaveChanges();

                    return View(new MAKBUZ_SOZLESME());
                }
            }
        }
        [HttpPost]
        public ActionResult AddOrEditKiraTakipKonut(MAKBUZ_SOZLESME sozlesme)
        {
            using (Entities db = new Entities())
            {
                if (sozlesme.MakbuzSozlesmeID == 0)
                {
                    if (db.MAKBUZ_SOZLESME.Count() != 0)
                    {
                        sozlesme.MakbuzSozlesmeID = db.MAKBUZ_SOZLESME.Max(item => item.MakbuzSozlesmeID) + 1;
                    }
                    else
                    {
                        sozlesme.MakbuzSozlesmeID = 1;
                    }
                    sozlesme.userID = Int16.Parse(Session["userID"].ToString());

                    db.MAKBUZ_SOZLESME.Add(sozlesme);

                    db.SaveChanges();

                    return Json(new { success = true, message = "Başarıyla Kaydedildi" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    sozlesme.userID = Int16.Parse(Session["userID"].ToString());
                    db.Entry(sozlesme).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Başarıyla Güncellendi" }, JsonRequestBehavior.AllowGet);
                }
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
                if (konutisyeri.ID == 0)
                {
                    if (Request.Files.Count < 35 && Path.GetFileName(Request.Files[0].FileName) != "")
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

                        long tKIsyeri = 0, tArsaTarla = 0, tBina = 0;
                        if (db.KONUT_ISYERI.Count() != 0) { tKIsyeri = db.KONUT_ISYERI.Max(item => item.ID); }
                        if (db.ARSA_TARLA.Count() != 0) { tArsaTarla = db.ARSA_TARLA.Max(item => item.ID); }
                        if (db.BINA.Count() != 0) { tBina = db.BINA.Max(item => item.ID); }
                        if (tKIsyeri > tArsaTarla && tKIsyeri > tBina) { konutisyeri.ID = tKIsyeri + 1; }
                        else if (tArsaTarla > tKIsyeri && tArsaTarla > tBina) { konutisyeri.ID = tArsaTarla + 1; }
                        else if (tBina > tKIsyeri && tBina > tArsaTarla) { konutisyeri.ID = tBina + 1; }
                        else { konutisyeri.ID = 1; }

                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            string imgname = Path.GetFileName(Request.Files[i].FileName);
                            string ext = Path.GetExtension(imgname);

                            string imgpath = Path.Combine(Server.MapPath("~/EmlakImages"), "Kisyeri_" + konutisyeri.ID.ToString() + "_" + (i + 1).ToString() + ext);
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
                                case 8:
                                    konutisyeri.picture9 = imgpath;
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
                                case 25:
                                    konutisyeri.picture26 = imgpath;
                                    break;
                                case 26:
                                    konutisyeri.picture27 = imgpath;
                                    break;
                                case 27:
                                    konutisyeri.picture28 = imgpath;
                                    break;
                                case 28:
                                    konutisyeri.picture29 = imgpath;
                                    break;
                                case 29:
                                    konutisyeri.picture30 = imgpath;
                                    break;
                                case 30:
                                    konutisyeri.picture31 = imgpath;
                                    break;
                                case 31:
                                    konutisyeri.picture32 = imgpath;
                                    break;
                                case 32:
                                    konutisyeri.picture33 = imgpath;
                                    break;
                                case 33:
                                    konutisyeri.picture34 = imgpath;
                                    break;
                                case 34:
                                    konutisyeri.picture35 = imgpath;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else if (Request.Files.Count > 35 && Path.GetFileName(Request.Files[0].FileName) != "") { return Json(new { status = "error", message = "25'den fazla resim giremezsiniz!" }); }

                    if (Path.GetFileName(Request.Files[0].FileName) == "")
                    {
                        long tKIsyeri = 0, tArsaTarla = 0, tBina = 0;
                        if (db.KONUT_ISYERI.Count() != 0) { tKIsyeri = db.KONUT_ISYERI.Max(item => item.ID); }
                        if (db.ARSA_TARLA.Count() != 0) { tArsaTarla = db.ARSA_TARLA.Max(item => item.ID); }
                        if (db.BINA.Count() != 0) { tBina = db.BINA.Max(item => item.ID); }
                        if (tKIsyeri > tArsaTarla && tKIsyeri > tBina) { konutisyeri.ID = tKIsyeri + 1; }
                        else if (tArsaTarla > tKIsyeri && tArsaTarla > tBina) { konutisyeri.ID = tArsaTarla + 1; }
                        else if (tBina > tKIsyeri && tBina > tArsaTarla) { konutisyeri.ID = tBina + 1; }
                        else { konutisyeri.ID = 1; }
                    }

                    konutisyeri.UserID = Int16.Parse(Session["userID"].ToString());
                    konutisyeri.Isyeri_Konut = "Isyeri";
                    konutisyeri.Tarih = DateTime.Now;
                    konutisyeri.Kira_Satilik = "Satılık";
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
                if (konutisyeri.ID == 0)
                {
                    if (Request.Files.Count < 35 && Path.GetFileName(Request.Files[0].FileName) != "")
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

                        long tKIsyeri = 0, tArsaTarla = 0, tBina = 0;
                        if (db.KONUT_ISYERI.Count() != 0) { tKIsyeri = db.KONUT_ISYERI.Max(item => item.ID); }
                        if (db.ARSA_TARLA.Count() != 0) { tArsaTarla = db.ARSA_TARLA.Max(item => item.ID); }
                        if (db.BINA.Count() != 0) { tBina = db.BINA.Max(item => item.ID); }
                        if (tKIsyeri > tArsaTarla && tKIsyeri > tBina) { konutisyeri.ID = tKIsyeri + 1; }
                        else if (tArsaTarla > tKIsyeri && tArsaTarla > tBina) { konutisyeri.ID = tArsaTarla + 1; }
                        else if (tBina > tKIsyeri && tBina > tArsaTarla) { konutisyeri.ID = tBina + 1; }
                        else { konutisyeri.ID = 1; }

                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            string imgname = Path.GetFileName(Request.Files[i].FileName);
                            string ext = Path.GetExtension(imgname);

                            string imgpath = Path.Combine(Server.MapPath("~/EmlakImages"), "Kisyeri_" + konutisyeri.ID.ToString() + "_" + (i + 1).ToString() + ext);
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
                                case 8:
                                    konutisyeri.picture9 = imgpath;
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
                                case 25:
                                    konutisyeri.picture26 = imgpath;
                                    break;
                                case 26:
                                    konutisyeri.picture27 = imgpath;
                                    break;
                                case 27:
                                    konutisyeri.picture28 = imgpath;
                                    break;
                                case 28:
                                    konutisyeri.picture29 = imgpath;
                                    break;
                                case 29:
                                    konutisyeri.picture30 = imgpath;
                                    break;
                                case 30:
                                    konutisyeri.picture31 = imgpath;
                                    break;
                                case 31:
                                    konutisyeri.picture32 = imgpath;
                                    break;
                                case 32:
                                    konutisyeri.picture33 = imgpath;
                                    break;
                                case 33:
                                    konutisyeri.picture34 = imgpath;
                                    break;
                                case 34:
                                    konutisyeri.picture35 = imgpath;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else if (Request.Files.Count > 35 && Path.GetFileName(Request.Files[0].FileName) != "") { return Json(new { status = "error", message = "25'den fazla resim giremezsiniz!" }); }

                    if (Path.GetFileName(Request.Files[0].FileName) == "")
                    {
                        long tKIsyeri = 0, tArsaTarla = 0, tBina = 0;
                        if (db.KONUT_ISYERI.Count() != 0) { tKIsyeri = db.KONUT_ISYERI.Max(item => item.ID); }
                        if (db.ARSA_TARLA.Count() != 0) { tArsaTarla = db.ARSA_TARLA.Max(item => item.ID); }
                        if (db.BINA.Count() != 0) { tBina = db.BINA.Max(item => item.ID); }
                        if (tKIsyeri > tArsaTarla && tKIsyeri > tBina) { konutisyeri.ID = tKIsyeri + 1; }
                        else if (tArsaTarla > tKIsyeri && tArsaTarla > tBina) { konutisyeri.ID = tArsaTarla + 1; }
                        else if (tBina > tKIsyeri && tBina > tArsaTarla) { konutisyeri.ID = tBina + 1; }
                        else { konutisyeri.ID = 1; }
                    }

                    konutisyeri.UserID = Int16.Parse(Session["userID"].ToString());
                    konutisyeri.Isyeri_Konut = "Isyeri";
                    konutisyeri.Tarih = DateTime.Now;
                    konutisyeri.Kira_Satilik = "Kiralık";
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
                if (konutisyeri.ID == 0)
                {
                    if (Request.Files.Count < 35 && Path.GetFileName(Request.Files[0].FileName) != "")
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

                        long tKIsyeri = 0, tArsaTarla = 0, tBina = 0;
                        if (db.KONUT_ISYERI.Count() != 0) { tKIsyeri = db.KONUT_ISYERI.Max(item => item.ID); }
                        if (db.ARSA_TARLA.Count() != 0) { tArsaTarla = db.ARSA_TARLA.Max(item => item.ID); }
                        if (db.BINA.Count() != 0) { tBina = db.BINA.Max(item => item.ID); }
                        if (tKIsyeri > tArsaTarla && tKIsyeri > tBina) { konutisyeri.ID = tKIsyeri + 1; }
                        else if (tArsaTarla > tKIsyeri && tArsaTarla > tBina) { konutisyeri.ID = tArsaTarla + 1; }
                        else if (tBina > tKIsyeri && tBina > tArsaTarla) { konutisyeri.ID = tBina + 1; }
                        else { konutisyeri.ID = 1; }

                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            string imgname = Path.GetFileName(Request.Files[i].FileName);
                            string ext = Path.GetExtension(imgname);

                            string imgpath = Path.Combine(Server.MapPath("~/EmlakImages"), "Kisyeri_" + konutisyeri.ID.ToString() + "_" + (i + 1).ToString() + ext);
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
                                case 8:
                                    konutisyeri.picture9 = imgpath;
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
                                case 25:
                                    konutisyeri.picture26 = imgpath;
                                    break;
                                case 26:
                                    konutisyeri.picture27 = imgpath;
                                    break;
                                case 27:
                                    konutisyeri.picture28 = imgpath;
                                    break;
                                case 28:
                                    konutisyeri.picture29 = imgpath;
                                    break;
                                case 29:
                                    konutisyeri.picture30 = imgpath;
                                    break;
                                case 30:
                                    konutisyeri.picture31 = imgpath;
                                    break;
                                case 31:
                                    konutisyeri.picture32 = imgpath;
                                    break;
                                case 32:
                                    konutisyeri.picture33 = imgpath;
                                    break;
                                case 33:
                                    konutisyeri.picture34 = imgpath;
                                    break;
                                case 34:
                                    konutisyeri.picture35 = imgpath;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else if (Request.Files.Count > 35 && Path.GetFileName(Request.Files[0].FileName) != "") { return Json(new { status = "error", message = "25'den fazla resim giremezsiniz!" }); }

                    if (Path.GetFileName(Request.Files[0].FileName) == "")
                    {
                        long tKIsyeri = 0, tArsaTarla = 0, tBina = 0;
                        if (db.KONUT_ISYERI.Count() != 0) { tKIsyeri = db.KONUT_ISYERI.Max(item => item.ID); }
                        if (db.ARSA_TARLA.Count() != 0) { tArsaTarla = db.ARSA_TARLA.Max(item => item.ID); }
                        if (db.BINA.Count() != 0) { tBina = db.BINA.Max(item => item.ID); }
                        if (tKIsyeri > tArsaTarla && tKIsyeri > tBina) { konutisyeri.ID = tKIsyeri + 1; }
                        else if (tArsaTarla > tKIsyeri && tArsaTarla > tBina) { konutisyeri.ID = tArsaTarla + 1; }
                        else if (tBina > tKIsyeri && tBina > tArsaTarla) { konutisyeri.ID = tBina + 1; }
                        else { konutisyeri.ID = 1; }
                    }

                    konutisyeri.UserID = Int16.Parse(Session["userID"].ToString());
                    konutisyeri.Isyeri_Konut = "Isyeri";
                    konutisyeri.Tarih = DateTime.Now;
                    konutisyeri.Kira_Satilik = "Satılık";
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
                if (konutisyeri.ID == 0)
                {
                    if (Request.Files.Count < 35 && Path.GetFileName(Request.Files[0].FileName) != "")
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

                        long tKIsyeri = 0, tArsaTarla = 0, tBina = 0;
                        if (db.KONUT_ISYERI.Count() != 0) { tKIsyeri = db.KONUT_ISYERI.Max(item => item.ID); }
                        if (db.ARSA_TARLA.Count() != 0) { tArsaTarla = db.ARSA_TARLA.Max(item => item.ID); }
                        if (db.BINA.Count() != 0) { tBina = db.BINA.Max(item => item.ID); }
                        if (tKIsyeri > tArsaTarla && tKIsyeri > tBina) { konutisyeri.ID = tKIsyeri + 1; }
                        else if (tArsaTarla > tKIsyeri && tArsaTarla > tBina) { konutisyeri.ID = tArsaTarla + 1; }
                        else if (tBina > tKIsyeri && tBina > tArsaTarla) { konutisyeri.ID = tBina + 1; }
                        else { konutisyeri.ID = 1; }

                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            string imgname = Path.GetFileName(Request.Files[i].FileName);
                            string ext = Path.GetExtension(imgname);

                            string imgpath = Path.Combine(Server.MapPath("~/EmlakImages"), "Kisyeri_" + konutisyeri.ID.ToString() + "_" + (i + 1).ToString() + ext);
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
                                case 8:
                                    konutisyeri.picture9 = imgpath;
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
                                case 25:
                                    konutisyeri.picture26 = imgpath;
                                    break;
                                case 26:
                                    konutisyeri.picture27 = imgpath;
                                    break;
                                case 27:
                                    konutisyeri.picture28 = imgpath;
                                    break;
                                case 28:
                                    konutisyeri.picture29 = imgpath;
                                    break;
                                case 29:
                                    konutisyeri.picture30 = imgpath;
                                    break;
                                case 30:
                                    konutisyeri.picture31 = imgpath;
                                    break;
                                case 31:
                                    konutisyeri.picture32 = imgpath;
                                    break;
                                case 32:
                                    konutisyeri.picture33 = imgpath;
                                    break;
                                case 33:
                                    konutisyeri.picture34 = imgpath;
                                    break;
                                case 34:
                                    konutisyeri.picture35 = imgpath;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else if (Request.Files.Count > 35 && Path.GetFileName(Request.Files[0].FileName) != "") { return Json(new { status = "error", message = "25'den fazla resim giremezsiniz!" }); }

                    if (Path.GetFileName(Request.Files[0].FileName) == "")
                    {
                        long tKIsyeri = 0, tArsaTarla = 0, tBina = 0;
                        if (db.KONUT_ISYERI.Count() != 0) { tKIsyeri = db.KONUT_ISYERI.Max(item => item.ID); }
                        if (db.ARSA_TARLA.Count() != 0) { tArsaTarla = db.ARSA_TARLA.Max(item => item.ID); }
                        if (db.BINA.Count() != 0) { tBina = db.BINA.Max(item => item.ID); }
                        if (tKIsyeri > tArsaTarla && tKIsyeri > tBina) { konutisyeri.ID = tKIsyeri + 1; }
                        else if (tArsaTarla > tKIsyeri && tArsaTarla > tBina) { konutisyeri.ID = tArsaTarla + 1; }
                        else if (tBina > tKIsyeri && tBina > tArsaTarla) { konutisyeri.ID = tBina + 1; }
                        else { konutisyeri.ID = 1; }
                    }

                    konutisyeri.UserID = Int16.Parse(Session["userID"].ToString());
                    konutisyeri.Isyeri_Konut = "Isyeri";
                    konutisyeri.Tarih = DateTime.Now;
                    konutisyeri.Kira_Satilik = "Kiralık";
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
        }


        [HttpGet]
        public ActionResult AddOrEditArsaSatilik(int id = 0)
        {
            setViewBags();
            if (id == 0)
            {
                return View(new ARSA_TARLA());
            }
            else
            {
                using (Entities db = new Entities())
                {
                    return View(db.ARSA_TARLA.Where(x => x.ID == id).FirstOrDefault<ARSA_TARLA>());
                }
            }
        }
        [HttpPost]
        public ActionResult AddOrEditArsaSatilik(ARSA_TARLA arsaTarla)
        {
            using (Entities db = new Entities())
            {
                if (arsaTarla.ID == 0)
                {
                    if (Request.Files.Count < 35 && Path.GetFileName(Request.Files[0].FileName) != "")
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

                        long tKIsyeri = 0, tArsaTarla = 0, tBina = 0;
                        if (db.KONUT_ISYERI.Count() != 0) { tKIsyeri = db.KONUT_ISYERI.Max(item => item.ID); }
                        if (db.ARSA_TARLA.Count() != 0) { tArsaTarla = db.ARSA_TARLA.Max(item => item.ID); }
                        if (db.BINA.Count() != 0) { tBina = db.BINA.Max(item => item.ID); }
                        if (tKIsyeri > tArsaTarla && tKIsyeri > tBina) { arsaTarla.ID = tKIsyeri + 1; }
                        else if (tArsaTarla > tKIsyeri && tArsaTarla > tBina) { arsaTarla.ID = tArsaTarla + 1; }
                        else if (tBina > tKIsyeri && tBina > tArsaTarla) { arsaTarla.ID = tBina + 1; }
                        else { arsaTarla.ID = 1; }

                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            string imgname = Path.GetFileName(Request.Files[i].FileName);
                            string ext = Path.GetExtension(imgname);

                            string imgpath = Path.Combine(Server.MapPath("~/EmlakImages"), "ArTarla_" + arsaTarla.ID.ToString() + "_" + (i + 1).ToString() + ext);
                            Request.Files[i].SaveAs(imgpath);
                            switch (i)
                            {
                                case 0:
                                    arsaTarla.picture1 = imgpath;
                                    break;
                                case 1:
                                    arsaTarla.picture2 = imgpath;
                                    break;
                                case 2:
                                    arsaTarla.picture3 = imgpath;
                                    break;
                                case 3:
                                    arsaTarla.picture4 = imgpath;
                                    break;
                                case 4:
                                    arsaTarla.picture5 = imgpath;
                                    break;
                                case 5:
                                    arsaTarla.picture6 = imgpath;
                                    break;
                                case 6:
                                    arsaTarla.picture7 = imgpath;
                                    break;
                                case 7:
                                    arsaTarla.picture8 = imgpath;
                                    break;
                                case 8:
                                    arsaTarla.picture9 = imgpath;
                                    break;
                                case 9:
                                    arsaTarla.picture10 = imgpath;
                                    break;
                                case 10:
                                    arsaTarla.picture11 = imgpath;
                                    break;
                                case 11:
                                    arsaTarla.picture12 = imgpath;
                                    break;
                                case 12:
                                    arsaTarla.picture13 = imgpath;
                                    break;
                                case 13:
                                    arsaTarla.picture14 = imgpath;
                                    break;
                                case 14:
                                    arsaTarla.picture15 = imgpath;
                                    break;
                                case 15:
                                    arsaTarla.picture16 = imgpath;
                                    break;
                                case 16:
                                    arsaTarla.picture17 = imgpath;
                                    break;
                                case 17:
                                    arsaTarla.picture18 = imgpath;
                                    break;
                                case 18:
                                    arsaTarla.picture19 = imgpath;
                                    break;
                                case 19:
                                    arsaTarla.picture20 = imgpath;
                                    break;
                                case 20:
                                    arsaTarla.picture21 = imgpath;
                                    break;
                                case 21:
                                    arsaTarla.picture22 = imgpath;
                                    break;
                                case 22:
                                    arsaTarla.picture23 = imgpath;
                                    break;
                                case 23:
                                    arsaTarla.picture24 = imgpath;
                                    break;
                                case 24:
                                    arsaTarla.picture25 = imgpath;
                                    break;
                                case 25:
                                    arsaTarla.picture26 = imgpath;
                                    break;
                                case 26:
                                    arsaTarla.picture27 = imgpath;
                                    break;
                                case 27:
                                    arsaTarla.picture28 = imgpath;
                                    break;
                                case 28:
                                    arsaTarla.picture29 = imgpath;
                                    break;
                                case 29:
                                    arsaTarla.picture30 = imgpath;
                                    break;
                                case 30:
                                    arsaTarla.picture31 = imgpath;
                                    break;
                                case 31:
                                    arsaTarla.picture32 = imgpath;
                                    break;
                                case 32:
                                    arsaTarla.picture33 = imgpath;
                                    break;
                                case 33:
                                    arsaTarla.picture34 = imgpath;
                                    break;
                                case 34:
                                    arsaTarla.picture35 = imgpath;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else if (Request.Files.Count > 35 && Path.GetFileName(Request.Files[0].FileName) != "") { return Json(new { status = "error", message = "25'den fazla resim giremezsiniz!" }); }

                    if (Path.GetFileName(Request.Files[0].FileName) == "")
                    {
                        long tKIsyeri = 0, tArsaTarla = 0, tBina = 0;
                        if (db.KONUT_ISYERI.Count() != 0) { tKIsyeri = db.KONUT_ISYERI.Max(item => item.ID); }
                        if (db.ARSA_TARLA.Count() != 0) { tArsaTarla = db.ARSA_TARLA.Max(item => item.ID); }
                        if (db.BINA.Count() != 0) { tBina = db.BINA.Max(item => item.ID); }
                        if (tKIsyeri > tArsaTarla && tKIsyeri > tBina) { arsaTarla.ID = tKIsyeri + 1; }
                        else if (tArsaTarla > tKIsyeri && tArsaTarla > tBina) { arsaTarla.ID = tArsaTarla + 1; }
                        else if (tBina > tKIsyeri && tBina > tArsaTarla) { arsaTarla.ID = tBina + 1; }
                        else { arsaTarla.ID = 1; }
                    }

                    arsaTarla.UserID = Int16.Parse(Session["userID"].ToString());
                    arsaTarla.Tarih = DateTime.Now;
                    arsaTarla.Tarla_Arsa = "Arsa";
                    arsaTarla.Kira_Satilik = "Satılık";
                    db.ARSA_TARLA.Add(arsaTarla);

                    db.SaveChanges();

                    return Json(new { success = true, message = "Başarıyla Kaydedildi" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    arsaTarla.UserID = Int16.Parse(Session["userID"].ToString());
                    db.Entry(arsaTarla).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Başarıyla Güncellendi" }, JsonRequestBehavior.AllowGet);
                }
            }
        }


        [HttpGet]
        public ActionResult AddOrEditArsaKiralik(int id = 0)
        {
            setViewBags();
            if (id == 0)
            {
                return View(new ARSA_TARLA());
            }
            else
            {
                using (Entities db = new Entities())
                {
                    return View(db.ARSA_TARLA.Where(x => x.ID == id).FirstOrDefault<ARSA_TARLA>());
                }
            }
        }
        [HttpPost]
        public ActionResult AddOrEditArsaKiralik(ARSA_TARLA arsaTarla)
        {
            using (Entities db = new Entities())
            {
                if (arsaTarla.ID == 0)
                {
                    if (Request.Files.Count < 35 && Path.GetFileName(Request.Files[0].FileName) != "")
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

                        long tKIsyeri = 0, tArsaTarla = 0, tBina = 0;
                        if (db.KONUT_ISYERI.Count() != 0) { tKIsyeri = db.KONUT_ISYERI.Max(item => item.ID); }
                        if (db.ARSA_TARLA.Count() != 0) { tArsaTarla = db.ARSA_TARLA.Max(item => item.ID); }
                        if (db.BINA.Count() != 0) { tBina = db.BINA.Max(item => item.ID); }
                        if (tKIsyeri > tArsaTarla && tKIsyeri > tBina) { arsaTarla.ID = tKIsyeri + 1; }
                        else if (tArsaTarla > tKIsyeri && tArsaTarla > tBina) { arsaTarla.ID = tArsaTarla + 1; }
                        else if (tBina > tKIsyeri && tBina > tArsaTarla) { arsaTarla.ID = tBina + 1; }
                        else { arsaTarla.ID = 1; }

                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            string imgname = Path.GetFileName(Request.Files[i].FileName);
                            string ext = Path.GetExtension(imgname);

                            string imgpath = Path.Combine(Server.MapPath("~/EmlakImages"), "ArTarla_" + arsaTarla.ID.ToString() + "_" + (i + 1).ToString() + ext);
                            Request.Files[i].SaveAs(imgpath);
                            switch (i)
                            {
                                case 0:
                                    arsaTarla.picture1 = imgpath;
                                    break;
                                case 1:
                                    arsaTarla.picture2 = imgpath;
                                    break;
                                case 2:
                                    arsaTarla.picture3 = imgpath;
                                    break;
                                case 3:
                                    arsaTarla.picture4 = imgpath;
                                    break;
                                case 4:
                                    arsaTarla.picture5 = imgpath;
                                    break;
                                case 5:
                                    arsaTarla.picture6 = imgpath;
                                    break;
                                case 6:
                                    arsaTarla.picture7 = imgpath;
                                    break;
                                case 7:
                                    arsaTarla.picture8 = imgpath;
                                    break;
                                case 8:
                                    arsaTarla.picture9 = imgpath;
                                    break;
                                case 9:
                                    arsaTarla.picture10 = imgpath;
                                    break;
                                case 10:
                                    arsaTarla.picture11 = imgpath;
                                    break;
                                case 11:
                                    arsaTarla.picture12 = imgpath;
                                    break;
                                case 12:
                                    arsaTarla.picture13 = imgpath;
                                    break;
                                case 13:
                                    arsaTarla.picture14 = imgpath;
                                    break;
                                case 14:
                                    arsaTarla.picture15 = imgpath;
                                    break;
                                case 15:
                                    arsaTarla.picture16 = imgpath;
                                    break;
                                case 16:
                                    arsaTarla.picture17 = imgpath;
                                    break;
                                case 17:
                                    arsaTarla.picture18 = imgpath;
                                    break;
                                case 18:
                                    arsaTarla.picture19 = imgpath;
                                    break;
                                case 19:
                                    arsaTarla.picture20 = imgpath;
                                    break;
                                case 20:
                                    arsaTarla.picture21 = imgpath;
                                    break;
                                case 21:
                                    arsaTarla.picture22 = imgpath;
                                    break;
                                case 22:
                                    arsaTarla.picture23 = imgpath;
                                    break;
                                case 23:
                                    arsaTarla.picture24 = imgpath;
                                    break;
                                case 24:
                                    arsaTarla.picture25 = imgpath;
                                    break;
                                case 25:
                                    arsaTarla.picture26 = imgpath;
                                    break;
                                case 26:
                                    arsaTarla.picture27 = imgpath;
                                    break;
                                case 27:
                                    arsaTarla.picture28 = imgpath;
                                    break;
                                case 28:
                                    arsaTarla.picture29 = imgpath;
                                    break;
                                case 29:
                                    arsaTarla.picture30 = imgpath;
                                    break;
                                case 30:
                                    arsaTarla.picture31 = imgpath;
                                    break;
                                case 31:
                                    arsaTarla.picture32 = imgpath;
                                    break;
                                case 32:
                                    arsaTarla.picture33 = imgpath;
                                    break;
                                case 33:
                                    arsaTarla.picture34 = imgpath;
                                    break;
                                case 34:
                                    arsaTarla.picture35 = imgpath;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else if (Request.Files.Count > 35 && Path.GetFileName(Request.Files[0].FileName) != "") { return Json(new { status = "error", message = "25'den fazla resim giremezsiniz!" }); }

                    if (Path.GetFileName(Request.Files[0].FileName) == "")
                    {
                        long tKIsyeri = 0, tArsaTarla = 0, tBina = 0;
                        if (db.KONUT_ISYERI.Count() != 0) { tKIsyeri = db.KONUT_ISYERI.Max(item => item.ID); }
                        if (db.ARSA_TARLA.Count() != 0) { tArsaTarla = db.ARSA_TARLA.Max(item => item.ID); }
                        if (db.BINA.Count() != 0) { tBina = db.BINA.Max(item => item.ID); }
                        if (tKIsyeri > tArsaTarla && tKIsyeri > tBina) { arsaTarla.ID = tKIsyeri + 1; }
                        else if (tArsaTarla > tKIsyeri && tArsaTarla > tBina) { arsaTarla.ID = tArsaTarla + 1; }
                        else if (tBina > tKIsyeri && tBina > tArsaTarla) { arsaTarla.ID = tBina + 1; }
                        else { arsaTarla.ID = 1; }
                    }

                    arsaTarla.UserID = Int16.Parse(Session["userID"].ToString());
                    arsaTarla.Tarih = DateTime.Now;
                    arsaTarla.Tarla_Arsa = "Arsa";
                    arsaTarla.Kira_Satilik = "Kiralık";
                    db.ARSA_TARLA.Add(arsaTarla);

                    db.SaveChanges();

                    return Json(new { success = true, message = "Başarıyla Kaydedildi" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    arsaTarla.UserID = Int16.Parse(Session["userID"].ToString());
                    db.Entry(arsaTarla).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Başarıyla Güncellendi" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpGet]
        public ActionResult AddOrEditTarlaSatilik(int id = 0)
        {
            setViewBags();
            if (id == 0)
            {
                return View(new ARSA_TARLA());
            }
            else
            {
                using (Entities db = new Entities())
                {
                    return View(db.ARSA_TARLA.Where(x => x.ID == id).FirstOrDefault<ARSA_TARLA>());
                }
            }
        }
        [HttpPost]
        public ActionResult AddOrEditTarlaSatilik(ARSA_TARLA arsaTarla)
        {
            using (Entities db = new Entities())
            {
                if (arsaTarla.ID == 0)
                {
                    if (Request.Files.Count < 35 && Path.GetFileName(Request.Files[0].FileName) != "")
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

                        long tKIsyeri = 0, tArsaTarla = 0, tBina = 0;
                        if (db.KONUT_ISYERI.Count() != 0) { tKIsyeri = db.KONUT_ISYERI.Max(item => item.ID); }
                        if (db.ARSA_TARLA.Count() != 0) { tArsaTarla = db.ARSA_TARLA.Max(item => item.ID); }
                        if (db.BINA.Count() != 0) { tBina = db.BINA.Max(item => item.ID); }
                        if (tKIsyeri > tArsaTarla && tKIsyeri > tBina) { arsaTarla.ID = tKIsyeri + 1; }
                        else if (tArsaTarla > tKIsyeri && tArsaTarla > tBina) { arsaTarla.ID = tArsaTarla + 1; }
                        else if (tBina > tKIsyeri && tBina > tArsaTarla) { arsaTarla.ID = tBina + 1; }
                        else { arsaTarla.ID = 1; }

                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            string imgname = Path.GetFileName(Request.Files[i].FileName);
                            string ext = Path.GetExtension(imgname);

                            string imgpath = Path.Combine(Server.MapPath("~/EmlakImages"), "ArTarla_" + arsaTarla.ID.ToString() + "_" + (i + 1).ToString() + ext);
                            Request.Files[i].SaveAs(imgpath);
                            switch (i)
                            {
                                case 0:
                                    arsaTarla.picture1 = imgpath;
                                    break;
                                case 1:
                                    arsaTarla.picture2 = imgpath;
                                    break;
                                case 2:
                                    arsaTarla.picture3 = imgpath;
                                    break;
                                case 3:
                                    arsaTarla.picture4 = imgpath;
                                    break;
                                case 4:
                                    arsaTarla.picture5 = imgpath;
                                    break;
                                case 5:
                                    arsaTarla.picture6 = imgpath;
                                    break;
                                case 6:
                                    arsaTarla.picture7 = imgpath;
                                    break;
                                case 7:
                                    arsaTarla.picture8 = imgpath;
                                    break;
                                case 8:
                                    arsaTarla.picture9 = imgpath;
                                    break;
                                case 9:
                                    arsaTarla.picture10 = imgpath;
                                    break;
                                case 10:
                                    arsaTarla.picture11 = imgpath;
                                    break;
                                case 11:
                                    arsaTarla.picture12 = imgpath;
                                    break;
                                case 12:
                                    arsaTarla.picture13 = imgpath;
                                    break;
                                case 13:
                                    arsaTarla.picture14 = imgpath;
                                    break;
                                case 14:
                                    arsaTarla.picture15 = imgpath;
                                    break;
                                case 15:
                                    arsaTarla.picture16 = imgpath;
                                    break;
                                case 16:
                                    arsaTarla.picture17 = imgpath;
                                    break;
                                case 17:
                                    arsaTarla.picture18 = imgpath;
                                    break;
                                case 18:
                                    arsaTarla.picture19 = imgpath;
                                    break;
                                case 19:
                                    arsaTarla.picture20 = imgpath;
                                    break;
                                case 20:
                                    arsaTarla.picture21 = imgpath;
                                    break;
                                case 21:
                                    arsaTarla.picture22 = imgpath;
                                    break;
                                case 22:
                                    arsaTarla.picture23 = imgpath;
                                    break;
                                case 23:
                                    arsaTarla.picture24 = imgpath;
                                    break;
                                case 24:
                                    arsaTarla.picture25 = imgpath;
                                    break;
                                case 25:
                                    arsaTarla.picture26 = imgpath;
                                    break;
                                case 26:
                                    arsaTarla.picture27 = imgpath;
                                    break;
                                case 27:
                                    arsaTarla.picture28 = imgpath;
                                    break;
                                case 28:
                                    arsaTarla.picture29 = imgpath;
                                    break;
                                case 29:
                                    arsaTarla.picture30 = imgpath;
                                    break;
                                case 30:
                                    arsaTarla.picture31 = imgpath;
                                    break;
                                case 31:
                                    arsaTarla.picture32 = imgpath;
                                    break;
                                case 32:
                                    arsaTarla.picture33 = imgpath;
                                    break;
                                case 33:
                                    arsaTarla.picture34 = imgpath;
                                    break;
                                case 34:
                                    arsaTarla.picture35 = imgpath;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else if (Request.Files.Count > 35 && Path.GetFileName(Request.Files[0].FileName) != "") { return Json(new { status = "error", message = "25'den fazla resim giremezsiniz!" }); }

                    if (Path.GetFileName(Request.Files[0].FileName) == "")
                    {
                        long tKIsyeri = 0, tArsaTarla = 0, tBina = 0;
                        if (db.KONUT_ISYERI.Count() != 0) { tKIsyeri = db.KONUT_ISYERI.Max(item => item.ID); }
                        if (db.ARSA_TARLA.Count() != 0) { tArsaTarla = db.ARSA_TARLA.Max(item => item.ID); }
                        if (db.BINA.Count() != 0) { tBina = db.BINA.Max(item => item.ID); }
                        if (tKIsyeri > tArsaTarla && tKIsyeri > tBina) { arsaTarla.ID = tKIsyeri + 1; }
                        else if (tArsaTarla > tKIsyeri && tArsaTarla > tBina) { arsaTarla.ID = tArsaTarla + 1; }
                        else if (tBina > tKIsyeri && tBina > tArsaTarla) { arsaTarla.ID = tBina + 1; }
                        else { arsaTarla.ID = 1; }
                    }

                    arsaTarla.UserID = Int16.Parse(Session["userID"].ToString());
                    arsaTarla.Tarih = DateTime.Now;
                    arsaTarla.Tarla_Arsa = "Tarla";
                    arsaTarla.Kira_Satilik = "Satılık";
                    db.ARSA_TARLA.Add(arsaTarla);

                    db.SaveChanges();

                    return Json(new { success = true, message = "Başarıyla Kaydedildi" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    arsaTarla.UserID = Int16.Parse(Session["userID"].ToString());
                    db.Entry(arsaTarla).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Başarıyla Güncellendi" }, JsonRequestBehavior.AllowGet);
                }
            }
        }


        [HttpGet]
        public ActionResult AddOrEditTarlaKiralik(int id = 0)
        {
            setViewBags();
            if (id == 0)
            {
                return View(new ARSA_TARLA());
            }
            else
            {
                using (Entities db = new Entities())
                {
                    return View(db.ARSA_TARLA.Where(x => x.ID == id).FirstOrDefault<ARSA_TARLA>());
                }
            }
        }
        [HttpPost]
        public ActionResult AddOrEditTarlaKiralik(ARSA_TARLA arsaTarla)
        {
            using (Entities db = new Entities())
            {
                if (arsaTarla.ID == 0)
                {
                    if (Request.Files.Count < 35 && Path.GetFileName(Request.Files[0].FileName) != "")
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

                        long tKIsyeri = 0, tArsaTarla = 0, tBina = 0;
                        if (db.KONUT_ISYERI.Count() != 0) { tKIsyeri = db.KONUT_ISYERI.Max(item => item.ID); }
                        if (db.ARSA_TARLA.Count() != 0) { tArsaTarla = db.ARSA_TARLA.Max(item => item.ID); }
                        if (db.BINA.Count() != 0) { tBina = db.BINA.Max(item => item.ID); }
                        if (tKIsyeri > tArsaTarla && tKIsyeri > tBina) { arsaTarla.ID = tKIsyeri + 1; }
                        else if (tArsaTarla > tKIsyeri && tArsaTarla > tBina) { arsaTarla.ID = tArsaTarla + 1; }
                        else if (tBina > tKIsyeri && tBina > tArsaTarla) { arsaTarla.ID = tBina + 1; }
                        else { arsaTarla.ID = 1; }

                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            string imgname = Path.GetFileName(Request.Files[i].FileName);
                            string ext = Path.GetExtension(imgname);

                            string imgpath = Path.Combine(Server.MapPath("~/EmlakImages"), "ArTarla_" + arsaTarla.ID.ToString() + "_" + (i + 1).ToString() + ext);
                            Request.Files[i].SaveAs(imgpath);
                            switch (i)
                            {
                                case 0:
                                    arsaTarla.picture1 = imgpath;
                                    break;
                                case 1:
                                    arsaTarla.picture2 = imgpath;
                                    break;
                                case 2:
                                    arsaTarla.picture3 = imgpath;
                                    break;
                                case 3:
                                    arsaTarla.picture4 = imgpath;
                                    break;
                                case 4:
                                    arsaTarla.picture5 = imgpath;
                                    break;
                                case 5:
                                    arsaTarla.picture6 = imgpath;
                                    break;
                                case 6:
                                    arsaTarla.picture7 = imgpath;
                                    break;
                                case 7:
                                    arsaTarla.picture8 = imgpath;
                                    break;
                                case 8:
                                    arsaTarla.picture9 = imgpath;
                                    break;
                                case 9:
                                    arsaTarla.picture10 = imgpath;
                                    break;
                                case 10:
                                    arsaTarla.picture11 = imgpath;
                                    break;
                                case 11:
                                    arsaTarla.picture12 = imgpath;
                                    break;
                                case 12:
                                    arsaTarla.picture13 = imgpath;
                                    break;
                                case 13:
                                    arsaTarla.picture14 = imgpath;
                                    break;
                                case 14:
                                    arsaTarla.picture15 = imgpath;
                                    break;
                                case 15:
                                    arsaTarla.picture16 = imgpath;
                                    break;
                                case 16:
                                    arsaTarla.picture17 = imgpath;
                                    break;
                                case 17:
                                    arsaTarla.picture18 = imgpath;
                                    break;
                                case 18:
                                    arsaTarla.picture19 = imgpath;
                                    break;
                                case 19:
                                    arsaTarla.picture20 = imgpath;
                                    break;
                                case 20:
                                    arsaTarla.picture21 = imgpath;
                                    break;
                                case 21:
                                    arsaTarla.picture22 = imgpath;
                                    break;
                                case 22:
                                    arsaTarla.picture23 = imgpath;
                                    break;
                                case 23:
                                    arsaTarla.picture24 = imgpath;
                                    break;
                                case 24:
                                    arsaTarla.picture25 = imgpath;
                                    break;
                                case 25:
                                    arsaTarla.picture26 = imgpath;
                                    break;
                                case 26:
                                    arsaTarla.picture27 = imgpath;
                                    break;
                                case 27:
                                    arsaTarla.picture28 = imgpath;
                                    break;
                                case 28:
                                    arsaTarla.picture29 = imgpath;
                                    break;
                                case 29:
                                    arsaTarla.picture30 = imgpath;
                                    break;
                                case 30:
                                    arsaTarla.picture31 = imgpath;
                                    break;
                                case 31:
                                    arsaTarla.picture32 = imgpath;
                                    break;
                                case 32:
                                    arsaTarla.picture33 = imgpath;
                                    break;
                                case 33:
                                    arsaTarla.picture34 = imgpath;
                                    break;
                                case 34:
                                    arsaTarla.picture35 = imgpath;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else if (Request.Files.Count > 35 && Path.GetFileName(Request.Files[0].FileName) != "") { return Json(new { status = "error", message = "25'den fazla resim giremezsiniz!" }); }

                    if (Path.GetFileName(Request.Files[0].FileName) == "")
                    {
                        long tKIsyeri = 0, tArsaTarla = 0, tBina = 0;
                        if (db.KONUT_ISYERI.Count() != 0) { tKIsyeri = db.KONUT_ISYERI.Max(item => item.ID); }
                        if (db.ARSA_TARLA.Count() != 0) { tArsaTarla = db.ARSA_TARLA.Max(item => item.ID); }
                        if (db.BINA.Count() != 0) { tBina = db.BINA.Max(item => item.ID); }
                        if (tKIsyeri > tArsaTarla && tKIsyeri > tBina) { arsaTarla.ID = tKIsyeri + 1; }
                        else if (tArsaTarla > tKIsyeri && tArsaTarla > tBina) { arsaTarla.ID = tArsaTarla + 1; }
                        else if (tBina > tKIsyeri && tBina > tArsaTarla) { arsaTarla.ID = tBina + 1; }
                        else { arsaTarla.ID = 1; }
                    }

                    arsaTarla.UserID = Int16.Parse(Session["userID"].ToString());
                    arsaTarla.Tarih = DateTime.Now;
                    arsaTarla.Tarla_Arsa = "Tarla";
                    arsaTarla.Kira_Satilik = "Kiralık";
                    db.ARSA_TARLA.Add(arsaTarla);

                    db.SaveChanges();

                    return Json(new { success = true, message = "Başarıyla Kaydedildi" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    arsaTarla.UserID = Int16.Parse(Session["userID"].ToString());
                    db.Entry(arsaTarla).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Başarıyla Güncellendi" }, JsonRequestBehavior.AllowGet);
                }
            }
        }


        [HttpGet]
        public ActionResult AddOrEditBinaSatilik(int id = 0)
        {
            setViewBags();
            if (id == 0)
            {
                return View(new BINA());
            }
            else
            {
                using (Entities db = new Entities())
                {
                    return View(db.BINA.Where(x => x.ID == id).FirstOrDefault<BINA>());
                }
            }
        }
        [HttpPost]
        public ActionResult AddOrEditBinaSatilik(BINA binaObj)
        {
            using (Entities db = new Entities())
            {
                if (binaObj.ID == 0)
                {
                    if (Request.Files.Count < 35 && Path.GetFileName(Request.Files[0].FileName) != "")
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

                        long tKIsyeri = 0, tArsaTarla = 0, tBina = 0;
                        if (db.KONUT_ISYERI.Count() != 0) { tKIsyeri = db.KONUT_ISYERI.Max(item => item.ID); }
                        if (db.ARSA_TARLA.Count() != 0) { tArsaTarla = db.ARSA_TARLA.Max(item => item.ID); }
                        if (db.BINA.Count() != 0) { tBina = db.BINA.Max(item => item.ID); }
                        if (tKIsyeri > tArsaTarla && tKIsyeri > tBina) { binaObj.ID = tKIsyeri + 1; }
                        else if (tArsaTarla > tKIsyeri && tArsaTarla > tBina) { binaObj.ID = tArsaTarla + 1; }
                        else if (tBina > tKIsyeri && tBina > tArsaTarla) { binaObj.ID = tBina + 1; }
                        else { binaObj.ID = 1; }

                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            string imgname = Path.GetFileName(Request.Files[i].FileName);
                            string ext = Path.GetExtension(imgname);

                            string imgpath = Path.Combine(Server.MapPath("~/EmlakImages"), "Bina_" + binaObj.ID.ToString() + "_" + (i + 1).ToString() + ext);
                            Request.Files[i].SaveAs(imgpath);
                            switch (i)
                            {
                                case 0:
                                    binaObj.picture1 = imgpath;
                                    break;
                                case 1:
                                    binaObj.picture2 = imgpath;
                                    break;
                                case 2:
                                    binaObj.picture3 = imgpath;
                                    break;
                                case 3:
                                    binaObj.picture4 = imgpath;
                                    break;
                                case 4:
                                    binaObj.picture5 = imgpath;
                                    break;
                                case 5:
                                    binaObj.picture6 = imgpath;
                                    break;
                                case 6:
                                    binaObj.picture7 = imgpath;
                                    break;
                                case 7:
                                    binaObj.picture8 = imgpath;
                                    break;
                                case 8:
                                    binaObj.picture9 = imgpath;
                                    break;
                                case 9:
                                    binaObj.picture10 = imgpath;
                                    break;
                                case 10:
                                    binaObj.picture11 = imgpath;
                                    break;
                                case 11:
                                    binaObj.picture12 = imgpath;
                                    break;
                                case 12:
                                    binaObj.picture13 = imgpath;
                                    break;
                                case 13:
                                    binaObj.picture14 = imgpath;
                                    break;
                                case 14:
                                    binaObj.picture15 = imgpath;
                                    break;
                                case 15:
                                    binaObj.picture16 = imgpath;
                                    break;
                                case 16:
                                    binaObj.picture17 = imgpath;
                                    break;
                                case 17:
                                    binaObj.picture18 = imgpath;
                                    break;
                                case 18:
                                    binaObj.picture19 = imgpath;
                                    break;
                                case 19:
                                    binaObj.picture20 = imgpath;
                                    break;
                                case 20:
                                    binaObj.picture21 = imgpath;
                                    break;
                                case 21:
                                    binaObj.picture22 = imgpath;
                                    break;
                                case 22:
                                    binaObj.picture23 = imgpath;
                                    break;
                                case 23:
                                    binaObj.picture24 = imgpath;
                                    break;
                                case 24:
                                    binaObj.picture25 = imgpath;
                                    break;
                                case 25:
                                    binaObj.picture26 = imgpath;
                                    break;
                                case 26:
                                    binaObj.picture27 = imgpath;
                                    break;
                                case 27:
                                    binaObj.picture28 = imgpath;
                                    break;
                                case 28:
                                    binaObj.picture29 = imgpath;
                                    break;
                                case 29:
                                    binaObj.picture30 = imgpath;
                                    break;
                                case 30:
                                    binaObj.picture31 = imgpath;
                                    break;
                                case 31:
                                    binaObj.picture32 = imgpath;
                                    break;
                                case 32:
                                    binaObj.picture33 = imgpath;
                                    break;
                                case 33:
                                    binaObj.picture34 = imgpath;
                                    break;
                                case 34:
                                    binaObj.picture35 = imgpath;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else if (Request.Files.Count > 35 && Path.GetFileName(Request.Files[0].FileName) != "") { return Json(new { status = "error", message = "25'den fazla resim giremezsiniz!" }); }

                    if (Path.GetFileName(Request.Files[0].FileName) == "")
                    {
                        long tKIsyeri = 0, tArsaTarla = 0, tBina = 0;
                        if (db.KONUT_ISYERI.Count() != 0) { tKIsyeri = db.KONUT_ISYERI.Max(item => item.ID); }
                        if (db.ARSA_TARLA.Count() != 0) { tArsaTarla = db.ARSA_TARLA.Max(item => item.ID); }
                        if (db.BINA.Count() != 0) { tBina = db.BINA.Max(item => item.ID); }
                        if (tKIsyeri > tArsaTarla && tKIsyeri > tBina) { binaObj.ID = tKIsyeri + 1; }
                        else if (tArsaTarla > tKIsyeri && tArsaTarla > tBina) { binaObj.ID = tArsaTarla + 1; }
                        else if (tBina > tKIsyeri && tBina > tArsaTarla) { binaObj.ID = tBina + 1; }
                        else { binaObj.ID = 1; }
                    }

                    binaObj.UserID = Int16.Parse(Session["userID"].ToString());
                    binaObj.Tarih = DateTime.Now;
                    binaObj.Kira_Satilik = "Satılık";
                    db.BINA.Add(binaObj);

                    db.SaveChanges();

                    return Json(new { success = true, message = "Başarıyla Kaydedildi" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    binaObj.UserID = Int16.Parse(Session["userID"].ToString());
                    db.Entry(binaObj).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Başarıyla Güncellendi" }, JsonRequestBehavior.AllowGet);
                }
            }
        }


        [HttpGet]
        public ActionResult AddOrEditBinaKiralik(int id = 0)
        {
            setViewBags();
            if (id == 0)
            {
                return View(new BINA());
            }
            else
            {
                using (Entities db = new Entities())
                {
                    return View(db.BINA.Where(x => x.ID == id).FirstOrDefault<BINA>());
                }
            }
        }
        [HttpPost]
        public ActionResult AddOrEditBinaKiralik(BINA binaObj)
        {
            using (Entities db = new Entities())
            {
                if (binaObj.ID == 0)
                {
                    if (Request.Files.Count < 35 && Path.GetFileName(Request.Files[0].FileName) != "")
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

                        long tKIsyeri = 0, tArsaTarla = 0, tBina = 0;
                        if (db.KONUT_ISYERI.Count() != 0) { tKIsyeri = db.KONUT_ISYERI.Max(item => item.ID); }
                        if (db.ARSA_TARLA.Count() != 0) { tArsaTarla = db.ARSA_TARLA.Max(item => item.ID); }
                        if (db.BINA.Count() != 0) { tBina = db.BINA.Max(item => item.ID); }
                        if (tKIsyeri > tArsaTarla && tKIsyeri > tBina) { binaObj.ID = tKIsyeri + 1; }
                        else if (tArsaTarla > tKIsyeri && tArsaTarla > tBina) { binaObj.ID = tArsaTarla + 1; }
                        else if (tBina > tKIsyeri && tBina > tArsaTarla) { binaObj.ID = tBina + 1; }
                        else { binaObj.ID = 1; }

                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            string imgname = Path.GetFileName(Request.Files[i].FileName);
                            string ext = Path.GetExtension(imgname);

                            string imgpath = Path.Combine(Server.MapPath("~/EmlakImages"), "Bina_" + binaObj.ID.ToString() + "_" + (i + 1).ToString() + ext);
                            Request.Files[i].SaveAs(imgpath);
                            switch (i)
                            {
                                case 0:
                                    binaObj.picture1 = imgpath;
                                    break;
                                case 1:
                                    binaObj.picture2 = imgpath;
                                    break;
                                case 2:
                                    binaObj.picture3 = imgpath;
                                    break;
                                case 3:
                                    binaObj.picture4 = imgpath;
                                    break;
                                case 4:
                                    binaObj.picture5 = imgpath;
                                    break;
                                case 5:
                                    binaObj.picture6 = imgpath;
                                    break;
                                case 6:
                                    binaObj.picture7 = imgpath;
                                    break;
                                case 7:
                                    binaObj.picture8 = imgpath;
                                    break;
                                case 8:
                                    binaObj.picture9 = imgpath;
                                    break;
                                case 9:
                                    binaObj.picture10 = imgpath;
                                    break;
                                case 10:
                                    binaObj.picture11 = imgpath;
                                    break;
                                case 11:
                                    binaObj.picture12 = imgpath;
                                    break;
                                case 12:
                                    binaObj.picture13 = imgpath;
                                    break;
                                case 13:
                                    binaObj.picture14 = imgpath;
                                    break;
                                case 14:
                                    binaObj.picture15 = imgpath;
                                    break;
                                case 15:
                                    binaObj.picture16 = imgpath;
                                    break;
                                case 16:
                                    binaObj.picture17 = imgpath;
                                    break;
                                case 17:
                                    binaObj.picture18 = imgpath;
                                    break;
                                case 18:
                                    binaObj.picture19 = imgpath;
                                    break;
                                case 19:
                                    binaObj.picture20 = imgpath;
                                    break;
                                case 20:
                                    binaObj.picture21 = imgpath;
                                    break;
                                case 21:
                                    binaObj.picture22 = imgpath;
                                    break;
                                case 22:
                                    binaObj.picture23 = imgpath;
                                    break;
                                case 23:
                                    binaObj.picture24 = imgpath;
                                    break;
                                case 24:
                                    binaObj.picture25 = imgpath;
                                    break;
                                case 25:
                                    binaObj.picture26 = imgpath;
                                    break;
                                case 26:
                                    binaObj.picture27 = imgpath;
                                    break;
                                case 27:
                                    binaObj.picture28 = imgpath;
                                    break;
                                case 28:
                                    binaObj.picture29 = imgpath;
                                    break;
                                case 29:
                                    binaObj.picture30 = imgpath;
                                    break;
                                case 30:
                                    binaObj.picture31 = imgpath;
                                    break;
                                case 31:
                                    binaObj.picture32 = imgpath;
                                    break;
                                case 32:
                                    binaObj.picture33 = imgpath;
                                    break;
                                case 33:
                                    binaObj.picture34 = imgpath;
                                    break;
                                case 34:
                                    binaObj.picture35 = imgpath;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else if (Request.Files.Count > 35 && Path.GetFileName(Request.Files[0].FileName) != "") { return Json(new { status = "error", message = "25'den fazla resim giremezsiniz!" }); }

                    if (Path.GetFileName(Request.Files[0].FileName) == "")
                    {
                        long tKIsyeri = 0, tArsaTarla = 0, tBina = 0;
                        if (db.KONUT_ISYERI.Count() != 0) { tKIsyeri = db.KONUT_ISYERI.Max(item => item.ID); }
                        if (db.ARSA_TARLA.Count() != 0) { tArsaTarla = db.ARSA_TARLA.Max(item => item.ID); }
                        if (db.BINA.Count() != 0) { tBina = db.BINA.Max(item => item.ID); }
                        if (tKIsyeri > tArsaTarla && tKIsyeri > tBina) { binaObj.ID = tKIsyeri + 1; }
                        else if (tArsaTarla > tKIsyeri && tArsaTarla > tBina) { binaObj.ID = tArsaTarla + 1; }
                        else if (tBina > tKIsyeri && tBina > tArsaTarla) { binaObj.ID = tBina + 1; }
                        else { binaObj.ID = 1; }
                    }

                    binaObj.UserID = Int16.Parse(Session["userID"].ToString());
                    binaObj.Tarih = DateTime.Now;
                    binaObj.Kira_Satilik = "Kiralık";
                    db.BINA.Add(binaObj);

                    db.SaveChanges();

                    return Json(new { success = true, message = "Başarıyla Kaydedildi" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    binaObj.UserID = Int16.Parse(Session["userID"].ToString());
                    db.Entry(binaObj).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Başarıyla Güncellendi" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

    }
}