using Fretter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fretter.Repository.Maps.Fusion
{
    class MenuFretePeriodoMap : IEntityTypeConfiguration<MenuFretePeriodo>
    {
        public void Configure(EntityTypeBuilder<MenuFretePeriodo> builder)
        {
            builder.ToTable("Tb_MF_Periodo", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int");
            builder.Property(e => e.DsNome).HasColumnName("Ds_Nome").HasColumnType("varchar").HasMaxLength(256);
            builder.Property(e => e.HrInicio).HasColumnName("Hr_Inicio").HasColumnType("Time");
            builder.Property(e => e.HrTermino).HasColumnName("Hr_Termino").HasColumnType("Time");
            builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit");

            builder.Ignore(x => x.DataAlteracao);
            builder.Ignore(x => x.DataCadastro);
            builder.Ignore(x => x.UsuarioCadastro);
            builder.Ignore(x => x.UsuarioAlteracao);
        }
    }
}
