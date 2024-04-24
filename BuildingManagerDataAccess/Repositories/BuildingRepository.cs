using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
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
            _context.Set<Building>().Add(building);
            _context.SaveChanges();

            return building;
        }
    }
}
