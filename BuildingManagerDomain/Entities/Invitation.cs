using System;
using BuildingManagerDomain.Enums;

namespace BuildingManagerDomain.Entities
{
    public class Invitation
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public long Deadline { get; set; }
        public InvitationStatus Status { get; set; }

        public override bool Equals(object obj)
        {
            Invitation invitation = (Invitation)obj;
            return Name == invitation.Name && Email == invitation.Email && Deadline == invitation.Deadline && Status == invitation.Status;
        }
    }
}