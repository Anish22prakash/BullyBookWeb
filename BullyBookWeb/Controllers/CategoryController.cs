using BullyBookWeb.Data;
using BullyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BullyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        public readonly ApplicationDbContext _DbContext;

        public CategoryController(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public IActionResult Index()
        {
            //var AllCategories = _DbContext.Categories.ToList();

            IEnumerable<Category> categories = _DbContext.Categories;

            return View(categories);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if(category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The dispalyorder cannot same as category name");
            }
            if (ModelState.IsValid)
            {
                _DbContext.Categories.Add(category);
                _DbContext.SaveChanges();
                TempData["success"] = "Category is successfully created";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _DbContext.Categories.SingleOrDefault(c => c.Id == id);
            if (categoryFromDb == null)
            {
                return BadRequest();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {

            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The dispalyorder cannot same as category name");
            }
            if (ModelState.IsValid)
            {
                _DbContext.Categories.Update(category);
                _DbContext.SaveChanges();
                TempData["success"] = "Category is successfully updated";
                return RedirectToAction("Index");
            }
            return View(category);
        }


        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _DbContext.Categories.SingleOrDefault(c => c.Id == id);
            if (categoryFromDb == null)
            {
                return BadRequest();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {

            var category = _DbContext.Categories.Find(id);
            if (category != null)
            {
                _DbContext.Categories.Remove(category);
                _DbContext.SaveChanges();
                TempData["success"] = "Category is successfully deleted";
                return RedirectToAction("Delete");
            }
            TempData["error"] = "Category not able delete";
            return RedirectToAction("Index");
        }

    }
}
