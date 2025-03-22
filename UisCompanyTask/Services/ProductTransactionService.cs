using Azure;
using System.Linq.Expressions;
using UisCompanyTask.Helper;
using UisCompanyTask.IService;
using UisCompanyTask.Models;
using UisCompanyTask.UnitOfWork;
using UisCompanyTask.ViewModel;

namespace UisCompanyTask.Services
{
    public class ProductTransactionService : IProductTransactionService
    {
        private readonly IUnitOfWork<UisProductContext> unitOfWork;
        public ProductTransactionService(IUnitOfWork<UisProductContext> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<string>> Add(ProductTransactionViewModel productTransactionViewModel)
        {
            var productTransaction=(ProductTransaction)productTransactionViewModel;
            unitOfWork.Repository<ProductTransaction>().Add(productTransaction);
            try
            {
                unitOfWork.Complete();
                return (new ApiResponse<string>(200,true,"Save Successfully"));
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>(500, false, message: "An unexpected error occurred.");
            }
        }

        public  async Task<ApiResponse<List<ProductTransactionViewModel>>> GetAllTransactions(int PageNumber=1,int PageSize=10,bool OrderbyDescending=false, string? Orderby = null, DateTime? TransactionDate = null)
        {
            Expression<Func<ProductTransaction, bool>>? filter = null;

            Expression<Func<ProductTransaction, object>>? orderBy = Orderby
                switch
            {
                "Date" => x => x.Date,
                "TotalPrice" => x => x.TotalPrice,
                "Quantity" => x => x.Quantity,
                _ => x => x.Id
            };
            if (TransactionDate.HasValue)
            {
                filter = x => x.Date.Date == TransactionDate.Value.Date;
            }


            var Data = await unitOfWork.Repository<ProductTransaction>().GetAll(PageNumber,PageSize,filter,ProductTransactionViewModel.ProductTransactionProjection,false, orderBy, OrderbyDescending,nameof(ProductTransaction.Product));
            try
            {
                if (Data != null)
                {
                    return (new ApiResponse<List<ProductTransactionViewModel>>(200, true, data: Data.ToList()));
                }
                else
                {
                    return (new ApiResponse<List<ProductTransactionViewModel>>(404, false, data: Data.ToList()));
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<ProductTransactionViewModel>>(500, false, message: "An unexpected error occurred.");
            }
        }
    }
}
