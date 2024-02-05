using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class CanalVendaInterfaceMap : IEntityTypeConfiguration<CanalVendaInterface>
    {
        public void Configure(EntityTypeBuilder<CanalVendaInterface> builder)
        {
            builder.ToTable("Tb_MF_CanalVendaInterface", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.CanalVendaId).HasColumnName("Id_CanalVenda").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.TipoInterface).HasColumnName("Id_TipoInterface").HasColumnType("tinyint").HasMaxLength(1);
            builder.Property(e => e.DescricaoTransportador).HasColumnName("Id_DescricaoTransportador").HasColumnType("tinyint").HasMaxLength(1);
            builder.Property(e => e.TimeoutWS).HasColumnName("Nr_TimeoutWS").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.EmpresaTokenId).HasColumnName("Id_EmpresaToken").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.EmpresaId).HasColumnName("Id_Empresa").HasColumnType("int").HasMaxLength(4);

            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);
            builder.Ignore(e => e.Ativo);

            builder.HasOne(d => d.EmpresaToken)
                  .WithMany(p => p.CanalVendaInterfaces)
                  .HasForeignKey(d => d.EmpresaTokenId)
                  .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
