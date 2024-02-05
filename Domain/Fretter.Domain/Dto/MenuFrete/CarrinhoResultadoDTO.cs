using Fretter.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.MenuFrete
{
    public class CarrinhoResultadoDTO
    {
        public string msg { get; set; }
        public string protocolo { get; set; }
        public List<Modalidade> modalidades { get; set; }
    }
    public class Modalidade
    {
        public string modalidade { get; set; }
        public int prazo { get; set; }
        public int prazoTransit { get; set; }
        public int prazoExpedicao { get; set; }
        public decimal valor { get; set; }
        public decimal custo { get; set; }
        public string transportador { get; set; }
        public int idTransportador { get; set; }
        public int idMicroServico { get; set; }
        public List<Item> itens { get; set; }
    }
    public class Item
    {
        public string cdItem { get; set; }
        public int prazo { get; set; }
        public int prazoTransit { get; set; }
        public int prazoExpedicao { get; set; }
        public double valor { get; set; }
        public double custo { get; set; }
        public int idTabela { get; set; }
        public string cdMicroServico { get; set; }
        public List<ItemComposicao> composicao { get; set; }
    }

    public class ItemComposicao
    {
        public string chave { get; set; }
        public decimal valor { get; set; }
        public EnumCteComposicaoTipo tipo { get; set; }
    }
}
