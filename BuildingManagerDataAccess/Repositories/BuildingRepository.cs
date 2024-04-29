using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerIDataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagerDataAccess.Repositories
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly DbContext _context;
        public BuildingRepository(DbContext context)
        {
            _context = context;
        }
        public Building CreateBuilding(Building building)
        {
            if (_context.Set<Building>().Any(b => b.Name == building.Name))
            {
                throw new ValueDuplicatedException("Name");
            }

            if(HasDuplicatedOwnerEmail(building.Apartments))
            {
                throw new ValueDuplicatedException("Owner email");
            }

            if(HasSameLocationAndAddress(building))
            {
                throw new ValueDuplicatedException("Location and Address");
            }

            _context.Set<Building>().Add(building);
            _context.SaveChanges();
            return building;
        }

        private bool HasDuplicatedOwnerEmail(List<Apartment> apartments)
        {
            foreach (var apartment in apartments)
            {
                if (_context.Set<Apartment>().Any(a => a.Owner.Email == apartment.Owner.Email && (a.Owner.Name != apartment.Owner.Name || a.Owner.LastName != apartment.Owner.LastName)))
                {
                    return true;
                }
            }
            return false;
        }
    
        private bool HasSameLocationAndAddress(Building building)
        {
            return _context.Set<Building>().Any(b => b.Location == building.Location && b.Address == building.Address);
        }
    
        public Building DeleteBuilding(Guid buildingId)
        {
            var building = _context.Set<Building>().Find(buildingId);

            if (building == null)
            {
                throw new ValueNotFoundException("Building");
            }

            _context.Set<Building>().Remove(building);
            _context.SaveChanges();
            return building;
        }

        public Guid GetManagerIdBySessionToken(Guid sessionToken)
        {
            if (!_context.Set<User>().Any(u => u.SessionToken == sessionToken))
            {
                throw new ValueNotFoundException("Session token");
            }

            return _context.Set<User>().FirstOrDefault(u => u.SessionToken == sessionToken)!.Id;
        }

    }
}
