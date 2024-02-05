using Fretter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fretter.Repository.Maps.Fusion
{
    class MenuFreteRegiaoCepCapacidadeMap : IEntityTypeConfiguration<MenuFreteRegiaoCepCapacidade>
    {
        public void Configure(EntityTypeBuilder<MenuFreteRegiaoCepCapacidade> builder)
        {
            builder.ToTable("TB_MF_RegiaoCEPCapacidade", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int");
            builder.Property(e => e.IdRegiaoCEP).HasColumnName("Id_RegiaoCEP").HasColumnType("int");
            builder.Property(e => e.IdPeriodo).HasColumnName("Id_Periodo").HasColumnType("int");
            builder.Property(e => e.NrDia).HasColumnName("Nr_Dia").HasColumnType("tinyint");
            builder.Property(e => e.VlQuantidade).HasColumnName("Vl_Quantidade").HasColumnType("int");
            builder.Property(e => e.VlQuantidadeDisponivel).HasColumnName("Vl_QuantidadeDisponivel").HasColumnType("int");
            builder.Property(e => e.NrValor).HasColumnName("Nr_Valor").HasColumnType("decimal").HasPrecision(10,2);
            builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit");
            builder.Property(e => e.DataCadastro).HasColumnName("Dt_Cadastro").HasColumnType("datetime");

            builder.Ignore(x => x.DataAlteracao);
            builder.Ignore(x => x.UsuarioCadastro);
            builder.Ignore(x => x.UsuarioAlteracao);
        }
    }
}
