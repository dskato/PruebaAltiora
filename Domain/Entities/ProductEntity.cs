public class ProductEntity
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public decimal UnitPrice { get; set; }
    public ICollection<OrderProductEntity> OrderProductEntities { get; set; } = new List<OrderProductEntity>();

}