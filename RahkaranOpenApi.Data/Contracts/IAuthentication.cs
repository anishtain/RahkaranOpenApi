using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RahkaranOpenApi.Data.Contracts
{
    public interface IAuthentication
    {
        Task<AuthenticationServices.Models.AuthenticationSessionDetails> GetAuthenticationSession();

        Task<(string, string)> Login(string username, string password);
    }
}
