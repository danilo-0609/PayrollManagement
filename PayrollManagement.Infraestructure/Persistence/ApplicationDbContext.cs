using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PayrollManagement.Application.Data;
using PayrollManagement.Domain.Employees;
using PayrollManagement.Domain.Primitives;
using PayrollManagement.Domain.Users;
using PayrollManagement.Domain.ValueObjects;

namespace PayrollManagement.Infraestructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
    {
        private readonly IPublisher _publisher;
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(DbContextOptions options, IPublisher publisher, IConfiguration configuration)
            : base(options)
        {
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SqlServer")!);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var domainEvents = ChangeTracker.Entries<AggregateRoot>()
                .Select(e => e.Entity)
                .Where(e => e.GetDomainEvents().Any())
                .SelectMany(e => e.GetDomainEvents());

            var result = await base.SaveChangesAsync(cancellationToken);

            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }

            return result;
        }

    }
}
