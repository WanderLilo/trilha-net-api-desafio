using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Data.Mappings
{
    public class TarefaMapping : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.ToTable("Tarefas");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Titulo).IsRequired().HasMaxLength(200);
        }
    }
}
