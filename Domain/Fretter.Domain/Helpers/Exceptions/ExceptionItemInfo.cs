namespace Fretter.Domain.Helpers.Exceptions
{
    public class ExceptionItemInfo
    {
        public string Model { get; protected set; }
        public string Reference { get; protected set; }
        public string Message { get; protected set; }
        public object[] Arguments { get; protected set; }


        public ExceptionItemInfo(string model, string reference, string message, params object[] arguments)
        {
            this.Model = model;
            this.Reference = reference;
            this.Message = message;
            this.Arguments = arguments;
        }
    }
}
