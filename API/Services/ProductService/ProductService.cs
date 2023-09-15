
using Microsoft.EntityFrameworkCore;

public class ProductService : IProductService
{
    private readonly AppDbContext _dbContext;
    AutoMapper.IMapper mapper;
    public ProductService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        MapperConfig mapperConfig = new MapperConfig();
        mapper = mapperConfig.ConfigureProductMapping();
    }

    public async Task<object> AddProduct(ProductDto productDto)
    {
        try
        {
            //Verify if product already exists 
            var clientEntity = _dbContext.ProductEntity.Where(x => x.Name == productDto.Name).FirstOrDefault();
            if (clientEntity != null)
            {
                return "Product already registered!";
            }
            var product = mapper.Map<ProductEntity>(productDto);
            var result = await _dbContext.ProductEntity.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return "Product added succesfully!";
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public async Task<object> DeleteProductById(int id)
    {
        try
        {

            //Verify if product exits
            var product = await _dbContext.ProductEntity.Where(x => x.ProductId == id).FirstOrDefaultAsync();
            if (product == null)
            {
                return "Product not found!";
            }
            _dbContext.ProductEntity.Remove(product);
            await _dbContext.SaveChangesAsync();

            return "Product deleted succesfully!";

        }
        catch (Exception e)
        {
            return e;
        }
    }

    public async Task<object> GetAllProduct()
    {
        try
        {
            return await _dbContext.ProductEntity.ToListAsync();
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public async Task<object> GetProducttById(int id)
    {
          try
        {
            var product = await _dbContext.ProductEntity.Where(x => x.ProductId == id).FirstOrDefaultAsync();
            if (product == null)
            {
                return "Product not found!";
            }
            return product;
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public async Task<object> UpdateProduct(ProductDto productDto)
    {
         try
        {
            var product = await _dbContext.ProductEntity.Where(x => x.ProductId == productDto.ProductId).FirstOrDefaultAsync();
            if (product == null)
            {
                return "Product not found!";
            }
            product.Name = productDto.Name;
            product.UnitPrice = productDto.UnitPrice;
            
            await _dbContext.SaveChangesAsync();
            
            return "Product updated!";
        }
        catch (Exception e)
        {
            return e;
        }
    }
}