using Crudapplication.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Crudapplication.Models;

namespace Crudapplication.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        ProductDAL _productDAL=new ProductDAL();
        public ActionResult Index()
        {
            var productList= _productDAL.GetAllProducts();
            if(productList.Count == 0)
            {
                TempData["InfoMessage"] = "Currently product not avaliable in the database"; //when there is shift of work from one controller to another
            }
            ViewBag.Message = "Welcome to my website!"; //ViewBag property is typically used to pass data from the controller to the view
            return View(productList);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id) //create an object 
        {
            try
            {
                var product = _productDAL.GetProductsByID(id).FirstOrDefault();
                if (product == null)
                {
                    TempData["InfoMessage"] = "Product not available with id " + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch (Exception ex)
            {
                TempData["ErroMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            bool IsInserted = false;
            try
            {
              if(ModelState.IsValid)
                {
                    IsInserted=_productDAL.InsertProduct(product);
                    if(IsInserted)
                    {
                        TempData["SuccessMessage"] = "Product details save successfully..!";
                    }
                    else
                    {
                        TempData["ErroMessage"] = "Product details is already available";
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErroMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var product = _productDAL.GetProductsByID(id).FirstOrDefault();
            if (product == null)
            {
                TempData["ErroMessage"]="Product not available with ID"+id.ToString();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost,ActionName("Edit")]
        public ActionResult UpdateProduct(Product product)
        {
            try
            { 
                if(ModelState.IsValid)
                {
                    bool IsUpdated = _productDAL.UpdateProduct(product);
                    if (IsUpdated)
                    {
                        TempData["SuccessMessage"] = "Product details updated successfully..!";
                    }
                    else
                    {
                        TempData["ErroMessage"] = "Product details is already available";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErroMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            var product=_productDAL.GetProductsByID(id).FirstOrDefault();
            if(product == null)
            {
                TempData["ErroMessage"] = "Currently product not available with ID" + id.ToString();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdated = _productDAL.DeleteProduct(id);
                    if (IsUpdated)
                    {
                        TempData["SuccessMessage"] = "Product details deleted successfully..!";
                    }                  
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErroMessage"] = ex.Message;
                return View();
            }
        }
    }
}
