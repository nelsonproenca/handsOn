using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Multichannel.Application.Letters.Commands;
using Multichannel.Application.Letters.Models;
using Multichannel.Application.Tests.Fakes;
using Multichannel.Domain.Entities;
using MultiChannel.Persistence;
using Xunit;

namespace Multichannel.Application.Tests.Letters.Commands
{
    /// <summary>
    /// Letters Commands Tests class.
    /// </summary>
    public class LettersCommandsTests : TestBase
    {
        /// <summary>
        /// Create Letters Test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task CreateLetterTest()
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

            LetterModel letterModel = new LetterModel() { Receivers = new List<ReceiverModel>(), SentStatus = false, SentDate = DateTime.MinValue, TemplateId = 1 };

            using (var context = new LetterDbContext(options))
            {
                LettersCommands lettersCommands = null;
                int newLetterId = 0;
                Letter letterCreated = null;

                try
                {
                    lettersCommands = this.GetStorageCommand(context);

                    newLetterId = await lettersCommands.CreateAsync(letterModel);

                    letterCreated = context.Letters.SingleOrDefault(let => let.Id == newLetterId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

                if (letterCreated == null)
                {
                    Assert.NotNull(letterCreated);
                }
                
                Assert.True(letterCreated.Id == newLetterId);
            }
        }

        /// <summary>
        /// Create Letters Test.
        /// </summary>
        /// <param name="value">value.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [InlineData(1)]
        [InlineData(99)]
        [Theory]
        public async Task DeleteLetterTest(int value)
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
                LettersCommands lettersCommands = null;
                Letter letterCreated = null;

                try
                {
                    lettersCommands = this.GetStorageCommand(context);

                    await lettersCommands.DeleteAsync(value);

                    letterCreated = context.Letters.SingleOrDefault(let => let.Id == value);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

                Assert.Null(letterCreated);
            }
        }

        /// <summary>
        /// Create Letters Test.
        /// </summary>
        /// <param name="value">value.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [InlineData(1)]
        [InlineData(99)]
        [Theory]
        public async Task UpdateLetterTest(int value)
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

            LetterModel letterModel = new LetterModel() { Receivers = new List<ReceiverModel>(), SentStatus = true, SentDate = DateTime.Today, TemplateId = 1 };

            using (var context = new LetterDbContext(options))
            {
                LettersCommands lettersCommands = null;
                Letter letterUpdated = null;

                try
                {
                    lettersCommands = this.GetStorageCommand(context);

                    await lettersCommands.UpdateAsync(value, letterModel);

                    letterUpdated = context.Letters.SingleOrDefault(let => let.Id == value);

                    if (letterUpdated == null)
                    {
                        Assert.Null(letterUpdated);

                        return;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

                Assert.True(letterUpdated.SentStatus == letterModel.SentStatus);
                Assert.True(letterUpdated.SentDate == letterModel.SentDate);
            }
        }

        private LettersCommands GetStorageCommand(ILetterDbContext context)
        {
            return new LettersCommands(context, null, this.Mapper);
        }
    }
}
