using Microsoft.EntityFrameworkCore;
using ShopOnline.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ShopOnline.Service
{
    public class BrandService
    {
        [Obsolete]
        private readonly IHostingEnvironment _environment;
        private readonly ShopOnlineContext _context;

        [Obsolete]
        public BrandService(ShopOnlineContext context, IHostingEnvironment environment)
        {
            _environment = environment;
            _context = context;
        }

        public async Task<List<Brand>> GetAllBrand()
        {
            return await _context.Set<Brand>().ToListAsync();
        }

        public async Task<Brand?> GetBrandById(Guid Id)
        {
            return await _context.Set<Brand>().FirstOrDefaultAsync(p => p.Id == Id);
        }

        [Obsolete]
        public async Task<bool> AddBrand(Brand brand, IFormFile brandLogo)
        {
            if (Validate(brand))
            {
                try
                {
                    brand.Code = "Brand1";
                    if (_context.Set<Brand>().ToList().Count > 0)
                    {
                        brand.Code = "Brand" + (_context.Set<Brand>().Max(c => Convert.ToInt32(c.Code.Substring(5))) + 1);
                    }
                    brand.BrandLogo = brand.Id + ".png";
                    AddLogo(brandLogo, PathLogo(brand.Id));
                    _ = _context.Add(brand);
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

        [Obsolete]
        public async Task<bool> EditBrand(Brand brand, IFormFile brandLogo)
        {
            if (Validate(brand))
            {
                try
                {
                    brand.BrandLogo = brand.Id + ".png";
                    AddLogo(brandLogo, PathLogo(brand.Id));
                    _context.ChangeTracker.Clear();
                    _ = _context.Update(brand);
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

        [Obsolete]
        public async Task<int> DeleteBrand(Guid Id)
        {
            Brand? brand = await _context.Set<Brand>().FindAsync(Id);
            if (brand != null)
            {
                _ = _context.Set<Brand>().Remove(brand);
                File.Delete(PathLogo(brand.Id));
            }
            return await _context.SaveChangesAsync();
        }

        public bool Validate(Brand brand)
        {
            return true;
        }

        [Obsolete]
        public string PathLogo(Guid Id)
        {
            return Path.Combine(_environment.ContentRootPath, "wwwroot", "BrandLogo", Id.ToString() + ".png");
        }

        public async void AddLogo(IFormFile brandLogo, string path)
        {
            if (brandLogo != null)
            {
                using FileStream fileStream = new(path, FileMode.OpenOrCreate);
                await brandLogo.CopyToAsync(fileStream);
                fileStream.Close();
            }
        }
    }
}