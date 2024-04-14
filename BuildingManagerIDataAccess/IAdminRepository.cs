using BuildingManagerDomain.Entities;

namespace BuildingManagerIDataAccess
{
    public interface IAdminRepository
    {
        Admin CreateAdmin(Admin admin);
    }
}
