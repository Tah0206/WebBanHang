using WebBanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        private MyStoreEntities db = new MyStoreEntities(); // DbContext của bạn

        // Trang chủ
        public ActionResult Index()
        {
            // Lấy danh sách Categories và Products
            var categories = db.Categories.ToList();
            var products = db.Products.ToList();

            // Đưa dữ liệu vào ViewBag
            ViewBag.Categories = categories;
            ViewBag.Products = products;

            return View();
        }
        public ActionResult About()
        {
            // Có thể thêm ViewBag để truyền dữ liệu vào View nếu cần
            ViewBag.Message = "Chào mừng bạn đến với trang giới thiệu của chúng tôi.";
            return View();
        }
        public ActionResult Orders()
        {

            return View();
        }
    }
}