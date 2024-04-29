using System;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;

namespace BuildingManagerModels.Outer
{
    public class LogoutResponse
    {
        public Guid Token { get; set; }

        public LogoutResponse(Guid token)
        {
            Token = token;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (LogoutResponse)obj;
            return Token == other.Token;
        }
    }
}