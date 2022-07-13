using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.Model;
using MyClass.DAO;
using demomvc.Library;

namespace demomvc.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private MyDBContext db = new MyDBContext();

        ProductDAO productDAO = new ProductDAO();
        // GET: Admin/Product
        public ActionResult Index()
        {
            return View(productDAO.getlist("Index"));
        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            ViewBag.ListCatID = new SelectList(productDAO.getlist("Index"), "Id", "Name", 0);
            ViewBag.ListSuplierId = new SelectList(productDAO.getlist("Index"), "SuplierId", "Name", 0);
            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Slug = XString.str_Slug(product.Name);
                if (product.CatId == null)
                {
                    product.CatId = 0;
                }
                if (product.SuplierId == null)
                {
                    product.SuplierId = 1;
                }
                else
                {
                    product.SuplierId += 1;
                }
                product.CreateBy = Convert.ToInt32(Session["UserId"].ToString());
                product.CreateAt = DateTime.Now;
                productDAO.Insert(product);
                TempData["message"] = new XMessage("success", "Thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListCatID = new SelectList(productDAO.getlist("Index"), "Id", "Name", 0);
            ViewBag.ListSuplierId = new SelectList(productDAO.getlist("Index"), "SuplierId", "Name", 0);
            return View(product);
        }

        // GET: Admin/Product/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ListCatID = new SelectList(productDAO.getlist("Index"), "Id", "Name", 0);
            ViewBag.ListSuplierId = new SelectList(productDAO.getlist("Index"), "SuplierId", "Name", 0);
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

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CatId,SuplierId,Name,Slug,Detail,Number,PriceBuy,PriceSale,Img,MetaDesc,MetaKey,CreateBy,CreateAt,UpdateBy,UpdateAt,Status")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Slug = XString.str_Slug(product.Name);
                if (product.CatId == null)
                {
                    product.CatId = 0;
                }
                if (product.SuplierId == null)
                {
                    product.SuplierId = 1;
                }
                else
                {
                    product.SuplierId += 1;
                }
                product.CreateBy = Convert.ToInt32(Session["UserId"].ToString());
                product.CreateAt = DateTime.Now;
                productDAO.Update(product);
                TempData["message"] = new XMessage("success", "Thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListCatID = new SelectList(productDAO.getlist("Index"), "Id", "Name", 0);
            ViewBag.ListSuplierId = new SelectList(productDAO.getlist("Index"), "SuplierId", "Name", 0);
            return View(product);
        }

        // GET: Admin/Product/Delete/5
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

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = productDAO.getRow(id);
            productDAO.Delete(product);
            return RedirectToAction("Trash", "Product");
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            product.Status = (product.Status == 1) ? 2 : 1;
            product.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            product.UpdateAt = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success", "Thành công");
            return RedirectToAction("Index", "Product");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            product.Status = 0;
            product.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            product.UpdateAt = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success", " xoa Thành công");
            return RedirectToAction("Index", "Product");
        }
        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại không tồn tại");
                return RedirectToAction("Trash", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Product");
            }
            product.Status = 2;
            product.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            product.UpdateAt = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success", " xoa Thành công");
            return RedirectToAction("Trash", "Product");
        }
        public ActionResult Trash()
        {
            return View(productDAO.getlist("Trash"));
        }
    }
}
