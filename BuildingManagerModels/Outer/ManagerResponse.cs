using System;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;

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
    }
}