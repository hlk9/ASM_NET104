using Microsoft.AspNetCore.Mvc;
using Org_DAL.Models;
using Org_DAL.Repository;

namespace OrganicWebApp.Controllers
{
    public class UserController:Controller
    {
        private readonly UserRepository _repos;

        public UserController()
        {
            _repos = new UserRepository();
        }

        public IActionResult Index()
        {

            ICollection<User> users = _repos.GetAll();

            return View(users);
        }
       

    }
}
