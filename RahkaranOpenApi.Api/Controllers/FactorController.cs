using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RahkaranOpenApi.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RahkaranOpenApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FactorController : ControllerBase
    {
        private readonly Data.Contracts.IAuthentication _authentication;
        private readonly ICustomeRestRequest _restRequest;

        public FactorController(IAuthentication authentication, ICustomeRestRequest restRequest)
        {
            _authentication = authentication;
            _restRequest = restRequest;
        }

        [HttpPost("RegisterInvoice")]
        public async Task<IActionResult> RegisterInvoice(Dtos.Factors.RegisterInvoicesDetails input)
        {
            try
            {
                var identity = await _authentication.Login("0850029120", "987654321");

                var response = await _restRequest.Post<Dtos.Factors.RegisterInvoicesDetails, object>(
                    "Sales/OrderManagement/Services/OrderManagementService.svc/PlaceQuotation",
                    input,
                    new KeyValuePair<string, string>("Set-Cookie", $"sg-auth-sg={identity.Item2}"), new KeyValuePair<string, string>("Cookie", identity.Item1));
                return Ok(new { InvoiceId = response.Data });
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}
