using BuildingManagerDomain.Enums;
using System;

namespace BuildingManagerDomain.Entities
{
    public class InvitationAnswer 
    {
        public Guid InvitationId { get; set; }
        public InvitationStatus Status { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
