using Fretter.Domain.Entities.Fretter;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Repository.Maps
{
    public class ConciliacaoReenvioHistoricoMap : IEntityTypeConfiguration<ConciliacaoReenvioHistorico>
    {
        public void Configure(EntityTypeBuilder<ConciliacaoReenvioHistorico> builder)
        {
            builder.ToTable("ConciliacaoReenvioHistorico", "Fretter");

            builder.HasKey(e => e.Id);
            
            builder.Property(e => e.Id).HasColumnName("ConciliacaoReenvioHistoricoId").HasColumnType("int");
            builder.Property(e => e.ConciliacaoReenvioId).HasColumnType("int");
            builder.Property(e => e.FaturaConciliacaoId).HasColumnType("bigint");
            builder.Property(e => e.FaturaId).HasColumnType("int");
            builder.Property(e => e.ConciliacaoId).HasColumnType("bigint");
            builder.Property(e => e.UsuarioCadastro).HasColumnType("int");
            builder.Property(e => e.DataCadastro).HasColumnType("datetime");
            builder.Property(e => e.DataReprocessamento).HasColumnType("datetime");

            builder.Ignore(e => e.Ativo);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataAlteracao);
        }
    }
}
