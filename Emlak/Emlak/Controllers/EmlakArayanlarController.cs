using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emlak.Controllers
{
    public class EmlakArayanlarController : Controller
    {
        // GET: EmlakArayanlar
        public ActionResult Index()
        {
            return View();
        }
    }
}