using Org_DAL.Context;
using Org_DAL.Models;

namespace Org_DAL.Repository;

public class UserRoleRepository
{
    OrganicDbContext _context;
    public UserRoleRepository()
    {
        _context = new OrganicDbContext();
    }
    
    public ICollection<UserRole> GetAll()
    {
        return _context.UserRoles.ToList();
    }

    public ICollection <UserRole> GetByUserId(Guid id)
    {
        return _context.UserRoles.Where(x=>x.UserId == id).ToList();
    }
    
    public bool Add(UserRole userRole)
    {
        try
        {
            _context.UserRoles.Add(userRole);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    
    public bool Delete(int id)
    {
        try
        {
            var userRole = _context.UserRoles.Find(id);
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    
    public bool Update(UserRole userRole)
    {
        try
        {
            var userRoleToUpdate = _context.UserRoles.Find(userRole.Id);
            userRoleToUpdate.UserId = userRole.UserId;
            userRoleToUpdate.RoleId = userRole.RoleId;
            
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    
}