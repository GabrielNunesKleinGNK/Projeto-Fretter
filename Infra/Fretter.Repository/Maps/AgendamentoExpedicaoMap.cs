using Fretter.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Fretter.Domain.Entities.Fretter;

namespace Fretter.Repository.Maps
{
    public class AgendamentoExpedicaoMap : IEntityTypeConfiguration<AgendamentoExpedicao>
    {
        public void Configure(EntityTypeBuilder<AgendamentoExpedicao> builder)
        {
            builder.ToTable("Tb_Age_EmpresaTransportador");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int");
            builder.Property(e => e.EmpresaId).HasColumnName("Id_Empresa").HasColumnType("int");
            builder.Property(e => e.CanalId).HasColumnName("Id_Canal").HasColumnType("int");
            builder.Property(e => e.TransportadorId).HasColumnName("Id_Transportador").HasColumnType("int");
            builder.Property(e => e.TransportadorCnpjId).HasColumnName("Id_TransportadorCnpj").HasColumnType("int");
            builder.Property(e => e.ExpedicaoAutomatica).HasColumnName("Flg_ExpedicaoAutomatica").HasColumnType("bit");
            builder.Property(e => e.PrazoComercial).HasColumnName("Nr_PrazoComercial").HasColumnType("tinyint");
            
            //BaseMapping
            builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.DataCadastro).HasColumnName("Dt_Cadastro").HasColumnType("datetime");
            builder.Property(e => e.UsuarioCadastro).HasColumnName("Us_Cadastro").HasColumnType("int");
            builder.Property(e => e.DataAlteracao).HasColumnName("Dt_Alteracao").HasColumnType("datetime");
            builder.Ignore(e => e.UsuarioAlteracao);

            //Refências
            builder.HasOne(x => x.Canal).WithMany().HasForeignKey(x => x.CanalId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Transportador).WithMany().HasForeignKey(x => x.TransportadorId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.TransportadorCnpj).WithMany().HasForeignKey(x => x.TransportadorCnpjId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
