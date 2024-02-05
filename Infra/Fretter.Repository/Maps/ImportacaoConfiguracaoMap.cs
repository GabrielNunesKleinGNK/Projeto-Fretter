using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class ImportacaoConfiguracaoMap : IEntityTypeConfiguration<ImportacaoConfiguracao>
    {
        public void Configure(EntityTypeBuilder<ImportacaoConfiguracao> builder)
        {
            builder.ToTable("ImportacaoConfiguracao", "Fretter");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("ImportacaoConfiguracaoId").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.ImportacaoConfiguracaoTipoId).HasColumnName("ImportacaoConfiguracaoTipoId").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.EmpresaId).HasColumnName("EmpresaId").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.TransportadorId).HasColumnName("TransportadorId").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.ImportacaoArquivoTipoId).HasColumnName("ImportacaoArquivoTipoId").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.Diretorio).HasColumnName("Diretorio").HasColumnType("varchar").HasMaxLength(512);
            builder.Property(e => e.DiretorioSucesso).HasColumnName("DiretorioSucesso").HasColumnType("varchar").HasMaxLength(256);
            builder.Property(e => e.DiretorioErro).HasColumnName("DiretorioErro").HasColumnType("varchar").HasMaxLength(256);
            builder.Property(e => e.Usuario).HasColumnName("Usuario").HasColumnType("varchar").HasMaxLength(256);
            builder.Property(e => e.Senha).HasColumnName("Senha").HasColumnType("varchar").HasMaxLength(256);
            builder.Property(e => e.Outro).HasColumnName("Outro").HasColumnType("varchar").HasMaxLength(512);
            builder.Property(e => e.UltimaExecucao).HasColumnName("UltimaExecucao").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.UltimoRetorno).HasColumnName("UltimoRetorno").HasColumnType("varchar").HasMaxLength(512);
            builder.Property(e => e.Sucesso).HasColumnName("Sucesso").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.UsuarioCadastro).HasColumnName("UsuarioCadastro").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.UsuarioAlteracao).HasColumnName("UsuarioAlteracao").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.DataCadastro).HasColumnName("DataCadastro").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.DataAlteracao).HasColumnName("DataAlteracao").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.Compactado).HasColumnName("Compactado").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.Ativo).HasColumnName("Ativo").HasColumnType("bit").HasMaxLength(1);

            builder.HasQueryFilter(p => p.Ativo);

            builder.HasOne(e => e.Transportador).WithMany().HasForeignKey(e => e.TransportadorId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.Empresa).WithMany().HasForeignKey(e => e.EmpresaId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.ArquivoTipo).WithMany(x => x.ImportacaoConfiguracoes).HasForeignKey(e => e.ImportacaoArquivoTipoId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.ConfiguracaoTipo).WithMany(x => x.ImportacaoConfiguracoes).HasForeignKey(e => e.ImportacaoConfiguracaoTipoId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
