using Org_DAL.Context;
using Org_DAL.Models;

namespace Org_DAL.Repository;

public class CartItemRepository
{
    OrganicDbContext _context;
    public CartItemRepository()
    {
        _context = new OrganicDbContext();
    }
    
    public ICollection<CartItem> GetAll()
    {
        return _context.CartItems.ToList();
    }
    
    public bool Add(CartItem cartItem)
    {
        try
        {
            _context.CartItems.Add(cartItem);
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
            var cartItem = _context.CartItems.Find(id);
            _context.CartItems.Remove(cartItem);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    
    public bool Update(CartItem cartItem)
    {
        try
        {
           var cartItemToUpdate = _context.CartItems.Find(cartItem.Id);
           cartItemToUpdate.Quantity = cartItem.Quantity;
           cartItemToUpdate.Status = cartItem.Status;
           
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public List<CartItem> GetAllByIdCard(int id)
    {
        return _context.CartItems.Where(x => x.CartId == id).ToList();
    }

    public CartItem Contain_CID_And_PID(int cid,int pid)
    {
        var ci = _context.CartItems.FirstOrDefault(x => x.CartId == cid && x.ProductId == pid);
        if (ci!=null)
        {
            return ci;
        }    
        return null;
    }

    public CartItem GetById(int id)
    {
        return _context.CartItems.Find(id);
    }
    
    
    
    
    
}