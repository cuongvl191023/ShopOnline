using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Models;
using ShopOnline.Service;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ShopOnline.Controllers
{
    public class BrandController : Controller
    {
        private readonly BrandService _brandService;

        [Obsolete]
        public BrandController(ShopOnlineContext context, IHostingEnvironment environment)
        {
            _brandService = new BrandService(context, environment);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _brandService.GetAllBrand());
        }

        public async Task<IActionResult> Details(string id)
        {
            List<Brand> lstBrand = await _brandService.GetAllBrand();
            if (id == null || lstBrand.Count == 0)
            {
                return NotFound();
            }
            Brand? brand = await _brandService.GetBrandById(new Guid(id));
            return brand == null ? NotFound() : View(brand);
        }

        public IActionResult Create()
        {
            return View();
        }

        [Obsolete]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BrandName, BrandNation, BrandWebsite, BrandDescribe")] Brand brand, IFormFile BrandLogo)
        {
            return await _brandService.AddBrand(brand, BrandLogo) ? RedirectToAction(nameof(Index)) : View(brand);
        }

        public async Task<IActionResult> Edit(string id)
        {
            List<Brand> lstBrand = await _brandService.GetAllBrand();
            if (id == null || lstBrand.Count == 0)
            {
                return NotFound();
            }
            Brand? brand = await _brandService.GetBrandById(new Guid(id));
            return brand == null ? NotFound() : View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<IActionResult> Edit(string id, [Bind("Id, Code, BrandName, BrandNation, BrandWebsite, BrandDescribe, Status")] Brand brand, IFormFile BrandLogo)
        {
            Brand? brandCheck = await _brandService.GetBrandById(new Guid(id));
            if (id != brand.Id.ToString())
            {
                return NotFound();
            }
            try
            {
                if (await _brandService.EditBrand(brand, BrandLogo))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return brandCheck == null ? NotFound() : RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        public async Task<IActionResult> Delete(string id)
        {
            List<Brand> lstBrand = await _brandService.GetAllBrand();
            if (id == null || lstBrand.Count == 0)
            {
                return NotFound();
            }
            Brand? brand = await _brandService.GetBrandById(new Guid(id));
            return brand == null ? NotFound() : View(brand);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            List<Brand> lstBrand = await _brandService.GetAllBrand();
            if (lstBrand.Count == 0)
            {
                return Problem("__________________________________");
            }
            Brand? brand = await _brandService.GetBrandById(new Guid(id));
            if (brand != null)
            {
                _ = await _brandService.DeleteBrand(new Guid(id));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}