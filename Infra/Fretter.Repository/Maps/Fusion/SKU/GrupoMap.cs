using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities.Fusion.SKU;

namespace Fretter.Repository.Maps.Fusion.SKU
{
    internal class GrupoMap : IEntityTypeConfiguration<Grupo>
    {
        public void Configure(EntityTypeBuilder<Grupo> builder)
        {
            builder.ToTable("Tb_SKU_Grupo", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.EmpresaId).HasColumnName("Id_Empresa").HasColumnType("int");
            builder.Property(e => e.Codigo).HasColumnName("Cd_Grupo").HasColumnType("varhcar").HasMaxLength(50);
            builder.Property(e => e.Descricao).HasColumnName("Ds_Grupo").HasColumnType("varhcar").HasMaxLength(200);

            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);
            builder.Ignore(e => e.Ativo);

            builder.HasOne(d => d.Empresa).WithMany().HasForeignKey(b => b.EmpresaId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
