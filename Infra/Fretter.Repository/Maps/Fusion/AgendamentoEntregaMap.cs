using Fretter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fretter.Repository.Maps.Fusion
{
    class AgendamentoEntregaMap : IEntityTypeConfiguration<AgendamentoEntrega>
    {
        public void Configure(EntityTypeBuilder<AgendamentoEntrega> builder)
        {   
            builder.ToTable("Tb_Age_Entrega", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int");
            builder.Property(e => e.IdCanal).HasColumnName("Id_Canal").HasColumnType("int");
            builder.Property(e => e.IdEmpresa).HasColumnName("Id_Empresa").HasColumnType("int");
            builder.Property(e => e.IdRegiaoCEPCapacidade).HasColumnName("Id_RegiaoCEPCapacidade").HasColumnType("int");
            builder.Property(e => e.IdTransportador).HasColumnName("Id_Transportador").HasColumnType("int");
            builder.Property(e => e.IdTransportadorCnpj).HasColumnName("Id_TransportadorCnpj").HasColumnType("int").IsRequired(false);
            builder.Property(e => e.CdNotaFiscal).HasColumnName("Cd_NotaFiscal").HasColumnType("varchar").HasMaxLength(16);
            builder.Property(e => e.CdSerie).HasColumnName("Cd_Serie").HasColumnType("varchar").HasMaxLength(8);
            builder.Property(e => e.CdSro).HasColumnName("Cd_Sro").HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.CdEntrega).HasColumnName("Cd_Entrega").HasColumnType("varchar").HasMaxLength(256);
            builder.Property(e => e.CdPedido).HasColumnName("Cd_Pedido").HasColumnType("varchar").HasMaxLength(256);
            builder.Property(e => e.CdCepOrigem).HasColumnName("Cd_CepOrigem").HasColumnType("varchar").HasMaxLength(16);
            builder.Property(e => e.CdCepDestino).HasColumnName("Cd_CepDestino").HasColumnType("varchar").HasMaxLength(16);
            builder.Property(e => e.CdProtocolo).HasColumnName("Cd_Protocolo").HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.NrPrazoTransportador).HasColumnName("Nr_PrazoTransportador").HasColumnType("int");
            builder.Property(e => e.VlQuantidade).HasColumnName("Vl_Quantidade").HasColumnType("int");
            builder.Property(e => e.DtAgendamento).HasColumnName("Dt_Agendamento").HasColumnType("date");
            builder.Property(e => e.DsObservacao).HasColumnName("Ds_Observacao").HasColumnType("varchar").HasMaxLength(2048);

            builder.Property(e => e.DataCadastro).HasColumnName("Dt_Cadastro").HasColumnType("date");
            builder.Property(e => e.UsuarioCadastro).HasColumnName("Us_Cadastro").HasColumnType("int");
            builder.Property(e => e.DataAlteracao).HasColumnName("Dt_Alteracao").HasColumnType("date");
            builder.Property(e => e.UsuarioAlteracao).HasColumnName("Us_Alteracao").HasColumnType("int");
            builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit");

            builder.HasMany(x => x.Destinatarios).WithOne().HasForeignKey(x => x.IdEntrega).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.Produtos).WithOne().HasForeignKey(x => x.EntregaId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Canal).WithMany().HasForeignKey(x => x.IdCanal).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.MenuFreteRegiaoCepCapacidade).WithMany().HasForeignKey(x => x.IdRegiaoCEPCapacidade).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
