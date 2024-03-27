using Microsoft.EntityFrameworkCore;
using Novella.Data;
using Novella.EfModels;
using System;

namespace Novella.Repositories
{
    public class ProductOrderRepo
    {
        private readonly NovellaContext _db;

        public ProductOrderRepo(NovellaContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void SaveProductOrder(ProductOrder productOrder)
        {
            if (productOrder == null)
            {
                throw new ArgumentNullException(nameof(productOrder));
            }

            try
            {
                _db.ProductOrders.Add(productOrder);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Exception caught: {ex?.ToString() ?? "NULL"}");
                 throw;  
            }
        }
    }
}
