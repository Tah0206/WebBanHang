using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHang.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult TrangSanPham_LotChuot()
        {
            return View();
        }

        public ActionResult ThanhToan()
        {
            return View();
        }
        public ActionResult GioHang() 
        {
            return View(); 
        }
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }

}