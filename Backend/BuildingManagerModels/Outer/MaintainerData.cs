using BuildingManagerDomain.Entities;
using System;

namespace BuildingManagerModels.Outer
{
    public class MaintainerData
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }

        public MaintainerData(MaintenanceStaff maintainer)
        {
            Id = maintainer.Id;
            Name = maintainer.Name;
            Lastname = maintainer.Lastname;
            Email = maintainer.Email;
        }
    }
}
