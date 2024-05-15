using System.Diagnostics;
using System.Threading;
using CandidateJourney.Domain;
using CandidateJourney.Infrastructure.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CandidateJourney.Infrastructure
{
    public class CandidateJourneyDbContext : DbContext
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        private bool _updating;

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<ContactHistory> ContactHistories { get; set; }

        public CandidateJourneyDbContext(DbContextOptions<CandidateJourneyDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CandidateJourneyDbContext).Assembly);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            if (!_updating)
            {
                _updating = true;
                UpdateBeforeSaveChanges();
                _updating = false;
            }
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            if (!_updating)
            {
                _updating = true;
                UpdateBeforeSaveChanges();
                _updating = false;
            }
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateBeforeSaveChanges()
        {
            var addedEvents = ChangeTracker.Entries<Event>().Where(eventEntry => eventEntry.State == EntityState.Added);
            var addedContactHistories = ChangeTracker.Entries<ContactHistory>().Where(eventEntry => eventEntry.State == EntityState.Added);
            var changedEvents = ChangeTracker.Entries<Event>().Where(eventEntry => eventEntry.State == EntityState.Modified);
            var deletedEvents = ChangeTracker.Entries<Event>().Where(eventEntry => eventEntry.State == EntityState.Deleted);
            var deletedUsers = ChangeTracker.Entries<User>().Where(userEntry => userEntry.State == EntityState.Deleted);
            var id = "149D6235-13E5-4149-BDBC-19F7AB8046A4";

            foreach (var addedEvent in addedEvents)
            {
                addedEvent.Property<Guid>("CreatedById").CurrentValue = Guid.Parse(id);
                addedEvent.Property<DateTime>("CreatedOn").CurrentValue = DateTime.UtcNow;
            }
            foreach (var changedEvent in changedEvents)
            {
                changedEvent.Property<Guid?>("UpdatedById").CurrentValue = Guid.Parse(id);
                changedEvent.Property<DateTime?>("UpdatedOn").CurrentValue = DateTime.UtcNow;
            }
            foreach (var deletedEvent in deletedEvents)
            {
                deletedEvent.State = EntityState.Modified;
                deletedEvent.Property(@event => @event.IsDeleted).CurrentValue = true;
            }
            foreach (var deletedUser in deletedUsers)
            {
                deletedUser.State = EntityState.Modified;
                deletedUser.Property(user => user.IsDeleted).CurrentValue = true;
            }

            foreach (var contactHistory in addedContactHistories)
            {
                contactHistory.Property<Guid>("CreatedById").CurrentValue = Guid.NewGuid();
                contactHistory.Property<DateTime>("CreatedOn").CurrentValue = DateTime.UtcNow;
            }
        }
    }
}