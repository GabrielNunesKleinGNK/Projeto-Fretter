using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class ContratoTransportadorMap : IEntityTypeConfiguration<ContratoTransportador>
    {
        public void Configure(EntityTypeBuilder<ContratoTransportador> builder)
        {
            builder.ToTable(nameof(ContratoTransportador), "Fretter");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName($"{nameof(ContratoTransportador)}Id").HasColumnType("int");
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
            builder.Property(e => e.ToleranciaInferior).HasColumnType("decimal").HasMaxLength(6);
            builder.Property(e => e.ToleranciaSuperior).HasColumnType("decimal").HasMaxLength(6);
            builder.Property(e => e.PermiteTolerancia).HasColumnType("bit");
            builder.Property(e => e.FaturaAutomatica).HasColumnType("bit");
            builder.Property(e => e.MicroServicoId).HasColumnType("int");
            builder.Property(e => e.RecotaPesoTransportador).HasColumnType("bit");
            builder.Property(e => e.ConciliaEntregaFinalizada).HasColumnType("bit");

            //BaseMapping
            builder.Property(e => e.DataCadastro).HasColumnType("DateTime");
            builder.Property(e => e.DataAlteracao).HasColumnType("DateTime");
            builder.Property(e => e.UsuarioCadastro).HasColumnType("int");
            builder.Property(e => e.UsuarioAlteracao).HasColumnType("int");
            builder.Property(e => e.Ativo).HasColumnType("bit");

            //Referencias
            builder.HasOne(x => x.FaturaCiclo).WithMany(y => y.ContratoTransportadores).HasForeignKey(x => x.FaturaCicloId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Transportador).WithMany().HasForeignKey(x => x.TransportadorId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.TransportadorCnpj).WithMany().HasForeignKey(x => x.TransportadorCnpjId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.TransportadorCnpjCobranca).WithMany().HasForeignKey(x => x.TransportadorCnpjCobrancaId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ToleranciaTipo).WithMany().HasForeignKey(x => x.ToleranciaTipoId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.CadastroUsuario).WithMany().HasForeignKey(x => x.UsuarioCadastro).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.AlteracaoUsuario).WithMany().HasForeignKey(x => x.UsuarioAlteracao).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
