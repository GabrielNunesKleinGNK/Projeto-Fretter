using Fluid;
using System;
using Fluid.Values;
using System.Globalization;

namespace Fretter.Util.HtmlTemplate
{
    public class TemplateRendererBuilder
    {
        private readonly TemplateContext _context;

        private string htmlTemplate;
        private string objectContext;
        private object @classToRender;

        public TemplateRendererBuilder() { _context = new TemplateContext(); }

        public TemplateRendererBuilder WithFilter(string filterName, FilterDelegate func)
        {
            _context.Filters.AddFilter(filterName, func);
            return this;
        }

        public TemplateRendererBuilder WithCurrencyFilter()
        {
            return WithFilter("currency", (input, arguments, context) =>
            {
                var language = arguments.At(0)?.ToStringValue();
                return new StringValue(string.Format(CultureInfo.GetCultureInfo(string.IsNullOrEmpty(language) ? "pt-BR" : language), "{0:C}", input.ToNumberValue()));
            });
        }

        public TemplateRendererBuilder WithCustomTypes(Type type)
        {
            _context.MemberAccessStrategy.Register(type);
            return this;
        }

        public TemplateRendererBuilder WithTemplate(string htmlTemplate)
        {
            if (string.IsNullOrEmpty(htmlTemplate)) throw new ArgumentNullException(nameof(htmlTemplate), "O parâmetro é obrigatório");

            this.htmlTemplate = htmlTemplate;
            return this;
        }

        public TemplateRendererBuilder SetObjectTemplateContext(string objectContext, object @class)
        {
            if (string.IsNullOrEmpty(objectContext) || @class == null) throw new ArgumentNullException("Os parametros são obrigatórios!");

            _context.SetValue(objectContext, @class);
            return WithCustomTypes(@class.GetType());
        }

        public string Build()
        {
            if (FluidTemplate.TryParse(this.htmlTemplate, out var template))
            {
                return template.Render(_context);
            }

            throw new InvalidOperationException("Não foi possivel realizar o parse do html!");
        }
    }
}
