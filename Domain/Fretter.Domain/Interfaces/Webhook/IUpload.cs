using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Interfaces.Webhook
{
    public interface IUpload
    {
        int Id { get; set; }
        string Key { get; set; }
    }

    public class Upload : IUpload
    {
        public int Id { get; set; }
        public string Key { get; set; }
    }
}
