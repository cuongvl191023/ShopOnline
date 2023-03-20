using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Models;
using ShopOnline.Service;

namespace ShopOnline.Controllers
{
    public class ColorController : Controller
    {
        private readonly ColorService _colorService;

        public ColorController(ShopOnlineContext context)
        {
            _colorService = new ColorService(context);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _colorService.GetAllColor());
        }

        public async Task<IActionResult> Details(string id)
        {
            List<Color> lstColor = await _colorService.GetAllColor();
            if (id == null || lstColor.Count == 0)
            {
                return NotFound();
            }
            Color? color = await _colorService.GetColorById(new Guid(id));
            return color == null ? NotFound() : View(color);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ColorCode, ColorName")] Color color)
        {
            return await _colorService.AddColor(color) ? RedirectToAction(nameof(Index)) : View(color);
        }

        public async Task<IActionResult> Edit(string id)
        {
            List<Color> lstColor = await _colorService.GetAllColor();
            if (id == null || lstColor.Count == 0)
            {
                return NotFound();
            }
            Color? color = await _colorService.GetColorById(new Guid(id));
            return color == null ? NotFound() : View(color);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id, Code, ColorCode, ColorName, Status")] Color color)
        {
            Color? colorCheck = await _colorService.GetColorById(new Guid(id));
            if (id != color.Id.ToString())
            {
                return NotFound();
            }
            try
            {
                if (await _colorService.EditColor(color))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return colorCheck == null ? NotFound() : RedirectToAction(nameof(Index));
            }
            return View(color);
        }

        public async Task<IActionResult> Delete(string id)
        {
            List<Color> lstColor = await _colorService.GetAllColor();
            if (id == null || lstColor.Count == 0)
            {
                return NotFound();
            }
            Color? color = await _colorService.GetColorById(new Guid(id));
            return color == null ? NotFound() : View(color);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            List<Color> lstColor = await _colorService.GetAllColor();
            if (lstColor.Count == 0)
            {
                return Problem("__________________________________");
            }
            Color? color = await _colorService.GetColorById(new Guid(id));
            if (color != null)
            {
                _ = await _colorService.DeleteColor(new Guid(id));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}