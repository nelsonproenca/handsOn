using System;
using System.Collections.Generic;
using System.Linq;
using Multichannel.Domain.Entities;
using MultiChannel.Persistence;

namespace Multichannel.Application.Tests.Fakes
{
    /// <summary>
    /// LetterDbContextInitializer class.
    /// </summary>
    public class LetterDbContextInitializer
    {
        /// <summary>
        /// Initialize DbContext.
        /// </summary>
        /// <param name="context">DbContext to initialize.</param>
        public static void Initialize(ILetterDbContext context)
        {
            var initializer = new LetterDbContextInitializer();

            initializer.Seed(context);
        }

        private void Seed(ILetterDbContext context)
        {
            if (context.Letters.Any())
            {
                return; // Db has been seeded
            }

            SeedReceivers(context);
            SeedLetters(context);
            SeedTemplates(context);
        }

        private void SeedTemplates(ILetterDbContext context)
        {
            var templates = new[]
            {
                new Template { Description = "Description test1", Path = "c:\\temp\\templates\\letterTemp1.docx" },
                new Template { Description = "Description test2", Path = "c:\\temp\\templates\\letterTemp2.docx" }
            };

            context.Templates.AddRange(templates);

            context.SaveChanges();
        }

        private void SeedLetters(ILetterDbContext context)
        {
            var letters = new[]
            {
                new Letter { Receivers = new List<Receiver>(), SentStatus = false, Template = new Template { Description = "Description test1", Path = "c:\\temp\\templates\\letterTemp2.docx" }, SentDate = DateTime.MinValue },
                new Letter { Receivers = new List<Receiver>(), SentStatus = false, Template = new Template { Description = "Description test2", Path = "c:\\temp\\templates\\letterTemp2.docx" }, SentDate = DateTime.MinValue },
            };

            context.Letters.AddRange(letters);

            context.SaveChanges();
        }

        private void SeedReceivers(ILetterDbContext context)
        {
            var receivers = new[]
{
                new Receiver { Address = "Rua teste, 1", DebtValue = 10000, DueDate = new DateTime(2001, 11, 01), Name = "Fulano de Tal", NumberContract = Guid.NewGuid().ToString().Replace("-", string.Empty), PostalCode = "1234-142" },
                new Receiver { Address = "Rua teste, 2", DebtValue = 20000, DueDate = new DateTime(2002, 12, 02), Name = "Fulano de Tal", NumberContract = Guid.NewGuid().ToString().Replace("-", string.Empty), PostalCode = "1232-142" },
            };

            context.Receivers.AddRange(receivers);

            context.SaveChanges();
        }
    }
}
