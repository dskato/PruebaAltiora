
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderConfig : IEntityTypeConfiguration<OrderEntity>
{


    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.ToTable("order");
        builder.HasKey(x => x.OrderId);
        builder.HasMany(p => p.OrderProductEntities).WithOne(p => p.OrderEntity).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(r => r.ClientEntity)
                        .WithMany(a => a.OrderEntities)
                        .HasForeignKey(r => r.ClientId).IsRequired(false);

    }
}