using System;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using BuildingManagerModels.CustomExceptions;

namespace BuildingManagerModels.Inner
{
    public class LoginRequest
    {
        public string Email { get;}
        public string Password { get; }

        public LoginRequest(string email, string password)
        {
            Email = email;
            Password = password;
            Validate();
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Email))
            {
                throw new InvalidArgumentException("email");
            }
            if (string.IsNullOrEmpty(Password))
            {
                throw new InvalidArgumentException("password");
            }
        }
    }
}