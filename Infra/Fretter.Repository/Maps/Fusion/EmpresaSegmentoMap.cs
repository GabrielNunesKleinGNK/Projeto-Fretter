using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class EmpresaSegmentoMap : IEntityTypeConfiguration<EmpresaSegmento>
    {
        public void Configure(EntityTypeBuilder<EmpresaSegmento> builder)
        {
            builder.ToTable("Tb_Adm_Segmento", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.Inclusao).HasColumnName("Dt_Inclusao").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.Segmento).HasColumnName("Ds_Segmento").HasColumnType("varchar");
            builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.Padrao).HasColumnName("Flg_Default").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.EmpresaId).HasColumnName("Id_Empresa");
            builder.Property(e => e.OrigemImportacaoId).HasColumnName("Id_OrigemImportacao").HasColumnType("tinyint").HasMaxLength(1);

            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);

            builder.HasOne(d => d.Canal)
                .WithOne(p => p.Segmento)
                .HasForeignKey<Canal>(d => d.SegmentoId);           
        }
    }
}
