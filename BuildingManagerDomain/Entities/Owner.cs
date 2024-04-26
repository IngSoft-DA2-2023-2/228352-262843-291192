using System;

namespace BuildingManagerDomain.Entities
{
    public class Owner
    {
        private string name;

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

        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
