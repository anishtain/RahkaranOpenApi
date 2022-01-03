using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RahkaranOpenApi.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RahkaranOpenApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly Data.Contracts.IAuthentication _authentication;
        private readonly ICustomeRestRequest _restRequest;

        public CustomerController(IAuthentication authentication, ICustomeRestRequest restRequest)
        {
            _authentication = authentication;
            _restRequest = restRequest;
        }

        /// <summary>
        /// ثبت مشتری جدید
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("RegisterCustomer")]
        public async Task<IActionResult> GetLoginDetails([FromBody] Dtos.Customers.RegisterNewCustomerDetails input)
        {
            try
            {
                var identity = await _authentication.Login("0850029120", "987654321");

                var response = await _restRequest.Post<Dtos.Customers.RegisterNewCustomerDetails, object>(
                    "/Sales/PartyManagement/Services/PartyManagementService.svc/PlaceCustomer",
                    input,
                    new KeyValuePair<string, string>("Set-Cookie", $"sg-auth-sg={identity.Item2}"), new KeyValuePair<string, string>("Cookie", identity.Item1));
                return Ok(response.Data);
            }
            catch(Exception error)
            {
                return BadRequest(error.Message);
            }
        }


    }
}
