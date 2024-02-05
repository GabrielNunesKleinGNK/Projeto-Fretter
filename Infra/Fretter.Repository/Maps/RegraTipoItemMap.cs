using Fretter.Domain.Entities.Fretter;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Repository.Maps
{
    public class RegraTipoItemMap : IEntityTypeConfiguration<RegraTipoItem>
    {
        public void Configure(EntityTypeBuilder<RegraTipoItem> builder)
        {
            builder.ToTable("Tb_Age_RegraTipoItem", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int");
            builder.Property(e => e.RegraTipoId).HasColumnName("Id_RegraTipo").HasColumnType("int");
            builder.Property(e => e.Nome).HasColumnName("Ds_Nome").HasColumnType("varchar").HasMaxLength(256);
            builder.Property(e => e.DadoTipo).HasColumnName("Ds_DadoTipo").HasColumnType("int");
            builder.Property(e => e.Range).HasColumnName("Flg_Range").HasColumnType("bit").HasMaxLength(1); ;

            //BaseMapping
            builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);

            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.DataAlteracao);
            builder.Ignore(e => e.UsuarioAlteracao);
        }
    }
}
