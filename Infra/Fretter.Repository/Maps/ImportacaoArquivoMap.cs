using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class ImportacaoArquivoMap : IEntityTypeConfiguration<ImportacaoArquivo>
    {
        public void Configure(EntityTypeBuilder<ImportacaoArquivo> builder)
        {
            builder.ToTable("ImportacaoArquivo", "Fretter");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName($"{nameof(ImportacaoArquivo)}Id").HasColumnType("int");
            builder.Property(e => e.Nome).HasColumnType("varchar").HasMaxLength(128);
            builder.Property(e => e.Diretorio).HasColumnType("varchar").HasMaxLength(512);
            builder.Property(e => e.Identificador).HasColumnType("varchar").HasMaxLength(256);
            builder.Property(e => e.EmpresaId).HasColumnType("int");
            builder.Property(e => e.TransportadorId).HasColumnType("int");
            builder.Property(e => e.ImportacaoArquivoStatusId).HasColumnType("int").IsRequired();
            builder.Property(e => e.ImportacaoArquivoTipoId).HasColumnType("int").IsRequired();
            builder.Property(e => e.DataProcessamento).HasColumnType("datetime");
            builder.Property(e => e.Mensagem).HasColumnType("varchar").HasMaxLength(512);
            builder.Property(e => e.ImportacaoConfiguracaoId).HasColumnType("int");
            builder.Property(e => e.ImportacaoArquivoTipoItemId).HasColumnType("int");

            //BaseMapping
            builder.Property(e => e.Ativo).HasColumnName("Ativo").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.UsuarioCadastro).HasColumnName("UsuarioCadastro").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.UsuarioAlteracao).HasColumnName("UsuarioAlteracao").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.DataCadastro).HasColumnName("DataCadastro").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.DataAlteracao).HasColumnName("DataAlteracao").HasColumnType("datetime").HasMaxLength(8);

            builder.HasOne(e => e.ImportacaoArquivoStatus).WithMany(x => x.ImportacaoArquivos).HasForeignKey(e => e.ImportacaoArquivoStatusId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.ImportacaoArquivoTipo).WithMany().HasForeignKey(e => e.ImportacaoArquivoTipoId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.ImportacaoArquivoTipoItem).WithMany().HasForeignKey(e => e.ImportacaoArquivoTipoItemId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
