using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerIDataAccess.Exceptions;
using BuildingManagerIImporter;
using BuildingManagerILogic;
using BuildingManagerILogic.Exceptions;
using System.Reflection;

namespace BuildingManagerLogic
{
    public class ImporterLogic : IImporterLogic
    {
        private readonly IUserLogic _userLogic;
        private readonly IConstructionCompanyRepository _companyRepository;
        private List<IImporter> Importers = new List<IImporter>();
        public ImporterLogic(IUserLogic userLogic, IConstructionCompanyRepository companyRepository)
        {
            _userLogic = userLogic;
            _companyRepository = companyRepository;
        }
        public List<ImporterBuilding> ImportData(string importerName, string data, Guid companyAdminSessionToken)
        {
            Guid contrstructionCompanyAdminId = _userLogic.GetUserIdFromSessionToken(companyAdminSessionToken);
            Guid companyId = _companyRepository.GetCompanyIdFromUserId(contrstructionCompanyAdminId);
            List<IImporter> importers = ListImporters();
            if (importers.Any(i => i.Name.Equals(importerName)) == false)
            {
                throw new NotFoundException(new ValueNotFoundException("Importer not found"), "Importer not found");
            }
            IImporter importer = importers.Find(i => i.Name.Equals(importerName));
            List<ImporterBuilding> buildings = importer.Import(data, companyId);
            List<Building> buildingsToCreate = new List<Building>();
            foreach (ImporterBuilding building in buildings)
            {
                Guid buildingId = Guid.NewGuid();
                foreach (ImporterApartment apartment in building.Apartments)
                {
                    Apartment a = new Apartment()
                    {
                        Floor = apartment.Floor,
                        Number = apartment.Number,
                        Bathrooms = apartment.Bathrooms,
                        HasTerrace = apartment.HasTerrace,
                        Rooms = apartment.Rooms,
                        BuildingId = buildingId,
                        Owner = apartment.Owner,
                    };

                }
            }


            return importer.Import(data, companyId);
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
