using System;
using System.Collections.Generic;
using System.Text;

namespace RahkaranOpenApi.Data.AuthenticationServices.Models
{
    public record AuthenticationSessionDetails(string id, RsaDetails rsa);

    public record RsaDetails(string M, string E);
}
