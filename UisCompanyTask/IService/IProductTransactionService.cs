using UisCompanyTask.Helper;
using UisCompanyTask.Models;
using UisCompanyTask.ViewModel;

namespace UisCompanyTask.IService
{
    public interface IProductTransactionService
    {
        Task<ApiResponse<List<ProductTransactionViewModel>>> GetAllTransactions(int PageNumber, int PageSize,  bool OrderbyDescending, string? Orderby, DateTime? TransactionDate);
        Task<ApiResponse<string>> Add(ProductTransactionViewModel productTransactionViewModel);
    }
}
