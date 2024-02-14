using Novella.Data;
using Novella.EfModels;
using Novella.ViewModels;
using System.Linq;

namespace Novella.Repositories
{
    public class ProductRepo
    {
        private readonly NovellaContext _db;

        public ProductRepo(NovellaContext db)
        {
            _db = db;
        }

        public List<ProductAdminVM> GetProductsForAdmin()
        {
            var products = _db.Products
                .Select(p => new ProductAdminVM
                {
                    ProductId = p.PkProductId.ToString(), // Assuming ProductId in ProductAdminVM is a string
                    ProductName = p.ProductName,
                    QuantityInStock = p.QuantityAvailable // Changed to QuantityAvailable
                }).ToList();

            return products;
        }

        public ProductAdminVM GetProductById(int productId) // Changed parameter type to int
        {
            var product = _db.Products
                .Where(p => p.PkProductId == productId)
                .Select(p => new ProductAdminVM
                {
                    ProductId = p.PkProductId.ToString(), // Assuming ProductId in ProductAdminVM is a string
                    ProductName = p.ProductName,
                    QuantityInStock = p.QuantityAvailable // Changed to QuantityAvailable
                }).FirstOrDefault();

            return product;
        }

        public bool EditProduct(ProductAdminVM productVM)
        {
            if (int.TryParse(productVM.ProductId, out int productId))
            {
                var product = _db.Products.Find(productId);
                if (product != null)
                {
                    product.ProductName = productVM.ProductName;
                    product.QuantityAvailable = productVM.QuantityInStock; // Changed to QuantityAvailable
                    // Update other fields as necessary

                    _db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool DeleteProduct(int productId)  
        {
            var product = _db.Products.Find(productId);
            if (product != null)
            {
                _db.Products.Remove(product);
                _db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
