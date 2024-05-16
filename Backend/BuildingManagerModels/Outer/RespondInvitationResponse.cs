using System;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;

namespace BuildingManagerModels.Outer
{
    public class RespondInvitationResponse
    {
        public Guid Id { get; set; }
        public InvitationStatus Status { get; set; }
        public string Email { get; set; }

        public RespondInvitationResponse(InvitationAnswer answer)
        {
            Id = answer.InvitationId;
            Email = answer.Email;
            Status = answer.Status;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (RespondInvitationResponse)obj;
            return Id == other.Id && Email == other.Email && Status == other.Status;
        }

    }
}