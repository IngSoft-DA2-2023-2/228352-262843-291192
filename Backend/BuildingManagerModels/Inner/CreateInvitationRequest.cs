using System;
using System.Text.RegularExpressions;
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
            if (!IsValidEmail(Email))
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

        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Owner email must not be empty");
            }

            string emailRegexPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(emailRegexPattern);

            if (!regex.IsMatch(email))
            {
                throw new ArgumentException("Invalid email format");
            }

            return true;
        }
    }
}