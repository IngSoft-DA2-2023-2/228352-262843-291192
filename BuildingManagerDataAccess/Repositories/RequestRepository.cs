using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using BuildingManagerIDataAccess;
using BuildingManagerIDataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BuildingManagerDataAccess.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly DbContext _context;
        public RequestRepository(DbContext context)
        {
            _context = context;
        }

        public Request AssignStaff(Guid id, Guid maintenanceStaffId)
        {
            try
            {
                Request request = _context.Set<Request>().Find(id);
                request.MaintainerStaffId = maintenanceStaffId;
                _context.SaveChanges();
                return request;
            }
            catch (Exception)
            {
                throw new ValueNotFoundException("Request or maintenance staff not found.");
            }
        }

        public Request AttendRequest(Guid id , Guid managerSessionToken)
        {
            try
            {
                Request request = _context.Set<Request>().Find(id);
                request.State = RequestState.ATTENDING;
                request.MaintainerStaffId = managerSessionToken;
                request.AttendedAt = DateTimeOffset.Now.ToUnixTimeSeconds();
                _context.SaveChanges();
                return request;
            }
            catch(Exception)
            {
                throw new ValueNotFoundException("Request not found.");
            }
            
        }

        public Request CompleteRequest(Guid id, int cost)
        {
            try
            {
                Request request = _context.Set<Request>().Find(id);
                request.State = RequestState.CLOSE;
                request.CompletedAt = DateTimeOffset.Now.ToUnixTimeSeconds();
                request.Cost = cost;
                _context.SaveChanges();
                return request;
            }
            catch (Exception)
            {
                throw new ValueNotFoundException("Request not found.");
            }

        }

        public Request CreateRequest(Request request)
        {
            try
            {
                _context.Set<Request>().Add(request);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new ValueNotFoundException("CategoryId or ApartmentNumber with ApartmentFloor in BuildingId not found.");
            }

            return request;
        }

        public object GetAssignedRequests(Guid sessionToken)
        {
            return _context.Set<Request>().Where(r => r.MaintainerStaffId == sessionToken).ToList();
        }

        public List<Request> GetRequests()
        {
            return _context.Set<Request>().Include(r => r.MaintenanceStaff).Include(r => r.Category).ToList();
        }

        public List<Request> GetRequestsByManager(Guid managerSessionToken)
        {
            Manager manager;
            try
            {
                manager = _context.Set<Manager>().First(i => i.SessionToken == managerSessionToken);
            }
            catch (InvalidOperationException)
            {
                throw new ValueNotFoundException("User not found.");
            }
            Guid managerId = manager.Id;
            return _context.Set<Request>().Where(r => r.ManagerId == managerId).ToList();
        }
    }
}