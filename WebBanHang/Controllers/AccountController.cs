using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebBanHang.Models.ViewModel;
using WebBanHang.Models;
using System.Web.UI.WebControls;

namespace _23DH110809_MyStore.Controllers
{
    public class AccountController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                // kiem tra ten dang nhap
                var existingUser = db.Users.SingleOrDefault(u => u.Username == model.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Username", "Tên đăng nhập này đã tồn tại!");
                    return View(model);
                }

                // neu chua ton tai thi tao ban ghi thong tin tk trong bang user
                var user = new User
                {
                    Username = model.Username,
                    Password = model.Password,
                    UserRole = "C"

                };
                db.Users.Add(user);
                // va tao ban ghi thong tin khach hang trong bang customer
                var customer = new Customer
                {
                    CustomerName = model.CustomerName,
                    CustomerEmail = model.CustomerEmail,
                    CustomerPhone = model.CustomerPhone,
                    CustomerAddress = model.CustomerAddress,
                    Username = model.Username,
                };
                db.Customers.Add(customer);
                // luu thong tin tai khoan vao csdl
                db.SaveChanges();
                return RedirectToAction("Home", "Home");
            }
            return View(model);
        }
        // Get: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.Username == model.UserName
                && u.Password == model.Password
                && u.UserRole == "C");
                if (user != null)
                {
                    //Lưu trạng thái đăng nhập vào session
                    Session["UserName"] = user.Username;
                    Session["UserRole"] = user.UserRole;


                }
                if (model.RememberMe)
                {
                    FormsAuthentication.SetAuthCookie(user.Username, true);
                }

                // Chuyển hướng đến trang chủ
                return RedirectToAction("Home", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
            }
            return View(model);
        }

        //GET: Account/Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
