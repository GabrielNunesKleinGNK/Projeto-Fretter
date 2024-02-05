namespace Fretter.Domain.Dto.TabelaArquivo
{
    public class RegiaoCep
    {
        public int Id { get; set; }
        public int IdRegiao { get; set; }
        public string CepInicio { get; set; }
        public string CepFim { get; set; }
        public byte? Prazo { get; set; }
    }
}
