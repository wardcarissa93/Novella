using Microsoft.EntityFrameworkCore;
using Novella.Data;
using Novella.EfModels;
using Novella.ViewModels;
using System;

namespace Novella.Repositories
{
    public class OrderRepo
    {
        private readonly NovellaContext _db;

        public OrderRepo(NovellaContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public int SaveOrderAndGetId(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            try
            {
                _db.Orders.Add(order);
                _db.SaveChanges();
                return order.PkOrderId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex?.ToString() ?? "NULL"}");
                return -1;
            }
        }
    }
}