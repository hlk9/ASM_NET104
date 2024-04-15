using Org_DAL.Context;
using Org_DAL.Models;

namespace Org_DAL.Repository;

public class CategoryRepository
{
    OrganicDbContext _context;
    public CategoryRepository()
    {
        _context = new OrganicDbContext();
    }
    
    public ICollection<Category> GetAll()
    {
        return _context.Categories.ToList();
    }
    
    public bool Add(Category category)
    {
        try
        {
            _context.Categories.Add(category);
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
            var category = _context.Categories.Find(id);
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    
    public bool Update(Category category)
    {
        try
        {
           var categoryToUpdate = _context.Categories.Find(category.Id);
           categoryToUpdate.CategoryName = category.CategoryName;
           categoryToUpdate.Status = category.Status;
           categoryToUpdate.Description = category.Description;
           categoryToUpdate.Status = category.Status;
           categoryToUpdate.ImageUrl = category.ImageUrl;
           
            _context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    
    
}