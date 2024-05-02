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

        public bool ExistsFromSessionToken(Guid sessionToken)
        {
            return _userRepository.ExistsFromSessionToken(sessionToken);
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

        public Guid Logout(Guid sessionToken)
        {
            try
            {
                return _userRepository.Logout(sessionToken);
            }
            catch (ValueNotFoundException e)
            {
                throw new NotFoundException(e, e.Message);
            }
        }

        public RoleType RoleFromSessionToken(Guid sessionToken)
        {
            try
            {
                return _userRepository.RoleFromSessionToken(sessionToken);
            }
            catch (ValueNotFoundException e)
            {
                throw new NotFoundException(e, e.Message);
            }
        }
    }
}
