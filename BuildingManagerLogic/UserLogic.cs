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
            catch (ValueDuplicatedException e)
            {
                throw new DuplicatedValueException(e, e.Message);
            }
        }

        public User DeleteUser(Guid userId, RoleType role)
        {
            try
            {
                return _userRepository.DeleteUser(userId, role);
            }
            catch (ValueNotFoundException e)
            {
                throw new NotFoundException(e, e.Message);
            }
        }

        public bool Exists(Guid userId)
        {
            return _userRepository.ExistsFromSessionToken(userId);
        }

        public Guid Login(string email, string password)
        {
            try
            {
                return _userRepository.Login(email, password);
            }
            catch (ValueNotFoundException e)
            {
                throw new NotFoundException(e, e.Message);
            }
        }

        public RoleType Role(Guid userId)
        {
            return _userRepository.RoleFromSessionToken(userId);
        }
    }
}
