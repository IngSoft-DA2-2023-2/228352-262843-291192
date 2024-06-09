using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using System;
using System.Collections.Generic;

namespace BuildingManagerILogic
{
    public interface IUserLogic
    {
        public User CreateUser(User user);
        public bool ExistsFromSessionToken(Guid sessionToken);
        public RoleType RoleFromSessionToken(Guid sessionToken);
        public User DeleteUser(Guid userId, RoleType role);
        public User Login(string email, string password);
        public Guid Logout(Guid sessionToken);
        public Guid GetUserIdFromSessionToken(Guid sessionToken);
        public List<Manager> GetManagers();
        public List<MaintenanceStaff> GetMaintenanceStaff();
        public Guid GetManagerIdFromEmail(string email);
    }
}
