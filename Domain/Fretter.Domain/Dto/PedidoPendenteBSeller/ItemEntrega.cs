namespace Fretter.Domain.Dto.PedidoPendenteBSeller
{
    public class ItemEntrega
    {
        public ItemEntrega(string cdEntrega)
        {
            Entrega = cdEntrega;
        }
        public string Entrega { get; set; }
    }
}
