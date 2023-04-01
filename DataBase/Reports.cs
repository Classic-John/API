using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Tema_1.DataBase
{
    public class Reports : DbContext
    {
        private readonly IConfiguration _configuration;

        public Reports(DbContextOptions<Reports> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<City> Cities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SQLEXPRESS"));
        }

        public class City
        {
            [Key]
            public int Id { get; set; }
            public string Name { get; set; }
            public int CovidCases { get; set; }
            public int Cured { get; set; }
            public int Deceased { get; set; }
        }

    }
}