using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using System;

namespace BuildingManagerILogic
{
    public interface IUserLogic
    {
        public User CreateUser(User user);
        public bool Exists(Guid userId);
        public RoleType Role(Guid userId);

    }
}
