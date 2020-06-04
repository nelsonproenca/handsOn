using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Multichannel.Domain.Entities;

namespace MultiChannel.Persistence
{
    /// <summary>
    /// Letter DbContext class.
    /// </summary>
    public class LetterDbContext : DbContext, ILetterDbContext
    {
        private readonly string connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="LetterDbContext"/> class.
        /// </summary>
        /// <param name="options">DbContext options.</param>
        /// <param name="configuration">configuration.</param>
        public LetterDbContext(DbContextOptions<LetterDbContext> options, IConfiguration configuration)
            : base(options)
        {
            connectionString = ConfigurationExtensions.GetConnectionString(configuration, "LetterConnectionString");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LetterDbContext"/> class.
        /// </summary>
        /// <param name="options">DbContext options.</param>
        public LetterDbContext(DbContextOptions<LetterDbContext> options)
            : base(options)
        {
        }

        /// <inheritdoc/>
        public DbSet<Letter> Letters { get; set; }

        /// <inheritdoc/>
        public DbSet<Template> Templates { get; set; }

        /// <inheritdoc/>
        public DbSet<Receiver> Receivers { get; set; }

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (this.connectionString != null)
                {
                    optionsBuilder.UseSqlServer(connectionString);
                }
                else
                {
                    optionsBuilder.UseSqlServer(@"Server=WSPTVWDBMSSRV03\\APPSDEV;Database=EFProviders.InMemory;Trusted_Connection=True;ConnectRetryCount=0");
                }
            }

            base.OnConfiguring(optionsBuilder);
        }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Letter>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Template>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Receiver>().HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<Letter>().HasKey(key => key.Id);
            modelBuilder.Entity<Template>().HasKey(key => key.Id);
            modelBuilder.Entity<Receiver>().HasKey(key => key.Id);

            modelBuilder.Entity<Template>().Property(p => p.Path).HasColumnType("varchar (2048)");
            modelBuilder.Entity<Template>().Property(p => p.Description).HasColumnType("varchar (512)");

            modelBuilder.Entity<Receiver>().Property(p => p.Address).HasColumnType("varchar (512)");
            modelBuilder.Entity<Receiver>().Property(p => p.DebtValue).HasColumnType("decimal");
            modelBuilder.Entity<Receiver>().Property(p => p.Name).HasColumnType("varchar (100)");
            modelBuilder.Entity<Receiver>().Property(p => p.NumberContract).HasColumnType("varchar (50)");
            modelBuilder.Entity<Receiver>().Property(p => p.PostalCode).HasColumnType("varchar (10)");
        }
    }
}
