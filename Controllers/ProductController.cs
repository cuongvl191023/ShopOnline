using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Models;
using ShopOnline.Service;

namespace ShopOnline.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ShopOnlineContext context)
        {
            _productService = new ProductService(context);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetAllProduct());
        }

        public async Task<IActionResult> Details(string id)
        {
            List<Product> lstProduct = await _productService.GetAllProduct();
            if (id == null || lstProduct.Count == 0)
            {
                return NotFound();
            }
            Product? product = await _productService.GetProductById(new Guid(id));
            return product == null ? NotFound() : View(product);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdBrand, IdColor, IdMaterial, IdShoe, IdSize, Price, Describe")] Product product)
        {
            return await _productService.AddProduct(product) ? RedirectToAction(nameof(Index)) : View(product);
        }

        public async Task<IActionResult> Edit(string id)
        {
            List<Product> lstProduct = await _productService.GetAllProduct();
            if (id == null || lstProduct.Count == 0)
            {
                return NotFound();
            }
            Product? product = await _productService.GetProductById(new Guid(id));
            return product == null ? NotFound() : View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id, Code, IdBrand, IdColor, IdMaterial, IdShoe, IdSize, Price, Describe, Status")] Product product)
        {
            Product? productCheck = await _productService.GetProductById(new Guid(id));
            if (id != product.Id.ToString())
            {
                return NotFound();
            }
            try
            {
                if (await _productService.EditProduct(product))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return productCheck == null ? NotFound() : RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public async Task<IActionResult> Delete(string id)
        {
            List<Product> lstProduct = await _productService.GetAllProduct();
            if (id == null || lstProduct.Count == 0)
            {
                return NotFound();
            }
            Product? product = await _productService.GetProductById(new Guid(id));
            return product == null ? NotFound() : View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            List<Product> lstProduct = await _productService.GetAllProduct();
            if (lstProduct.Count == 0)
            {
                return Problem("__________________________________");
            }
            Product? product = await _productService.GetProductById(new Guid(id));
            if (product != null)
            {
                _ = await _productService.DeleteProduct(new Guid(id));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}