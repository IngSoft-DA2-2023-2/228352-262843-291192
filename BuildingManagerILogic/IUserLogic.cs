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
        public User DeleteUser(Guid userId, RoleType role);
        public Guid Login(string email, string password);
        public Guid Logout(Guid sessionToken);
    }
}
