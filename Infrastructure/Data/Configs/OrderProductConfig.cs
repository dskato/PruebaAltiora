using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderProductConfig : IEntityTypeConfiguration<OrderProductEntity>
{


    public void Configure(EntityTypeBuilder<OrderProductEntity> builder)
    {
        builder.ToTable("order_product");
        builder.HasKey(x => new { x.OrderId, x.ProductId });
        builder.HasOne(r => r.ProductEntity)
                         .WithMany(a => a.OrderProductEntities)
                         .HasForeignKey(r => r.ProductId).IsRequired(false);
        builder.HasOne(r => r.OrderEntity)
                         .WithMany(a => a.OrderProductEntities)
                         .HasForeignKey(r => r.OrderId).IsRequired(false);
    }
}