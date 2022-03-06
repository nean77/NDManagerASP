using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using NDManager.Data;
using NDManager.Data.Models;
using NDManager.ReportLogic;
using NDManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NDManager.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly INdmRepository<Payment> _repository;

        public PaymentController(ILogger<PaymentController> logger, INdmRepository<Payment> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var groups = await _repository.GetAllActiveGroupsAsync();

            var paymentGroups = new List<PaymentGroupViewModel>();

            foreach (var g in groups)
            {
                paymentGroups.Add(new PaymentGroupViewModel
                {
                    Id = g.Id,
                    Name = g.Name,
                    WorkingDays = 20,
                    Month = (byte)DateTime.Today.Month
                });
            }
            ViewBag.PaymentGroups = new SelectList(paymentGroups, "Id", "Name");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NewList(PaymentGroupViewModel group)
        {
            var list = await _repository.GetAllKidsByGroupAsync(group.Id);

            var paymentList = new List<PaymentViewModel>();
            foreach (var kid in list)
            {
                paymentList.Add(new PaymentViewModel
                {
                    KidId = kid.Id,
                    FirstName = kid.FirstName,
                    LastName = kid.LastName,
                    Value = (kid.AttendanceDailyRate + kid.MealDailyRate) * group.WorkingDays,
                    Deduction = 0,
                    MonthNo = (byte)DateTime.Today.Month,
                    WorkingDays = group.WorkingDays
                });
            }

            return View(paymentList);
        }
        [HttpPost]
        public async Task<IActionResult> List(PaymentGroupViewModel group)
        {
            var list = await _repository.GetPaymentsByGroupId(group.Id);
            if (list.Count() != 0) ViewBag.GroupId = group.Id;
            else ViewBag.GroupId = -1;
            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GeneratePayment(IEnumerable<PaymentViewModel> listViewModels)
        {
            foreach (var item in listViewModels)
            {
                var payment = await _repository.GetPaymentByKidIdAsync(item.KidId);
                if (payment != null)
                {
                    payment.MonthNo = item.MonthNo;
                    payment.Value = item.Value - item.Deduction;
                    payment.WorkingDays = item.WorkingDays;
                    await _repository.UpdateAsync(payment);
                }
                else
                {
                    payment = new Payment
                    {
                        KidId = item.KidId,
                        MonthNo = item.MonthNo,
                        Value = item.Value - item.Deduction,
                        WorkingDays = item.WorkingDays
                    };
                    await _repository.InsertAsync(payment);
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GeneratePdf(int id)
        {
            var p = await _repository.GetPaymentByIdAsync(id);
            var l = new List<Payment>();
            l.Add(p);

            return new FileStreamResult(PaymentPdfGenerator.Generate(l), "application/pdf");
        }
        [HttpGet]
        [Route("/Payment/GenerateGroupPdf/{id}")]
        public async Task<IActionResult> GenerateGroupPdf(int id)
        {
            var paymentList = await _repository.GetPaymentsByGroupId(id);

            return new FileStreamResult(PaymentPdfGenerator.Generate(paymentList.ToList()), "application/pdf");
        }
    }
}
