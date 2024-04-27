using System;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using BuildingManagerModels.CustomExceptions;

namespace BuildingManagerModels.Inner
{
    public class CreateInvitationRequest
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public long Deadline { get; set; }
        public Invitation ToEntity()
        {
            Validate();
            return new Invitation()
            {
                Email = this.Email,
                Name = this.Name,
                Deadline = this.Deadline,
                Status = InvitationStatus.PENDING,
            };
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Email))
            {
                throw new InvalidArgumentException("email");
            }
            if (string.IsNullOrEmpty(Name))
            {
                throw new InvalidArgumentException("name");
            }
            if (string.IsNullOrEmpty(Deadline.ToString()))
            {
                throw new InvalidArgumentException("deadline");
            }
            long today = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            if (today >= Deadline)
            {
                throw new InvalidArgumentException("deadline");
            }
        }
    }
}