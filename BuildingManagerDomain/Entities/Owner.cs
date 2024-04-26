using System;

namespace BuildingManagerDomain.Entities
{
    public class Owner
    {
        private string name;
        private string lastName;

        public string Name
        {
            get { return name; }
            set
            {
                if (value == null)
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
                if (value == null)
                {
                    throw new ArgumentException("Owner last name must not be empty");
                }
                lastName = value;
            }
        }

        public string Email { get; set; }
    }
}
