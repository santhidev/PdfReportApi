using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    private readonly AppDbContext _context;

    public HealthController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("db")]
    public async Task<IActionResult> CheckDatabaseConnection()
    {
        try
        {
            // ลองเชื่อมต่อและ query ง่าย ๆ
            var canConnect = await _context.Database.CanConnectAsync();
            if (canConnect)
                return Ok("เชื่อมต่อฐานข้อมูลสำเร็จ");
            else
                return StatusCode(500, "ไม่สามารถเชื่อมต่อฐานข้อมูลได้");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"เกิดข้อผิดพลาด: {ex.Message}");
        }
    }
}
