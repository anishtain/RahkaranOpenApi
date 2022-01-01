using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RahkaranOpenApi.Api.Controllers.Dtos.Customers
{
    public record RegisterNewCustomerDetails(DateTime DeclarationDate, string ExchangeCode, RegisterNewCustomerDetailsParty Party, int Type);

    public record RegisterNewCustomerDetailsParty(string FirstName, string LastName, string Mobile, string NationalID, int Type);
}
