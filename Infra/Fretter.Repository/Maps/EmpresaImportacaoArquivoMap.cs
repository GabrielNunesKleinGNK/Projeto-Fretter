using Fretter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fretter.Repository.Maps
{
    class EmpresaImportacaoArquivoMap : IEntityTypeConfiguration<EmpresaImportacaoArquivo>
    {
        public void Configure(EntityTypeBuilder<EmpresaImportacaoArquivo> builder)
        {
            builder.ToTable("EmpresaImportacaoArquivo", "Fretter");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName($"{nameof(EmpresaImportacaoArquivo)}Id").HasColumnType("int");
            builder.Property(e => e.Nome).HasColumnName("Nome").HasColumnType("varchar").HasMaxLength(512);
            builder.Property(e => e.Descricao).HasColumnName("Descricao").HasColumnType("varchar").HasMaxLength(2048);
            builder.Property(e => e.EmpresaId).HasColumnName("EmpresaId").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.ArquivoURL).HasColumnName("ArquivoURL").HasColumnType("varchar").HasMaxLength(512);
            builder.Property(e => e.QuantidadeEmpresa).HasColumnName("QuantidadeEmpresa").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.Processado).HasColumnName("Processado").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.Sucesso).HasColumnName("Sucesso").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.DataCadastro).HasColumnName("DataCadastro").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.UsuarioCadastro).HasColumnName("UsuarioCadastro").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.Ativo).HasColumnName("Ativo").HasColumnType("bit").HasMaxLength(1);

            builder.Ignore(e => e.UsuarioAlteracao);            
            builder.Ignore(e => e.DataAlteracao);
        }
    }
}
