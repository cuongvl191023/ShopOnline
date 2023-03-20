using Microsoft.EntityFrameworkCore;
using ShopOnline.Models;

namespace ShopOnline.Service
{
    public class MaterialService
    {
        private readonly ShopOnlineContext _context;

        public MaterialService(ShopOnlineContext context)
        {
            _context = context;
        }

        public async Task<List<Material>> GetAllMaterial()
        {
            return await _context.Set<Material>().ToListAsync();
        }

        public async Task<Material?> GetMaterialById(Guid Id)
        {
            return await _context.Set<Material>().FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<bool> AddMaterial(Material material)
        {
            if (Validate(material))
            {
                try
                {
                    material.Code = "Material1";
                    if (_context.Set<Material>().ToList().Count > 0)
                    {
                        material.Code = "Material" + (_context.Set<Material>().Max(c => Convert.ToInt32(c.Code.Substring(8))) + 1);
                    }
                    _ = _context.Add(material);
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

        public async Task<bool> EditMaterial(Material material)
        {
            if (Validate(material))
            {
                try
                {
                    _context.ChangeTracker.Clear();
                    _ = _context.Update(material);
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

        public async Task<int> DeleteMaterial(Guid Id)
        {
            Material? material = await _context.Set<Material>().FindAsync(Id);
            if (material != null)
            {
                _ = _context.Set<Material>().Remove(material);
            }
            return await _context.SaveChangesAsync();
        }

        public bool Validate(Material material)
        {
            return true;
        }
    }
}