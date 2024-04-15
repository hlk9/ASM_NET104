using Org_DAL.Context;
using Org_DAL.Models;

namespace Org_DAL.Repository;

public class RoleRepository
{
    OrganicDbContext _context;
    public RoleRepository()
    {
        _context = new OrganicDbContext();
    }
    
public ICollection<Role> GetAll()
    {
        return _context.Roles.ToList();
    }

    public bool Add(Role role)
    {
        try
        {
            _context.Roles.Add(role);
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
            var role = _context.Roles.Find(id);
            _context.Roles.Remove(role);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    
    public bool Update(Role role)
    {
        try
        {
            var roleToUpdate = _context.Roles.Find(role.Id);
            roleToUpdate.RoleName = role.RoleName;
         
            
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    
}