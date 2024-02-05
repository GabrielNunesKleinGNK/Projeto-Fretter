using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fretter.Repository.Maps
{
    class EmpresaMap : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.ToTable("Tb_Adm_Empresa", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.Inclusao).HasColumnName("Dt_Inclusao").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.RazaoSocial).HasColumnName("Ds_RazaoSocial").HasColumnType("varchar").HasMaxLength(100);
            builder.Property(e => e.NomeFantasia).HasColumnName("Ds_NomeFantasia").HasColumnType("varchar").HasMaxLength(50);
            builder.Property(e => e.Cnpj).HasColumnName("Cd_Cnpj").HasColumnType("char").HasMaxLength(14);
            builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.TokenId).HasColumnName("Id_Token").HasMaxLength(16);
            builder.Property(e => e.TokenConsultaId).HasColumnName("Id_TokenConsulta").HasMaxLength(16);
            builder.Property(e => e.TomticketId).HasColumnName("Id_Tomticket").HasColumnType("varchar").HasMaxLength(50);
            builder.Property(e => e.Marketplace).HasColumnName("Fl_Marketplace").HasColumnType("bit").HasMaxLength(1);
            //builder.Property(e => e.CnpjUnico).HasColumnName("Cd_CnpjUnico").HasColumnType("bigint").HasMaxLength(8).ValueGeneratedOnAddOrUpdate();
            //builder.Property(e => e.NomeFantasiaUnico).HasColumnName("Ds_NomeFantasiaUnico").HasColumnType("varchar").HasMaxLength(8000).ValueGeneratedOnAddOrUpdate();
            builder.Property(e => e.TokenWebHookId).HasColumnName("Id_TokenWebHook").HasMaxLength(16);
            //builder.Property(e => e.Formatado).HasColumnName("Ds_Formatado").HasColumnType("varchar").HasMaxLength(353).ValueGeneratedOnAddOrUpdate();
            builder.Property(e => e.DesconsideraTransportadorNoRatreio).HasColumnName("Flg_DesconsideraTransportadorNoRatreio").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.TrocaTransportadorNoRoteiro).HasColumnName("Flg_TrocaTransportadorNoRoteiro").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.OrigemImportacaoId).HasColumnName("Id_OrigemImportacao").HasColumnType("tinyint").HasMaxLength(1);
            builder.Property(e => e.ControlaSaldoProdutos).HasColumnName("Flg_ControlaSaldoProdutos").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.CalculaPrazoEntregaMicroServico).HasColumnName("Fl_CalculaPrazoEntregaMicroServico").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.NaoUsaEndpointExterno).HasColumnName("Flg_MF_NaoUsaEndpointExterno").HasColumnType("bit").HasMaxLength(1);

            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);

            builder.HasOne(d => d.EmpresaSegmento)
                 .WithOne(p => p.Empresa)
                 .HasForeignKey<EmpresaSegmento>(b => b.EmpresaId);

            builder.HasOne(d => d.Canal)
                 .WithOne(p => p.Empresa)
                 .HasForeignKey<Canal>(b => b.EmpresaId);
        }
    }
}
