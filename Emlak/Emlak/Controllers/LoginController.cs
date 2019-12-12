using Emlak.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emlak.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
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
            }
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(string ID,string Sifre)
        {
            using (Entities db = new Entities())
            {
                var userDetails = db.USERS.Where(x => x.UserNickName == ID && x.UserPassword == Sifre).FirstOrDefault();

                if (userDetails == null)
                {
                    USERS usr=new USERS();
                    usr.LoginErrorMessage = "Kullanıcı Adı veya Şifre Hatalı !";
                    return View("Index", usr);
                }
                else
                {
                    Session["userID"] = userDetails.UserID;
                    Session["UserName"] = userDetails.UserName;
                    Session["UserSurName"] = userDetails.UserSurname;
                    Session["UserType"] = userDetails.USERTYPES.UserType;
                    Session["UserNickName"] = userDetails.UserNickName;
                    Session["Unvan"] = userDetails.Unvan;
                    Session["CepTel"] = userDetails.CepTelefonu;
                    Session["IsTel"] = userDetails.IsTelefonu;
                    Session["UserPassword"] = userDetails.UserPassword;
                    Session["Userpp"] = userDetails.ppicture;

                    return RedirectToAction("Index","KullaniciBilgileri");
                }
            }
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index","Login");
        }
    }
}