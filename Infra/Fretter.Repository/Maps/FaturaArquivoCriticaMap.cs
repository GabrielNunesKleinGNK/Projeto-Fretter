using Fretter.Domain.Entities.Fretter;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Repository.Maps
{
    public class FaturaArquivoCriticaMap : IEntityTypeConfiguration<FaturaArquivoCritica>
    {
        public void Configure(EntityTypeBuilder<FaturaArquivoCritica> builder)
        {
            builder.ToTable("FaturaArquivoCritica", "Fretter");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("FaturaArquivoId").HasColumnType("int");
            builder.Property(e => e.FaturaArquivoId).HasColumnType("int");
            builder.Property(e => e.Linha).HasColumnType("int");
            builder.Property(e => e.Posicao).HasColumnType("int");
            builder.Property(e => e.Descricao).HasColumnType("varchar").HasMaxLength(128);

            //BaseMapping
            builder.Property(e => e.Ativo).HasColumnName("Ativo").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.UsuarioCadastro).HasColumnName("UsuarioCadastro").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.UsuarioAlteracao).HasColumnName("UsuarioAlteracao").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.DataCadastro).HasColumnName("DataCadastro").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.DataAlteracao).HasColumnName("DataAlteracao").HasColumnType("datetime").HasMaxLength(8);
        }
    }
}
