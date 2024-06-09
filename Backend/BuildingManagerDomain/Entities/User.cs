using BuildingManagerDomain.Enums;
using System;
using System.Security.Policy;

namespace BuildingManagerDomain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Lastname { get; set; }
        public RoleType Role { get; set;}  
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid? SessionToken {get; set;}
    }
}
