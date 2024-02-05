using Fluid;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Fretter.Util.Extentions
{
    public static class TextProvider
    {
        private const string REGEX_PATTERN = @"\${[^\}]*\}";

        /// <summary>
        /// Método responsável por renderizar um template liquid "https://shopify.github.io/liquid/"
        /// </summary>
        /// <param name="htmlTemplate">template html com as variáveis para serem substituidas</param>
        /// <param name="objectContextName">nome da variável de objeto referenciada no template</param>
        /// <param name="class">objeto com os dados para a renderização</param>
        /// <param name="typesToRegister">caso o objeto seja composto por outros objetos, passar os tipos dos objetos compostos</param>
        /// <returns>string do html com os valores das variáveis substituidas</returns>
        /// <example>        
        /// <code>
        /// static void Main(string[] args)
        /// {
        ///     var pessoa = new Pessoa 
        ///     { 
        ///         Nome = "Teste",
        ///         Idade = 1,
        ///         Documento = new Documento { Tipo = "RG", Descricao="1111111"  } 
        ///     }
        ///     
        ///     var html = @"<div>
        ///                     <div>Nome: {{ pessoa.Nome }}</div>
        ///                     <div>Idade: {{ pessoa.Idade }}</div>
        ///                     <span>{{ pessoa.Tipo }}: {{ pessoa.Descricao }}</span><br />
        ///                 </div>";
        /// 
        ///     var htmlTemplate = RenderTemplate(html, nameof(pessoa), pessoa, typeof(Documento));
        /// }
        /// </code>
        /// </example> 
        public static string RenderEmailTemplate(string htmlTemplate, string objectContextName, object @class, params Type[] typesToRegister)
        {
            if (FluidTemplate.TryParse(htmlTemplate, out var template))
            {
                var context = new TemplateContext();

                context.MemberAccessStrategy.Register(@class.GetType());

                foreach (var type in typesToRegister)
                    context.MemberAccessStrategy.Register(type);

                context.SetValue(objectContextName, @class);

                return template.Render(context);
            }

            throw new InvalidOperationException("Não foi possivel realizar o parse do html!");
        }

        public static string BuildParaEmail(string template, Dictionary<string, string> variables)
        {
            var _template = System.IO.File.ReadAllText(template);
            return Build(_template, variables);
        }

        public static string Build(string template, Dictionary<string, string> variables)
        {
            var matches = Regex.Matches(template, REGEX_PATTERN);
            foreach (Match match in matches)
            {
                var variableName = GetVariableName(match.Value);
                if (!variables.ContainsKey(variableName))
                {
                    Debug.WriteLine($"Variable {variableName} not found...");
                    continue;
                }
                template = template.Replace(match.Value, variables[variableName]);
            }

            return template;
        }
        private static string GetVariableName(string variable) => variable.Substring(2, variable.Length - 3);
    }
}
