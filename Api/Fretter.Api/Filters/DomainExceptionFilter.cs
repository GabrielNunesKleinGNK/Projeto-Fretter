using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Fretter.Api.Response;
using Fretter.Domain.Exceptions;

namespace Fretter.Api.Filters
{
    public class DomainExceptionFilter : ExceptionFilterAttribute
    {
        //private IStringLocalizer _localizer;
        public DomainExceptionFilter()
        {
            //IStringLocalizer localizer
            //this._localizer = localizer;
        }
        public override void OnException(ExceptionContext context)
        {

            if (context.Exception is DomainSummaryException)
                context.Result = DomainSummaryException(context.Exception as DomainSummaryException);

            if (context.Exception is DomainException)
                context.Result = DomainException(context.Exception as DomainException);

            if (context.Exception.InnerException != null)
            {
                if (context.Exception.InnerException is DomainSummaryException)
                    context.Result = DomainSummaryException(context.Exception.InnerException as DomainSummaryException);

                if (context.Exception.InnerException is DomainException)
                    context.Result = DomainException(context.Exception.InnerException as DomainException);
            }

            if (!(context.Exception is DomainException) && !(context.Exception is DomainSummaryException))
                context.Result = Exception(context.Exception);

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (context.Exception is UsuarioExpiradoException)
            {
                context.Result = DomainException(context.Exception as DomainException);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }

            //Debug.WriteLine(context.Exception.GetFullMessage());
        }


        #region Metodos

        protected virtual JsonResult Exception(Exception exception)
        {
            //Debug.WriteLine($"{exception.GetType().FullName}: {exception.GetFullMessage()}");

            var error = new Error(exception.GetType().FullName, exception.Message);
            var result = new Result(error);
            return new JsonResult(result);
        }

        protected virtual JsonResult DomainException(DomainException exception)
        {
            string msg = ObterMensagem(exception.ExceptionItemInfo);

            Debug.WriteLine($"{exception.ExceptionItemInfo.Model}.{exception.ExceptionItemInfo.Reference}: {msg}");

            var error = new Error(exception.ExceptionItemInfo.Model, msg);
            var result = new Result(error);
            return new JsonResult(result);
        }

        protected virtual JsonResult DomainSummaryException(DomainSummaryException exception)
        {
            var error = exception.Exceptions.Select(ex =>
            {
                Debug.WriteLine($"{ex.Model}.{ex.Reference}: {ex.Message}");
                return new Error(ex.Model, ObterMensagem(ex));
            });
            var result = new Result(error);
            return new JsonResult(result);
        }

        protected virtual string ObterDadosRequest(HttpContext context)
        {
            if (!context.Items.Any(x => x.Key.Equals("body"))) return string.Empty;
            var body = context.Items.FirstOrDefault(x => x.Key.Equals("body"));
            return body.Value.ToString();
        }

        private string ObterMensagem(ExceptionItemInfo exception)
        {
            return exception.Message;// string.IsNullOrEmpty(_localizer[exception.Message]) ? exception.Message : _localizer[exception.Message, ObterArgumentos(exception.Arguments)];
        }

        private object[] ObterArgumentos(object[] argumentos)
        {
            return argumentos.Select(arg =>
            {
                return arg.ToString();// string.IsNullOrWhiteSpace(_localizer[arg.ToString()]) ? arg.ToString() : _localizer[arg.ToString()];
            }).ToArray();
        }


        #endregion

    }
}
