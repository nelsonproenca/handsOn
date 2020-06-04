using System;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Multichannel.Application.Letters.Queries;
using Multichannel.Application.Tests.Fakes;
using MultiChannel.Persistence;
using Xunit;

namespace Multichannel.Application.Tests.Letters.Queries
{
    /// <summary>
    /// Letters Queries Tests class.
    /// </summary>
    public class LettersQueriesTests : TestBase
    {
        /// <summary>
        /// Get one.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        public async Task GetOneTest(int id)
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
            }

            using (var context = new LetterDbContext(options))
            {
                LettersQueries lettersQueries = GetLettersQueries(context);

                var result = await lettersQueries.GetOneAsync(id);

                if (result == null)
                {
                    Assert.Null(result);
                }
                else
                {
                    Assert.True(result.LetterID > 0);
                    Assert.True(id.Equals(result.LetterID));
                }
            }
        }

        /// <summary>
        /// Get by request.
        /// </summary>
        /// <param name="request">request id.</param> 
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Theory]
        [InlineData("4BCFF718-BE73-46BA-8FAF-AC3C666C183C")]
        [InlineData("6051677D-41DD-4712-8A5D-1196A539049C")]
        public async Task GetByRequestTests(string request)
        {
            Guid requestId = Guid.Parse(request);

            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<LetterDbContext>()
                .UseSqlite(connection)
                .Options;

            using (var context = new LetterDbContext(options))
            {
                context.Database.EnsureCreated();

                LetterDbContextInitializer.Initialize(context);
            }

            using (var context = new LetterDbContext(options))
            {
                LettersQueries lettersQueries = GetLettersQueries(context);

                var result = await lettersQueries.GetByRequestAsync(requestId);

                if (result == null)
                {
                    Assert.Null(result);
                }
                else
                {
                    Assert.True(result.LetterID > 0);
                    Assert.True(requestId.Equals(result.RequestID));
                }
            }
        }

        /// <summary>
        /// Get by consumer.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task GetByConsumerTests()
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
            }

            using (var context = new LetterDbContext(options))
            {
                LettersQueries lettersQueries = GetLettersQueries(context);

                var result = await lettersQueries.GetByTenantAsync(Guid.NewGuid());

                if (result == null)
                {
                    Assert.Null(result);
                }
                else
                {
                    Assert.NotNull(result);
                    Assert.True(result.Count > 0);
                }
            }
        }

        private LettersQueries GetLettersQueries(ILetterDbContext context)
        {
            return new LettersQueries(context, null, Mapper);
        }
    }
}
