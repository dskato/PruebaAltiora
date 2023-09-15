public interface IProductService
{
    Task<object> AddProduct(ProductDto productDto);
    Task<object> GetAllProduct();
    Task<object> GetProducttById(int id);
    Task<object> UpdateProduct(ProductDto productDto);
    Task<object> DeleteProductById(int id);
}