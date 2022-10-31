using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bb_project.Client.Authentication
{
    public interface IUserAuthenticatorService
    {
        bool Authenticated { get; }

        string UserId { get; }
        Task<bool> AuthenticateAsync();
    }
}
