using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerIDataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;

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

            if (HasDuplicatedOwnerEmail(building.Apartments))
            {
                throw new ValueDuplicatedException("Owner email");
            }

            if (HasSameLocationAndAddress(building))
            {
                throw new ValueDuplicatedException("Location and Address");
            }

            UpdateOwnerIfAlreadyExists(building.Apartments);

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
                if (_context.Set<Owner>().Any(o => o.Email == apartment.Owner.Email && (o.Name != apartment.Owner.Name || o.LastName != apartment.Owner.LastName)))
                {
                    return true;
                }
            }
            return false;
        }

        private void UpdateOwnerIfAlreadyExists(List<Apartment> apartments)
        {
            foreach (var apartment in apartments)
            {
                Owner existingOwner = _context.Set<Owner>().FirstOrDefault(o => o.Email == apartment.Owner.Email && o.Name == apartment.Owner.Name && o.LastName == apartment.Owner.LastName);
                if (existingOwner != null)
                {
                    apartment.Owner = existingOwner;
                }
            }

            for (int i = 0; i < apartments.Count; i++)
            {
                for (int j = i + 1; j < apartments.Count; j++)
                {
                    if (apartments[i].Owner.Email == apartments[j].Owner.Email &&
                        apartments[i].Owner.Name == apartments[j].Owner.Name &&
                        apartments[i].Owner.LastName == apartments[j].Owner.LastName)
                    {
                        apartments[j].Owner = apartments[i].Owner;
                    }
                }
            }
        }

        private bool HasSameLocationAndAddress(Building building)
        {
            return _context.Set<Building>().Any(b => b.Location == building.Location && b.Address == building.Address && b.Id != building.Id);
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

        public Building UpdateBuilding(Building newBuilding)
        {
            if (_context.Set<Building>().Any(b => b.Name == newBuilding.Name && b.Id != newBuilding.Id))
            {
                throw new ValueDuplicatedException("Name");
            }

            if (HasSameLocationAndAddress(newBuilding))
            {
                throw new ValueDuplicatedException("Location and Address");
            }

            Building buildToUpdate = _context.Set<Building>().Include(b => b.Apartments)
                                                             .ThenInclude(a => a.Owner)
                                                             .FirstOrDefault(b => b.Id == newBuilding.Id);

            buildToUpdate.Name = newBuilding.Name;
            buildToUpdate.Address = newBuilding.Address;
            buildToUpdate.Location = newBuilding.Location;
            buildToUpdate.ConstructionCompanyId = newBuilding.ConstructionCompanyId;
            buildToUpdate.CommonExpenses = newBuilding.CommonExpenses;

            List<Apartment> apartmentsInDB = buildToUpdate.Apartments;
            List<Apartment> apartmentsToDelete = new List<Apartment>();

            bool[] deletedApartments = new bool[newBuilding.Apartments.Count];
            Array.Fill(deletedApartments, false);

            foreach (Apartment apartment in apartmentsInDB)
            {
                if (newBuilding.Apartments.Find(a => a.Floor == apartment.Floor && a.Number == apartment.Number) != null)
                {
                    Apartment newAparmentData = newBuilding.Apartments.First(a => a.Floor == apartment.Floor && a.Number == apartment.Number);
                    deletedApartments[newBuilding.Apartments.IndexOf(newAparmentData)] = true;

                    apartment.Rooms = newAparmentData.Rooms;
                    apartment.Bathrooms = newAparmentData.Bathrooms;
                    apartment.HasTerrace = newAparmentData.HasTerrace;

                    Owner existingOwner = _context.Set<Owner>().FirstOrDefault(o => o.Email == newAparmentData.Owner.Email);

                    if (existingOwner != null)
                    {
                        existingOwner.Name = newAparmentData.Owner.Name;
                        existingOwner.LastName = newAparmentData.Owner.LastName;
                        apartment.Owner = existingOwner;
                    }
                    else
                    {
                        apartment.Owner = newAparmentData.Owner;
                    }
                }
                else
                {
                    apartmentsToDelete.Add(apartment);
                }
            }

            foreach (Apartment apartment in newBuilding.Apartments)
            {
                if (deletedApartments[newBuilding.Apartments.IndexOf(apartment)] == false)
                {
                    Owner existingOwner = _context.Set<Owner>().FirstOrDefault(o => o.Email == apartment.Owner.Email);

                    if (existingOwner != null)
                    {
                        existingOwner.Name = apartment.Owner.Name;
                        existingOwner.LastName = apartment.Owner.LastName;
                        apartment.Owner = existingOwner;
                    }

                    buildToUpdate.Apartments.Add(apartment);
                }
            }

            foreach (Apartment apartment in apartmentsToDelete)
            {
                buildToUpdate.Apartments.Remove(apartment);
            }

            _context.SaveChanges();
            return _context.Set<Building>().Find(newBuilding.Id)!;
        }

        public List<Building> ListBuildings()
        {
            return _context.Set<Building>().ToList();
        }

        public Guid GetConstructionCompanyFromBuildingId(Guid buildingId)
        {
            if (!_context.Set<Building>().Any(b => b.Id == buildingId)){
                throw new ValueNotFoundException("Building");
            }
            return _context.Set<Building>().Find(buildingId)!.ConstructionCompanyId;
        }

        public Building GetBuildingById(Guid buildingId)
        {
            throw new NotImplementedException();
        }
    }
}
