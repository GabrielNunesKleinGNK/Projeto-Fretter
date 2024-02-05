using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fusion.SKU;

namespace Fretter.Repository.Maps.Fusion.SKU
{
    internal class RegraEstoqueMap : IEntityTypeConfiguration<RegraEstoque>
    {
        public void Configure(EntityTypeBuilder<RegraEstoque> builder)
        {
            builder.ToTable("Tb_SKU_RegraEstoque", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.EmpresaId).HasColumnName("Id_Empresa").HasColumnType("int");
            builder.Property(e => e.CanalIdOrigem).HasColumnName("Id_CanalOrigem").HasColumnType("int");
            builder.Property(e => e.CanalIdDestino).HasColumnName("Id_CanalDestino").HasColumnType("int");
            builder.Property(e => e.GrupoId).HasColumnName("Id_Grupo").HasColumnType("int");
            builder.Property(e => e.Skus).HasColumnName("Skus").HasColumnType("varchar").HasMaxLength(4000);

            builder.Property(e => e.UsuarioCadastro).HasColumnName("Cd_UsuarioCadastro").HasColumnType("int");
            builder.Property(e => e.UsuarioAlteracao).HasColumnName("Cd_UsuarioAlteracao").HasColumnType("int");
            builder.Property(e => e.DataCadastro).HasColumnName("Dt_Cadastro").HasColumnType("DateTime");
            builder.Property(e => e.DataAlteracao).HasColumnName("Dt_Alteracao").HasColumnType("DateTime");
            builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit");

            builder.HasOne(d => d.Empresa).WithMany().HasForeignKey(b => b.EmpresaId).OnDelete(DeleteBehavior.Restrict);
            //builder.HasOne(d => d.CanalOrigem).WithMany().HasForeignKey(b => b.CanalIdOrigem).OnDelete(DeleteBehavior.Restrict);
            //builder.HasOne(d => d.CanalDestino).WithMany().HasForeignKey(b => b.CanalIdDestino).OnDelete(DeleteBehavior.Restrict);
            //builder.HasOne(d => d.Grupo).WithMany().HasForeignKey(b => b.GrupoId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Grupo)
                    .WithMany()
                    .HasForeignKey(m => m.GrupoId);

            builder.HasOne(d => d.CanalDestino)
                    .WithMany()
                    .HasForeignKey(m => m.CanalIdDestino);

            builder.HasOne(d => d.CanalOrigem)
                    .WithMany()
                    .HasForeignKey(m => m.CanalIdOrigem);
        }
    }
}
