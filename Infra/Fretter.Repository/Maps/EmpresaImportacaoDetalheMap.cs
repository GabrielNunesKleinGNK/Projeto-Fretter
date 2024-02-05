using Fretter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fretter.Repository.Maps
{
    class EmpresaImportacaoDetalheMap : IEntityTypeConfiguration<EmpresaImportacaoDetalhe>
    {
        public void Configure(EntityTypeBuilder<EmpresaImportacaoDetalhe> builder)
        {

            builder.ToTable("EmpresaImportacaoDetalhe", "Fretter");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName($"{nameof(EmpresaImportacaoDetalhe)}Id").HasColumnType("int");
            builder.Property(e => e.EmpresaImportacaoArquivoId).HasColumnName("EmpresaImportacaoArquivoId").HasColumnType("int").HasMaxLength(4).IsRequired();
            builder.Property(e => e.EmpresaId).HasColumnName("EmpresaId").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.Nome).HasColumnName("Nome").HasColumnType("varchar").HasMaxLength(512);
            builder.Property(e => e.Cnpj).HasColumnName("Cnpj").HasColumnType("varchar").HasMaxLength(32);
            builder.Property(e => e.CEP).HasColumnName("Cep").HasColumnType("varchar").HasMaxLength(16);
            builder.Property(e => e.Token).HasColumnName("Token").HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.UF).HasColumnName("UF").HasColumnType("varchar").HasMaxLength(16);
            builder.Property(e => e.Descricao).HasColumnName("Descricao").HasColumnType("varchar").HasMaxLength(256);
            builder.Property(e => e.SucessoEmpresa).HasColumnName("SucessoEmpresa").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.SucessoCanal).HasColumnName("SucessoCanal").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.SucessoPermissao).HasColumnName("SucessoPermissao").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.CorreioBalcao).HasColumnName("CorreioBalcao").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.ConsomeApiFrete).HasColumnName("ConsomeApiFrete").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.DataCadastro).HasColumnName("DataCadastro").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.UsuarioCadastro).HasColumnName("UsuarioCadastro").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.Ativo).HasColumnName("Ativo").HasColumnType("bit").HasMaxLength(1);

            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataAlteracao);

            builder.HasOne(d => d.EmpresaImportacaoArquivo)
                  .WithMany(p => p.Detalhes)
                  .HasForeignKey(d => d.EmpresaImportacaoArquivoId)
                  .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.Empresa)
                  .WithMany(p => p.EmpresaImportacaoDetalhes)
                  .HasForeignKey(d => d.EmpresaId)
                  .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
