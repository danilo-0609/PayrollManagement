using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PayrollManagement.Users.Application.Data;
using PayrollManagement.Users.Domain.Primitives;
using PayrollManagement.Users.Domain.Users;

namespace PayrollManagement.Users.Infraestructure
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
    {
        private readonly IPublisher _publisher;
        private readonly IConfiguration _configuration;
        private const string ConnectionString = "SqlServer";

        public ApplicationDbContext(
            DbContextOptions options, 
            IPublisher publisher, 
            IConfiguration configuration) : base(options)
        {
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).
                                                            Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_configuration.
                        GetConnectionString(ConnectionString)!);
        }

        public async override Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            var domainEvents = ChangeTracker.Entries<AggregateRoot>()
                .Select(d => d.Entity)
                .Where(d => d.GetDomainEvents().Any())
                .SelectMany(d => d.GetDomainEvents());

            var result = await base.SaveChangesAsync(cancellationToken);

            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }

            return result;
        }
    }
}
