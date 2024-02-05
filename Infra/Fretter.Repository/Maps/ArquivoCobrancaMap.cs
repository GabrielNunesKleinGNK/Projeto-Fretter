using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class ArquivoCobrancaMap : IEntityTypeConfiguration<ArquivoCobranca>
    {
        public void Configure(EntityTypeBuilder<ArquivoCobranca> builder)
        {
            builder.ToTable("ArquivoCobranca", "Fretter");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName($"{nameof(ArquivoCobranca)}Id").HasColumnType("int");
			builder.Property(e => e.FaturaId).HasColumnType("int").IsRequired();
            builder.Property(e => e.IdentificacaoRemetente).HasColumnType("varchar").HasMaxLength(35);
            builder.Property(e => e.IdentificacaoDestinatario).HasColumnType("varchar").HasMaxLength(35);
            builder.Property(e => e.Data).HasColumnType("datetime");
            builder.Property(e => e.QtdTotal).HasColumnType("int");
            builder.Property(e => e.QtdItens).HasColumnType("int");
            builder.Property(e => e.ValorTotal).HasColumnType("decimal").HasMaxLength(10);
            builder.Property(e => e.ArquivoUrl).HasColumnType("varchar").HasMaxLength(512);

            //BaseMapping
            builder.Property(e => e.Ativo).HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.UsuarioCadastro).HasColumnType("int");
            builder.Property(e => e.UsuarioAlteracao).HasColumnType("int");
            builder.Property(e => e.DataCadastro).HasColumnType("datetime");
            builder.Property(e => e.DataAlteracao).HasColumnType("datetime");
        }
    }
}
