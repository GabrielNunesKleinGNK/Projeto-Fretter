using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class TabelaArquivoStatusMap : IEntityTypeConfiguration<TabelaArquivoStatus>
    {
        public void Configure(EntityTypeBuilder<TabelaArquivoStatus> builder)
        {
            builder.ToTable("Tb_MF_TabelaArquivoStatus", "dbo");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Status).HasColumnName("Ds_Status").HasColumnType("varchar").HasMaxLength(200);
			builder.Property(e => e.Criacao).HasColumnName("Dt_Criacao").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);

            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);
        }
    }
}
