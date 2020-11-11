using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestGraphQLWithHotChocolate
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Opportunity> Opportunities { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<SystemUser> SystemUsers { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AddDataSeeding(modelBuilder);
        }


        private void AddDataSeeding(ModelBuilder modelBuilder)
        {
            var systemUser1Id = new Guid("f0c74a06-1849-4ad7-a28f-3981697c3e71");
            var systemUser1 = new SystemUser()
            {
                Id = systemUser1Id,
                Name = "Daniel Diaz",
                Email = "daniel@diaz.com"
            };
            modelBuilder.Entity<SystemUser>().HasData(systemUser1);

            var account1Id = new Guid("64553146-c836-49eb-ad87-a8c639a4f054");
            var account1 = new Account()
            {
                Id = account1Id,
                Name = "Microsoft",
            };
            var account2Id = new Guid("00553146-c836-49eb-ad87-a8c639a4f054");
            var account2 = new Account()
            {
                Id = account2Id,
                Name = "Google",
            };
            modelBuilder.Entity<Account>().HasData(account1, account2);

            var opportunitity1 = new Opportunity()
            {
                Id = new Guid("bab9706e-deb7-4b55-a145-79245fad3608"),
                AccountId = account1Id,
                Subject = "Oportunidad 1",
                OwnerId = systemUser1Id
            };
            var opportunitity2 = new Opportunity()
            {
                Id = new Guid("47fb11ed-f63f-48a9-83d8-89cea36e4de3"),
                AccountId = account1Id,
                Subject = "Oportunidad 2",
                OwnerId = systemUser1Id
            };
            var opportunitity3 = new Opportunity()
            {
                Id = new Guid("00fb11ed-f63f-48a9-83d8-89cea36e4de3"),
                AccountId = account2Id,
                Subject = "Oportunidad 3",
                OwnerId = systemUser1Id
            };
            modelBuilder.Entity<Opportunity>().HasData(opportunitity1, opportunitity2, opportunitity3);
        }

    }
}
