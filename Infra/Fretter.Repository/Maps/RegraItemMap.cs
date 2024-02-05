using Fretter.Domain.Entities.Fretter;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Nest;

namespace Fretter.Repository.Maps
{
    internal class RegraItemMap : IEntityTypeConfiguration<RegraItem>
    {
        public void Configure(EntityTypeBuilder<RegraItem> builder)
        {
            builder.ToTable("Tb_Age_RegraItem", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int");

            builder.Property(e => e.RegraId).HasColumnName("Id_Regra").HasColumnType("int");
            builder.Property(e => e.RegraGrupoItemId).HasColumnName("Id_RegraGrupoItem").HasColumnType("int");
            builder.Property(e => e.RegraTipoItemId).HasColumnName("Id_RegraTipoItem").HasColumnType("int");
            builder.Property(e => e.RegraTipoOperadorId).HasColumnName("Id_RegraTipoOperador").HasColumnType("int");
            builder.Property(e => e.Valor).HasColumnName("Ds_Valor").HasColumnType("varchar").HasMaxLength(256);
            builder.Property(e => e.ValorInicial).HasColumnName("Ds_ValorInicial").HasColumnType("varchar").HasMaxLength(256);
            builder.Property(e => e.ValorFinal).HasColumnName("Ds_ValorFinal").HasColumnType("varchar").HasMaxLength(256);

            //BaseMapping
            builder.Property(e => e.DataCadastro).HasColumnName("Dt_Inclusao").HasColumnType("datetime");
            builder.Property(e => e.UsuarioCadastro).HasColumnName("Us_Inclusao").HasColumnType("int");
            builder.Property(e => e.DataAlteracao).HasColumnName("Dt_Alteracao").HasColumnType("datetime");
            builder.Property(e => e.UsuarioAlteracao).HasColumnName("Us_Alteracao").HasColumnType("int");
            builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit");

            builder.HasOne(x => x.RegraGrupoItem).WithMany().HasForeignKey(x => x.RegraGrupoItemId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
