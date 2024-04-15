using Org_DAL.Context;
using Org_DAL.Models;

namespace Org_DAL.Repository;

public class UserAddressRepository
{
    OrganicDbContext _context;
    public UserAddressRepository()
    {
        _context = new OrganicDbContext();
    }
    
    public ICollection<UserAddress> GetAll()
    {
        return _context.UserAddresses.ToList();
    }
    
    public bool Add(UserAddress userAddress)
    {
        try
        {
            _context.UserAddresses.Add(userAddress);
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
            var userAddress = _context.UserAddresses.Find(id);
            _context.UserAddresses.Remove(userAddress);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    
    public bool Update(UserAddress userAddress)
    {
        try
        {
            var userAddressToUpdate = _context.UserAddresses.Find(userAddress.Id);
            userAddressToUpdate.Address = userAddress.Address;
            userAddressToUpdate.UserId = userAddress.UserId;
            userAddressToUpdate.AddressType = userAddress.AddressType;
            userAddressToUpdate.Status = userAddress.Status;
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}