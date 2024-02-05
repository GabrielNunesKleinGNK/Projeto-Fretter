using Fretter.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Fretter.Domain.Entities.Fretter;

namespace Fretter.Repository.Maps
{
    public class FaturaArquivoMap : IEntityTypeConfiguration<FaturaArquivo>
    {
        public void Configure(EntityTypeBuilder<FaturaArquivo> builder)
        {
            builder.ToTable("FaturaArquivo", "Fretter");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("FaturaArquivoId").HasColumnType("int");
            builder.Property(e => e.EmpresaId).HasColumnType("int");
            builder.Property(e => e.NomeArquivo).HasColumnType("varchar").HasMaxLength(256);
            builder.Property(e => e.UrlBlobStorage).HasColumnType("varchar").HasMaxLength(256);
            builder.Property(e => e.QtdeRegistros).HasColumnType("int");
            builder.Property(e => e.QtdeCriticas).HasColumnType("int");
            builder.Property(e => e.ValorTotal).HasColumnType("decimal");
            builder.Property(e => e.TransportadorId).HasColumnType("int");
            builder.Property(e => e.Faturado).HasColumnType("bit");

            //BaseMapping
            builder.Property(e => e.Ativo).HasColumnName("Ativo").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.UsuarioCadastro).HasColumnName("UsuarioCadastro").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.UsuarioAlteracao).HasColumnName("UsuarioAlteracao").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.DataCadastro).HasColumnName("DataCadastro").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.DataAlteracao).HasColumnName("DataAlteracao").HasColumnType("datetime").HasMaxLength(8);
        }
    }
}
