using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using WebBanHang.Models.ViewModel;
using PagedList;
using PagedList.Mvc;
using WebBanHang.Models.ViewModel;
using WebBanHang.Models;



namespace _23DH110809_MyStore.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();

        public ActionResult Index(string searchTerm, decimal? MinPrice, decimal? MaxPrice, string SortOrder, int? page)
        {
            var model = new ProductSearchVM();
            var product = db.Products.AsQueryable();

            // Áp dụng các bộ lọc tìm kiếm
            if (!string.IsNullOrEmpty(searchTerm))
            {
                product = product.Where(p =>
                    p.ProductName.Contains(searchTerm) ||
                    p.ProductDescription.Contains(searchTerm) ||
                    p.Category.CategoryName.Contains(searchTerm));
            }
            if (MinPrice.HasValue)
            {
                product = product.Where(p => p.ProductPrice >= MinPrice.Value);
            }
            if (MaxPrice.HasValue)
            {
                product = product.Where(p => p.ProductPrice <= MaxPrice.Value);
            }

            // Sắp xếp sản phẩm theo yêu cầu
            switch (SortOrder)
            {
                case "name_asc":
                    product = product.OrderBy(p => p.ProductName);
                    break;
                case "name_desc":
                    product = product.OrderByDescending(p => p.ProductName);
                    break;
                case "price_asc":
                    product = product.OrderBy(p => p.ProductPrice); // Sắp xếp theo giá tăng dần
                    break;
                case "price_desc":
                    product = product.OrderByDescending(p => p.ProductPrice); // Sắp xếp theo giá giảm dần
                    break;
                default:
                    product = product.OrderBy(p => p.ProductName); // Mặc định sắp xếp theo tên sản phẩm
                    break;
            }

            // Lưu lại các giá trị SortOrder và tìm kiếm trong model
            model.SortOrder = SortOrder;

            // Phân trang
            int pageNumber = page ?? 1;
            int pageSize = 2;
            model.Products = product.ToPagedList(pageNumber, pageSize);

            return View(model);
        }


        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,CategoryID,ProductName,ProductDecription,ProductPrice,ProductImage")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,CategoryID,ProductName,ProductDecription,ProductPrice,ProductImage")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
