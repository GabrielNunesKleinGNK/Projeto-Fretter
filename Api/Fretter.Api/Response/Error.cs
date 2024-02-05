//using Fretter.Util;

namespace Fretter.Api.Response
{
    /// <summary>
    /// Represents an Error Message
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Error Code
        /// </summary>
        public string Reference { get; private set; }
        /// <summary>
        /// Error Message
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Initializes an Error instance with a Code and Message
        /// </summary>
        /// <param name="reference">Error Reference</param>
        /// <param name="message">Error Message</param>
        public Error(string reference, string message)
        {
            this.SetReference(reference);
            this.SetMessage(message);
        }

        /// <summary>
        /// Initializes an Error instance with a Message
        /// </summary>
        /// <param name="message">Error Message</param>
        public Error(string message)
        {
            this.SetMessage(message);
        }

        /// <summary>
        /// Set the Code
        /// </summary>
        /// <param name="reference">Code</param>
        private void SetReference(string reference)
        {
            //Throw.IfIsNullOrWhiteSpace(reference);

            this.Reference = reference;
        }

        /// <summary>
        /// Set the Message
        /// </summary>
        /// <param name="message">Message</param>
        private void SetMessage(string message)
        {
            //Throw.IfIsNullOrWhiteSpace(message);

            this.Message = message;
        }

        /// <summary>
        /// Returns the Error Message
        /// </summary>
        /// <returns>Error Message</returns>
        public override string ToString() => this.Message;
    }
}
