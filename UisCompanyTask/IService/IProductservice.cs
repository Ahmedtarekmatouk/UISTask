using UisCompanyTask.Helper;
using UisCompanyTask.Models;
using UisCompanyTask.ViewModel;

namespace UisCompanyTask.IService
{
    public interface IProductservice
    {
        Task<ApiResponse<string>> AddProduct(ProductViewModel productviewmodel);
        Task<ApiResponse<List<ProductViewModel>>> GetAll();
        Task<ApiResponse<ProductViewModel>> GetById(int id);
        Task<ApiResponse<string>> Update(int id,ProductViewModel productviewmodel);
    }
}
