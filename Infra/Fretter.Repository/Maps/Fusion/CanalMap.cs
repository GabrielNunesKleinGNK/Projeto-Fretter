using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class CanalMap : IEntityTypeConfiguration<Canal>
    {
        public void Configure(EntityTypeBuilder<Canal> builder)
        {
            builder.ToTable("Tb_Adm_Canal", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.Inclusao).HasColumnName("Dt_Inclusao").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.RazaoSocial).HasColumnName("Ds_RazaoSocial").HasColumnType("varchar").HasMaxLength(100);
            builder.Property(e => e.NomeFantasia).HasColumnName("Ds_NomeFantasia").HasColumnType("varchar").HasMaxLength(50);
            builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.Cnpj).HasColumnName("Cd_Cnpj").HasColumnType("char").HasMaxLength(14);
            builder.Property(e => e.CanalNome).HasColumnName("Cd_Canal").HasColumnType("varchar").HasMaxLength(100);
            builder.Property(e => e.SegmentoId).HasColumnName("Id_Segmento").HasColumnType("int").HasMaxLength(4);
            //builder.Property(e => e.CnpjUnico).HasColumnName("Cd_CnpjUnico").HasColumnType("bigint").HasMaxLength(8);
            builder.Property(e => e.OrigemImportacaoId).HasColumnName("Id_OrigemImportacao").HasColumnType("tinyint");
            builder.Property(e => e.InscricaoEstadual).HasColumnName("Ds_InscricaoEstadual").HasColumnType("varchar").HasMaxLength(100);
            builder.Property(e => e.EmpresaId).HasColumnName("Id_Empresa").HasColumnType("int").HasMaxLength(4);

            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);

            builder.HasOne(d => d.CanalConfig)
                 .WithOne(p => p.Canal)
                 .HasForeignKey<CanalConfig>(b => b.Id);                       
        }
    }
}
