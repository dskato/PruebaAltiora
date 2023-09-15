public class OrderEntity
{
    public string OrderId { get; set; }
    public DateTime OrderDate { get; set; }

    //FK
    public int ClientId { get; set; }
    public ClientEntity ClientEntity { get; set; }
    public ICollection<OrderProductEntity> OrderProductEntities { get; set; } = new List<OrderProductEntity>();
}