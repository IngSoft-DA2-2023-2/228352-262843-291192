using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagerDomain.Entities
{
    public class ListManagersResponse
    {
        public List<Manager> Managers { get; set; }

        public ListManagersResponse(List<Manager> managers)
        {
            Managers = new List<Manager>();
            foreach (var manager in managers)
            {
                Manager newManager = new Manager();
                newManager.Id = manager.Id;
                newManager.Name = manager.Name;
                newManager.Lastname = manager.Lastname;
                newManager.Email = manager.Email;

                Managers.Add(newManager);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (ListManagersResponse)obj;
            foreach (var manager in Managers)
            {
                foreach (var otherManager in other.Managers)
                {
                    if (manager.Name != otherManager.Name ||
                        manager.Role != otherManager.Role ||
                        manager.Lastname != otherManager.Lastname ||
                        manager.Email != otherManager.Email)
                        return false;
                }
            }
            return true;
        }
    }
}
