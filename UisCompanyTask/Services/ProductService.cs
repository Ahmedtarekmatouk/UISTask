using System.Linq.Expressions;
using UisCompanyTask.Helper;
using UisCompanyTask.IService;
using UisCompanyTask.Models;
using UisCompanyTask.UnitOfWork;
using UisCompanyTask.ViewModel;

namespace UisCompanyTask.Services
{
    public class ProductService : IProductservice
    {
        private readonly IUnitOfWork<UisProductContext> unitOfWork;
        public ProductService(IUnitOfWork<UisProductContext> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<string>> AddProduct(ProductViewModel productviewmodel)
        {
            var product = (Product)productviewmodel;
            unitOfWork.Repository<Product>().Add(product);
            try
            {
                unitOfWork.Complete();
                return (new ApiResponse<string>(200, true, "Save Successfully"));
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>(500, false, message: "An unexpected error occurred.");
            }
        }
        public async Task<ApiResponse<List<ProductViewModel>>> GetAll()
        {
            var Data = await unitOfWork.Repository<Product>().GetAll(selection:ProductViewModel.ProductViewModelProjection);
            try
            {
                if (Data != null)
                {
                    return (new ApiResponse<List<ProductViewModel>>(200, true, data: Data.ToList()));
                }
                else
                {
                    return (new ApiResponse<List<ProductViewModel>>(404, false, data: Data.ToList()));
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<ProductViewModel>>(500, false, message: "An unexpected error occurred.");
            }
        }

        public async Task<ApiResponse<ProductViewModel>> GetById(int id)
        {
            var Data = await unitOfWork.Repository<Product>().Get(e=>e.Id==id, selection: ProductViewModel.ProductViewModelProjection);
            try
            {
                if (Data != null)
                {
                    return (new ApiResponse<ProductViewModel>(200, true, data: Data));
                }
                else
                {
                    return (new ApiResponse<ProductViewModel>(404, false, data: Data));
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProductViewModel>(500, false, message: "An unexpected error occurred.");
            }
        }

        public async Task<ApiResponse<string>> Update(int id, ProductViewModel productviewmodel)
        {
            var productData = await unitOfWork.Repository<Product>().Get<Product>(e => e.Id == id,tracked:true);
            productData.Quantity=productviewmodel.Quantity;
            var product = (Product)productviewmodel;
            unitOfWork.Repository<Product>().Update(productData);
            try
            {
                unitOfWork.Complete();
                return (new ApiResponse<string>(200, true, "Save Successfully"));
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>(500, false, message: "An unexpected error occurred.");
            }
        }
    }
}
