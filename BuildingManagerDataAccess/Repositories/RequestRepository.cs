using BuildingManagerDomain.Entities;
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

        public List<Request> GetRequests()
        {
            return _context.Set<Request>().Include(r => r.MaintenanceStaff).Include(r => r.Category).ToList();
        }
    }
}