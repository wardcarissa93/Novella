using Microsoft.AspNetCore.Mvc;
using Novella.Repositories;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

[Authorize(Roles = "Admin")]
public class TransactionsController : Controller
{
    private readonly TransactionRepo _transactionRepo;

    public TransactionsController(TransactionRepo transactionRepo)
    {
        _transactionRepo = transactionRepo;
    }

    public async Task<IActionResult> Index()
    {
        var orders = await _transactionRepo.GetAllOrdersAsync();
        return View(orders);
    }

    // Add actions for Create, Update, Delete...
}
