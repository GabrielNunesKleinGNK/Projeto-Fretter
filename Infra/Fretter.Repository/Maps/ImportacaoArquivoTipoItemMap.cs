using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class ImportacaoArquivoTipoItemMap : IEntityTypeConfiguration<ImportacaoArquivoTipoItem>
    {
        public void Configure(EntityTypeBuilder<ImportacaoArquivoTipoItem> builder)
        {
            builder.ToTable("ImportacaoArquivoTipoItem", "Fretter");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName($"{nameof(ImportacaoArquivoTipoItem)}Id").HasColumnType("int");
            builder.Property(e => e.ImportacaoArquivoTipoId).HasColumnType("int");            

            //BaseMapping
            builder.Property(e => e.Ativo).HasColumnName("Ativo").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.UsuarioCadastro).HasColumnName("UsuarioCadastro").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.DataCadastro).HasColumnName("DataCadastro").HasColumnType("datetime").HasMaxLength(8);

            builder.HasOne(e => e.ImportacaoArquivoTipo).WithMany(x => x.ImportacaoArquivoTipoItems).HasForeignKey(e => e.ImportacaoArquivoTipoId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.ConciliacaoTipo).WithMany(x => x.ImportacaoArquivoTipoItems).HasForeignKey(e => e.ConciliacaoTipoId).OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataAlteracao);
        }
    }
}
