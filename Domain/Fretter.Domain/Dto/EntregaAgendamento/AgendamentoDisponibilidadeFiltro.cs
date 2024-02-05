namespace Fretter.Domain.Dto.EntregaAgendamento
{
    public class AgendamentoDisponibilidadeFiltro
    {
        public int? CanalId { get; set; }
        public string Cep { get; set; }
        public int QuantidadeItens { get; set; }
        public int Pagina { get; set; }
    }
}
