using comercializadora_de_pulpo_api.Models;
using comercializadora_de_pulpo_api.Models.DTOs.Products;

namespace comercializadora_de_pulpo_api.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync(bool onlyActives);
        Task<Product?> GetProductByIdAsync(Guid productId);
        Task<int> GetTotalProductsAsync();
        Task<bool> VerifyNameAsync(string name);
        Task<Response<Product>> CreateProductAsync(Product newProduct);
        Task<Response<Product>> UpdateProductAsync(Product updatedProduct);
    }
}
