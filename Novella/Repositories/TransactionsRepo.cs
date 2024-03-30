using Novella.EfModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Novella.ViewModels;

public class TransactionRepo
{
    private readonly NovellaContext _context;

    public TransactionRepo(NovellaContext context)
    {
        _context = context;
    }

    public async Task<List<OrderViewModel>> GetAllOrdersAsync()
    {
        var orders = await _context.Orders
            .Include(o => o.FkShippingAddress)
            .Include(o => o.FkBillingAddress)
            .Include(o => o.FkUser)
            .Include(o => o.FkOrderStatus)
            .Include(o => o.ProductOrders)
                .ThenInclude(po => po.FkProduct) // Assuming ProductOrders has a navigation property to Product
            .Select(o => new OrderViewModel
            {
                OrderId = o.PkOrderId,
                Email = o.FkUserId, // Adjust based on your User model
                Name = o.FkUser.FirstName + " " + o.FkUser.LastName, // Adjust based on your User model
                Role = o.FkUser.Role, // Adjust based on your User model
                SKU = o.PaypalTransactionId,
                Status = o.FkOrderStatus.StatusName,
                DateOrdered = o.DateOrdered.ToString("yyyy-MM-dd"),
                ShippingAddress = o.FkShippingAddress.AddressLineOne + ", " + o.FkShippingAddress.City,
                BillingAddress = o.FkBillingAddress.AddressLineOne + ", " + o.FkBillingAddress.City,
                TotalPrice = o.ProductOrders.Sum(po => po.QuantityInOrder * po.FkProduct.Price) // Ensure this calculation is valid
            })
            .ToListAsync();

        return orders;
    }



    public async Task<Order> GetOrderByIdAsync(int orderId)
    {
        return await _context.Orders
            .Include(o => o.FkShippingAddress)
            .Include(o => o.FkBillingAddress)
            .Include(o => o.FkUser)
            .Include(o => o.FkOrderStatus)
            .Include(o => o.ProductOrders)
            .FirstOrDefaultAsync(o => o.PkOrderId == orderId);
    }

    public async Task<bool> UpdateOrderAsync(Order order)
    {
        var existingOrder = await _context.Orders.FindAsync(order.PkOrderId);
        if (existingOrder == null) return false;

        // Update properties
        _context.Entry(existingOrder).CurrentValues.SetValues(order);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task DeleteOrderAsync(int orderId)
    {
        var order = await _context.Orders
                                  .Include(o => o.ProductOrders) // Make sure to include ProductOrders
                                  .FirstOrDefaultAsync(o => o.PkOrderId == orderId);

        if (order != null)
        {
            // Remove related ProductOrder records
            foreach (var productOrder in order.ProductOrders.ToList())
            {
                _context.ProductOrders.Remove(productOrder);
            }

            // Now, remove the Order
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<OrderViewModel>> GetAllOrdersWithTotalPriceAsync()
    {
        var orders = await _context.Orders
            .Include(o => o.FkShippingAddress)
            .Include(o => o.FkBillingAddress)
            .Include(o => o.FkUser)
            .Include(o => o.FkOrderStatus)
            .Include(o => o.ProductOrders)
                .ThenInclude(po => po.FkProduct) // Assuming a navigation property to Product
            .Select(o => new OrderViewModel
            {
                OrderId = o.PkOrderId,
                Email = o.FkUserId,
                DateOrdered = o.DateOrdered.ToString(),
                TotalPrice = o.ProductOrders.Sum(po => po.QuantityInOrder * po.FkProduct.Price), // Correctly access Price from Product
                                                                                               // Populate other necessary fields here
            })
            .ToListAsync();

        return orders;
    }



    // Implement additional CRUD operations as needed...
}
