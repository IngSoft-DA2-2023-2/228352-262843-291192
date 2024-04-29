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
    }
}