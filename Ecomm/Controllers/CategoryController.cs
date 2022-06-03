using Ecomm.Data;
using Ecomm.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomm.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext context;


        public CategoryController(ApplicationDbContext dbcontext)
        {
            context = dbcontext;
        }

         public IActionResult Index()
        {
            IEnumerable<Category> objlist = context.Category;
            return View(objlist);
        }

        //public IActionResult Index()
        //{

        //    IEnumerable<Category> objlist = context.Category;
        //    return View(objlist);
        //    //var cat = context.Categories.ToList();
        //    //   return View(cat);

        //}

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                context.Category.Add(obj);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(obj);
        }



        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Category obj)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        context.Category.Add(obj);
        //        context.SaveChanges();

        //        return RedirectToAction("Index");
        //    }
        //    return View(obj);
        //}

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = context.Category.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                context.Category.Update(obj);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = context.Category.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            {
                var obj = context.Category.Find(id);

                if (obj == null)
                {

                    return NotFound();
                }    
                context.Category.Remove(obj);
                context.SaveChanges();
                return RedirectToAction("Index");
            }


        }


      
    }
}
