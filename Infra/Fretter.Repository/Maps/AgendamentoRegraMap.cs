using Fretter.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Fretter.Domain.Entities.Fretter;

namespace Fretter.Repository.Maps
{
    public class AgendamentoRegraMap : IEntityTypeConfiguration<AgendamentoRegra>
    {
        public void Configure(EntityTypeBuilder<AgendamentoRegra> builder)
        {
            builder.ToTable("Tb_Age_Regra", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int");
            builder.Property(e => e.EmpresaTransportadorId).HasColumnName("Id_EmpresaTransportador").HasColumnType("int");
            builder.Property(e => e.EmpresaId).HasColumnName("Id_Empresa").HasColumnType("int");
            builder.Property(e => e.CanalId).HasColumnName("Id_Canal").HasColumnType("int");
            builder.Property(e => e.TransportadorId).HasColumnName("Id_Transportador").HasColumnType("int");
            builder.Property(e => e.TransportadorCnpjId).HasColumnName("Id_TransportadorCnpj").HasColumnType("int");
            builder.Property(e => e.RegraStatusId).HasColumnName("Id_RegraStatus").HasColumnType("int");
            builder.Property(e => e.RegraTipoId).HasColumnName("Id_RegraTipo").HasColumnType("int");
            builder.Property(e => e.Nome).HasColumnName("Ds_Nome").HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.DataInicio).HasColumnName("Dt_Inicio").HasColumnType("Datetime");
            builder.Property(e => e.DataTermino).HasColumnName("Dt_Termino").HasColumnType("Datetime");
            
            //BaseMapping
            builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);

            builder.Property(e => e.DataCadastro).HasColumnName("Dt_Inclusao").HasColumnType("datetime");
            builder.Property(e => e.UsuarioCadastro).HasColumnName("Us_Inclusao").HasColumnType("int");

            builder.Property(e => e.DataAlteracao).HasColumnName("Dt_Alteracao").HasColumnType("datetime");
            builder.Property(e => e.UsuarioAlteracao).HasColumnName("Us_Alteracao").HasColumnType("int");


            //Refências
            builder.HasMany(d => d.RegraItens).WithOne(p => p.AgendamentoRegra).HasForeignKey(b => b.RegraId);
            builder.HasOne(x => x.Canal).WithMany().HasForeignKey(x => x.CanalId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Transportador).WithMany().HasForeignKey(x => x.TransportadorId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.TransportadorCnpj).WithMany().HasForeignKey(x => x.TransportadorCnpjId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
