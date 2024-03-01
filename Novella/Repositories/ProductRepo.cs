using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public List<ProductHomeVM> GetProductsWithReviewsForHome()
        {
            var productsWithReviews = new List<ProductHomeVM>();

            var products = _db.Products.ToList(); // Fetch all products

            foreach (var product in products)
            {
                var productVM = new ProductHomeVM
                {
                  
                    ProductId = product.PkProductId.ToString(),
                    ProductName = product.ProductName,
                    Price = product.Price,
                    Description = product.ProductDescription,
                    Review = _db.Ratings
                                .Where(r => r.FkProductId == product.PkProductId && r.Review != null)
                                .Select(r => r.Review)
                                .ToList()
                };

                productsWithReviews.Add(productVM);
            }

            return productsWithReviews;
        }



        public List<ProductHomeVM> GetProductsForHome()
        {
            var products = _db.Products.Select(p => new ProductHomeVM
            {
                ProductId = p.PkProductId.ToString(),
                ProductName = p.ProductName,
                Description = p.ProductDescription,
                Price = p.Price,
                Rating = p.Ratings.Any() ? p.Ratings.Average(r => r.RatingValue) : 0
            })
            .ToList();
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
                                    Price = p.Price,
                                    Description = p.ProductDescription,
                                    Rating = p.Ratings.Any() ? p.Ratings.Average(r => r.RatingValue) : 0
                                })
                                .ToList();
            return products;
        }

        public List<ProductCategoryVM> GetProductsForChoker()
        {
            int chokerCategoryId = 2; 

            var products = _db.Products
                               .Where(p => p.FkCategoryId == chokerCategoryId)
                               .Select(p => new ProductCategoryVM
                               {
                                   ProductId = p.PkProductId.ToString(),
                                   ProductName = p.ProductName,
                                   Price = p.Price,
                                   Description = p.ProductDescription,
                                   Rating = p.Ratings.Any() ? p.Ratings.Average(r => r.RatingValue) : 0
                               })
                               .ToList();

            return products;
        }

        public List<ProductCategoryVM> GetProductsForChain()
        {
            int chainCategoryId = 3; 

            var products = _db.Products
                               .Where(p => p.FkCategoryId == chainCategoryId)
                               .Select(p => new ProductCategoryVM
                               {
                                   ProductId = p.PkProductId.ToString(),
                                   ProductName = p.ProductName,
                                   Price = p.Price,
                                   Description = p.ProductDescription,
                                   Rating = p.Ratings.Any() ? p.Ratings.Average(r => r.RatingValue) : 0
                               })
                               .ToList();

            return products;
        }
        public List<ProductAdminVM> GetProductsForAdmin()
        {
            var products = _db.Products
                .Select(p => new ProductAdminVM
                {
                    ProductId = p.PkProductId.ToString(),
                    ProductName = p.ProductName,
                    QuantityInStock = p.QuantityAvailable
                }).ToList();

            return products;
        }

        // Fetch a single product by ID
        public Product GetProductById(string productId)
        {
            var product = _db.Products
                .Where(p => p.PkProductId.ToString() == productId)
                .Select(p => p).FirstOrDefault();

            return product;
        }

        public ProductVM GetProductVMById(string productId)
        {
            var productVM = _db.Products
                .Where(p => p.PkProductId.ToString() == productId)
                .Select(p => new ProductVM
                {
                    ProductId = (int)p.PkProductId,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    ProductDescription = p.ProductDescription,
                    QuantityAvailable = p.QuantityAvailable,
                    Rating = p.Ratings.Any() ? p.Ratings.Average(r => r.RatingValue) : 0,
                    CategoryId = p.FkCategoryId,
                    ImageFilenames = p.ImageStores.Select(i => i.FileName).ToList(),
                }).FirstOrDefault();

            return productVM;
        }

        // Update a product
        //public bool EditProduct(ProductAdminVM productVM)
        //{
        //    var product = _db.Products.Include(p => p.ImageStores).FirstOrDefault(p => p.PkProductId.ToString() == productVM.ProductId);
        //    if (product != null)
        //    {
        //        product.ProductName = productVM.ProductName;
        //        product.QuantityAvailable = productVM.QuantityInStock;

        //        // Handle new images
        //        foreach (var filename in productVM.NewImageFilenames)
        //        {
        //            var newImage = new ImageStore { FileName = (string)filename, FkProductId = product.PkProductId };
        //            _db.ImageStores.Add(newImage);
        //        }

        //        // Handle image deletions
        //        foreach (var imageId in productVM.ImageIdsToDelete)
        //        {
        //            if (imageId!=null) 
        //            {
        //                var imageToDelete = _db.ImageStores.FirstOrDefault(i => i.PkImageId == imageId.Value && i.FkProductId == product.PkProductId);
        //                if (imageToDelete != null)
        //                {
        //                    _db.ImageStores.Remove(imageToDelete);
        //                }
        //            }
        //        }

        //        _db.SaveChanges();
        //        return true;
        //    }
        //    return false;
        //}

        public Product AddProduct(Product product)
        {
            try
            {
                _db.Products.Add(product);
                _db.SaveChanges();
                return product;
            }
            catch (Exception e)
            {
                
                return new Product(); ;
            }
        }

        // Delete a product
        public bool DeleteProduct(int productId)
        {
            try
            {
                var imageStores = _db.ImageStores.Where(image => image.FkProductId == productId).ToList();
                if (imageStores.Any())
                {
                    _db.ImageStores.RemoveRange(imageStores); // Correctly remove dependent records
                }
                var product = _db.Products.Find(productId);
                if (product == null) return false;
                _db.Products.Remove(product);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return false;
            }
        }
   
    //Add Product Image
    public bool AddImage(ImageStore imageStore)
        {
            try
            {
                _db.ImageStores.Add(imageStore);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
      
                return false;
            }
        }

    }
}
