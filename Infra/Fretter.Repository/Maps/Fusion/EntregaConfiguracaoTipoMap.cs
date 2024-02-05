using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class EntregaConfiguracaoTipoMap : IEntityTypeConfiguration<EntregaConfiguracaoTipo>
    {
        public void Configure(EntityTypeBuilder<EntregaConfiguracaoTipo> builder)
        {
            builder.ToTable("Tb_Edi_EntregaConfiguracaoTipo", "dbo");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Tipo).HasColumnName("Ds_Tipo").HasColumnType("varchar").HasMaxLength(64);
        }
    }
}
