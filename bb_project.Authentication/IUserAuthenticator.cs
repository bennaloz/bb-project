using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bb_project.Authentication
{
    public interface IUserAuthenticator
    {
        Task<bool> AuthenticateAsync();
    }
}
