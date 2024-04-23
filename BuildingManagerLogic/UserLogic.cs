using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using BuildingManagerIDataAccess;
using BuildingManagerIDataAccess.Exceptions;
using BuildingManagerILogic;
using BuildingManagerILogic.Exceptions;
using System;

namespace BuildingManagerLogic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserRepository _userRepository;
        public UserLogic(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public User CreateUser(User user)
        {
            try
            {
                return _userRepository.CreateUser(user);
            }
            catch(ValueDuplicatedException e)
            {
                throw new DuplicatedValueException(e, e.Message);
            }
        }

        public bool Exists(Guid userId)
        {
            return _userRepository.Exists(userId);
        }

        public RoleType Role(Guid userId)
        {
            return _userRepository.Role(userId);
        }
    }
}
