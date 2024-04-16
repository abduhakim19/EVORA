using API.Contracts;
using API.DTOs.TransactionEvents;
using API.Utilities.Enums;
using Client.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Data;

namespace Client.Controllers.Staff
{
    [Authorize(Roles = "Staff,Admin")]
    public class StaffController : Controller
    {
        private readonly ITransactionRepos _transactionRepository;
        private readonly IAccountRepos _accountRepository;

        public StaffController(ITransactionRepos transactionRepository, IAccountRepos accountRepository)
        {
            this._transactionRepository = transactionRepository;
            this._accountRepository = accountRepository;
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

        public async Task<IActionResult> StatusApprove(Guid id)
        {
            var updateStatus = new ChangeTransactionStatusDto
            {
                Status = (StatusTransaction)3,
                Guid = id
            };
            var updateTransaction = await _transactionRepository.ChangeStatus(updateStatus);
            var cekData = updateTransaction.Data;
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> StatusDecline(Guid id)
        {
            var updateStatusDec = new ChangeTransactionStatusDto
            {
                Status = 0,
                Guid = id
            };
            var updateTransaction = await _transactionRepository.ChangeStatus(updateStatusDec);
            var cekData = updateTransaction.Data;
            return RedirectToAction(nameof(Index));
        }
        public IActionResult ListEvent()
        {
            return View();
        }
        [Route("events/get")]
        public async Task<IActionResult> GetEvents()
        {
            var getTransaction = await _transactionRepository.DetailAll();
            var cekData = getTransaction.Data;
            var eventList = cekData.Select(e => new
            {
                id = e.Guid,
                title = e.Package+"-"+e.FirstName,
                start = e.EventDate.ToString("yyyy-MM-ddTHH:mm:ss"),
            // end = e.EventDate.ToString("yyyy-MM-ddTHH:mm:ss"),
        });
            return new JsonResult(eventList);
        }
        public IActionResult Packages()
        {
            return View();
        }
        public IActionResult Histories()
        {
            return View();
        }
    }
}
