using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerILogic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuildingManagerLogic
{
    public class ImporterLogic : IImporterLogic
    {
        public readonly List<IImporter> Importers;
        private readonly IUserLogic _userLogic;
        private readonly IConstructionCompanyRepository _companyRepository;
        public ImporterLogic(IUserLogic userLogic, IConstructionCompanyRepository companyRepository) 
        {
            Importers = new List<IImporter>();
            _userLogic = userLogic;
            _companyRepository = companyRepository;
        }
        public List<Building> ImportData(string importerName, string path, Guid companyAdminSessionToken)
        {
            Guid contrstructionCompanyAdminId = _userLogic.GetUserIdFromSessionToken(companyAdminSessionToken);
            Guid companyId = _companyRepository.GetCompanyIdFromUserId(contrstructionCompanyAdminId);
            IImporter importer = Importers.Find(i => i.Name.Equals(importerName));
            return importer.Import(path, companyId);
        }

        public List<string> ListImporters()
        {
            return Importers.Select(i => i.Name).ToList();
        }
        public void RegisterImporter(IImporter importer)
        {
            if (Importers.Any(i => i.Name == importer.Name))
            {
                throw new Exception($"Importer with name {importer.Name} is already registered.");
            }
            Importers.Add(importer);
        }
    }
}
