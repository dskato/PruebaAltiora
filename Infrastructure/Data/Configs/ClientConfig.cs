
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ClientConfig : IEntityTypeConfiguration<ClientEntity>
{


    public void Configure(EntityTypeBuilder<ClientEntity> builder)
    {
        builder.ToTable("client");
        builder.HasKey(x => x.ClientId);
        
    }
}