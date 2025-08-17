using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

QuestPDF.Settings.License = LicenseType.Community;

var document = Document.Create(container =>
{
    container.Page(page =>
    {
        page.Size(PageSizes.A4);
        page.Margin(40);
        page.DefaultTextStyle(x => x.FontFamily("Tahoma").FontSize(16));

        var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "images", "BAAC_Logo.png");
        page.Header().Row(header =>
        {
            header.Spacing(5, Unit.Millimetre);

            header.AutoItem().AlignLeft().Height(50).Image(logoPath, ImageScaling.FitArea);
            header.AutoItem().AlignTop().AlignLeft().Text("ธนาคารเพื่อการเกษตรและสหกรณ์การเกษตร").FontSize(12).Bold();
            header.AutoItem().AlignTop().AlignLeft().Text("สาขา ยางชุมน้อย").FontSize(12).Bold();
            header.RelativeColumn().AlignTop().AlignRight().Text("302.02.43").FontSize(12);
        });

        page.Content().Column(content =>
        {
            content.Spacing(5);
            content.Item().AlignCenter().PaddingTop(-10).Text("หนังสือบอกกล่าวผู้ค้ำประกัน").FontSize(12).Bold();
            content.Item().PaddingBottom(10);
            content.Item().AlignRight().Text("วันที่ 20 สิงหาคม 2568");
            content.Item().Row(row => {
                row.Spacing(10);

                row.AutoItem().Text("เรียน: นายสันติ  นามวงศ์");
                row.AutoItem().Text("กลุ่มที่: 12345");
                row.AutoItem().Text("CIF: 67890");
            });
            content.Item().Text("ที่อยู่: 65/1");
            content.Item().Row(row =>
            {
                row.Spacing(10);
                row.AutoItem().Text("          หมู่ที่: ");
                row.AutoItem().Text("ตำบล/แขวง: ");
                row.AutoItem().Text("อำเภอ/เขต: อื่นๆ");
            });
            content.Item().Row(row =>
            {
                row.Spacing(10);
                row.AutoItem().Text("          จังหวัด: อื่นๆ");
                row.AutoItem().Text("รหัสไปรษณีย์: ");
            });
            content.Item().Text("ธนาคาร ขอเรียนให้ท่านทราบในฐานะผู้คำประกันเงินกู้ของ นายสันติ นามวงศ์ เจ้าของเลขที่ สัญญา 800112121121");
            content.Item().Text("ลูกค้า กลุ่มที่: 0    CIF: 12345    มีหนี้ครบกำหนดชำระ ณ วันที่ 30/06/2568 ดังนี้");
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

                table.Cell().Element(CellStyle).AlignLeft().Text("800442614095");
                table.Cell().Element(CellStyle).AlignRight().Text("299944.69");
                table.Cell().Element(CellStyle).AlignRight().Text("0.00");
                table.Cell().Element(CellStyle).AlignRight().Text("59300.23");

                static IContainer CellStyle(IContainer container)
                    => container.Border(1).Padding(5);
            });
            content.Item().PaddingTop(10).Text("          บัดนี้ ปรากฎว่าลูกหนี้ผิดนัดไม่ชำระหนี้ตามกำหนด ธนาคารจึงขอบอกกล่าวมายังท่าน และขอให้ท่านแจ้งลูกหนี้ " +
                "ให้ชำระหนี้แก่ธนาคารให้เสร็จโดยเร็วต่อไป ธนาคารขออภัยหากลูกหนี้ได้ส่งชำระหนี้แล้ว");
            content.Item().PaddingTop(20).PaddingBottom(20).Text("จึงเรียนมาเพื่อโปรดทราบ");

            content.Item().AlignCenter().Column(column => {
                column.Item().AlignCenter().PaddingTop(20).Text("ขอแสดงความนับถือ").Bold();
                column.Item().AlignCenter().PaddingTop(40).Text("ลงชื่อ.............................");
                column.Item().AlignCenter().PaddingTop(5).Text("ตำแหน่ง.............................");
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
                            column.Item().AlignCenter().PaddingTop(5).Text("(...............................)");
                            column.Item().AlignCenter().PaddingTop(5).Text("........../........../..........");
                        });
                    });
            });

            //row.RelativeItem().Column();
        });

        //page.Footer().AlignCenter().Text(x =>
        //{
        //    x.Span("หน้า ");
        //    x.CurrentPageNumber();
        //    x.Span(" จาก ");
        //    x.TotalPages();
        //});
    });
});

// แสดงผลใน Companion App
document.ShowInCompanion();
