using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RahkaranOpenApi.Api.Controllers.Dtos.Factors
{
    public record RegisterInvoicesDetails(string currencyId, string customerId, DateTime date,
        int payerType, string salesAreaId, string salesOfficeId, string salesTypeId,
        int recipientType, string agentId, IList<RegisterInvoicesDetailsItem> items);

    public record RegisterInvoicesDetailsItem(string productId, string quantity, string salesAreaId, string unitId, int type);
}
