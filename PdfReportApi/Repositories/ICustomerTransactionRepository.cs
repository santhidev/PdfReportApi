using PdfReportApi.Models;

namespace PdfReportApi.Repositories
{
    public interface ICustomerTransactionRepository
    {

        Task<IEnumerable<CustomerTransaction>> GetAllAsync();
        Task<IEnumerable<CustomerTransaction>?> GetByAccountAsync(string acn);
        Task<CustomerTransaction?> GetByIdAsync(int id);
        Task AddAsync(CustomerTransaction transaction);
        void Update(CustomerTransaction transaction);
        void Remove(CustomerTransaction transaction);

    }
}
