using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Models;
using ShopOnline.Service;

namespace ShopOnline.Controllers
{
    public class ShoeController : Controller
    {
        private readonly ShoeService _shoeService;

        public ShoeController(ShopOnlineContext context)
        {
            _shoeService = new ShoeService(context);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _shoeService.GetAllShoe());
        }

        public async Task<IActionResult> Details(string id)
        {
            List<Shoe> lstShoe = await _shoeService.GetAllShoe();
            if (id == null || lstShoe.Count == 0)
            {
                return NotFound();
            }
            Shoe? shoe = await _shoeService.GetShoeById(new Guid(id));
            return shoe == null ? NotFound() : View(shoe);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShoeName, ShoeDescribe")] Shoe shoe)
        {
            return await _shoeService.AddShoe(shoe) ? RedirectToAction(nameof(Index)) : View(shoe);
        }

        public async Task<IActionResult> Edit(string id)
        {
            List<Shoe> lstShoe = await _shoeService.GetAllShoe();
            if (id == null || lstShoe.Count == 0)
            {
                return NotFound();
            }
            Shoe? shoe = await _shoeService.GetShoeById(new Guid(id));
            return shoe == null ? NotFound() : View(shoe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id, Code, ShoeName, ShoeDescribe, Status")] Shoe shoe)
        {
            Shoe? shoeCheck = await _shoeService.GetShoeById(new Guid(id));
            if (id != shoe.Id.ToString())
            {
                return NotFound();
            }
            try
            {
                if (await _shoeService.EditShoe(shoe))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return shoeCheck == null ? NotFound() : RedirectToAction(nameof(Index));
            }
            return View(shoe);
        }

        public async Task<IActionResult> Delete(string id)
        {
            List<Shoe> lstShoe = await _shoeService.GetAllShoe();
            if (id == null || lstShoe.Count == 0)
            {
                return NotFound();
            }
            Shoe? shoe = await _shoeService.GetShoeById(new Guid(id));
            return shoe == null ? NotFound() : View(shoe);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            List<Shoe> lstShoe = await _shoeService.GetAllShoe();
            if (lstShoe.Count == 0)
            {
                return Problem("__________________________________");
            }
            Shoe? shoe = await _shoeService.GetShoeById(new Guid(id));
            if (shoe != null)
            {
                _ = await _shoeService.DeleteShoe(new Guid(id));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}