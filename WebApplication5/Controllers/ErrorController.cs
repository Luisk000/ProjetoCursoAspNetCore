using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Controllers
{
    public class ErrorController : Controller
    {
        ILogger<ErrorController> _logger;
        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }
        [Route("/Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404: 
                    ViewBag.ErrorMessage = "O link requisitado não foi encontrado";
                    _logger.LogWarning($"Erro 404 ocorreu. Caminho = {statusCodeResult.OriginalPath} e QueryString = {statusCodeResult.OriginalQueryString}");
                    break;
            }
            return View("NotFound");
        }
        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _logger.LogError($"O caminho {exceptionDetails.Path} contém uma exceção: {exceptionDetails.Error}");
            return View("Error");
        }
    }
}
