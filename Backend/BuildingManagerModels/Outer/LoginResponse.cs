using System;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;

namespace BuildingManagerModels.Outer
{
    public class LoginResponse
    {
        public Guid Token { get; set; }
        public Guid UserId { get; set; }
        public RoleType Role { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }

        public LoginResponse(User user)
        {
            Token = (Guid)user.SessionToken;
            UserId = user.Id;
            Role = user.Role;
            Email = user.Email;
            Name = user.Name;
            Lastname = user.Lastname;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (LoginResponse)obj;
            return Token == other.Token && 
                   UserId == other.UserId && 
                   Role == other.Role && 
                   Email == other.Email && 
                   Name == other.Name && 
                   Lastname == other.Lastname;
        }
    }
}