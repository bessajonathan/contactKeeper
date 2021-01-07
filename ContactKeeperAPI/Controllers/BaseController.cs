using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace ContactKeeperAPI.Controllers
{
    public class BaseController : Controller
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
        public string UserName => User.Claims.First(x => x.Type == "userName").Value;
        public int UserId => int.Parse(User.Claims.First(x => x.Type == "id").Value);
    }
}
