using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PdfReportApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly PdfReportService _pdfReportService;

        public ReportController(IUnitOfWork unitOfWork, PdfReportService pdfReportService)
        {
            _unitOfWork = unitOfWork;
            _pdfReportService = pdfReportService; // หรือใช้ DI ก็ได้
        }

        //[HttpGet("pdf/by-account/{acn}")]
        //public async Task<IActionResult> GetCustomerTransactionPdfByAccount(string acn)
        //{
        //    var transactions = await _unitOfWork.CustomerTransactions.GetByAccountAsync(acn);

        //    if (transactions == null)
        //        return NotFound($"ไม่พบข้อมูลสำหรับเลขบัญชี {acn}");

        //    var transactionList = transactions.ToList();

        //    var pdfBytes = _pdfReportService.GenerateCustomerTransactionReport(transactionList, "นายชื่อล็อกอิน ตรงนี้");
        //    return File(pdfBytes, "application/pdf", $"transaction_{acn}.pdf");
        //}

        [HttpGet("pdf/by-cid/{cid}")]
        public async Task<IActionResult> GetCustomerTransactionPdfByCID(string cid)
        {
            var transactions = await _unitOfWork.CustomerTransactions.GetByCIDAsync(cid);

            if (transactions == null)
                return NotFound($"ไม่พบข้อมูลสำหรับเลขบัญชี {cid}");

            var transactionList = transactions.ToList();

            var pdfBytes = _pdfReportService.GenerateCustomerTransactionReport(transactionList, "นายชื่อล็อกอิน ตรงนี้");
            return File(pdfBytes, "application/pdf", $"transaction_{cid}.pdf");
        }

    }
}
