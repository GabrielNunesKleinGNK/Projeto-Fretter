using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Extentions
{
    public static class BusExtensions
    {
        public static T As<T>(this Message message) where T : class
        {
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(message.Body));
        }
        public static Message AsMessage(this object obj)
        {
            return new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })));
        }

        public static bool Any(this IList<Message> collection)
        {
            return collection != null && collection.Count > 0;
        }
    }
}
