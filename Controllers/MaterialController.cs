using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Models;
using ShopOnline.Service;

namespace ShopOnline.Controllers
{
    public class MaterialController : Controller
    {
        private readonly MaterialService _materialService;

        public MaterialController(ShopOnlineContext context)
        {
            _materialService = new MaterialService(context);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _materialService.GetAllMaterial());
        }

        public async Task<IActionResult> Details(string id)
        {
            List<Material> lstMaterial = await _materialService.GetAllMaterial();
            if (id == null || lstMaterial.Count == 0)
            {
                return NotFound();
            }
            Material? material = await _materialService.GetMaterialById(new Guid(id));
            return material == null ? NotFound() : View(material);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaterialName, MaterialReliability, MaterialLevel")] Material material)
        {
            return await _materialService.AddMaterial(material) ? RedirectToAction(nameof(Index)) : View(material);
        }

        public async Task<IActionResult> Edit(string id)
        {
            List<Material> lstMaterial = await _materialService.GetAllMaterial();
            if (id == null || lstMaterial.Count == 0)
            {
                return NotFound();
            }
            Material? material = await _materialService.GetMaterialById(new Guid(id));
            return material == null ? NotFound() : View(material);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id, Code, MaterialName, MaterialReliability, MaterialLevel, Status")] Material material)
        {
            Material? materialCheck = await _materialService.GetMaterialById(new Guid(id));
            if (id != material.Id.ToString())
            {
                return NotFound();
            }
            try
            {
                if (await _materialService.EditMaterial(material))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return materialCheck == null ? NotFound() : RedirectToAction(nameof(Index));
            }
            return View(material);
        }

        public async Task<IActionResult> Delete(string id)
        {
            List<Material> lstMaterial = await _materialService.GetAllMaterial();
            if (id == null || lstMaterial.Count == 0)
            {
                return NotFound();
            }
            Material? material = await _materialService.GetMaterialById(new Guid(id));
            return material == null ? NotFound() : View(material);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            List<Material> lstMaterial = await _materialService.GetAllMaterial();
            if (lstMaterial.Count == 0)
            {
                return Problem("__________________________________");
            }
            Material? material = await _materialService.GetMaterialById(new Guid(id));
            if (material != null)
            {
                _ = await _materialService.DeleteMaterial(new Guid(id));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}