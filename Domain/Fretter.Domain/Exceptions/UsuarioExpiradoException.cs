using System;

namespace Fretter.Domain.Exceptions
{
    public class UsuarioExpiradoException : Exception
    {
        public ExceptionItemInfo ExceptionItemInfo { get; set; }
        public UsuarioExpiradoException(string model, string reference, string message) : this(new ExceptionItemInfo(model, reference, message))
        {

        }
        public UsuarioExpiradoException(ExceptionItemInfo exceptionItemInfo) : base(exceptionItemInfo.Message) => this.ExceptionItemInfo = exceptionItemInfo;
    }
}
