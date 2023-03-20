using Microsoft.EntityFrameworkCore;
using ShopOnline.Models;

namespace ShopOnline.Service
{
    public class ColorService
    {
        private readonly ShopOnlineContext _context;

        public ColorService(ShopOnlineContext context)
        {
            _context = context;
        }

        public async Task<List<Color>> GetAllColor()
        {
            return await _context.Set<Color>().ToListAsync();
        }

        public async Task<Color?> GetColorById(Guid Id)
        {
            return await _context.Set<Color>().FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<bool> AddColor(Color color)
        {
            if (Validate(color))
            {
                try
                {
                    color.Code = "Color1";
                    if (_context.Set<Color>().ToList().Count > 0)
                    {
                        color.Code = "Color" + (_context.Set<Color>().Max(c => Convert.ToInt32(c.Code.Substring(5))) + 1);
                    }
                    _ = _context.Add(color);
                    _ = await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> EditColor(Color color)
        {
            if (Validate(color))
            {
                try
                {
                    _context.ChangeTracker.Clear();
                    _ = _context.Update(color);
                    _ = await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<int> DeleteColor(Guid Id)
        {
            Color? color = await _context.Set<Color>().FindAsync(Id);
            if (color != null)
            {
                _ = _context.Set<Color>().Remove(color);
            }
            return await _context.SaveChangesAsync();
        }

        public bool Validate(Color color)
        {
            return true;
        }
    }
}