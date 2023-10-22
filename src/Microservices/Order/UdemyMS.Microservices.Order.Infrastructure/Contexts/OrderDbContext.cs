using Microsoft.EntityFrameworkCore;

namespace UdemyMS.Microservices.Order.Infrastructure.Contexts;
public class OrderDbContext : DbContext
{
    private const string DEFAULT_SCHEMA = "ordering";
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

    public DbSet<Domain.Orders.Order> Orders { get; set; }
    public DbSet<Domain.Orders.Entities.OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Orders.Order>()
                    .ToTable("Orders", DEFAULT_SCHEMA);

        modelBuilder.Entity<Domain.Orders.Entities.OrderItem>()
                    .ToTable("OrderItems", DEFAULT_SCHEMA)
                    .Property(x => x.Price)
                    .HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Domain.Orders.Order>()
                    .OwnsOne(x => x.Address)
                    .WithOwner();
    }
}