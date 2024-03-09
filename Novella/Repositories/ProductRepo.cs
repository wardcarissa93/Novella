using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;

        public ProductRepo(NovellaContext db,
                           UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
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
        public ProductVM GetProductById(string productId)
        {
            var product = _db.Products
                .Where(p => p.PkProductId.ToString() == productId)
                .Select(p => new ProductVM
                {
                    ProductId = p.PkProductId,
                    ProductName = p.ProductName,
                    QuantityAvailable = p.QuantityAvailable,
                    Price = p.Price,
                    ProductDescription = p.ProductDescription,
                    Rating = p.Ratings.Any() ? p.Ratings.Average(r => r.RatingValue) : 0,
                    Reviews = p.Ratings.Where(r => !string.IsNullOrEmpty(r.Review))
                                       .Select(r => new RatingVM
                                       {
                                           RatingId = r.PkRatingId,
                                           ProductId = r.FkProductId,
                                           Review = r.Review,
                                           FirstName = r.FkUser.FirstName,
                                           LastName = r.FkUser.LastName,
                                           DateRated = r.DateRated
                                       })
                                       .ToList()
                }).FirstOrDefault();

            // Order reviews by DateRated after fetching from the database
            if (product != null)
            {
                product.Reviews = product.Reviews
                                    .OrderByDescending(r => r.DateRated)
                                    .ToList();
                return product;
            }

            return null;
        }

        // Update a product
        public bool EditProduct(ProductAdminVM productVM)
        {
            var product = _db.Products.Find(productVM.ProductId);
            if (product != null)
            {
                product.ProductName = productVM.ProductName;
                product.QuantityAvailable = productVM.QuantityInStock;
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public Product AddProduct(Product product)
        {
            try
            {
                _db.Products.Add(product);
                _db.SaveChanges();
                return product;
            }
            catch (Exception)
            {
                // Log the error or handle it as needed
                return null;
            }
        }

        // Delete a product
        public bool DeleteProduct(int productId)
        {
            try
            {
                var product = _db.Products.Find(productId);
                if (product == null) return false;

                _db.Products.Remove(product);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Submit a rating for an individual product
        public bool SubmitRating(string userId, int productId, decimal ratingValue, string review)
        {
            try
            {
                var rating = new Rating
                {
                    FkUserId = userId,
                    FkProductId = productId,
                    RatingValue = ratingValue,
                    Review = review,
                    DateRated = DateTime.Now
                };

                _db.Ratings.Add(rating);
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Add Product Image
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
