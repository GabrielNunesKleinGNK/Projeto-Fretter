using Fretter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fretter.Repository.Maps.Fusion
{
    class AgendamentoEmpresaTransportadorMap : IEntityTypeConfiguration<AgendamentoEmpresaTransportador>
    {
        public void Configure(EntityTypeBuilder<AgendamentoEmpresaTransportador> builder)
        {
            builder.ToTable("Tb_Age_EmpresaTransportador", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").IsRequired(true);
            builder.Property(e => e.Id_Empresa).HasColumnName("Id_Empresa").HasColumnType("int").IsRequired(true);
            builder.Property(e => e.Id_Transportador).HasColumnName("Id_Transportador").HasColumnType("int").IsRequired(true);
            builder.Property(e => e.Id_TransportadorCnpj).HasColumnName("Id_TransportadorCnpj").HasColumnType("int").IsRequired(false);
            builder.Property(e => e.Flg_ExpedicaoAutomatica).HasColumnName("Flg_ExpedicaoAutomatica").HasColumnType("bit").IsRequired(true);
            builder.Property(e => e.Nr_PrazoComercial).HasColumnName("Nr_PrazoComercial").HasColumnType("tinyint").IsRequired(true);
        }
    }
}
