using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class CanalVendaEntradaMap : IEntityTypeConfiguration<CanalVendaEntrada>
    {
        public void Configure(EntityTypeBuilder<CanalVendaEntrada> builder)
        {
            builder.ToTable("Tb_Adm_CanalVendaEntrada", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.CanalVendaNome).HasColumnName("Ds_CanalVenda").HasColumnType("varchar").HasMaxLength(200);
            builder.Property(e => e.EmpresaId).HasColumnName("Id_Empresa").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.CanalVendaId).HasColumnName("Id_CanalVenda").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.OrigemImportacao).HasColumnName("Id_OrigemImportacao").HasColumnType("tinyint").HasMaxLength(1);

            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);
            builder.Ignore(e => e.Ativo);

            builder.HasOne(d => d.CanalVenda)
                  .WithMany(p => p.CanalVendaEntradas)
                  .HasForeignKey(d => d.CanalVendaId)
                  .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
