using Org_DAL.Context;
using Org_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org_DAL.Repository
{
    public class CartRepository
    {
        OrganicDbContext _context;
        public CartRepository()
        {
            _context = new OrganicDbContext();
        }

        public ICollection<Cart> GetAll()
        {
            return _context.Carts.ToList();
        }

        public bool Add(Cart cart)
        {
            try
            {
                _context.Carts.Add(cart);
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
                var cart = _context.Carts.Find(id);
                _context.Carts.Remove(cart);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Cart GetbyUserId(Guid userId)
        {
            return _context.Carts.FirstOrDefault(x => x.UserId == userId);
        }
        
        

    }
}
