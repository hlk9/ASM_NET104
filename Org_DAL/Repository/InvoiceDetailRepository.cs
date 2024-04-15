using Org_DAL.Context;
using Org_DAL.Models;

namespace Org_DAL.Repository;

public class InvoiceDetailRepository
{
    OrganicDbContext _context;
    public InvoiceDetailRepository()
    {
        _context = new OrganicDbContext();
    }
    
    public ICollection<InvoiceDetail> GetAll()
    {
        return _context.InvoicesDetail.ToList();
    }
    
    public bool Add(InvoiceDetail invoiceDetail)
    {
        try
        {
            _context.InvoicesDetail.Add(invoiceDetail);
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
            var invoiceDetail = _context.InvoicesDetail.Find(id);
            _context.InvoicesDetail.Remove(invoiceDetail);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    
    public bool Update(InvoiceDetail invoiceDetail)
    {
        try
        {
           var invoiceDetailToUpdate = _context.InvoicesDetail.Find(invoiceDetail.Id);
          invoiceDetailToUpdate.Price = invoiceDetail.Price;
          invoiceDetailToUpdate.ProductId = invoiceDetail.ProductId;
            invoiceDetailToUpdate.Quantity = invoiceDetail.Quantity;
            invoiceDetailToUpdate.InvoiceId = invoiceDetail.InvoiceId;
            invoiceDetailToUpdate.Total = invoiceDetail.Total;
           
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}