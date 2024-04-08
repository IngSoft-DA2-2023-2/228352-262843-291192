using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerILogic;
using System;

namespace BuildingManagerLogic
{
    public class AdminLogic : IAdminLogic
    {
        private readonly IAdminRepository _adminRepository;
        public AdminLogic(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public Admin CreateAdmin(Admin admin)
        {
           return _adminRepository.CreateAdmin(admin);
        }
    }
}
