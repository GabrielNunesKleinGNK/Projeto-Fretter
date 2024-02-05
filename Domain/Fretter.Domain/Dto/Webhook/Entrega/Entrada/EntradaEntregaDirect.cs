using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Webhook.Entrega.Entrada
{
    public class EntradaEntregaDirect
    {
        /// <summary>
        /// Usuario
        /// </summary>
        public string usuario { get; set; }

        /// <summary>
        /// Senha
        /// </summary>
        public string senha { get; set; }

        /// <summary>
        /// Codigo Pedido
        /// </summary>
        public string codigoPedido { get; set; }

        /// <summary>
        /// Codigo Correio
        /// </summary>
        public string codigoCorreio { get; set; }

        public string awb { get; set; }

        /// <summary>
        /// Data Postagem
        /// </summary>
        public DateTime dataPostagem { get; set; }

        /// <summary>
        /// Cep
        /// </summary>
        public string cep { get; set; }

        /// <summary>
        /// Descricao Cliente
        /// </summary>
        public string DescricaoCliente { get; set; }

        /// <summary>
        /// logr
        /// </summary>
        public string logr { get; set; }

        /// <summary>
        /// endereco
        /// </summary>
        public string endereco { get; set; }
        /// <summary>
        /// nroEndereco
        /// </summary>
        public string nroEndereco { get; set; }
        /// <summary>
        /// complemento
        /// </summary>
        public string complemento { get; set; }
        /// <summary>
        /// bairro
        /// </summary>
        public string bairro { get; set; }
        /// <summary>
        /// cidade
        /// </summary>
        public string cidade { get; set; }
        /// <summary>
        /// uf
        /// </summary>
        public string uf { get; set; }
        /// <summary>
        /// ddd
        /// </summary>
        public string ddd { get; set; }
        /// <summary>
        /// fone
        /// </summary>
        public string fone { get; set; }
        /// <summary>
        /// AvisoRecebimento
        /// </summary>
        public string AvisoRecebimento { get; set; }
        /// <summary>
        /// RecebeuAviso
        /// </summary>
        public string RecebeuAviso { get; set; }
        /// <summary>
        /// ValorDeclarado
        /// </summary>
        public string ValorDeclarado { get; set; }
        /// <summary>
        /// dddCelular
        /// </summary>
        public string dddCelular { get; set; }
        /// <summary>
        /// foneCelular
        /// </summary>
        public string foneCelular { get; set; }
        /// <summary>
        /// emailCliente
        /// </summary>
        public string emailCliente { get; set; }
        /// <summary>
        /// praca
        /// </summary>
        public string praca { get; set; }
        /// <summary>
        /// RL
        /// </summary>
        public string RL { get; set; }
    }
}
