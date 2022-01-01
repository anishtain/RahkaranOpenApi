using RahkaranOpenApi.Utilities.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RahkaranOpenApi.Data.AuthenticationServices
{
    public class AuthenticationService : Contracts.IAuthentication
    {
        private readonly Contracts.ICustomeRestRequest _customeRestRequest;

        public AuthenticationService(Contracts.ICustomeRestRequest customeRestRequest)
        {
            _customeRestRequest = customeRestRequest;
        }

        public async Task<Models.AuthenticationSessionDetails> GetAuthenticationSession()
        {
            var response = await _customeRestRequest
                .Get<object, Models.AuthenticationSessionDetails>("Framework/Services/AuthenticationService.svc/session", null);

            return JsonSerializer.Deserialize<Models.AuthenticationSessionDetails>(response.Content);
        }

        public async Task<(string, string)> Login(string username, string password)
        {
            var sessionDetails = await GetAuthenticationSession();

            var rsa = new RSACryptoServiceProvider(1024);
            rsa.ImportParameters(new RSAParameters()
            {
                Exponent = sessionDetails.rsa.E.HexStringToBytes(),
                Modulus = sessionDetails.rsa.M.HexStringToBytes()
            });

            var encryptedPassword = $"{sessionDetails.id}**{password}";

            var response = await _customeRestRequest.Post<object, object>("Framework/Services/AuthenticationService.svc/login", new
            {
                sessionId = sessionDetails.id,
                username = username,
                password = rsa.Encrypt(Encoding.Default.GetBytes(encryptedPassword), false).BytesToHexString()
            });
            var setCookieHeader = response.Headers.FirstOrDefault(x => x.Name == "Set-Cookie").Value.ToString();
            var sgAuthHeader = setCookieHeader
                .Split(',')[1];
            string authCookie = sgAuthHeader
                .Split('=')[1].Split(';')[0];
            return (sessionDetails.id, authCookie);
        }
    }
}
