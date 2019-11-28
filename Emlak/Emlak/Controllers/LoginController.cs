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
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(Models.USERS userModel)
        {
            using (Entities db = new Entities())
            {
                var userDetails = db.USERS.Where(x => x.UserNickName == userModel.UserNickName && x.UserPassword == userModel.UserPassword).FirstOrDefault();

                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Kullanıcı Adı veya Şifre Hatalı !";
                    return View("Index",userModel);
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

                    return RedirectToAction("Index","Owner");
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