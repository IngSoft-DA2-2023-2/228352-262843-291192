using System;
using BuildingManagerDomain.Entities;
using BuildingManagerModels.CustomExceptions;

namespace BuildingManagerModels.Inner
{
    public class CreateInvitationRequest
    {
        private long _deadline;
        private string _name;

        public string Email { get; set; }
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidArgumentException("name");
                }
                _name = value;
            }
        }
        public long Deadline
        {
            get { return _deadline; }
            set
            {
                long today = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

                if (today >= value)
                {
                    throw new InvalidArgumentException("deadline");
                }
                _deadline = value;
            }
        }
        public Invitation ToEntity()
        {
            return new Invitation()
            {
                Email = this.Email,
                Name = this.Name,
                Deadline = this.Deadline,
            };
        }


    }
}