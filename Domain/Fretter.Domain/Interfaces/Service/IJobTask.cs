using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IJobTask
    {
        bool CanExecute();
    }
}
