using System;
using BuildingManagerDomain.Enums;

namespace BuildingManagerDomain.Entities
{
    public class Manager
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
