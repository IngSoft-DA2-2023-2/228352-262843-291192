using System;

namespace BuildingManagerDomain.Entities
{
    public class Building
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
