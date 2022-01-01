using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RahkaranOpenApi.Api.Controllers.Dtos.Factors
{
    public record RegisterInvoicesDetails(string CurrencyId, string CustomerId, DateTime Date,
        int PayerType, string SalesAreaId, string SalesOfficeId, string SalesTypeId,
        int RecipientType, string AgentID, IList<RegisterInvoicesDetailsItem> Items);

    public record RegisterInvoicesDetailsItem(string ProductId, string Quantity, string SalesAreaId, string UnitId, int Type);
}
