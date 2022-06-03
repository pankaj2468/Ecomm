using Ecomm.Data;
using Ecomm.Models;
using Ecomm.Models.ProductViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomm.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        


        public ProductController(ApplicationDbContext dbcontext, IWebHostEnvironment webHostEnvironment)
        {
            context = dbcontext;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Index()
        {

            IEnumerable<Product> objlist = context.Product;
            foreach(var obj in objlist)
            {
                obj.Category = context.Category.FirstOrDefault(u => u.Id == obj.CategoryId);
            };
            return View(objlist);
            //var cat = context.Categories.ToList();
            //   return View(cat);


        }
        public IActionResult Upsert(int? id)
        {
            //IEnumerable<SelectListItem> CategoryDropDown = context.Category.Select(i => new SelectListItem
            //{
            //    Text = i.Name,
            //    Value = i.Id.ToString()
            //});

            //ViewData["CategoryDropDown"] = CategoryDropDown;

            //Product product = new Product();
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategorySelectList = context.Category.Select(i => new SelectListItem
                {
                    Text = i.Name,      
                    Value = i.Id.ToString()
                })
            };

            if (id == null)
            { 
             return View(productVM);
            }
            else
            {
                productVM.Product = context.Product.Find(id);
                
                if (productVM.Product == null)
                {
                    return NotFound();
                }
                return View(productVM);
            }
        }

        //public async Task <IActionResult> Get()
        //{
        //    var chd = context.Category.ToList();
        //    return View(chd);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;
                if (productVM.Product.Id == 0)
                {
                    string upload = webRootPath + WC.ImagePath;
                    string filename = Guid.NewGuid().ToString();
                    //string ImagePath = Guid.NewGuid().ToString();//
                    string extension = Path.GetExtension(files[0].FileName);
                    using (var filesStream = new FileStream(Path.Combine(upload, filename + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filesStream);
                        
                    }
                    productVM.Product.Image = filename + extension;
                  
                    context.Product.Add(productVM.Product);
                }
                else
                {

                }
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        
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
