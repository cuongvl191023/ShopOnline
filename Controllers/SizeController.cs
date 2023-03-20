using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Models;
using ShopOnline.Service;

namespace ShopOnline.Controllers
{
    public class SizeController : Controller
    {
        private readonly SizeService _sizeService;

        public SizeController(ShopOnlineContext context)
        {
            _sizeService = new SizeService(context);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _sizeService.GetAllSize());
        }

        public async Task<IActionResult> Details(string id)
        {
            List<Size> lstSize = await _sizeService.GetAllSize();
            if (id == null || lstSize.Count == 0)
            {
                return NotFound();
            }
            Size? size = await _sizeService.GetSizeById(new Guid(id));
            return size == null ? NotFound() : View(size);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SizeName, Size1")] Size size)
        {
            return await _sizeService.AddSize(size) ? RedirectToAction(nameof(Index)) : View(size);
        }

        public async Task<IActionResult> Edit(string id)
        {
            List<Size> lstSize = await _sizeService.GetAllSize();
            if (id == null || lstSize.Count == 0)
            {
                return NotFound();
            }
            Size? size = await _sizeService.GetSizeById(new Guid(id));
            return size == null ? NotFound() : View(size);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id, Code, SizeName, Size1, Status")] Size size)
        {
            Size? sizeCheck = await _sizeService.GetSizeById(new Guid(id));
            if (id != size.Id.ToString())
            {
                return NotFound();
            }
            try
            {
                if (await _sizeService.EditSize(size))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return sizeCheck == null ? NotFound() : RedirectToAction(nameof(Index));
            }
            return View(size);
        }

        public async Task<IActionResult> Delete(string id)
        {
            List<Size> lstSize = await _sizeService.GetAllSize();
            if (id == null || lstSize.Count == 0)
            {
                return NotFound();
            }
            Size? size = await _sizeService.GetSizeById(new Guid(id));
            return size == null ? NotFound() : View(size);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            List<Size> lstSize = await _sizeService.GetAllSize();
            if (lstSize.Count == 0)
            {
                return Problem("__________________________________");
            }
            Size? size = await _sizeService.GetSizeById(new Guid(id));
            if (size != null)
            {
                _ = await _sizeService.DeleteSize(new Guid(id));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}