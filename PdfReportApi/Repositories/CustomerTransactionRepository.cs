using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PdfReportApi.Models;
using PdfReportApi.Repositories;

public class CustomerTransactionRepository : ICustomerTransactionRepository
{
    private readonly AppDbContext _context;

    public CustomerTransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CustomerTransaction>?> GetByAccountAsync(string acn)
    {

        var param = new SqlParameter("@ACN", acn);
        return await _context.CustomerTransactions
            .FromSqlRaw("EXEC GetGL_Letter_By_EmployeeLevel @ACN", param)
            .ToListAsync();

    }


    public async Task<IEnumerable<CustomerTransaction>> GetAllAsync()
    {
        return await _context.CustomerTransactions.ToListAsync();
    }

    public async Task<CustomerTransaction?> GetByIdAsync(int id)
    {
        return await _context.CustomerTransactions.FindAsync(id);
    }

    public async Task AddAsync(CustomerTransaction transaction)
    {
        await _context.CustomerTransactions.AddAsync(transaction);
    }

    public void Update(CustomerTransaction transaction)
    {
        _context.CustomerTransactions.Update(transaction);
    }

    public void Remove(CustomerTransaction transaction)
    {
        _context.CustomerTransactions.Remove(transaction);
    }
}
