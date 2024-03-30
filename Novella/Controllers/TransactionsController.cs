using Microsoft.AspNetCore.Mvc;
using Novella.Repositories;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Novella.EfModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "Admin")]
public class TransactionsController : Controller
{
    private readonly TransactionRepo _transactionRepo;
    private readonly NovellaContext _context; 


    public TransactionsController(TransactionRepo transactionRepo, NovellaContext context)
    {
        _transactionRepo = transactionRepo;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var orders = await _transactionRepo.GetAllOrdersAsync();
        return View(orders);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var order = await _context.Orders
                                  .Include(o => o.FkShippingAddress)
                                  .Include(o => o.FkOrderStatus)
                                  .Include(o => o.FkUser)
                                  .FirstOrDefaultAsync(m => m.PkOrderId == id);

        if (order == null)
        {
            return NotFound();
        }

        // Simplified for direct editing without dropdowns
        return View(order);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("PkOrderId,FkUserId,FkOrderStatusId,FkShippingAddressId,DateOrdered,PaypalTransactionId,FkShippingAddress,FkOrderStatus,FkUser")] Order order)
    {
        if (id != order.PkOrderId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(order);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(order.PkOrderId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(order);
    }

    private bool OrderExists(int id)
    {
        return _context.Orders.Any(e => e.PkOrderId == id);
    }



    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _transactionRepo.DeleteOrderAsync(id);
        return RedirectToAction(nameof(Index));
    }


    // Add actions for Create, Update, Delete...
}
