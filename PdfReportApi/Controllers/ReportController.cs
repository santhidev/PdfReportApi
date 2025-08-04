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

        public ReportController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _pdfReportService = new PdfReportService(); // หรือใช้ DI ก็ได้
        }

        [HttpGet("pdf/by-account/{acn}")]
        public async Task<IActionResult> GetCustomerTransactionPdfByAccount(string acn)
        {
            var transactions = await _unitOfWork.CustomerTransactions.GetAllAsync();
            var transaction = transactions.FirstOrDefault(t => t.ACN == acn);

            if (transaction == null)
                return NotFound($"ไม่พบข้อมูลสำหรับเลขบัญชี {acn}");

            var pdfBytes = _pdfReportService.GenerateCustomerTransactionReport(transaction);
            return File(pdfBytes, "application/pdf", $"transaction_{acn}.pdf");
        }


    }
}
