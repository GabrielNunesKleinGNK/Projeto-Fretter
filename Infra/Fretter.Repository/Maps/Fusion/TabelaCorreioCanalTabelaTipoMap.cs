using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class TabelaCorreioCanalTabelaTipoMap : IEntityTypeConfiguration<TabelaCorreioCanalTabelaTipo>
    {
        public void Configure(EntityTypeBuilder<TabelaCorreioCanalTabelaTipo> builder)
        {
            builder.ToTable("Tb_MFC_TabelaCorreioCanalTabelaTipo", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.TabelaCorreioCanalId).HasColumnName("Id_TabelaCorreioCanal").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.TabelaTipoId).HasColumnName("Id_TabelaTipo").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.Inclusao).HasColumnName("Dt_Inclusao").HasColumnType("smalldatetime").HasMaxLength(4);

            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);
            builder.Ignore(e => e.Ativo);
        }
    }
}
