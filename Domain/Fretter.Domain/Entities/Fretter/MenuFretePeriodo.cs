using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class MenuFretePeriodo : EntityBase
    {
        #region Construtores
        private MenuFretePeriodo()
        {

        }

        public MenuFretePeriodo(int id, string dsNome, TimeSpan? hrInicio, TimeSpan? hrTermino)
        {
            this.Id = id;
            this.DsNome = dsNome;
            this.HrInicio = hrInicio;
            this.HrTermino = hrTermino;
        }
        #endregion

        #region "Propriedades"
        public string DsNome { get; protected set; }
        public TimeSpan? HrInicio { get; protected set; }
        public TimeSpan? HrTermino { get; protected set; }
        #endregion
    }
}
