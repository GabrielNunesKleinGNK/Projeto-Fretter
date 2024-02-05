using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class TabelaCorreioCanalMap : IEntityTypeConfiguration<TabelaCorreioCanal>
    {
        public void Configure(EntityTypeBuilder<TabelaCorreioCanal> builder)
        {
            builder.ToTable("Tb_MFC_TabelaCorreioCanal", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.TabelaCorreio).HasColumnName("Id_TabelaCorreio").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.CanalId).HasColumnName("Id_Canal").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.VigenciaInicio).HasColumnName("Dt_VigenciaInicio").HasColumnType("date").HasMaxLength(3);
            builder.Property(e => e.VigenciaFim).HasColumnName("Dt_VigenciaFim").HasColumnType("date").HasMaxLength(3);
            builder.Property(e => e.Inclusao).HasColumnName("Dt_Inclusao").HasColumnType("smalldatetime").HasMaxLength(4);
            builder.Property(e => e.MicroServicoId).HasColumnName("Id_MicroServico").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.Cep).HasColumnName("Nr_Cep").HasColumnType("int").HasMaxLength(4);

            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);
            builder.Ignore(e => e.Ativo);

        }
    }
}
