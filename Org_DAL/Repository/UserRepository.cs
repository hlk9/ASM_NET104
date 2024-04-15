using Org_DAL.Context;
using Org_DAL.Models;

namespace Org_DAL.Repository;

public class UserRepository
{
    OrganicDbContext _context;
    public UserRepository()
    {
        _context = new OrganicDbContext();
    }
    
    public ICollection<User> GetAll()
    {
        return _context.Users.ToList();
    }
    
    public bool Add(User user)
    {
        try
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }


    public bool AddCustomer(User user)
    {
        try
        {
            _context.Users.Add(user);
            _context.UserRoles.Add(new UserRole
            {
                UserId = user.Id,
                RoleId = 3
               
            });
            _context.Carts.Add(new Cart
            {
                UserId = user.Id
            });
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
            var user = _context.Users.Find(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    
    public bool Update(User user)
    {
        try
        {
            var userToUpdate = _context.Users.Find(user.Id);
            userToUpdate.FullName = user.FullName;
            userToUpdate.DateOfBirth = user.DateOfBirth;
            userToUpdate.Email = user.Email;
            userToUpdate.Password = user.Password;
            userToUpdate.Phone = user.Phone;
            userToUpdate.Gender = user.Gender;
            userToUpdate.UserName = user.UserName;
            userToUpdate.Status = user.Status;
            userToUpdate.UpdatedDate = user.UpdatedDate;
            
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public User GetByNameAndHash(string uName,string hash)
    {
        uName = uName.Trim();        
        return _context.Users.SingleOrDefault(x => x.UserName == uName && x.Password == hash);
    }

    public User GetByUserName(string  uName)
    {

        return _context.Users.FirstOrDefault(x => x.UserName == uName);

    }

    public User GetByEmail(string email)
    {

        return _context.Users.FirstOrDefault(x => x.Email == email);

    }
}