using PdfReportApi.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public ICustomerTransactionRepository CustomerTransactions { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        CustomerTransactions = new CustomerTransactionRepository(_context);
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
