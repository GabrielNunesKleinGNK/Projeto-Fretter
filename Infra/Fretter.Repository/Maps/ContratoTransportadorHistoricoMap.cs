using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class ContratoTransportadorHistoricoMap : IEntityTypeConfiguration<ContratoTransportadorHistorico>
    {
        public void Configure(EntityTypeBuilder<ContratoTransportadorHistorico> builder)
        {
            builder.ToTable(nameof(ContratoTransportadorHistorico), "Fretter");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName($"{nameof(ContratoTransportadorHistorico)}Id").HasColumnType("int");
            builder.Property(e => e.ContratoTransportadorId).HasColumnType("int");
            builder.Property(e => e.Descricao).HasColumnType("varchar").HasMaxLength(256);
            builder.Property(e => e.QuantidadeTentativas).HasColumnType("int");
            builder.Property(e => e.TaxaTentativaAdicional).HasColumnType("decimal").HasMaxLength(9);
            builder.Property(e => e.TaxaRetornoRemetente).HasColumnType("decimal").HasMaxLength(9);
            builder.Property(e => e.VigenciaInicial).HasColumnType("DateTime");
            builder.Property(e => e.VigenciaFinal).HasColumnType("DateTime");
            builder.Property(e => e.TransportadorId).HasColumnType("int");
            builder.Property(e => e.TransportadorCnpjId).HasColumnType("int");
            builder.Property(e => e.TransportadorCnpjCobrancaId).HasColumnType("int");
            builder.Property(e => e.EmpresaId).HasColumnType("int");
            builder.Property(e => e.ToleranciaTipoId).HasColumnType("int");
            builder.Property(e => e.ToleranciaInferior).HasColumnType("int");
            builder.Property(e => e.ToleranciaSuperior).HasColumnType("int");
            builder.Property(e => e.PermiteTolerancia).HasColumnType("bit");
            builder.Property(e => e.MicroServicoId).HasColumnType("int");

            //BaseMapping
            builder.Property(e => e.DataCadastro).HasColumnType("DateTime");
            builder.Ignore(e => e.DataAlteracao);
            builder.Property(e => e.UsuarioCadastro).HasColumnType("int");
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Property(e => e.Ativo).HasColumnType("bit");

            //Referencias
            builder.HasOne(x => x.FaturaCiclo).WithMany().HasForeignKey(x => x.FaturaCicloId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Transportador).WithMany().HasForeignKey(x => x.TransportadorId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.TransportadorCnpj).WithMany().HasForeignKey(x => x.TransportadorCnpjId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.TransportadorCnpjCobranca).WithMany().HasForeignKey(x => x.TransportadorCnpjCobrancaId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ToleranciaTipo).WithMany().HasForeignKey(x => x.ToleranciaTipoId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ContratoTransportador).WithMany(y => y.Historicos).HasForeignKey(x => x.ContratoTransportadorId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.CadastroUsuario).WithMany().HasForeignKey(x => x.UsuarioCadastro).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
