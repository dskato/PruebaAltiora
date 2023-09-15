
using Microsoft.EntityFrameworkCore;

public class OrderService : IOrderService
{
    private readonly AppDbContext _dbContext;
    AutoMapper.IMapper mapper;
    public OrderService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        MapperConfig mapperConfig = new MapperConfig();
        mapper = mapperConfig.ConfigureOrderMapping();
    }

    public async Task<object> AddOrder(OrderDto orderDto)
    {
        try
        {
            //Verify if order already exists 
            var orderEntity = _dbContext.OrderEntity.Where(x => x.OrderId == orderDto.OrderId).FirstOrDefault();
            if (orderEntity != null)
            {
                return "Order already registered!";
            }
            //Add order info
            var order = mapper.Map<OrderEntity>(orderDto);
            order.OrderId = GenerateOrderId();
            await _dbContext.OrderEntity.AddAsync(order);

            //Assign the product relationship
            var orderProduct = mapper.Map<OrderProductEntity>(orderDto);
            orderProduct.OrderId = order.OrderId;
            await _dbContext.OrderProductEntity.AddAsync(orderProduct);

            await _dbContext.SaveChangesAsync();

            return "Order added succesfully!";
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public async Task<object> DeleteOrderById(string id)
    {
        //Verify if product exits
        var order = await _dbContext.OrderEntity.Where(x => x.OrderId == id).FirstOrDefaultAsync();
        if (order == null)
        {
            return "Order not found!";
        }
        _dbContext.OrderEntity.Remove(order);

        //Remove also in OrderProduct
        var orderP = await _dbContext.OrderProductEntity.Where(x => x.OrderId == id).FirstOrDefaultAsync();
        _dbContext.OrderProductEntity.Remove(orderP);

        await _dbContext.SaveChangesAsync();
        return "Order deleted succesfully!";
    }

    public async Task<object> GetAllOrders()
    {
        try
        {
            return await _dbContext.OrderEntity.ToListAsync();
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public async Task<object> GetOrderById(string id)
    {
        try
        {
            return await _dbContext.OrderEntity.Where(x => x.OrderId == id).ToListAsync();
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public async Task<object> GetOrderByClientId(int id)
    {
        try
        {
            return await _dbContext.OrderEntity.Where(x => x.ClientId == id).ToListAsync();
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public async Task<object> GetOrderByProductId(int id)
    {
        try
        {
            var orderIds = await _dbContext.OrderProductEntity.Where(x => x.ProductId == id).Select(x => x.OrderId).ToListAsync();
            return await _dbContext.OrderEntity.Where(x => orderIds.Contains(x.OrderId)).ToListAsync();
        }
        catch (Exception e)
        {
            return e;
        }
    }

    public Task<object> UpdateOrder(OrderDto orderDto)
    {
        throw new NotImplementedException();
    }


    private string GenerateOrderId()
    {
        string prefix = "OC-";
        int numberOfDigits = 6;

        // Get the last used order number
        int lastOrderNumber = _dbContext.OrderEntity
            .Where(o => o.OrderId.StartsWith(prefix))
            .Select(o => o.OrderId.Replace(prefix, "")) // Remove the prefix
            .Select(int.Parse)
            .DefaultIfEmpty(0) // Default to 0 if no orders exist
            .Max();

        // Increment the order number
        int nextOrderNumber = lastOrderNumber + 1;

        // Format the order number with leading zeros
        string sequentialPart = nextOrderNumber.ToString().PadLeft(numberOfDigits, '0');

        // Combine the prefix and sequential part to create the final order ID
        return prefix + sequentialPart;
    }
}