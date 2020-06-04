using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MultiChannel.Persistence
{
    /// <summary>
    /// Letter dbcontext design time factory class.
    /// </summary>
    public class LetterDbContextDesignTimeFactory
    {
        private const string ConnectionName = "LetterConnectionString";

        /// <summary>
        /// Create DbContext.
        /// </summary>
        /// <param name="args">array args.</param>
        /// <returns>EmailDbContex.</returns>
        public LetterDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();

            var optionsBuilder = new DbContextOptionsBuilder<LetterDbContext>();

            string connString = configuration.GetConnectionString(ConnectionName);

            optionsBuilder.UseSqlServer(connString);

            return new LetterDbContext(optionsBuilder.Options, configuration);
        }
    }
}
