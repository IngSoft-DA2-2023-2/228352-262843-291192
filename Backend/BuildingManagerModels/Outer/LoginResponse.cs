using System;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;

namespace BuildingManagerModels.Outer
{
    public class LoginResponse
    {
        public Guid Token { get; set; }

        public LoginResponse(Guid token)
        {
            Token = token;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (LoginResponse)obj;
            return Token == other.Token;
        }
    }
}