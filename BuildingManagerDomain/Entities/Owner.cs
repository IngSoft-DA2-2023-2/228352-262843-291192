using System;
using System.Text.RegularExpressions;

namespace BuildingManagerDomain.Entities
{
    public class Owner
    {
        private string name;
        private string lastName;
        private string email;

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Owner name must not be empty");
                }
                name = value;
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Owner last name must not be empty");
                }
                lastName = value;
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Owner email must not be empty");
                }

                string emailRegexPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                Regex regex = new Regex(emailRegexPattern);

                if (!regex.IsMatch(value))
                {
                    throw new ArgumentException("Invalid email format");
                }

                email = value;
            }
        }

        public override bool Equals(object obj)
        {
            Owner owner = (Owner)obj;
            return Name == owner.Name && LastName == owner.LastName && Email == owner.Email;
        }
    }
}
