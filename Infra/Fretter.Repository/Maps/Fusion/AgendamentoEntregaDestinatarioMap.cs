using Fretter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fretter.Repository.Maps.Fusion
{
    class AgendamentoEntregaDestinatarioMap : IEntityTypeConfiguration<AgendamentoEntregaDestinatario>
    {
        public void Configure(EntityTypeBuilder<AgendamentoEntregaDestinatario> builder)
        {
            builder.ToTable("Tb_Age_EntregaDestinatario", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int");
            builder.Property(e => e.IdEntrega).HasColumnName("Id_Entrega").HasColumnType("int");
            builder.Property(e => e.Nome).HasColumnName("Ds_Nome").HasColumnType("varchar").HasMaxLength(1024);
            builder.Property(e => e.CpfCnpj).HasColumnName("Cd_CpfCnpj").HasColumnType("varchar").HasMaxLength(14);
            builder.Property(e => e.InscricaoEstadual).HasColumnName("Cd_InscricaoEstadual").HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.DocumentoHash).HasColumnName("Cd_DocumentoHash").HasColumnType("varchar").HasMaxLength(128);
            builder.Property(e => e.Cep).HasColumnName("Cd_Cep").HasColumnType("varchar").HasMaxLength(16);
            builder.Property(e => e.Logradouro).HasColumnName("Ds_Logradouro").HasColumnType("varchar").HasMaxLength(512);
            builder.Property(e => e.Numero).HasColumnName("Ds_Numero").HasColumnType("varchar").HasMaxLength(128);
            builder.Property(e => e.Complemento).HasColumnName("Ds_Complemento").HasColumnType("varchar").HasMaxLength(128);
            builder.Property(e => e.PontoReferencia).HasColumnName("Ds_PontoReferencia").HasColumnType("varchar").HasMaxLength(256);
            builder.Property(e => e.Bairro).HasColumnName("Ds_Bairro").HasColumnType("varchar").HasMaxLength(128);
            builder.Property(e => e.Cidade).HasColumnName("Ds_Cidade").HasColumnType("varchar").HasMaxLength(512);
            builder.Property(e => e.UF).HasColumnName("Ds_UF").HasColumnType("varchar").HasMaxLength(8);
            builder.Property(e => e.Email).HasColumnName("Ds_Email").HasColumnType("varchar").HasMaxLength(512);
            builder.Property(e => e.Telefone).HasColumnName("Nr_Telefone").HasColumnType("varchar").HasMaxLength(128);
            builder.Property(e => e.Celular).HasColumnName("Nr_Celular").HasColumnType("varchar").HasMaxLength(128);
            builder.Property(e => e.Whatsapp).HasColumnName("Flg_Whatsapp").HasColumnType("bit");

            builder.Property(e => e.DataCadastro).HasColumnName("Dt_Cadastro").HasColumnType("date");
            builder.Property(e => e.UsuarioCadastro).HasColumnName("Us_Cadastro").HasColumnType("int");
            builder.Property(e => e.DataAlteracao).HasColumnName("Dt_Alteracao").HasColumnType("date");
            builder.Property(e => e.UsuarioAlteracao).HasColumnName("Us_Alteracao").HasColumnType("int");
            builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit");

            //builder.HasOne(x => x.Entrega).WithMany(x => x.Destinatarios).HasForeignKey(x => x.IdEntrega).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
