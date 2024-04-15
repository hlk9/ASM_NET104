using Org_DAL.Context;
using Org_DAL.Models;

namespace Org_DAL.Repository;

public class CouponRepository
{
    OrganicDbContext _context;
    public CouponRepository()
    {
        _context = new OrganicDbContext();
    }
    
    public ICollection<Coupon> GetAll()
    {
        return _context.Coupons.ToList();
    }
    
    public bool Add(Coupon coupon)
    {
        try
        {
            _context.Coupons.Add(coupon);
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
            var coupon = _context.Coupons.Find(id);
            _context.Coupons.Remove(coupon);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    
    public bool Update(Coupon coupon)
    {
        try
        {
           var couponToUpdate = _context.Coupons.Find(coupon.Id);
           couponToUpdate.CouponCode = coupon.CouponCode;
           couponToUpdate.Discount = coupon.Discount;
           couponToUpdate.IsActived = coupon.IsActived;
           couponToUpdate.ExpriationDate = coupon.ExpriationDate;
           
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    
    
    

}