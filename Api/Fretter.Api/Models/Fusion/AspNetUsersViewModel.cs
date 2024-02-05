using Fretter.Api.Helpers;
using Fretter.Domain.Entities.Fusion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fretter.Api.Models.Fusion
{
    public class AspNetUsersViewModel : IViewModel<AspNetUsers>
    {
        public int Id { get;  set; }
        public string UserName { get;  set; }

        public AspNetUsers Model()
        {
            throw new NotImplementedException();
        }
    }
}
