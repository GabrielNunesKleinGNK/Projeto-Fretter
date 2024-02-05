using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class ConciliacaoStatusMap : IEntityTypeConfiguration<ConciliacaoStatus>
    {
        public void Configure(EntityTypeBuilder<ConciliacaoStatus> builder)
        {
            builder.ToTable("ConciliacaoStatus", "Fretter");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName($"{nameof(ConciliacaoStatus)}Id").HasColumnType("int");
			builder.Property(e => e.Nome).HasColumnType("varchar").HasMaxLength(128);

            //BaseMapping
            builder.Property(e => e.Ativo).HasColumnName("Ativo").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.UsuarioCadastro).HasColumnType("int");
            builder.Property(e => e.UsuarioAlteracao).HasColumnType("int");
            builder.Property(e => e.DataCadastro).HasColumnType("DateTime");
            builder.Property(e => e.DataAlteracao).HasColumnType("DateTime");
        }
    }
}
