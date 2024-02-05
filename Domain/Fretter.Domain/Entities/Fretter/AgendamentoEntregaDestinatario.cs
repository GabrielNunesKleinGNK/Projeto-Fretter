namespace Fretter.Domain.Entities
{
    public class AgendamentoEntregaDestinatario : EntityBase
    {
        #region Construtores
        public AgendamentoEntregaDestinatario()
        {
        }
        public AgendamentoEntregaDestinatario(int id, int? idEntrega, string dsNome, string cdCpfCnpj, string cdInscricaoEstadual, string cdDocumentoHash, string cdCep, 
            string dsLogradouro, string dsNumero, string dsComplemento, string dsPontoReferencia, string dsBairro, string dsCidade, string dsUF, string dsEmail, 
            string nrTelefone, string nrCelular, bool flgWhatsapp)
        {
            this.Id = id;
            this.IdEntrega= idEntrega;
            this.Nome = dsNome;
            this.CpfCnpj = cdCpfCnpj;
            this.InscricaoEstadual = cdInscricaoEstadual;
            this.DocumentoHash = cdDocumentoHash;
            this.Cep = cdCep;
            this.Logradouro = dsLogradouro;
            this.Numero = dsNumero;
            this.Complemento = dsComplemento;
            this.PontoReferencia = dsPontoReferencia;
            this.Bairro = dsBairro;
            this.Cidade = dsCidade;
            this.UF = dsUF;
            this.Email = dsEmail;
            this.Telefone = nrTelefone;
            this.Celular = nrCelular;
            this.Whatsapp = flgWhatsapp;
        }
        #endregion

        #region "Propriedades"
        public int? IdEntrega { get; protected set; }
        public string Nome { get; protected set; }
        public string CpfCnpj { get; protected set; }
        public string InscricaoEstadual { get; protected set; }
        public string DocumentoHash { get; protected set; }
        public string Cep { get; protected set; }
        public string Logradouro { get; protected set; }
        public string Numero { get; protected  set; }
        public string Complemento { get; protected set; }
        public string PontoReferencia { get; protected set; }
        public string Bairro { get; protected set; }
        public string Cidade { get; protected set; }
        public string UF { get; protected set; }
        public string Email { get; protected set; }
        public string Telefone { get; protected set; }
        public string Celular { get; protected set; }
        public bool Whatsapp { get; protected set; }
        #endregion


        public void AtualizarDocumentoHash(string hash) => this.DocumentoHash = hash;
    }
}
