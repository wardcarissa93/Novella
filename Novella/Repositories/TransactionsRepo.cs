using Novella.EfModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

public class TransactionRepo
{
    private readonly NovellaContext _context;

    public TransactionRepo(NovellaContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders
            .Include(o => o.FkShippingAddress)
            .Include(o => o.FkBillingAddress)
            .Include(o => o.FkUser) // Include UserAccount data
            .Include(o => o.FkOrderStatus) // Include OrderStatus data

            .Include(o => o.ProductOrders)
            .ToListAsync();
    }

    // Implement additional CRUD operations as needed...
}
