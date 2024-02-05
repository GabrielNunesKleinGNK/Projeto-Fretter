using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class ArquivoCobrancaDocumentoMap : IEntityTypeConfiguration<ArquivoCobrancaDocumento>
    {
        public void Configure(EntityTypeBuilder<ArquivoCobrancaDocumento> builder)
        {
            builder.ToTable("ArquivoCobrancaDocumento", "Fretter");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName($"{nameof(ArquivoCobrancaDocumento)}Id").HasColumnType("int");
			builder.Property(e => e.ArquivoCobrancaId).HasColumnType("int").IsRequired();
            builder.Property(e => e.FilialEmissora).HasColumnType("varchar").HasMaxLength(10);
            builder.Property(e => e.Tipo).HasColumnType("int");
            builder.Property(e => e.Serie).HasColumnType("varchar").HasMaxLength(3);
            builder.Property(e => e.Numero).HasColumnType("varchar").HasMaxLength(10);
            builder.Property(e => e.DataEmissao).HasColumnType("datetime");
            builder.Property(e => e.DataVencimento).HasColumnType("datetime");
            builder.Property(e => e.ValorTotal).HasColumnType("decimal").HasMaxLength(9);
            builder.Property(e => e.TipoCobranca).HasColumnType("varchar").HasMaxLength(3);
            builder.Property(e => e.CFOP).HasColumnType("varchar").HasMaxLength(5);
            builder.Property(e => e.CodigoAcessoNFe).HasColumnType("varchar").HasMaxLength(9);
            builder.Property(e => e.ChaveAcessoNFe).HasColumnType("varchar").HasMaxLength(45);
            builder.Property(e => e.ProtocoloNFe).HasColumnType("varchar").HasMaxLength(15);

            //BaseMapping
            builder.Property(e => e.Ativo).HasColumnType("bit").HasMaxLength(1);
            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);

            builder.HasOne(x => x.ArquivoCobranca).WithMany(y => y.ArquivoCobrancaDocumentos).HasForeignKey(x => x.ArquivoCobrancaId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
