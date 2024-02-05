using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class EntregaDevolucaoHistoricoMap : IEntityTypeConfiguration<EntregaDevolucaoHistorico>
    {
        public void Configure(EntityTypeBuilder<EntregaDevolucaoHistorico> builder)
        {
            builder.ToTable("Tb_Edi_EntregaDevolucaoHistorico", "dbo");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.EntregaDevolucaoId).HasColumnName("Id_EntregaDevolucao").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.CodigoColeta).HasColumnName("Cd_CodigoColeta").HasColumnType("varchar").HasMaxLength(64);
			builder.Property(e => e.CodigoRastreio).HasColumnName("Cd_CodigoRastreio").HasColumnType("varchar").HasMaxLength(64);
			builder.Property(e => e.Validade).HasColumnName("Dt_Validade").HasColumnType("date").HasMaxLength(3);
			builder.Property(e => e.Mensagem).HasColumnName("Ds_Mensagem").HasColumnType("varchar").HasMaxLength(512);
			builder.Property(e => e.Retorno).HasColumnName("Ds_Retorno").HasColumnType("varchar").HasMaxLength(512);
			builder.Property(e => e.EntregaDevolucaoStatusAnteriorId).HasColumnName("Id_EntregaDevolucaoStatusAnterior").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.EntregaDevolucaoStatusAtualId).HasColumnName("Id_EntregaDevolucaoStatusAtual").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.UsuarioCadastro).HasColumnName("Id_UsuarioCadastro").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.XmlRetorno).HasColumnName("Ds_XmlRetorno").HasColumnType("varchar").HasMaxLength(4096);
			builder.Property(e => e.Inclusao).HasColumnName("Dt_Inclusao").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);

			builder.HasOne(e => e.StatusAtual).WithMany().HasForeignKey(e => e.EntregaDevolucaoStatusAtualId).OnDelete(DeleteBehavior.Restrict);
			builder.HasOne(e => e.StatusAnterior).WithMany().HasForeignKey(e => e.EntregaDevolucaoStatusAnteriorId).OnDelete(DeleteBehavior.Restrict);

			builder.Ignore(e => e.UsuarioAlteracao);
			builder.Ignore(e => e.DataCadastro);
			builder.Ignore(e => e.DataAlteracao);
		}
    }
}
