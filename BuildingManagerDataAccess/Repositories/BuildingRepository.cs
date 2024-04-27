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

            if (HasDuplicatedApartment(building.Apartments))
            {
                throw new ValueDuplicatedException("Apartment floor and number");
            }

            _context.Set<Building>().Add(building);
            _context.SaveChanges();
            return building;
        }

        private bool HasDuplicatedApartment(List<Apartment> apartments)
        {
            for (int i = 0; i < apartments.Count; i++)
            {
                for (int j = i + 1; j < apartments.Count; j++)
                {
                    if (apartments[i].Floor == apartments[j].Floor && apartments[i].Number == apartments[j].Number)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
