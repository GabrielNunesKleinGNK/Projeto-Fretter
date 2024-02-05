namespace System
{
    public static class ExceptionExtentions
    {
        public static string GetFullMessage(this Exception ex)
        {
            return ex.InnerException == null
                ? ex.Message
                : ex.Message + " --> " + ex.InnerException.GetFullMessage();
        }
    }
}