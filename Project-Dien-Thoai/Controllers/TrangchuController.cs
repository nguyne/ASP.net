using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Dien_Thoai.Models;
namespace Project_Dien_Thoai.Controllers
{
    public class TrangchuController : Controller
    {
        private Project_Dien_ThoaiDBContexxt db = new Project_Dien_ThoaiDBContexxt();
        // GET: Trangchu
        public ActionResult Index()
        {
            ViewBag.SoMauTin = db.SanPhams.Count();
            return View();
        }
    }
}