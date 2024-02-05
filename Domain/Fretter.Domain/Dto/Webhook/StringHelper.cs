using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Fretter.Domain.Dto.Webhook
{

    public static class StringHelper
    {
        public static string ToCamelCaseWord(this string value)
        {
            return $"{value.Substring(0, 1).ToLower()}{value.Substring(1)}";
        }

        public static string ToPascalCaseWord(this string value)
        {
            return $"{value.Substring(0, 1).ToUpper()}{value.Substring(1)}";
        }

        public static string ToPascalCase(this string value)
        {
            var strb = new StringBuilder();
            var arr = value.Replace("-", "").Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var s in arr)
                strb.AppendFormat("{0}{1}", s.Substring(0, 1).ToUpper(), s.Substring(1));
            return strb.ToString();
        }

        public static string RemoveSpecialCharacters(this string str)
        {
            str = str.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (var c in str.Where(c => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark))
                sb.Append(c);
            return sb.ToString();
        }

        public static string ToCode(this string str)
        {
            return str.RemoveSpecialCharacters().ToPascalCase();
        }

        public static string CleanCnpj(this string cnpj)
        {
            return string.IsNullOrEmpty(cnpj?.Trim())
                ? null
                : string.Concat(cnpj.Replace(" ", "").Where(char.IsNumber));
        }

        public static string CleanOnlyNumber(this string cnpj)
        {
            return string.IsNullOrEmpty(cnpj?.Trim())
                ? null
                : string.Concat(cnpj.Replace(" ", "").Where(char.IsNumber));
        }

        public static string FormatCNPJ(this string str)
        {
            return Convert.ToUInt64(str).ToString(@"00\.000\.000\/0000\-00");
        }

        public static string FormatCEP(this string str)
        {
            return Convert.ToUInt64(str).ToString(@"00000\-000");
        }

        public static bool CalculaDigitoVerificador(this string cnpj, out string digito, int? id_empresa = null)
        {
            var multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length > 12)
            {
                digito = string.Empty;
                return false;
            }

            var tempCnpj = cnpj.PadLeft(12, '0');

            var soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            var resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto;

            return true;
        }

        public static bool IsCnpj(this string cnpj, out string digito, int? id_empresa = null)
        {
            var multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
            {
                digito = string.Empty;
                return false;
            }

            var tempCnpj = cnpj.Substring(0, 12);

            var soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            var resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto;

            if (!cnpj.EndsWith(digito))
            {
                ////using (var c = Conexao.New)
                ////{
                ////    c.Execute("INSERT INTO Tb_Adm_CnpjInvalido VALUES (@cnpj,@id_empresa,GETDATE(),@erro)",
                ////        new DynamicParameter(new
                ////        {
                ////            cnpj,
                ////            id_empresa,
                ////            erro = "CNPJ inválido, o Digito Verificador correto seria: " + digito
                ////        }));
                ////}
                //throw new Exception("CNPJ inválido, o Digito Verificador correto seria: " + digito);
            }

            return cnpj.EndsWith(digito);
        }

        public static string CnpjValido(this string cnpj, out string digito, int? id_empresa = null)
        {
            var multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (!new Regex(@"^[0-9]*$").IsMatch(cnpj))
                throw new Exception($"CNPJ inválido ({cnpj})");

            if (cnpj.Length != 14)
            {
                digito = string.Empty;
                return string.Empty;
            }

            var tempCnpj = cnpj.Substring(0, 12);

            var soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            var resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto;

            if (!cnpj.EndsWith(digito))
            {
                if (id_empresa == 1765 || id_empresa == 1360)
                {
                    return "Cadastro";
                }
                throw new Exception("CNPJ inválido, o Digito Verificador correto seria: " + digito);
            }

            return string.Empty;
        }

        public static string StringSemAcentosECaracteresEspeciais(string str)
        {
            /** Troca os caracteres acentuados por não acentuados **/
            string[] acentos = new string[] { "ç", "Ç", "á", "é", "í", "ó", "ú", "ý", "Á", "É", "Í", "Ó", "Ú", "Ý", "à", "è", "ì", "ò", "ù", "À", "È", "Ì", "Ò", "Ù", "ã", "õ", "ñ", "ä", "ë", "ï", "ö", "ü", "ÿ", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "â", "ê", "î", "ô", "û", "Â", "Ê", "Î", "Ô", "Û" };
            string[] semAcento = new string[] { "c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U" };

            for (int i = 0; i < acentos.Length; i++)
            {
                str = str.Replace(acentos[i], semAcento[i]);
            }
            /** Troca os caracteres especiais da string por "" **/
            string[] caracteresEspeciais = { "\\.", ",", "-", ":", "\\(", "\\)", "ª", "\\|", "\\\\", "°" };

            for (int i = 0; i < caracteresEspeciais.Length; i++)
            {
                str = str.Replace(caracteresEspeciais[i], " ");
            }

            /** Troca os espaços no início por "" **/
            str = str.Replace("^\\s+", "");
            /** Troca os espaços no início por "" **/
            str = str.Replace("\\s+$", "");
            /** Troca os espaços duplicados, tabulações e etc por " " **/
            str = str.Replace("\\s+", " ");
            return str;
        }

        public static bool IsCorreio(this string sro)
        {
            var regex = new Regex(@"^[a-zA-Z]{2}\d{9}(BR|br)");
            return regex.IsMatch(sro?.Trim() ?? "");
        }

        public static string EstadoParaUF(this string estado)
        {
            var dict = new Dictionary<string, string>
            {
                {"ACRE", "AC"},
                {"ALAGOAS", "AL"},
                {"AMAPA", "AP"},
                {"AMAZONAS", "AM"},
                {"BAHIA", "BA"},
                {"CEARA", "CE"},
                {"DISTRITO FEDERAL", "DF"},
                {"ESPIRITO SANTO", "ES"},
                {"GOIAS", "GO"},
                {"MARANHAO", "MA"},
                {"MATO GROSSO", "MT"},
                {"MATO GROSSO DO SUL", "MS"},
                {"MINAS GERAIS", "MG"},
                {"PARA", "PA"},
                {"PARAIBA", "PB"},
                {"PARANA", "PR"},
                {"PERNAMBUCO", "PE"},
                {"PIAUI", "PI"},
                {"RIO DE JANEIRO", "RJ"},
                {"RIO GRANDE DO NORTE", "RN"},
                {"RIO GRANDE DO SUL", "RS"},
                {"RONDONIA", "RO"},
                {"RORAIMA", "RR"},
                {"SANTA CATARINA", "SC"},
                {"SAO PAULO", "SP"},
                {"SERGIPE", "SE"},
                {"TOCANTINS", "TO"}
            };

            return dict[estado.RemoveSpecialCharacters().ToUpper()];
        }
    }
}
