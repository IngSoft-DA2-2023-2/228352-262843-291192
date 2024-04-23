using BuildingManagerDomain.Entities;

namespace BuildingManagerIDataAccess
{
    public interface IUserRepository
    {
        User CreateUser(User user);
    }
}
