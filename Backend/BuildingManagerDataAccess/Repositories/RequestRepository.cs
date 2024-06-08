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
                Request request = _context.Set<Request>().Include(r => r.Category).Include(r => r.Building).First(r => r.Id == id);
                request.MaintainerStaffId = maintenanceStaffId;
                _context.SaveChanges();
                request = _context.Set<Request>().Include(r => r.MaintenanceStaff).Include(r => r.Category).Include(r => r.Building).First(r => r.Id == id);
                return request;
            }
            catch (Exception)
            {
                throw new ValueNotFoundException("Request or maintenance staff not found.");
            }
        }

        public Request AttendRequest(Guid id, Guid maintainerStaffId)
        {
            try
            {
                Request request = _context.Set<Request>().Include(r => r.MaintenanceStaff).Include(r => r.Category).Include(r => r.Building).First(r => r.Id == id);
                request.State = RequestState.ATTENDING;
                request.AttendedAt = DateTimeOffset.Now.ToUnixTimeSeconds();
                _context.SaveChanges();
                return request;
            }
            catch (InvalidOperationException e)
            {
                throw new ValueNotFoundException(e.Message);
            }
            catch (Exception)
            {
                throw new ValueNotFoundException("Request not found.");
            }

        }

        public Request CompleteRequest(Guid id, int cost)
        {
            try
            {
                Request request = _context.Set<Request>().Include(r => r.MaintenanceStaff).Include(r => r.Category).Include(r => r.Building).First(r => r.Id == id);
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

        public Request CreateRequest(Request request, Guid managerSessionToken)
        {
            try
            {
                Manager manager = _context.Set<Manager>().First(i => i.SessionToken == managerSessionToken);
                request.ManagerId = manager.Id;
                _context.Set<Request>().Add(request);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new ValueNotFoundException("CategoryId or ApartmentNumber with ApartmentFloor in BuildingId not found.");
            }

            return request;
        }

        public List<Request> GetAssignedRequests(Guid sessionToken)
        {
            Guid maintainerStaffId = Guid.Empty;
            try
            {
                MaintenanceStaff maintenanceStaff = _context.Set<MaintenanceStaff>().First(i => i.SessionToken == sessionToken);
                maintainerStaffId = maintenanceStaff.Id;
            }
            catch (InvalidOperationException)
            {
                throw new ValueNotFoundException("User not found.");
            }
            return _context.Set<Request>().Where(r => r.MaintainerStaffId == maintainerStaffId).Include(r => r.MaintenanceStaff).Include(r => r.Category).Include(r => r.Building).ToList();
        }

        public List<Request> GetRequests()
        {
            return _context.Set<Request>().Include(r => r.MaintenanceStaff).Include(r => r.Category).Include(r => r.Building).ToList();
        }

        public List<Request> GetRequestsByManager(Guid managerSessionToken, string? category)
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
            if (!string.IsNullOrEmpty(category))
            {
                return _context.Set<Request>().Where(r => r.ManagerId == managerId && r.Category.Name == category).Include(r => r.Category).Include(r => r.MaintenanceStaff).Include(r => r.Building).ToList();
            }
            else
            {
                return _context.Set<Request>().Where(r => r.ManagerId == managerId).Include(r => r.Category).Include(r => r.MaintenanceStaff).Include(r => r.Building).ToList();
            }
        }
    }
}