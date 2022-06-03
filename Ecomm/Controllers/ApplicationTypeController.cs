using Ecomm.Data;
using Ecomm.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomm.Controllers
{
    public class ApplicationTypeController : Controller
    {
        private readonly ApplicationDbContext context;

        public ApplicationTypeController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<ApplicationType> objlist = context.ApplicationType;
            return View(objlist);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationType obj)
        {
            if (ModelState.IsValid)
            {
                context.ApplicationType.Add(obj);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = context.ApplicationType.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationType obj)
        {
            if (ModelState.IsValid)
            {
                context.ApplicationType.Update(obj);
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
            var obj = context.ApplicationType.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            {
                var obj = context.ApplicationType.Find(id);

                if (obj == null)
                {
                    return NotFound();
                }
                context.ApplicationType.Remove(obj);
                context.SaveChanges();
                return RedirectToAction("Index");
            }


        }
    }

}
