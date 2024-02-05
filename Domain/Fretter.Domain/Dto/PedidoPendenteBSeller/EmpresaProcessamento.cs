namespace Fretter.Domain.Dto.PedidoPendenteBSeller
{
    public class EmpresaProcessamento
    {
        public int EmpresaId { get; set; }
        public int EstabelecimentoBSellerId { get; set; }
        public int? TimeoutRequest { get; set; }
        public int? PeriodoEmDias { get; set; }

    }
}
