using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TestWithBD.Models;

namespace TestWithBD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var categories = _context.Categories.ToList();
            ViewBag.Categories = categories;
            var JoinTable = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();

            return View(JoinTable);
        }

        public IActionResult AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteModal(Product product)
        {
            var delProduct = _context.Products.FirstOrDefault(p => p.Id == product.Id);
            if (delProduct != null)
            {
                _context.Products.Remove(delProduct);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult DeleteModalCategory(Category category)
        {
            var delCategory = _context.Categories.FirstOrDefault(z => z.Id == category.Id);
            if (delCategory != null)
            {
                _context.Categories.Remove(delCategory);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult EditProduct(Product product)
        {
            var editProduct = _context.Products.FirstOrDefault(r => r.Id == product.Id);
            if (editProduct != null)
            {
                editProduct.Name = product.Name;
                editProduct.CategoryId = product.CategoryId;
            }
            _context.Products.Update(editProduct);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}