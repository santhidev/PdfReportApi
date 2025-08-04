using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PdfReportApi.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;

public class PdfReportService
{
    public byte[] GenerateCustomerTransactionReport(CustomerTransaction transaction)
    {
        using var stream = new MemoryStream();

        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(50);
                page.DefaultTextStyle(x => x.FontFamily("Tahoma")); // รองรับภาษาไทย

                // ส่วนหัว
                page.Header().Column(header =>
                {
                    header.Item().AlignCenter().Text("ธนาคารเพื่อการเกษตรและสหกรณ์การเกษตร").FontSize(18).Bold();
                    header.Item().AlignCenter().Text($"สาขา: {transaction.BOO_DESC}");
                    header.Item().AlignCenter().Text($"วันที่: {DateTime.Now.ToString("dd MMMM yyyy", new CultureInfo("th-TH"))}");
                    header.Item().AlignCenter().Text($"ชื่อรายการ: BOD_DESC");
                });

                // ข้อมูลลูกค้า
                page.Content().Column(content =>
                {
                    content.Item().Text("ข้อมูลลูกค้า").FontSize(14).Bold();
                    content.Item().Text($"ชื่อ: {transaction.NAM}");
                    content.Item().Text($"เลขที่บัตรประชาชน: {transaction.CID}");
                    content.Item().Text($"ชื่อบัญชี: {transaction.ACN}");
                    content.Item().Text($"สาขา: {transaction.BOO_DESC}");
                    //content.Item().Text($"ประเภทบัญชี: {transaction.ACCOUNTType}");

                    content.Item().PaddingTop(10).Text("รายการธุรกรรม").FontSize(14).Bold();

                    content.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("ยอดคงเหลือ").Bold();
                            header.Cell().Text("ยอดที่ต้องชำระ").Bold();
                            header.Cell().Text("ดอกเบี้ย").Bold();
                            header.Cell().Text("วันครบกำหนด").Bold();
                        });

                        table.Cell().Text(transaction.BAL);
                        table.Cell().Text(transaction.DUPR);
                        table.Cell().Text(transaction.DUIN);
                        table.Cell().Text(transaction.NEXT_DUE);
                    });


                    content.Item().Text("ข้าพเจ้ามีความประสงค์จะผ่อนชำระหนี้ดังกล่าว โดยแบ่งเป็นงวดๆ ตามความสามารถในการชำระของข้าพเจ้า ดังรายละเอียดต่อไปนี้");
                    content.Item().Text("เดือนละ จำนวนเงิน (บาท) ................ บาท (...................)");

                    content.Item().Text("จึงเรียนมาเพื่อโปรดพิจารณาอนุมัติด้วย จะเป็นพระคุณยิ่ง");

                    content.Item().Text("\nลงชื่อ...........................................ผู้กู้\n(..................................................)");
                    content.Item().Text("ลงชื่อ...........................................ผู้จัดการ\n(..................................................)");
                    content.Item().Text("ลงชื่อ...........................................พยาน\n(..................................................)");

            });

                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("หน้าที่ ");
                    x.CurrentPageNumber();
                    x.Span(" จาก ");
                    x.TotalPages();
                });
            });
        }).GeneratePdf(stream);

        return stream.ToArray();
    }
}
