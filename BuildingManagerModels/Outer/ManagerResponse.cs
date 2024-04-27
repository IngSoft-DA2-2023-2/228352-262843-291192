using System;
using BuildingManagerDomain.Entities;

namespace BuildingManagerModels.Outer
{
    public class ManagerResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        public ManagerResponse(User manager)
        {
            Id = manager.Id;
            Email = manager.Email;
            Name = manager.Name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (ManagerResponse)obj;
            return Id == other.Id && Email == other.Email && Name == other.Name;
        }
    }
}