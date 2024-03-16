using Common.Domain;
using Microsoft.EntityFrameworkCore;
using UM.Domain.RoleAgg;
using UM.Domain.UserAgg;
using UM.Infrastructure.Utilities.MediatR;


namespace UM.Infrastructure.Persistent.EFCore;

public class UserManagementContext(DbContextOptions<UserManagementContext> options, ICustomPublisher publisher) : DbContext(options)
{
    private readonly ICustomPublisher _publisher = publisher;

    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var modifiedEntities = GetModifiedEntities();
        await PublishEvents(modifiedEntities);
        return await base.SaveChangesAsync(cancellationToken);
    }
    private List<AggregateRoot> GetModifiedEntities() =>
        ChangeTracker.Entries<AggregateRoot>()
            .Where(x => x.State != EntityState.Detached)
            .Select(c => c.Entity)
            .Where(c => c.DomainEvents.Any()).ToList();

    private async Task PublishEvents(List<AggregateRoot> modifiedEntities)
    {
        foreach (var entity in modifiedEntities)
        {
            var events = entity.DomainEvents;
            foreach (var domainEvent in events)
            {
                await _publisher.Publish(domainEvent, PublishStrategy.ParallelNoWait);
            }
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserManagementContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
