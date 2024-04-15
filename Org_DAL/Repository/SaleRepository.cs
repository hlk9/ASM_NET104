using Org_DAL.Context;
using Org_DAL.Models;

namespace Org_DAL.Repository;

public class SaleRepository
{
    OrganicDbContext _context;
    public SaleRepository()
    {
        _context = new OrganicDbContext();
    }
    
    public ICollection<Sale> GetAll()
    {
        return _context.Sales.ToList();
    }
    
    public bool Add(Sale sale)
    {
        try
        {
            _context.Sales.Add(sale);
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
            var sale = _context.Sales.Find(id);
            _context.Sales.Remove(sale);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    
    public bool Update(Sale sale)
    {
        try
        {
            var saleToUpdate = _context.Sales.Find(sale.Id);
            saleToUpdate.Code = sale.Code;
            saleToUpdate.Value = sale.Value;
            saleToUpdate.ExpriationDate = sale.ExpriationDate;
            saleToUpdate.IsActived = sale.IsActived;

            
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    
}