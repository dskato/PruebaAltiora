
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProductConfig : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.ToTable("product");
        builder.HasKey(x => x.ProductId);
        builder.HasMany(p => p.OrderProductEntities).WithOne(p => p.ProductEntity).OnDelete(DeleteBehavior.NoAction);

    }
}