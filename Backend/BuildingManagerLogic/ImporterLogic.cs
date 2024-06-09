using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess.Exceptions;
using BuildingManagerIImporter;
using BuildingManagerIImporter.Exceptions;
using BuildingManagerILogic;
using BuildingManagerILogic.Exceptions;
using System.Reflection;

namespace BuildingManagerLogic
{
    public class ImporterLogic : IImporterLogic
    {
        private readonly IUserLogic _userLogic;
        private readonly IConstructionCompanyLogic _companyLogic;
        private readonly IBuildingLogic _buildingLogic;
        private List<IImporter> Importers = new List<IImporter>();
        public ImporterLogic(IUserLogic userLogic, IConstructionCompanyLogic companyLogic, IBuildingLogic buildingLogic)
        {
            _userLogic = userLogic;
            _companyLogic = companyLogic;
            _buildingLogic = buildingLogic;
        }
        public List<Building> ImportData(string importerName, string data, Guid companyAdminSessionToken)
        {
            Guid constructionCompanyAdminId = _userLogic.GetUserIdFromSessionToken(companyAdminSessionToken);
            Guid companyId = _companyLogic.GetCompanyIdFromUserId(constructionCompanyAdminId);
            List<IImporter> importers = ListImporters();
            if (importers.Any(i => i.Name.Equals(importerName)) == false)
            {
                throw new NotFoundException(new ValueNotFoundException("Importer not found"), "Importer not found");
            }
            IImporter importer = importers.Find(i => i.Name.Equals(importerName));
            List<ImporterBuilding> buildings;
            try
            {
                buildings = importer.Import(data, companyId);
            }
            catch (NotCorrectImporterException e)
            {
                throw new NotFoundException(e, "Error while importing the data");
            }
            List<Building> buildingsToCreate = new List<Building>();
            foreach (ImporterBuilding building in buildings)
            {
                Guid buildingId = Guid.NewGuid();
                List<Apartment> apartments = new List<Apartment>();
                foreach (ImporterApartment apartment in building.Apartments)
                {
                    Owner owner = _buildingLogic.GetOwnerFromEmail(apartment.OwnerEmail);
                    Apartment a = new Apartment()
                    {
                        Floor = apartment.Floor,
                        Number = apartment.Number,
                        Bathrooms = apartment.Bathrooms,
                        HasTerrace = apartment.HasTerrace,
                        Rooms = apartment.Rooms,
                        BuildingId = buildingId,
                        Owner = owner,
                    };
                    apartments.Add(a);
                }
                Guid managerId = _userLogic.GetManagerIdFromEmail(building.Manager);
                Building b = new Building()
                {
                    Id = buildingId,
                    Address = building.Address,
                    Apartments = apartments,
                    ConstructionCompanyId = companyId,
                    ManagerId = managerId,
                    Name = building.Name,
                    Location = building.Location,
                    CommonExpenses = building.CommonExpenses,
                };

                buildingsToCreate.Add(b);
            }
            if (buildingsToCreate.Count == buildings.Count)
            {
                foreach (Building b in buildingsToCreate)
                {
                    if (buildingsToCreate.Any(bc => bc.Id != b.Id && (bc.Name == b.Name || bc.Address == b.Address || bc.Location == b.Location)))
                    {
                        throw new DuplicatedValueException(new ValueDuplicatedException("Duplicated buildings"), "Duplicated buildings");
                    }
                }
            }
            foreach (Building b in buildingsToCreate)
            {
                _buildingLogic.CreateBuilding(b, companyAdminSessionToken);
            }

            return buildingsToCreate;
        }

        public List<IImporter> ListImporters()
        {
            string[] files = Directory.GetFiles("./Importers", "*.dll");
            List<IImporter> Importers = new List<IImporter>();

            foreach (string file in files)
            {
                Assembly assembly = Assembly.LoadFrom(file);

                foreach (Type type in assembly.GetTypes())
                {
                    if (Activator.CreateInstance(type) is IImporter importer)
                    {
                        Importers.Add(importer);
                    }
                }
            }
            this.Importers = Importers;
            return Importers;
        }

        public List<string> ListImportersNames()
        {
            List<IImporter> importers = ListImporters();
            List<string> importersNames = new List<string>();
            foreach (IImporter importer in importers)
            {
                importersNames.Add(importer.Name);
            }
            return importersNames;
        }
    }
}
