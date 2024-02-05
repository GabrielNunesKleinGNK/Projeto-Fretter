using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class ConciliacaoMap : IEntityTypeConfiguration<Conciliacao>
    {
        public void Configure(EntityTypeBuilder<Conciliacao> builder)
        {
            builder.ToTable("Conciliacao", "Fretter");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName($"{nameof(Conciliacao)}Id").HasColumnType("int");
            builder.Property(e => e.EmpresaId).HasColumnType("int");
            builder.Property(e => e.EntregaId).HasColumnType("int");
            builder.Property(e => e.TransportadorId).HasColumnType("int");
            builder.Property(e => e.ValorCustoFrete).HasColumnType("decimal").HasMaxLength(9);
            builder.Property(e => e.ValorCustoAdicional).HasColumnType("decimal").HasMaxLength(9);
            builder.Property(e => e.ValorCustoReal).HasColumnType("decimal").HasMaxLength(9);
            builder.Property(e => e.ValorCustoDivergencia).HasColumnType("decimal").HasMaxLength(9);
            builder.Property(e => e.QuantidadeTentativas).HasColumnType("int");
            builder.Property(e => e.DataEmissao).HasColumnType("DateTime");
            builder.Property(e => e.DataFinalizacao).HasColumnType("DateTime");
            builder.Property(e => e.FaturaId).HasColumnType("int");
            builder.Property(e => e.PossuiDivergenciaPeso).HasColumnType("bit");
            builder.Property(e => e.PossuiDivergenciaTarifa).HasColumnType("bit");
            builder.Property(e => e.DevolvidoRemetente).HasColumnType("bit");
            builder.Property(e => e.ProcessadoIndicador).HasColumnType("bit");
            builder.Property(e => e.JsonValoresRecotacao).HasColumnType("varchar(max)");
            builder.Property(e => e.JsonValoresCte).HasColumnType("varchar(max)");
            builder.Property(e => e.FaturaId).HasColumnType("int");
            builder.Property(e => e.ImportacaoCteId).HasColumnType("int");
            builder.Property(e => e.ImportacaoCteId).HasColumnType("int");

            //BaseMapping
            builder.Property(e => e.Ativo).HasColumnName("Ativo").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.UsuarioCadastro).HasColumnType("int");
            builder.Property(e => e.UsuarioAlteracao).HasColumnType("int");
            builder.Property(e => e.DataCadastro).HasColumnType("DateTime");
            builder.Property(e => e.DataAlteracao).HasColumnType("DateTime");

            builder.HasOne(e => e.ImportacaoCte).WithOne(x => x.Conciliacao).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.ConciliacaoTipo).WithOne(x => x.Conciliacao).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
