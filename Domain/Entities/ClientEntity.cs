public class ClientEntity
{
    public int ClientId { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }

     public ICollection<OrderEntity> OrderEntities { get; set; } = new List<OrderEntity>();

}