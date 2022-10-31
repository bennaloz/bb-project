using System;
using System.Collections.Generic;
using System.Text;

namespace bb_project.Client.Authentication.Models
{
    public class AuthenticationResult
    {
        public string UserId { get; set; }
        public Enums.AuthenticationResultCode ResultCode { get; set; }
    }
}
