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
        RoleType RoleFromSessionToken(Guid sessionToken);
        User DeleteUser(Guid userId, RoleType role);
        Guid Login(string email, string password);
    }
}
