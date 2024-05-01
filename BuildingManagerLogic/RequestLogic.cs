using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerIDataAccess.Exceptions;
using BuildingManagerILogic;
using BuildingManagerILogic.Exceptions;
using System;
using System.Collections.Generic;

namespace BuildingManagerLogic
{
    public class RequestLogic : IRequestLogic
    {
        private readonly IRequestRepository _requestRepository;
        public RequestLogic(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public Request AssignStaff(Guid id, Guid maintenanceStaffId)
        {
            try
            {
                return _requestRepository.AssignStaff(id, maintenanceStaffId);
            }
            catch (ValueNotFoundException e)
            {
                throw new NotFoundException(e, e.Message);
            }
        }

        public Request AttendRequest(Guid id, Guid managerSessionToken)
        {
            try
            {
                return _requestRepository.AttendRequest(id, managerSessionToken);
            }
            catch (ValueNotFoundException e)
            {
                throw new NotFoundException(e, e.Message);
            }
        }

        public Request CompleteRequest(Guid id, int cost)
        {
            try
            {
                return _requestRepository.CompleteRequest(id, cost);
            }
            catch (ValueNotFoundException e)
            {
                throw new NotFoundException(e, e.Message);
            }
        }

        public Request CreateRequest(Request request)
        {
            try
            {
                return _requestRepository.CreateRequest(request);
            }
            catch (ValueNotFoundException e)
            {
                throw new NotFoundException(e, e.Message);
            }

        }

        public List<Request> GetRequests()
        {
            return _requestRepository.GetRequests();
        }
    }
}