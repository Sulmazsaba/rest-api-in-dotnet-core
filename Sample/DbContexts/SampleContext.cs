using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sample.Entities;

namespace Sample.DbContexts
{
    public class SampleContext : DbContext
    {
        public SampleContext(DbContextOptions<SampleContext> options):base(options)
        {
            
        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasData(new List<Company>()
            {
                new Company()
                {
                    Id = Guid.Parse("e4978846-3eec-432d-b54d-db08b75a2a83"),
                    Activity = "web developing",
                    Name = "Best Company For Ever",
                    NumberOfStaff = 10,
                },
                new Company()
                {
                    Id = Guid.Parse("8702b207-d45e-4328-ae36-0216b95e19f5"),
                    Name = "Microsoft",
                    Activity = "developing software",
                    NumberOfStaff = 400,
                },
                new Company()
                {
                    Id = Guid.Parse("c8e7f681-fa1d-4e96-a897-d19439441e35"),
                    Name = "Huawei",
                    NumberOfStaff = 100,
                    Activity = "Hardware Manufacturing",
                    
                }
            });

            modelBuilder.Entity<JobPosition>().HasData(
                new JobPosition()
                {
                    Id = Guid.Parse("b01bf89a-8125-4518-93e9-6c826b3f4d73"),
                    CompanyId = Guid.Parse("e4978846-3eec-432d-b54d-db08b75a2a83"),
                    Title = ".Net Developer",
                    Degree = Degree.Senior,
                    Description = ".experienced with entity frame work," +
                                  ".good knowledge of sql server"
                },
                new JobPosition()
                {
                    Id = Guid.Parse("c8edaad1-c78f-4034-a447-130d12a2d59e"),
                    CompanyId = Guid.Parse("8702b207-d45e-4328-ae36-0216b95e19f5"),
                    Degree = Degree.Expert,
                    Title = "Dev op",
                    Description = ".having good knowledge of IIS",
                },
                new JobPosition()
                {
                    Id = Guid.Parse("ce159fbb-84aa-4d8a-aaf5-ec1fb727a664"),
                    CompanyId = Guid.Parse("8702b207-d45e-4328-ae36-0216b95e19f5"),
                    Degree = Degree.Junior,
                    Title = "Front End Developer",
                    Description = ".Experienced with CSS3 and Html5"
                }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
