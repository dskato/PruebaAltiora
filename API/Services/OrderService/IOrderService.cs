public interface IOrderService
{

    Task<object> AddOrder(OrderDto orderDto);
    Task<object> GetAllOrders();
    Task<object> GetOrderByClientId(int id);
    Task<object> GetOrderByProductId(int id);
    Task<object> DeleteOrderById(string id);
    Task<object> UpdateOrder(OrderDto orderDto);

}