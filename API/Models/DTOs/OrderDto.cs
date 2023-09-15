public class OrderDto
{
    public string OrderId { get; set; }
    public DateTime? OrderDate { get; set; }
    public int? ClientId { get; set; }
    public int? ProductId { get; set; }
    public int? Quantity { get; set; }
}