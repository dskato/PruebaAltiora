using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ClientConfig());
        modelBuilder.ApplyConfiguration(new OrderConfig());
        modelBuilder.ApplyConfiguration(new ProductConfig());
        modelBuilder.ApplyConfiguration(new OrderProductConfig());

    }

    public DbSet<ClientEntity> ClientEntity { get; set; }
    public DbSet<OrderEntity> OrderEntity { get; set; }
    public DbSet<ProductEntity> ProductEntity { get; set; }
    public DbSet<OrderProductEntity> OrderProductEntity { get; set; }

}