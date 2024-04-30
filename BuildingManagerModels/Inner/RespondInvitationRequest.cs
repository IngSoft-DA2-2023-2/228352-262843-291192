using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using BuildingManagerModels.CustomExceptions;
using System;

namespace BuildingManagerModels.Inner
{
    public class RespondInvitationRequest
    {
        public InvitationStatus Status { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public InvitationAnswer ToEntity(Guid id)
        {
            Validate();
            return new InvitationAnswer()
            {
                InvitationId = id,
                Status = this.Status,
                Email = this.Email,
                Password = this.Password
            };
        }

        public void Validate()
        { 
            if (Status.Equals(InvitationStatus.ACCEPTED))
            {
                if (string.IsNullOrEmpty(Email))
                {
                    throw new InvalidArgumentException("email");
                }
                if (string.IsNullOrEmpty(Password))
                {
                    throw new InvalidArgumentException("password");
                }
            }
        }

    }
}