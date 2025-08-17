using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PdfReportApi.Models;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;

public class PdfReportService
{

    private readonly IWebHostEnvironment _env;

    public PdfReportService(IWebHostEnvironment env)
    {
        _env = env;

        // ลงทะเบียนฟอนต์ THSarabunNew
        FontManager.RegisterFont(File.OpenRead(Path.Combine(_env.WebRootPath, "fonts", "THSarabun.ttf")));

    }

    public byte[] GenerateCustomerTransactionReport(List<CustomerTransaction> transactions, string senderName)
    {
        using var stream = new MemoryStream();

        Document.Create(container =>
        {
            foreach (var transaction in transactions)
            {
                var prefixTitle = string.Empty;
                if (transaction.PrefixCode.Equals("1"))
                    prefixTitle = "นาย";
                else if (transaction.PrefixCode.Equals("2"))
                    prefixTitle = "นาง";
                else if (transaction.PrefixCode.Equals("3"))
                    prefixTitle = "นางสาว";
                else
                    prefixTitle = "คุณ";

                var thaiCulture = new CultureInfo("th-TH");
                string thaiDate = "วันที่ " + DateTime.Now.ToString("d MMMM yyyy", thaiCulture);


                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40);
                    page.DefaultTextStyle(x => x.FontFamily("THSarabunNew").FontSize(11));

                    var logoPath = Path.Combine(_env.WebRootPath, "images", "BAAC_Logo.png");

                    page.Header().Row(header =>
                    {
                        header.Spacing(5, Unit.Millimetre);

                        header.AutoItem().AlignLeft().Height(50).Image(logoPath, ImageScaling.FitArea);
                        header.AutoItem().AlignTop().AlignLeft().Text("ธนาคารเพื่อการเกษตรและสหกรณ์การเกษตร").FontSize(12).Bold();
                        header.AutoItem().AlignTop().AlignLeft().Text($"สาขา {transaction.BOO_DESC}").FontSize(12).Bold();
                        header.RelativeColumn().AlignTop().AlignRight().Text("302.02.43").FontSize(12);
                    });

                    page.Content().Column(content =>
                    {
                        content.Spacing(5);
                        content.Item().AlignCenter().PaddingTop(-10).Text("หนังสือบอกกล่าวผู้ค้ำประกัน").FontSize(12).Bold();
                        content.Item().PaddingBottom(10);
                        content.Item().AlignRight().Text($"{thaiDate}");
                        content.Item().Row(row => {
                            row.Spacing(10);
                            row.AutoItem().Text($"เรียน: {transaction.NAMGU}   กลุ่มที่: 12345   CIF: {transaction.ACNGU}");
                        });
                        content.Item().Text("ที่อยู่: 65/1");
                        content.Item().Row(row =>
                        {
                            row.Spacing(10);
                            row.AutoItem().Text("          หมู่ที่:    ตำบล/แขวง:    อำเภอ/เขต: อื่นๆ");
                        });
                        content.Item().Row(row =>
                        {
                            row.Spacing(10);
                            row.AutoItem().Text("          จังหวัด: อื่นๆ   รหัสไปรษณีย์: ");
                        });
                        content.Item().Text($"ธนาคาร ขอเรียนให้ท่านทราบในฐานะผู้คำประกันเงินกู้ของ {transaction.NAM} เจ้าของเลขที่ สัญญา 800112121121");
                        content.Item().Text($"ลูกค้า กลุ่มที่: 0    CIF: {transaction.ACN}    มีหนี้ครบกำหนดชำระ ณ วันที่ {transaction.NEXT_DUE} ดังนี้");
                        content.Item().PaddingTop(10).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Cell().Element(CellStyle).AlignCenter().Text("เลขที่บัญชี").Bold();
                            table.Cell().Element(CellStyle).AlignCenter().Text("ต้นเงินกู้คงเหลือ").Bold();
                            table.Cell().Element(CellStyle).AlignCenter().Text("ต้นเงินค้างชำระทั้งหมด").Bold();
                            table.Cell().Element(CellStyle).AlignCenter().Text("ดอกเบี้ยพึงชำระ").Bold();

                            table.Cell().Element(CellStyle).AlignLeft().Text($"{transaction.CID}");
                            table.Cell().Element(CellStyle).AlignRight().Text($"{decimal.Parse(transaction.BAL).ToString("N2")}");
                            table.Cell().Element(CellStyle).AlignRight().Text($"{decimal.Parse(transaction.DUPR).ToString("N2")}");
                            table.Cell().Element(CellStyle).AlignRight().Text($"{decimal.Parse(transaction.DUIN).ToString("N2")}");

                            static IContainer CellStyle(IContainer container)
                                => container.Border(1).Padding(5);
                        });
                        content.Item().PaddingTop(10).Text("          บัดนี้ ปรากฎว่าลูกหนี้ผิดนัดไม่ชำระหนี้ตามกำหนด ธนาคารจึงขอบอกกล่าวมายังท่าน และขอให้ท่านแจ้งลูกหนี้ " +
                            "ให้ชำระหนี้แก่ธนาคารให้เสร็จโดยเร็วต่อไป ธนาคารขออภัยหากลูกหนี้ได้ส่งชำระหนี้แล้ว");
                        content.Item().PaddingTop(20).PaddingBottom(20).Text("จึงเรียนมาเพื่อโปรดทราบ");

                        content.Item().AlignCenter().Column(column => {
                            column.Item().AlignCenter().PaddingTop(20).Text("ขอแสดงความนับถือ").Bold();
                            column.Item().AlignCenter().PaddingTop(40).Text($"ลงชื่อ {prefixTitle} {transaction.EmpFname} {transaction.EmpLname}");
                            column.Item().AlignCenter().PaddingTop(5).Text("ตำแหน่ง ผู้จัดการสาขา");
                        });

                        content.Item().PaddingTop(40).MultiColumn(multiColumn => {
                            multiColumn.Columns(2);
                            multiColumn.BalanceHeight();

                            multiColumn
                                .Content().AlignCenter()
                                .Column(column =>
                                {
                                    //column.Spacing(15);

                                    column.Item().AlignCenter().Column(column =>
                                    {
                                        column.Item().AlignCenter().PaddingTop(5).Text("ลงชื่อ.............................ผู้รับ");
                                        column.Item().AlignCenter().PaddingTop(5).Text("(...............................)");
                                        column.Item().AlignCenter().PaddingTop(5).Text("........../........../..........");
                                    });
                                    column.Item().AlignCenter().Column(column =>
                                    {
                                        column.Item().AlignCenter().PaddingTop(5).Text("ลงชื่อ.............................ผู้ส่ง");
                                        column.Item().AlignCenter().PaddingTop(5).Text($"({senderName})");
                                        column.Item().AlignCenter().PaddingTop(5).Text("........../........../..........");
                                    });
                                });
                        });

                        //row.RelativeItem().Column();
                    });
                });
            }
        }).GeneratePdf(stream);

        return stream.ToArray();
    }


}
