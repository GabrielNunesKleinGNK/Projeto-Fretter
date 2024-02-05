using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class TabelaMap : IEntityTypeConfiguration<Tabela>
    {
        public void Configure(EntityTypeBuilder<Tabela> builder)
        {
            builder.ToTable("Tb_MF_Tabela", "dbo");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Empresa).HasColumnName("Id_Empresa").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Transportador).HasColumnName("Id_Transportador").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.TipoServico).HasColumnName("Id_TipoServico").HasColumnType("tinyint").HasMaxLength(1);
			builder.Property(e => e.VigenciaInicio).HasColumnName("Dt_VigenciaInicio").HasColumnType("date").HasMaxLength(3);
			builder.Property(e => e.VigenciaFim).HasColumnName("Dt_VigenciaFim").HasColumnType("date").HasMaxLength(3);
			builder.Property(e => e.Inclusao).HasColumnName("Dt_Inclusao").HasColumnType("smalldatetime").HasMaxLength(4);
			builder.Property(e => e.Nome).HasColumnName("Ds_Nome").HasColumnType("varchar").HasMaxLength(200);
			builder.Property(e => e.FatorCubagem).HasColumnName("Nr_FatorCubagem").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.RegiaoGenerica).HasColumnName("Flg_RegiaoGenerica").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.PesoMinimo).HasColumnName("Nr_PesoMinimo").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.TabelaStatus).HasColumnName("Id_TabelaStatus").HasColumnType("tinyint").HasMaxLength(1);
			builder.Property(e => e.TabelaCopiaDe).HasColumnName("Id_TabelaCopiaDe").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.UtilizaICMS).HasColumnName("Flg_UtilizaICMS").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.GrisPorNF).HasColumnName("Flg_GrisPorNF").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.AdValoremMinimo).HasColumnName("Nr_AdValoremMinimo").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.CalculaICMS).HasColumnName("Flg_CalculaICMS").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.ParametrosJson).HasColumnName("Ds_ParametrosJson").HasColumnType("varchar");
			builder.Property(e => e.Timeout).HasColumnName("Nr_Timeout").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.EndPoint).HasColumnName("Flg_EndPoint").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.MicroServicoId).HasColumnName("Id_MicroServico").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.LimitePesoCubico).HasColumnName("Nr_LimitePesoCubico").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.MedidaLimite).HasColumnName("Nr_MedidaLimite").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.MedidaLimiteSoma).HasColumnName("Nr_MedidaLimiteSoma").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.PesoCubadoMaximo).HasColumnName("Nr_PesoCubadoMaximo").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.PesoRealMaximo).HasColumnName("Nr_PesoRealMaximo").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.GrisMinimo).HasColumnName("Nr_GrisMinimo").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.FatorCubagemLimiteMinimo).HasColumnName("Nr_FatorCubagemLimiteMinimo").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.Hub).HasColumnName("Id_Hub").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.MicroServicoContingencia).HasColumnName("Id_MicroServicoContingencia").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.OrigemGenerica).HasColumnName("Flg_OrigemGenerica").HasColumnType("bit").HasMaxLength(1);

			builder.Ignore(e => e.UsuarioCadastro);
			builder.Ignore(e => e.UsuarioAlteracao);
			builder.Ignore(e => e.DataCadastro);
			builder.Ignore(e => e.DataAlteracao);
		}
    }
}
