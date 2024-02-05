using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Webhook
{
    public class EEmpresa
    {
        public virtual int Cd_Id { get; set; }
        public virtual DateTime Dt_Inclusao { get; set; }
        public virtual string Ds_RazaoSocial { get; set; }
        public virtual string Ds_NomeFantasia { get; set; }
        public virtual string Cd_Cnpj { get; set; }
        public virtual bool Flg_Ativo { get; set; }
        public virtual Guid Id_Token { get; set; }
        public virtual Guid Id_TokenConsulta { get; set; }
        public virtual string Id_Tomticket { get; set; }
        public virtual bool Fl_Marketplace { get; set; }
        public virtual bool? Flg_ControlaSaldoProdutos { get; set; }
        public virtual string Ds_Formatado { get; set; }
        public virtual bool Fl_CalculaPrazoEntregaMicroServico { get; set; }
        public virtual bool? Flg_MF_NaoUsaEndpointExterno { get; set; }

        public virtual List<EEmpresaParametro> LstEmpresaParametro { get; set; }

        public EEmpresa CloneObj()
        {
            return new EEmpresa
            {
                Cd_Id = Cd_Id,
                Dt_Inclusao = Dt_Inclusao,
                Ds_RazaoSocial = Ds_RazaoSocial,
                Ds_NomeFantasia = Ds_NomeFantasia,
                Cd_Cnpj = Cd_Cnpj,
                Flg_Ativo = Flg_Ativo,
                Id_Token = Id_Token,
                Id_TokenConsulta = Id_TokenConsulta,
                Id_Tomticket = Id_Tomticket,
                Fl_Marketplace = Fl_Marketplace,
                Flg_ControlaSaldoProdutos = Flg_ControlaSaldoProdutos,
                Ds_Formatado = Ds_Formatado,
                Fl_CalculaPrazoEntregaMicroServico = Fl_CalculaPrazoEntregaMicroServico,
                Flg_MF_NaoUsaEndpointExterno = Flg_MF_NaoUsaEndpointExterno,
                LstEmpresaParametro = LstEmpresaParametro
            };
        }
    }
}
