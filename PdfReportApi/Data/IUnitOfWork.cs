using PdfReportApi.Repositories;

public interface IUnitOfWork
{
    ICustomerTransactionRepository CustomerTransactions { get; }
    Task<int> CompleteAsync();
}
