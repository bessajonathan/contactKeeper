﻿using ContactKeeperApi.Application.Auth;
using ContactKeeperApi.Application.Auth.ViewModel;
using ContactKeeperApi.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Net;
using System.Threading.Tasks;

namespace ContactKeeperAPI.Controllers
{
    [Route("v1/auth")]
    public class AuthController : BaseController
    {
        [HttpPost]
        [AllowAnonymous]
        [OpenApiTag("Autenticação")]
        [ProducesResponseType(typeof(IViewModel<TokenViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SignIn([FromBody] SignInCommand command)
        {
            if (command is null)
                BadRequest();

            return Ok(await Mediator.Send(command));
        }
    }
}