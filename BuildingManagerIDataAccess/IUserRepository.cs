using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using System;

namespace BuildingManagerIDataAccess
{
    public interface IUserRepository
    {
        User CreateUser(User user);
        bool Exists(Guid userId);
        RoleType Role(Guid userId);
    }
}
