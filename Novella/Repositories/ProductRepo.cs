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


        public List<ProductHomeVM> GetProductsForHome()
        {
            var products = _db.Products.Select(p => new ProductHomeVM
            {
                ProductId = p.PkProductId.ToString(),
                ProductName = p.ProductName,
                Description = p.ProductDescription,
            }).ToList();
            return products;
        }

        public List<ProductCategoryVM> GetProductsForPendant()
        {
            int pendantCategoryId = 1; 

            var products = _db.Products
                               .Where(p => p.FkCategoryId == pendantCategoryId)
                               .Select(p => new ProductCategoryVM
                               {
                                   ProductId = p.PkProductId.ToString(),
                                   ProductName = p.ProductName,
                                   Price = p.Price
                               })
                               .ToList();

            return products;
        }

        public List<ProductCategoryVM> GetProductsForChoker()
        {
            int pendantCategoryId = 2; 

            var products = _db.Products
                               .Where(p => p.FkCategoryId == pendantCategoryId)
                               .Select(p => new ProductCategoryVM
                               {
                                   ProductId = p.PkProductId.ToString(),
                                   ProductName = p.ProductName,
                                   Price = p.Price
                               })
                               .ToList();

            return products;
        }

        public List<ProductCategoryVM> GetProductsForChain()
        {
            int pendantCategoryId = 3; 

            var products = _db.Products
                               .Where(p => p.FkCategoryId == pendantCategoryId)
                               .Select(p => new ProductCategoryVM
                               {
                                   ProductId = p.PkProductId.ToString(),
                                   ProductName = p.ProductName,
                                   Price = p.Price
                               })
                               .ToList();

            return products;
        }
        public List<ProductAdminVM> GetProductsForAdmin()
        {
            var products = _db.Products
                .Select(p => new ProductAdminVM
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    QuantityInStock = p.QuantityInStock
                }).ToList();

            return products;
        }

        // Fetch a single product by ID
        public ProductAdminVM GetProductById(string productId)
        {
            var product = _db.Products
                .Where(p => p.ProductId == productId)
                .Select(p => new ProductAdminVM
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    QuantityInStock = p.QuantityInStock
                }).FirstOrDefault();

            return product;
        }

        // Update a product
        public bool EditProduct(ProductAdminVM productVM)
        {
            var product = _db.Products.Find(productVM.ProductId);
            if (product != null)
            {
                product.ProductName = productVM.ProductName;
                product.QuantityInStock = productVM.QuantityInStock;
                // Update other fields as necessary

                _db.SaveChanges();
                return true;
            }
            return false;
        }

        // Delete a product
        public bool DeleteProduct(string productId)
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
