using Fretter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fretter.Repository.Maps
{
    class ContratoTransportadorArquivoTipoMap : IEntityTypeConfiguration<ContratoTransportadorArquivoTipo>
    {
        public void Configure(EntityTypeBuilder<ContratoTransportadorArquivoTipo> builder)
        {
            builder.ToTable("ContratoTransportadorArquivoTipo", "Fretter");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName($"{nameof(ContratoTransportadorArquivoTipo)}Id").HasColumnType("int");
            builder.Property(e => e.EmpresaId).HasColumnName("EmpresaId").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.TransportadorId).HasColumnName("TransportadorId").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.ImportacaoArquivoTipoItemId).HasColumnName("ImportacaoArquivoTipoItemId").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.Alias).HasColumnName("Alias").HasColumnType("varchar").HasMaxLength(64);
            
            builder.Property(e => e.DataCadastro).HasColumnName("DataCadastro").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.UsuarioCadastro).HasColumnName("UsuarioCadastro").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.Ativo).HasColumnName("Ativo").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.DataAlteracao).HasColumnName("DataAlteracao").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.UsuarioAlteracao).HasColumnName("UsuarioAlteracao").HasColumnType("int").HasMaxLength(4);

           // builder.HasOne(e => e.ImportacaoArquivoTipoItem).WithMany().HasForeignKey(e => e.ImportacaoArquivoTipoItemId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
