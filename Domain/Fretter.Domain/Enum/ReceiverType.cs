using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Enum
{
    public enum ReceiverType : int
    {
        Queue = 1,
        Topic = 2,
        DeadLetter = 3
    }
}
