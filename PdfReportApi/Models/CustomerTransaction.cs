using System.ComponentModel.DataAnnotations.Schema;

namespace PdfReportApi.Models
{
    [Table("GL_LETTER")]
    public class CustomerTransaction
    {

        // ข้อมูลลูกค้า
        public string ST { get; set; }               // สถานะ (D)
        public string CID { get; set; }              // เลขประจำตัวประชาชน
        public string ACNGU { get; set; }            // เลขบัญชีกลุ่ม
        public string ACN { get; set; }              // เลขบัญชีเดี่ยว
        public string BOO { get; set; }              // รหัสสาขา
        public string BOO_DESC { get; set; }         // ชื่อสาขา
        public string BKDISTCD { get; set; }         // รหัสเขต
        public string NAM { get; set; }              // ชื่อลูกค้า
        public string LNREGION { get; set; }         // ภูมิภาค
        public string OLDGRPCD { get; set; }         // รหัสกลุ่มเดิม

        // ข้อมูลกลุ่ม
        public string NAMGU { get; set; }            // ชื่อกลุ่ม
        public string BKDISTCDGU { get; set; }       // รหัสเขตกลุ่ม
        public string LNREGIONGU { get; set; }       // ภูมิภาคกลุ่ม
        public string OLDGRPCDGU { get; set; }       // รหัสกลุ่มเดิมของกลุ่ม

        // ข้อมูลบัญชี
        public string CCL { get; set; }              // รหัสบัญชี
        public string BAL { get; set; }             // ยอดคงเหลือ
        public string DUPR { get; set; }            // ยอดที่ต้องชำระ
        public string DUIN { get; set; }            // ดอกเบี้ย
        public string NEXT_DUE { get; set; }       // วันครบกำหนดถัดไป


        public string? PrefixCode { get; set; }
        public string? EmpFname { get; set; }
        public string? EmpLname { get; set; }
        public short? PositionLevel { get; set; }


    }
}
