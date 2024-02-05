using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class TabelaTipoCanalVendaMap : IEntityTypeConfiguration<TabelaTipoCanalVenda>
    {
        public void Configure(EntityTypeBuilder<TabelaTipoCanalVenda> builder)
        {
            builder.ToTable("Tb_MF_TabelaTipoCanalVenda", "dbo");
            builder.HasKey(t => new { t.Id, t.CanalVendaId });
            builder.Property(e => e.Id).HasColumnName("Id_TabelaTipo").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.CanalVendaId).HasColumnName("Id_CanalVenda").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.PriorizaPrazo).HasColumnName("Flg_PriorizaPrazo").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.Alias).HasColumnName("Ds_Alias").HasColumnType("varchar").HasMaxLength(50);
            builder.Property(e => e.AceitaPrazoZerado).HasColumnName("Fl_AceitaPrazoZerado").HasColumnType("bit").HasMaxLength(1);


            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);
            builder.Ignore(e => e.Ativo);
        }
    }
}
