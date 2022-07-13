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
    public class CategoryController : Controller
    {
        CategoryDAO categoryDAO = new CategoryDAO();

        // GET: Admin/Category
        public ActionResult Index()
        {
            return View(categoryDAO.getlist("Index"));
        }

        // GET: Admin/Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryDAO.getRow(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            ViewBag.ListCat = new SelectList(categoryDAO.getlist("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(categoryDAO.getlist("Index"), "Oders", "Name", 0);
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = XString.str_Slug(category.Name);
                if (category.ParentId == null)
                {
                    category.ParentId = 0;
                }
                if (category.Oders == null)
                {
                    category.Oders = 1;
                }
                else
                {
                    category.Oders += 1;    
                }
                category.CreateBy=Convert.ToInt32(Session["UserId"].ToString());
                category.CreateAt = DateTime.Now;
                categoryDAO.Insert(category);
                TempData["message"] = new XMessage("success", "Thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(categoryDAO.getlist("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(categoryDAO.getlist("Index"), "Oders", "Name", 0);
            return View(category);
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ListCat = new SelectList(categoryDAO.getlist("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(categoryDAO.getlist("Index"), "Oders", "Name", 0);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryDAO.getRow(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = XString.str_Slug(category.Name);
                if (category.ParentId == null)
                {
                    category.ParentId = 0;
                }
                if (category.Oders == null)
                {
                    category.Oders = 1;
                }
                else
                {
                    category.Oders += 1;
                }
                category.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
                category.UpdateAt = DateTime.Now;
                categoryDAO.Update(category);
                TempData["message"] = new XMessage("success", "Thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(categoryDAO.getlist("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(categoryDAO.getlist("Index"), "Oders", "Name", 0);
            return View(category);
        }

        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryDAO.getRow(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = categoryDAO.getRow(id);
            categoryDAO.Delete(category);
            return RedirectToAction("Trash","Category");
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại không tồn tại");
                return RedirectToAction("Index","Category");
            }
            Category category = categoryDAO.getRow(id);
            if (category == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Category");
            }
            category.Status = (category.Status == 1) ? 2 : 1;
            category.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            category.UpdateAt = DateTime.Now;
            categoryDAO.Update(category);
            TempData["message"] = new XMessage("success", "Thành công");
            return RedirectToAction("Index", "Category");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại không tồn tại");
                return RedirectToAction("Index", "Category");
            }
            Category category = categoryDAO.getRow(id);
            if (category == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Category");
            }
            category.Status = 0;
            category.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            category.UpdateAt = DateTime.Now;
            categoryDAO.Update(category);
            TempData["message"] = new XMessage("success", " xoa Thành công");
            return RedirectToAction("Index", "Category");
        }
        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại không tồn tại");
                return RedirectToAction("Trash", "Category");
            }
            Category category = categoryDAO.getRow(id);
            if (category == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Category");
            }
            category.Status = 2;
            category.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            category.UpdateAt = DateTime.Now;
            categoryDAO.Update(category);
            TempData["message"] = new XMessage("success", " xoa Thành công");
            return RedirectToAction("Trash", "Category");
        }
        public ActionResult Trash()
        {
            return View(categoryDAO.getlist("Trash"));
        }
    }
}
