using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bb_project.Authentication
{
    public class UserAuthenticatorService : IUserAuthenticatorService
    {
        public bool Authenticated => throw new NotImplementedException();

        public string UserId => throw new NotImplementedException();

        public async Task<bool> AuthenticateAsync()
        {
            return await Task.FromResult(true);
        }
    }
}
