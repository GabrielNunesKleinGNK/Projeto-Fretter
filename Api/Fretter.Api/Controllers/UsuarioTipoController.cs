using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Fretter.Api.Helpers;
using Fretter.Api.Models;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Applications;
using Fretter.Repository.Contexts;

namespace Fretter.Api.Controllers
{
    public class UsuarioTipoController : ControllerBase<FretterContext,UsuarioTipo, UsuarioTipoViewModel>
    {
        public UsuarioTipoController(IApplicationBase<FretterContext, UsuarioTipo> application, IUsuarioHelper user) :base(application)
        {
        }

    }
}
