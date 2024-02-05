using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class EntregaTransporteTipoMap : IEntityTypeConfiguration<EntregaTransporteTipo>
    {
        public void Configure(EntityTypeBuilder<EntregaTransporteTipo> builder)
        {
            builder.ToTable("Tb_Edi_EntregaTransporteTipo", "dbo");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.EntregaTransporteServicoId).HasColumnName("Id_EntregaTransporteServico").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Nome).HasColumnName("Ds_Nome").HasColumnType("varchar").HasMaxLength(64);
			builder.Property(e => e.URL).HasColumnName("Ds_URL").HasColumnType("varchar").HasMaxLength(32);
			builder.Property(e => e.Layout).HasColumnName("Ds_Layout").HasColumnType("varchar").HasMaxLength(512);
			builder.Property(e => e.ApiKey).HasColumnName("Ds_ApiKey").HasColumnType("varchar").HasMaxLength(512);
			builder.Property(e => e.Usuario).HasColumnName("Ds_Usuario").HasColumnType("varchar").HasMaxLength(128);
			builder.Property(e => e.Senha).HasColumnName("Ds_Senha").HasColumnType("varchar").HasMaxLength(128);
			builder.Property(e => e.DiasValidadeMinimo).HasColumnName("Nr_DiasValidadeMinimo").HasColumnType("smallint").HasMaxLength(2);
			builder.Property(e => e.DiasValidadeMaximo).HasColumnName("Nr_DiasValidadeMaximo").HasColumnType("smallint").HasMaxLength(2);
			builder.Property(e => e.Alias).HasColumnName("Cd_Alias").HasColumnType("varchar").HasMaxLength(32);
			builder.Property(e => e.CodigoIntegracao).HasColumnName("Cd_CodigoIntegracao").HasColumnType("varchar").HasMaxLength(32);
			builder.Property(e => e.Producao).HasColumnName("Flg_Producao").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);

			builder.Ignore(e => e.UsuarioCadastro);
			builder.Ignore(e => e.UsuarioAlteracao);
			builder.Ignore(e => e.DataCadastro);
			builder.Ignore(e => e.DataAlteracao);

			builder.HasOne(d => d.EntregaTransporteServico)
				 .WithMany(p => p.EntregaTransporteTipos)
				 .HasForeignKey(d => d.Id)
				 .OnDelete(DeleteBehavior.ClientSetNull);
		}
    }
}
