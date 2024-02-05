using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class ArquivoCobrancaDocumentoItemMap : IEntityTypeConfiguration<ArquivoCobrancaDocumentoItem>
    {
        public void Configure(EntityTypeBuilder<ArquivoCobrancaDocumentoItem> builder)
        {
            builder.ToTable("ArquivoCobrancaDocumentoItem", "Fretter");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName($"{nameof(ArquivoCobrancaDocumentoItem)}Id").HasColumnType("int");
			builder.Property(e => e.ArquivoCobrancaDocumentoId).HasColumnType("int").IsRequired();
            builder.Property(e => e.Filial).HasColumnType("varchar").HasMaxLength(10);
            builder.Property(e => e.Serie).HasColumnType("varchar").HasMaxLength(5);
            builder.Property(e => e.Numero).HasColumnType("varchar").HasMaxLength(12);
            builder.Property(e => e.DataEmissao).HasColumnType("datetime");
            builder.Property(e => e.ValorFrete).HasColumnType("decimal").HasMaxLength(9);
            builder.Property(e => e.DocumentoRemetente).HasColumnType("varchar").HasMaxLength(14);
            builder.Property(e => e.DocumentoEmissor).HasColumnType("varchar").HasMaxLength(14);
            builder.Property(e => e.DocumentoDestinatario).HasColumnType("varchar").HasMaxLength(14);
            builder.Property(e => e.UfEmbarcadora).HasColumnType("varchar").HasMaxLength(2);
            builder.Property(e => e.UfEmissora).HasColumnType("varchar").HasMaxLength(2);
            builder.Property(e => e.UfDestinataria).HasColumnType("varchar").HasMaxLength(2);
            builder.Property(e => e.Devolucao).HasColumnType("bit");

            //BaseMapping
            builder.Property(e => e.Ativo).HasColumnType("bit").HasMaxLength(1);
            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);

            builder.HasOne(x => x.ArquivoCobrancaDocumento).WithMany(y => y.CobrancaDocumentoItens).HasForeignKey(x => x.ArquivoCobrancaDocumentoId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
