
using Client.Contracts;
using Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Client.Controllers.DashboardAdmin
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IAccountRepos _accountRepository;
        private readonly ITransactionRepos _transactionRepository;

        public DashboardController(ILogger<DashboardController> logger, IAccountRepos accountRepository, ITransactionRepos transactionRepository)
        {
            _logger = logger;
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<IActionResult> Index()
        {
            string jwtToken = HttpContext.Session.GetString("JWToken");
            var getTransaction = await _transactionRepository.DetailAll();
            var dataUser = await _accountRepository.GetClaims(jwtToken);
            var cekData = getTransaction.Data;
            if (getTransaction.Status == "OK" && cekData != null)
            {
                HttpContext.Session.SetString("Name", dataUser.Data.Name);
                HttpContext.Session.SetString("Email", dataUser.Data.Email);
                return View(cekData);
            }
            else
            {
                return View();
            }
        }
        public IActionResult Employees()
        {
            return View();
        }
        public IActionResult Customers()
        {
            return View();
        }
        public IActionResult Locations()
        {
            return View();
        }
        public IActionResult Provinces()
        {
            return View();
        }
        public IActionResult City()
        {
            return View();
        }
        public IActionResult Packages()
        {
            return View();
        }
        public IActionResult Transactions()
        {
            return View();
        }
    }
}
