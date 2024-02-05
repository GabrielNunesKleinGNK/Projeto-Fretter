using Fretter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fretter.Repository.Maps.Fusion
{
    class AgendamentoEntregaProdutoMap : IEntityTypeConfiguration<AgendamentoEntregaProduto>
    {
        public void Configure(EntityTypeBuilder<AgendamentoEntregaProduto> builder)
        {
            builder.ToTable("Tb_Age_EntregaProduto", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int");
            builder.Property(e => e.EntregaId).HasColumnName("Id_Entrega").HasColumnType("int");
            builder.Property(e => e.SKU).HasColumnName("Cd_SKU").HasColumnType("varchar").HasMaxLength(128);
            builder.Property(e => e.EAN).HasColumnName("Cd_EAN").HasColumnType("varchar").HasMaxLength(128);
            builder.Property(e => e.Nome).HasColumnName("Ds_Nome").HasColumnType("varchar").HasMaxLength(2048);
            builder.Property(e => e.ValorProduto).HasColumnName("Vl_Produto").HasColumnType("decimal");
            builder.Property(e => e.ValorTotal).HasColumnName("Vl_Total").HasColumnType("decimal");

            builder.Property(e => e.Altura).HasColumnName("Vl_Altura").HasColumnType("decimal");
            builder.Property(e => e.Largura).HasColumnName("Vl_Largura").HasColumnType("decimal");
            builder.Property(e => e.Comprimento).HasColumnName("Vl_Comprimento").HasColumnType("decimal");
            builder.Property(e => e.Peso).HasColumnName("Vl_Peso").HasColumnType("decimal");

            builder.Property(e => e.DataCadastro).HasColumnName("Dt_Cadastro").HasColumnType("date");
            builder.Property(e => e.UsuarioCadastro).HasColumnName("Us_Cadastro").HasColumnType("int");
            builder.Property(e => e.DataAlteracao).HasColumnName("Dt_Alteracao").HasColumnType("date");
            builder.Property(e => e.UsuarioAlteracao).HasColumnName("Us_Alteracao").HasColumnType("int");
            builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit");

            builder.HasOne(x => x.Entrega).WithMany(x => x.Produtos).HasForeignKey(x => x.EntregaId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
