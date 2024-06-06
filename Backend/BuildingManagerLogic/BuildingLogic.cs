using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerIDataAccess.Exceptions;
using BuildingManagerILogic;
using BuildingManagerILogic.Exceptions;
using System;
using System.Collections.Generic;

namespace BuildingManagerLogic
{
    public class BuildingLogic : IBuildingLogic
    {
        private IBuildingRepository _buildingRepository;
        private IConstructionCompanyLogic _constructionCompanyLogic;
        private IUserLogic _userLogic;

        public BuildingLogic(IBuildingRepository buildingRepository, IConstructionCompanyLogic constructionCompanyLogic, IUserLogic userLogic)
        {
            _buildingRepository = buildingRepository;
            _constructionCompanyLogic = constructionCompanyLogic;
            _userLogic = userLogic;
        }

        public Building CreateBuilding(Building building, Guid sessionToken)
        {
            Guid userId = _userLogic.GetUserIdFromSessionToken(sessionToken);
            Guid companyId = _constructionCompanyLogic.GetCompanyIdFromUserId(userId);
            building.ConstructionCompanyId = companyId;
            try
            {
                if (HasDuplicatedApartment(building.Apartments))
                {
                    throw new ValueDuplicatedException("Apartment floor and number");
                }

                if (HasDistinctOwnersWithSameEmail(building.Apartments))
                {
                    throw new ValueDuplicatedException("Owner email");
                }

                return _buildingRepository.CreateBuilding(building);
            }
            catch (ValueDuplicatedException e)
            {
                throw new DuplicatedValueException(e, e.Message);
            }
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

        private bool HasDistinctOwnersWithSameEmail(List<Apartment> apartments)
        {
            for (int i = 0; i < apartments.Count; i++)
            {
                for (int j = i + 1; j < apartments.Count; j++)
                {
                    if (apartments[i].Owner.Email == apartments[j].Owner.Email &&
                        (apartments[i].Owner.Name != apartments[j].Owner.Name ||
                        apartments[i].Owner.LastName != apartments[j].Owner.LastName))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public Building DeleteBuilding(Guid buildingId, Guid sessionToken)
        {
            Guid userId = _userLogic.GetUserIdFromSessionToken(sessionToken);
            Guid companyId = GetConstructionCompanyFromBuildingId(buildingId);
            if (_constructionCompanyLogic.IsUserAssociatedToCompany(userId, companyId))
            {
                return _buildingRepository.DeleteBuilding(buildingId);
            }
            else throw new ValueNotFoundException("Building");
        }

        public Building UpdateBuilding(Building building)
        {
            try
            {
                if (HasDuplicatedApartment(building.Apartments))
                {
                    throw new ValueDuplicatedException("Apartment floor and number");
                }

                if (HasDistinctOwnersWithSameEmail(building.Apartments))
                {
                    throw new ValueDuplicatedException("Owner email");
                }

                return _buildingRepository.UpdateBuilding(building);
            }
            catch (ValueDuplicatedException e)
            {
                throw new DuplicatedValueException(e, e.Message);
            }
        }

        public List<BuildingResponse> ListBuildings()
        {
            return _buildingRepository.ListBuildings();
        }

        public Guid GetConstructionCompanyFromBuildingId(Guid buildingId)
        {
            try
            {
                return _buildingRepository.GetConstructionCompanyFromBuildingId(buildingId);
            }
            catch (ValueNotFoundException e)
            {
                throw new NotFoundException(e, e.Message);
            }
        }

        public Building GetBuildingById(Guid buildingId)
        {
            try
            {
                return _buildingRepository.GetBuildingById(buildingId);
            }
            catch (ValueNotFoundException e)
            {
                throw new NotFoundException(e, e.Message);
            }
        }

        public Guid ModifyBuildingManager(Guid managerId, Guid buildingId)
        {
            try
            {
                return _buildingRepository.ModifyBuildingManager(managerId, buildingId);
            }
            catch (ValueNotFoundException e)
            {
                throw new NotFoundException(e, e.Message);
            }
        }
    }
}
