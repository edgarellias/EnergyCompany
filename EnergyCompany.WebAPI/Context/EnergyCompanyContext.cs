using EnergyCompany.WebAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace EnergyCompany.WebAPI.Context
{
    public class EnergyCompanyContext : DbContext
    {
        public DbSet<Models.Endpoint> Endpoints { get;set; }
        public EnergyCompanyContext(DbContextOptions<EnergyCompanyContext> options) : base(options)
        {
               
        }

    }
}
