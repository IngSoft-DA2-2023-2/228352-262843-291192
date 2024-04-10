using System;

namespace BuildingManagerDomain.Entities
{
    public class Invitation
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public Int64 Deadline { get; set; }
    }
}