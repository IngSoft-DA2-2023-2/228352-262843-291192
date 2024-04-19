using System;
using BuildingManagerDomain.Entities;

namespace BuildingManagerModels.Outer
{
    public abstract class CreateUserResponse<T> where T : User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        protected CreateUserResponse(T user)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (CreateUserResponse<T>)obj;
            return Id == other.Id && Name == other.Name && Email == other.Email;
        }
    }
}
