using Microsoft.EntityFrameworkCore;
using ShopOnline.Models;

namespace ShopOnline.Service
{
    public class SizeService
    {
        private readonly ShopOnlineContext _context;

        public SizeService(ShopOnlineContext context)
        {
            _context = context;
        }

        public async Task<List<Size>> GetAllSize()
        {
            return await _context.Set<Size>().ToListAsync();
        }

        public async Task<Size?> GetSizeById(Guid Id)
        {
            return await _context.Set<Size>().FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<bool> AddSize(Size size)
        {
            if (Validate(size))
            {
                try
                {
                    size.Code = "Size1";
                    if (_context.Set<Size>().ToList().Count > 0)
                    {
                        size.Code = "Size" + (_context.Set<Size>().Max(c => Convert.ToInt32(c.Code.Substring(4))) + 1);
                    }
                    _ = _context.Add(size);
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

        public async Task<bool> EditSize(Size size)
        {
            if (Validate(size))
            {
                try
                {
                    _context.ChangeTracker.Clear();
                    _ = _context.Update(size);
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

        public async Task<int> DeleteSize(Guid Id)
        {
            Size? size = await _context.Set<Size>().FindAsync(Id);
            if (size != null)
            {
                _ = _context.Set<Size>().Remove(size);
            }
            return await _context.SaveChangesAsync();
        }

        public bool Validate(Size size)
        {
            return true;
        }
    }
}