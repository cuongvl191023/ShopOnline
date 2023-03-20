using Microsoft.EntityFrameworkCore;
using ShopOnline.Models;

namespace ShopOnline.Service
{
    public class ShoeService
    {
        private readonly ShopOnlineContext _context;

        public ShoeService(ShopOnlineContext context)
        {
            _context = context;
        }

        public async Task<List<Shoe>> GetAllShoe()
        {
            return await _context.Set<Shoe>().ToListAsync();
        }

        public async Task<Shoe?> GetShoeById(Guid Id)
        {
            return await _context.Set<Shoe>().FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<bool> AddShoe(Shoe shoe)
        {
            if (Validate(shoe))
            {
                try
                {
                    shoe.Code = "Shoe1";
                    if (_context.Set<Shoe>().ToList().Count > 0)
                    {
                        shoe.Code = "Shoe" + (_context.Set<Shoe>().Max(c => Convert.ToInt32(c.Code.Substring(4))) + 1);
                    }
                    _ = _context.Add(shoe);
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

        public async Task<bool> EditShoe(Shoe shoe)
        {
            if (Validate(shoe))
            {
                try
                {
                    _context.ChangeTracker.Clear();
                    _ = _context.Update(shoe);
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

        public async Task<int> DeleteShoe(Guid Id)
        {
            Shoe? shoe = await _context.Set<Shoe>().FindAsync(Id);
            if (shoe != null)
            {
                _ = _context.Set<Shoe>().Remove(shoe);
            }
            return await _context.SaveChangesAsync();
        }

        public bool Validate(Shoe shoe)
        {
            return true;
        }
    }
}