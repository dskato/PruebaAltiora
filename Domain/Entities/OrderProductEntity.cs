public class OrderProductEntity
{
    public string OrderId { get; set; }
    public OrderEntity OrderEntity { get; set; }

    public int ProductId { get; set; }
    public ProductEntity ProductEntity { get; set; }

    public int Quantity { get; set; }
}