using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bb_project.Client.Authentication
{
    public class UserAuthenticatorService : IUserAuthenticatorService
    {
        public bool Authenticated => throw new NotImplementedException();

        public string UserId => "Pigna";

        public async Task<bool> AuthenticateAsync()
        {
            return await Task.FromResult(true);
        }
    }
}
