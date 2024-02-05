using Fretter.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Fretter.Domain.Entities.Fretter;

namespace Fretter.Repository.Maps
{
    public class ConciliacaoReenvioMap : IEntityTypeConfiguration<ConciliacaoReenvio>
    {
        public void Configure(EntityTypeBuilder<ConciliacaoReenvio> builder)
        {
            builder.ToTable("ConciliacaoReenvio", "Fretter");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("ConciliacaoReenvioId").HasColumnType("int");
            builder.Property(e => e.FaturaConciliacaoId).HasColumnType("bigint");
            builder.Property(e => e.FaturaId).HasColumnType("int");
            builder.Property(e => e.ConciliacaoId).HasColumnType("bigint");
            builder.Property(e => e.UsuarioCadastro).HasColumnType("int");
            builder.Property(e => e.DataCadastro).HasColumnType("datetime");

            builder.Ignore(e => e.Ativo);
            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataAlteracao);
        }
    }
}