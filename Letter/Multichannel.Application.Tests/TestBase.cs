using System;
using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Multichannel.Application.Letters.Mappings;
using Multichannel.Application.Tests.Fakes;
using MultiChannel.Persistence;

namespace Multichannel.Application.Tests
{
    /// <summary>
    /// Test Base class.
    /// </summary>
    public class TestBase
    {
        private readonly SqliteConnection inMemorySqlite;
        private IMapper mapper;

        /// <summary>
        /// Gets or sets automapper instance.
        /// </summary>
        public IMapper Mapper { get => mapper; set => mapper = value; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestBase"/> class.
        /// </summary>
        public TestBase()
        {
            inMemorySqlite = new SqliteConnection("Data Source=:memory:");

            InitializeAutoMapper();
        }

        /// <summary>
        /// Creates a dbContext for testing.
        /// </summary>
        /// <param name="useSqlLite">Use SqlLite. If false, uses memory provider.</param>
        /// <returns>dbContext.</returns>
        public ILetterDbContext GetLetterDbContext(bool useSqlLite = false)
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<LetterDbContext>()
                .UseSqlite(connection)
                .Options;

            using (var context = new LetterDbContext(options))
            {
                context.Database.EnsureCreated();

                LetterDbContextInitializer.Initialize(context);

                return context;
            }
        }

        private void InitializeAutoMapper()
        {
            // Initialize AutoMapper
            // Add any type from the assemblies to load all profiles defined - one type only per assembly
            var config = new MapperConfiguration(cfg => cfg.AddMaps(new[]
            {
                // typeof(CustomerMappings),
                typeof(LettersMappings)
             }));

            config.AssertConfigurationIsValid();

            this.mapper = config.CreateMapper();
        }
    }
}
