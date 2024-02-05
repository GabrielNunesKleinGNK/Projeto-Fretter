using Fretter.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Fretter.Domain.Entities.Fretter;

namespace Fretter.Repository.Maps
{
    public class ImportacaoArquivoCriticaMap : IEntityTypeConfiguration<ImportacaoArquivoCritica>
    {
        public void Configure(EntityTypeBuilder<ImportacaoArquivoCritica> builder)
        {
            builder.ToTable("ImportacaoArquivoCritica", "Fretter");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName($"{nameof(ImportacaoArquivoCritica)}Id").HasColumnType("int");
            builder.Property(e => e.ImportacaoArquivoId).HasColumnType("int");
            builder.Property(e => e.Descricao).HasColumnType("varchar").HasMaxLength(128);
            builder.Property(e => e.Linha).HasColumnType("int");
            builder.Property(e => e.Campo).HasColumnType("varchar").HasMaxLength(64);

            //BaseMapping
            builder.Property(e => e.Ativo).HasColumnName("Ativo").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.UsuarioCadastro).HasColumnName("UsuarioCadastro").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.UsuarioAlteracao).HasColumnName("UsuarioAlteracao").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.DataCadastro).HasColumnName("DataCadastro").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.DataAlteracao).HasColumnName("DataAlteracao").HasColumnType("datetime").HasMaxLength(8);

            builder.HasOne(e => e.ImportacaoArquivo).WithMany(x => x.ImportacaoArquivoCriticas).HasForeignKey(e => e.ImportacaoArquivoId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}