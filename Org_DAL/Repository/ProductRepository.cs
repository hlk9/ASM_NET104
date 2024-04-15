using Org_DAL.Context;
using Org_DAL.Models;

namespace Org_DAL.Repository;

public class ProductRepository
{
    OrganicDbContext _context;
    public ProductRepository()
    {
        _context = new OrganicDbContext();
    }
    public ICollection<Product> GetAll()
    {
        return _context.Products.ToList();
    }
    public bool Add(Product productDetail)
    {
        try
        {
            _context.Products.Add(productDetail);
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
            var productDetail = _context.Products.Find(id);
            _context.Products.Remove(productDetail);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    
    public bool Update(Product productDetail)
    {
        try
        {
           var productDetailToUpdate = _context.Products.Find(productDetail.Id);        
         productDetailToUpdate.ProductName = productDetail.ProductName;
            productDetailToUpdate.Priced = productDetail.Priced;
           productDetailToUpdate.ListedPriced = productDetail.Priced;
           productDetailToUpdate.Weight = productDetail.Weight;
           productDetailToUpdate.EXP = productDetail.EXP;
           productDetailToUpdate.MFG = productDetail.MFG;
           productDetailToUpdate.SaleId = productDetail.SaleId;
           productDetailToUpdate.TypeClosure = productDetail.TypeClosure;
           productDetailToUpdate.Quantity = productDetail.Quantity;
           productDetailToUpdate.Status = productDetail.Status;
           productDetailToUpdate.ImageUrl = productDetail.ImageUrl;
            productDetailToUpdate.CategoryId = productDetail.CategoryId;
            productDetailToUpdate.ProductDescription = productDetail.ProductDescription;
            
           
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public Product GetById(int id)
    {
        return _context.Products.Find(id);
    }
}