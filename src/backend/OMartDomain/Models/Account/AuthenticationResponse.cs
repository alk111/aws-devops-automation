﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMartDomain.Models.Account
{
    public class AuthenticationResponse
    {
        //public string Message { get; set; }
        //public bool IsAuthenticated { get; set; }
        //public string Id { get; set; }
        //public string UserName { get; set; }
        //public string Email { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiration { get; set; }
    }
}
