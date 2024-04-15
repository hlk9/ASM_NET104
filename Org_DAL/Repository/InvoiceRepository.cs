using Org_DAL.Context;
using Org_DAL.Models;

namespace Org_DAL.Repository;

public class InvoiceRepository
{
    OrganicDbContext _context;

    public InvoiceRepository()
    {
        _context = new OrganicDbContext();
    }

    public ICollection<Invoice> GetAll()
    {
        return _context.Invoices.ToList();
    }

    public bool Add(Invoice invoice)
    {
        try
        {
            _context.Invoices.Add(invoice);
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
            var invoice = _context.Invoices.Find(id);
            _context.Invoices.Remove(invoice);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    
    public bool Update(Invoice invoice)
    {
        try
        {
            var invoiceToUpdate = _context.Invoices.Find(invoice.Id);
           invoiceToUpdate.CouponId = invoice.CouponId;
           invoiceToUpdate.UserId = invoice.UserId;
           invoiceToUpdate.CreateAt = invoice.CreateAt;
           invoiceToUpdate.Status = invoice.Status;
           invoiceToUpdate.TotalAmount = invoice.TotalAmount;
           invoiceToUpdate.UpdateAt = invoice.UpdateAt;
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public List<Invoice> GetAllByUserId(Guid id)
    {
        List<Invoice> list = _context.Invoices.Where(x=>x.UserId==id).ToList();
        return list;

    }

}