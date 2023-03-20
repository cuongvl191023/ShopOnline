using Microsoft.EntityFrameworkCore;
using ShopOnline.Models;

namespace ShopOnline.Service
{
    public class ProductService
    {
        private readonly ShopOnlineContext _context;

        public ProductService(ShopOnlineContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProduct()
        {
            return await _context.Set<Product>().Include(p => p.IdColorNavigation).Include(p => p.IdBrandNavigation).Include(p => p.IdMaterialNavigation).Include(p => p.IdShoeNavigation).Include(p => p.IdSizeNavigation).ToListAsync();
        }

        public async Task<Product?> GetProductById(Guid Id)
        {
            return await _context.Set<Product>().Include(p => p.IdColorNavigation).Include(p => p.IdBrandNavigation).Include(p => p.IdMaterialNavigation).Include(p => p.IdShoeNavigation).Include(p => p.IdSizeNavigation).FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<bool> AddProduct(Product product)
        {
            if (Validate(product))
            {
                try
                {
                    product.Code = "Product1";
                    if (_context.Set<Product>().ToList().Count > 0)
                    {
                        product.Code = "Product" + (_context.Set<Product>().Max(c => Convert.ToInt32(c.Code.Substring(7))) + 1);
                    }
                    _ = _context.Add(product);
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

        public async Task<bool> EditProduct(Product product)
        {
            if (Validate(product))
            {
                try
                {
                    _context.ChangeTracker.Clear();
                    _ = _context.Update(product);
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

        public async Task<int> DeleteProduct(Guid Id)
        {
            Product? product = await _context.Set<Product>().FindAsync(Id);
            if (product != null)
            {
                _ = _context.Set<Product>().Remove(product);
            }
            return await _context.SaveChangesAsync();
        }

        public bool Validate(Product product)
        {
            return true;
        }
    }
}