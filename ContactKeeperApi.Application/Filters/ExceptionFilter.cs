using ContactKeeperApi.Application.ViewModels;
using ContactKeeperApi.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;

namespace ContactKeeperApi.Application.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }
        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, string.Empty);
            context.HttpContext.Response.ContentType = "application/json";

            var response = new MessageResponseViewModel();

            if (context.Exception is NotFoundException)
            {
                response.Title = "Warning";
                response.Status = (int)HttpStatusCode.NotFound;
                response.Data = context.Exception.Message;
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }

            if (context.Exception is BusinessException)
            {
                response.Title = "Error";
                response.Status = (int)(HttpStatusCode)422;
                response.Data = context.Exception;
                context.HttpContext.Response.StatusCode = (int)(HttpStatusCode)422;
            }

            if (context.Exception is ValidationException)
            {
                var errorValues = ((ValidationException)context.Exception).Failures.Values;

                var message = "";

                foreach (var error in errorValues)
                    message += "- " + error[0];

                response.Title = "Warning";
                response.Status = (int)HttpStatusCode.BadRequest;
                response.Data = $"Erro de validação: {message}";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            context.Result = new JsonResult(
                new
                {
                    response.Title,
                    response.Status,
                    response.Data
                });
        }
    }
}
