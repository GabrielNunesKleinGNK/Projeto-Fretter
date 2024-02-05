using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Helpers
{
    public class MessageData<T>
    {
        public MessageData(string token, DateTime expiresAt, T body, bool isCompleted = false)
        {
            Token = token;
            ExpiresAt = expiresAt;
            Body = body;
            IsCompleted = isCompleted;
        }

        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public T Body { get; set; }
        public bool IsCompleted { get; set; }
    }
}
