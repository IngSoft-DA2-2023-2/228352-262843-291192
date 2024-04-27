using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using System;

namespace BuildingManagerIDataAccess
{
    public interface IUserRepository
    {
        User CreateUser(User user);
        bool Exists(Guid userId);
        bool EmailExists(string email);
        RoleType Role(Guid userId);
        User DeleteUser(Guid userId);
    }
}
