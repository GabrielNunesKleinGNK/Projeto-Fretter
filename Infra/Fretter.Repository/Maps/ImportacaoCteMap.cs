using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class ImportacaoCteMap : IEntityTypeConfiguration<ImportacaoCte>
    {
        public void Configure(EntityTypeBuilder<ImportacaoCte> builder)
        {
            builder.ToTable("ImportacaoCte", "Fretter");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName($"{nameof(ImportacaoCte)}Id").HasColumnType("int");
            builder.Property(e => e.ImportacaoArquivoId).HasColumnType("int").IsRequired();
            builder.Property(e => e.TipoAmbiente).HasColumnType("int");
            builder.Property(e => e.TipoCte).HasColumnType("int");
            builder.Property(e => e.TipoServico).HasColumnType("int");
            builder.Property(e => e.Chave).HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.Codigo).HasColumnType("varchar").HasMaxLength(16);
            builder.Property(e => e.Numero).HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.DigitoVerificador).HasColumnType("int");
            builder.Property(e => e.Serie).HasColumnType("varchar").HasMaxLength(8);
            builder.Property(e => e.DataEmissao).HasColumnType("datetime");
            builder.Property(e => e.ValorPrestacaoServico).HasColumnType("decimal").HasMaxLength(5);
            builder.Property(e => e.CNPJTransportador).HasColumnType("varchar").HasMaxLength(14);
            builder.Property(e => e.CNPJTomador).HasColumnType("varchar").HasMaxLength(14);
            builder.Property(e => e.CNPJEmissor).HasColumnType("varchar").HasMaxLength(14);
            builder.Property(e => e.ChaveComplementar).HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.JsonComposicaoValores).HasColumnType("varchar(max)");
            builder.Property(e => e.Modal).HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.CodigoMunicipioEnvio).HasColumnType("int");
            builder.Property(e => e.MunicipioEnvio).HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.UFEnvio).HasColumnType("varchar").HasMaxLength(2);
            builder.Property(e => e.CodigoMunicipioInicio).HasColumnType("int");
            builder.Property(e => e.MunicipioInicio).HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.UFInicio).HasColumnType("varchar").HasMaxLength(2);
            builder.Property(e => e.CodigoMunicipioFim).HasColumnType("int");
            builder.Property(e => e.MunicipioFim).HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.UFFim).HasColumnType("varchar").HasMaxLength(2);
            builder.Property(e => e.IETomadorIndicador).HasColumnType("smallint");
            builder.Property(e => e.ValorTributo).HasColumnType("decimal").HasMaxLength(5);
            builder.Property(e => e.CFOP).HasColumnType("varchar").HasMaxLength(8);
            builder.Property(e => e.VersaoProcesso).HasColumnType("varchar").HasMaxLength(36);
            builder.Property(e => e.VersaoAplicacao).HasColumnType("varchar").HasMaxLength(32);
            builder.Property(e => e.ChaveCte).HasColumnType("varchar").HasMaxLength(128);
            builder.Property(e => e.DigestValue).HasColumnType("varchar").HasMaxLength(256);
            builder.Property(e => e.DataAutorizacao).HasColumnType("datetime"); 
            builder.Property(e => e.StatusAutorizacao).HasColumnType("varchar").HasMaxLength(32);
            builder.Property(e => e.ProtocoloAutorizacao).HasColumnType("varchar").HasMaxLength(128);
            builder.Property(e => e.MotivoAutorizacao).HasColumnType("varchar").HasMaxLength(256);

            //BaseMapping
            builder.Property(e => e.Ativo).HasColumnName("Ativo").HasColumnType("bit").HasMaxLength(1);
            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);

            builder.HasOne(e => e.ImportacaoArquivo).WithMany(x => x.ImportacaoCtes).HasForeignKey(e => e.ImportacaoArquivoId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(e => e.ImportacaoCteCargas).WithOne(x => x.ImportacaoCte).HasForeignKey(e => e.ImportacaoCteId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(e => e.ImportacaoCteNotaFiscais).WithOne(x => x.ImportacaoCte).HasForeignKey(e => e.ImportacaoCteId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(e => e.ImportacaoCteComposicoes).WithOne(x => x.ImportacaoCte).HasForeignKey(e => e.ImportacaoCteId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
