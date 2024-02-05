using Fretter.Domain.Config;
using Fretter.Domain.Dto.Carrefour.Mirakl;
using Fretter.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Fretter.Domain.Dto.Carrefour
{
	public class EntregaStageFilaDTO
	{
		public EntregaStageFilaDTO()
		{ }
		public EntregaStageFilaDTO(OrderDTO order, int empresaId, bool eCarrefour = true)
		{
			try
			{
				if (eCarrefour)
					PreencherObjetoCarrefour(order, empresaId);
				else
					PreencherLeroy(order, empresaId);
			}
			catch (Exception ex)
			{
				throw new Exception($"Registro com problema: {order.order_id} - {ex.Message?.Take(2000)}");
			}
		}

		private void PreencherLeroy(OrderDTO order, int empresaId)
		{
			const string CNPJ_LEROY = "01438784006561";
			this.EmpresaId = empresaId;
			this.EmpresaMarketplaceCnpj = CNPJ_LEROY;
			this.CodigoIntegracao = order.order_id;
			this.Danfe = Regex.Replace(order.order_additional_fields.FirstOrDefault(x => x.code == "invoice-access-key")?.value.Trim() ?? string.Empty, "[^0-9]", "");

			if (!string.IsNullOrEmpty(order.order_additional_fields.FirstOrDefault(x => x.code == "invoice-date")?.value))
				this.EmissaoNota = DateTime.Parse(order.order_additional_fields.FirstOrDefault(x => x.code == "invoice-date")?.value);

			this.EntregaEntrada = order.order_id;
			this.EntregaSaida = order.order_id;
			this.ValorDeclarado = order.order_lines?.Sum(x => x.price_unit) ?? 0; //  order.total_price ;
			this.ValorTotal = order.total_price;
			this.PedidoCriacao = order.created_date;
			//this.Tomador = order.order_additional_fields.FirstOrDefault(x => x.code == "cnpj-seller")?.value;
			//this.MenuFreteMicroServico = order.order_additional_fields.FirstOrDefault(x => x.code == "intelipost-delivery-method-id")?.value;
			this.Modalidade = order.shipping_type_code;

			int idLojista = 0;
			int.TryParse(order.order_additional_fields.FirstOrDefault(x => x.code == "shop-id")?.value, out idLojista);
			this.Lojista = idLojista;

			this.DataPrevistaEntrega = order.shipping_deadline;
			this.EtiquetaURL = null;
			this.ContemIncidente = false;

			List<SkuDTO> listaSku = new List<SkuDTO>();

			for (int i = 0; i < order.order_lines.Count; i++)
			{
				SkuDTO skuDTO = new SkuDTO();
				skuDTO.Sku = order.order_lines[i].product_sku;
				skuDTO.peso = 1;
				skuDTO.altura = 1;
				skuDTO.largura = 1;
				skuDTO.profundidade = 1;
				listaSku.Add(skuDTO);
			}

			this.Itens = new List<EntregaStageItemFilaDTO>();
			foreach (OrderLineDTO orderLine in order.order_lines)
			{
				EntregaStageItemFilaDTO item = new EntregaStageItemFilaDTO();
				item.ValorItem = orderLine.price_unit;
				item.ValorTotal = orderLine.total_price;
				item.Frete = orderLine.shipping_price;
				item.FreteCobrado = orderLine.shipping_price;
				item.EntregaEntrada = orderLine.order_line_id;
				item.EntregaSaida = orderLine.order_line_id;
				item.IncidenteCodigo = orderLine.order_line_state_reason_code;
				item.IncidenteDescricao = orderLine.order_line_state_reason_label;

				item.Peso = listaSku.FirstOrDefault(x => x.Sku == orderLine.product_sku)?.peso;
				item.Largura = listaSku.FirstOrDefault(x => x.Sku == orderLine.product_sku)?.largura;
				item.Altura = listaSku.FirstOrDefault(x => x.Sku == orderLine.product_sku)?.altura;
				item.Profundidade = listaSku.FirstOrDefault(x => x.Sku == orderLine.product_sku)?.profundidade;
				item.Quantidade = orderLine.quantity;

				item.Sku = new EntregaStageSkuFilaDTO()
				{
					Codigo = orderLine.product_sku,
					Descricao = orderLine.product_title
				};

				Itens.Add(item);
			}

			string[] complemento = order.order_additional_fields.FirstOrDefault(x => x.code == "shipping-address-street-2")?.value?.Split("-");
			string numero = order.order_additional_fields.FirstOrDefault(x => x.code == "shipping-address-street-2")?.value ?? complemento[0]?.RemoveNonNumeric();

			if (complemento.Length > 1)
				numero = complemento[0]?.RemoveNonNumeric();
			else
				complemento = order.customer.shipping_address.street_2?.Split("-");

			EntregaStageDestinatarioFilaDTO destinatario = new EntregaStageDestinatarioFilaDTO();
			destinatario.Nome = ($"{order.customer.firstname} {order.customer.lastname}").ToTitleCase();
			destinatario.Endereco = ($"{order.customer.shipping_address.street_1}").ToTitleCase();
			destinatario.Numero = numero.ToTitleCase();
			destinatario.Complemento = ($"{(complemento.Length > 2? complemento[2]?.Trim() : "")}").ToTitleCase();
			destinatario.Bairro = ($"{complemento[1]?.Trim()}").ToTitleCase();
			destinatario.Cep = ($"{order.customer.shipping_address.zip_code.RemoveNonNumeric()}").ToTitleCase();
			destinatario.Cidade = ($"{order.customer.shipping_address.city}").ToTitleCase();
			destinatario.UF = ($"{order.customer.shipping_address.state}").ToUpper();
			destinatario.CodigoIntegracao = order.customer.customer_id;
			destinatario.CpfCnpj = order.customer.customer_id;
			destinatario.Email = null;
			destinatario.Telefone = order.order_additional_fields.FirstOrDefault(x => x.code == "shipping-address-phone")?.value;
			this.Destinatario = destinatario;

			#region campos fixos para POC Leroy 
			this.Tomador = CNPJ_LEROY;
			this.MenuFreteMicroServico = "01";

			EntregaStageRemetenteFilaDTO remetente = new EntregaStageRemetenteFilaDTO();
			remetente.Nome = "BAZAR DAS TORNEIRAS COMERCIO DE METAIS UNIPESSOAL LIMITADA".ToTitleCase();
			remetente.Endereco = "AV SAPOPEMBA".ToTitleCase();
			remetente.Cep = "03345-000".ToTitleCase();
			remetente.Numero = "1342".ToTitleCase();
			remetente.Bairro = "Vila Regente Feijó".ToTitleCase();
			remetente.Complemento = "".ToTitleCase();
			remetente.Cidade = "São Paulo".ToTitleCase();
			remetente.UF = "SP".ToUpper();
			remetente.Cnpj = "26439834000191";
			this.Remetente = remetente;
			#endregion

		}

		private void PreencherObjetoCarrefour(OrderDTO order, int empresaId)
		{
			this.EmpresaId = empresaId;
			this.EmpresaMarketplaceCnpj = "45543915000181";
			this.CodigoIntegracao = order.order_id;
			this.Danfe = Regex.Replace(order.order_additional_fields.FirstOrDefault(x => x.code == "code-nfe")?.value.Trim() ?? string.Empty, "[^0-9]", "");

			if (!string.IsNullOrEmpty(order.order_additional_fields.FirstOrDefault(x => x.code == "date-nfe")?.value))
				this.EmissaoNota = DateTime.Parse(order.order_additional_fields.FirstOrDefault(x => x.code == "date-nfe")?.value);

			this.EntregaEntrada = order.order_id;
			this.EntregaSaida = order.order_id;
			this.ValorDeclarado = order.order_lines?.Sum(x => x.price_unit) ?? 0; //  order.total_price ;
			this.ValorTotal = order.total_price;
			this.PedidoCriacao = order.created_date;
			this.Tomador = order.order_additional_fields.FirstOrDefault(x => x.code == "cnpj-seller")?.value;
			this.MenuFreteMicroServico = order.order_additional_fields.FirstOrDefault(x => x.code == "intelipost-delivery-method-id")?.value;
			this.Modalidade = order.order_additional_fields.FirstOrDefault(x => x.code == "intelipost-delivery-method-type")?.value;
			this.Lojista = order.shop_id;
			this.DataPrevistaEntrega = order.shipping_deadline;
			this.EtiquetaURL = order.order_additional_fields.FirstOrDefault(x => x.code == "etiquetaenvios")?.value;
			this.ContemIncidente = order.has_incident;

			List<SkuDTO> listaSku = new List<SkuDTO>();

			for (int i = 0; i < order.order_lines.Count; i++)
			{
				SkuDTO skuDTO = new SkuDTO();
				skuDTO.Sku = order.order_lines[i].product_sku;
				skuDTO.peso = Decimal.Parse(order.order_additional_fields.FirstOrDefault(x => x.code == "packagedweightkg")?.value.Split(";")[i] ?? "0") * 0.001M;
				skuDTO.altura = Decimal.Parse(order.order_additional_fields.FirstOrDefault(x => x.code == "packagedheight")?.value.Split(";")[i] ?? "0") * 0.01M;
				skuDTO.largura = Decimal.Parse(order.order_additional_fields.FirstOrDefault(x => x.code == "packagedwidth")?.value.Split(";")[i] ?? "0") * 0.01M;
				skuDTO.profundidade = Decimal.Parse(order.order_additional_fields.FirstOrDefault(x => x.code == "packageddepth")?.value.Split(";")[i] ?? "0") * 0.01M;
				listaSku.Add(skuDTO);
			}

			this.Itens = new List<EntregaStageItemFilaDTO>();
			foreach (OrderLineDTO orderLine in order.order_lines)
			{
				EntregaStageItemFilaDTO item = new EntregaStageItemFilaDTO();
				item.ValorItem = orderLine.price_unit;
				item.ValorTotal = orderLine.total_price;
				item.Frete = orderLine.shipping_price;
				item.FreteCobrado = orderLine.shipping_price;
				item.EntregaEntrada = orderLine.order_line_id;
				item.EntregaSaida = orderLine.order_line_id;
				item.IncidenteCodigo = orderLine.order_line_state_reason_code;
				item.IncidenteDescricao = orderLine.order_line_state_reason_label;

				item.Peso = listaSku.FirstOrDefault(x => x.Sku == orderLine.product_sku)?.peso;
				item.Largura = listaSku.FirstOrDefault(x => x.Sku == orderLine.product_sku)?.largura;
				item.Altura = listaSku.FirstOrDefault(x => x.Sku == orderLine.product_sku)?.altura;
				item.Profundidade = listaSku.FirstOrDefault(x => x.Sku == orderLine.product_sku)?.profundidade;
				item.Quantidade = orderLine.quantity;

				item.Sku = new EntregaStageSkuFilaDTO()
				{
					Codigo = orderLine.product_sku,
					Descricao = orderLine.product_title
				};
				Itens.Add(item);
			}

			EntregaStageDestinatarioFilaDTO destinatario = new EntregaStageDestinatarioFilaDTO();
			destinatario.Nome = ($"{order.customer.firstname} {order.customer.lastname}").ToTitleCase();
			destinatario.Endereco = order.order_additional_fields.FirstOrDefault(x => x.code == "delivery-address")?.value.ToTitleCase();
			destinatario.Numero = order.order_additional_fields.FirstOrDefault(x => x.code == "delivery-number-address")?.value.ToTitleCase();
			destinatario.Complemento = order.order_additional_fields.FirstOrDefault(x => x.code == "delivery-complement-address")?.value.ToTitleCase();
			destinatario.Bairro = order.order_additional_fields.FirstOrDefault(x => x.code == "delivery-district-address")?.value.ToTitleCase();
			destinatario.Cep = order.order_additional_fields.FirstOrDefault(x => x.code == "delivery-postal-code")?.value.ToTitleCase();
			destinatario.Cidade = order.order_additional_fields.FirstOrDefault(x => x.code == "delivery-town")?.value.ToTitleCase();
			destinatario.UF = order.order_additional_fields.FirstOrDefault(x => x.code == "delivery-state")?.value.ToUpper();
			destinatario.CodigoIntegracao = order.customer.customer_id;
			destinatario.CpfCnpj = order.order_additional_fields.FirstOrDefault(x => x.code == "customer-cpf")?.value;
			destinatario.Email = order.customer.email?.ToLower();
			this.Destinatario = destinatario;

			EntregaStageRemetenteFilaDTO remetente = new EntregaStageRemetenteFilaDTO();
			remetente.Nome = order.shop_name.ToTitleCase();
			remetente.Endereco = order.order_additional_fields.FirstOrDefault(x => x.code == "endereco-seller")?.value.ToTitleCase();
			remetente.Cep = order.order_additional_fields.FirstOrDefault(x => x.code == "cep-seller")?.value.ToTitleCase();
			remetente.Numero = order.order_additional_fields.FirstOrDefault(x => x.code == "numero-seller")?.value.ToTitleCase();
			remetente.Bairro = order.order_additional_fields.FirstOrDefault(x => x.code == "bairro-seller")?.value.ToTitleCase();
			remetente.Complemento = order.order_additional_fields.FirstOrDefault(x => x.code == "complemento-endereco-seller")?.value.ToTitleCase();
			remetente.Cidade = order.order_additional_fields.FirstOrDefault(x => x.code == "cidade-seller")?.value.ToTitleCase();
			remetente.UF = order.order_additional_fields.FirstOrDefault(x => x.code == "uf-seller")?.value.ToUpper();
			remetente.Cnpj = order.order_additional_fields.FirstOrDefault(x => x.code == "cnpj-seller")?.value;
			this.Remetente = remetente;
		}

		public int EmpresaId { get; set; }
		public string EmpresaMarketplaceCnpj { get; set; }
		public string Danfe { get; set; }
		public DateTime? EmissaoNota { get; set; }
		public string MenuFreteMicroServico { get; set; }
		public string Modalidade { get; set; }
		public string TipoServico { get; set; }
		public string Tomador { get; set; }
		public string CodigoIntegracao { get; set; }
		public string EntregaEntrada { get; set; }
		public string EntregaSaida { get; set; }
		public DateTime? PedidoCriacao { get; set; }
		public decimal? ValorDeclarado { get; set; }
		public decimal? ValorTotal { get; set; }
		public int? Lojista { get; set; }
		public string EtiquetaURL { get; set; }
		public DateTime? DataPrevistaEntrega { get; set; }
		public EntregaStageRemetenteFilaDTO Remetente { get; set; }
		public EntregaStageDestinatarioFilaDTO Destinatario { get; set; }
		public List<EntregaStageItemFilaDTO> Itens { get; set; }
		public bool? ContemIncidente { get; set; }
	}

	public class EntregaStageRemetenteFilaDTO
	{
		public string Nome { get; set; }
		public string CodigoIntegracao { get; set; }
		public string Cep { get; set; }
		public string Endereco { get; set; }
		public string Numero { get; set; }
		public string Complemento { get; set; }
		public string Bairro { get; set; }
		public string Cidade { get; set; }
		public string UF { get; set; }
		public string Cnpj { get; set; }
	}
	public class EntregaStageDestinatarioFilaDTO
	{
		public string Nome { get; set; }
		public string CodigoIntegracao { get; set; }
		public string CpfCnpj { get; set; }
		public string InscricaoEstadual { get; set; }
		public string Cep { get; set; }
		public string Endereco { get; set; }
		public string Numero { get; set; }
		public string Email { get; set; }
		public string Complemento { get; set; }
		public string Bairro { get; set; }
		public string Cidade { get; set; }
		public string UF { get; set; }
		public string Telefone { get; set; }
	}
	public class EntregaStageSkuFilaDTO
	{
		public string Codigo { get; set; }
		public string Descricao { get; set; }
	}
	public class EntregaStageItemFilaDTO
	{
		public string EntregaEntrada { get; set; }
		public string EntregaSaida { get; set; }
		public int Quantidade { get; set; }
		public decimal? ValorItem { get; set; }
		public decimal? ValorTotal { get; set; }
		public decimal? Frete { get; set; }
		public decimal? FreteCobrado { get; set; }
		public string Informacao { get; set; }
		public string IncidenteCodigo { get; set; }
		public string IncidenteDescricao { get; set; }
		public decimal? Peso { get; set; }
		public decimal? Largura { get; set; }
		public decimal? Altura { get; set; }
		public decimal? Profundidade { get; set; }
		public DateTime? SoliticacaoReversa { get; set; }
		public EntregaStageSkuFilaDTO Sku { get; set; }
	}


	public class SkuDTO
	{
		public string Sku { get; set; }
		public decimal altura { get; set; }
		public decimal largura { get; set; }
		public decimal profundidade { get; set; }
		public decimal peso { get; set; }
	}
}
