using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Data.Context
{
    public class OrganizadorContext : DbContext
    {
        public OrganizadorContext(DbContextOptions<OrganizadorContext> options) : base(options)
        {

        }

        public DbSet<Tarefa> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrganizadorContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}