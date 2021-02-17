using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace AzureIdentityLivestream.Web.Services.Sql
{
    public class LivestreamContext : DbContext
    {
        public LivestreamContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        public DbSet<Person> People => Set<Person>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Person>()
                .ToTable("Person")
                .HasKey(x => x.Id);
        }
    }
}
