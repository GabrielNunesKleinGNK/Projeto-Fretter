using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class FaturaStatusAcaoMap : IEntityTypeConfiguration<FaturaStatusAcao>
    {
        public void Configure(EntityTypeBuilder<FaturaStatusAcao> builder)
        {
            builder.ToTable("FaturaStatusAcao", "Fretter");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName($"{nameof(FaturaStatusAcao)}Id").HasColumnType("int");
            builder.Property(e => e.FaturaStatusId).HasColumnName("FaturaStatusId").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.FaturaAcaoId).HasColumnName("FaturaAcaoId").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.FaturaStatusResultadoId).HasColumnName("FaturaStatusResultadoId").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.Visivel).HasColumnName("Visivel").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.InformaMotivo).HasColumnName("InformaMotivo").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.Ativo).HasColumnName("Ativo").HasColumnType("bit").HasMaxLength(1);

            builder.HasOne(e => e.FaturaAcao).WithMany().HasForeignKey(e => e.FaturaAcaoId).OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);
        }
    }
}
