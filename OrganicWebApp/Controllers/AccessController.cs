using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org_DAL.Models;
using Org_DAL.Repository;
using OrganicWebApp.Models;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace OrganicWebApp.Controllers
{
    public class AccessController : Controller
    {
        private readonly UserRepository _userRepos = new UserRepository();
        private readonly UserRoleRepository _usrRoleRepos = new UserRoleRepository();
        private readonly IHttpContextAccessor _contextAccessor;

        public AccessController(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
        }
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var usr = _userRepos.GetByNameAndHash(username, HashPassword(password));

            if (usr == null)
            {
                ViewBag.Error = "Sai tài khoản hoặc mật khẩu";
                return View();
            }

            else
            {

                var listRole = _usrRoleRepos.GetByUserId(usr.Id);
                string role = "";
                if (listRole.Count > 0)
                {
                    foreach (var item in listRole)
                    {
                        role += item.RoleId;
                    }
                }
                _contextAccessor.HttpContext.Session.SetString("_Roles", role);
                _contextAccessor.HttpContext.Session.SetString("_Name", usr.FullName);
                _contextAccessor.HttpContext.Session.SetString("_UserName", usr.UserName);
                _contextAccessor.HttpContext.Session.SetString("_UserId", usr.Id.ToString());
                string usrId = HttpContext.Session.GetString("_UserId");
                if (usrId != null)
                {
                    CartItemRepository _cartItemRepository = new CartItemRepository();
                    CartRepository _cartRepository = new CartRepository();

                    var cart = _cartRepository.GetbyUserId(Guid.Parse(usrId));
                  
                        _contextAccessor.HttpContext.Session.SetInt32("_CartId", cart.Id);
                        _contextAccessor.HttpContext.Session.SetInt32("TotalInCart", _cartItemRepository.GetAllByIdCard(cart.Id).Count);
                    _contextAccessor.HttpContext.Session.SetString("TotalInCartString", _cartItemRepository.GetAllByIdCard(cart.Id).Count.ToString());

                    HttpContext.Session.SetInt32("TotalInCart", _cartItemRepository.GetAllByIdCard(cart.Id).Count);

                }

                return RedirectToAction("Index", "Home");
            }



        }

        public IActionResult Profile()
        {

            if (_contextAccessor.HttpContext.Session.GetString("_UserName") == null)
            {
                return RedirectToAction("Login", "Access");
            }
            else
            {
                var cur = _userRepos.GetByUserName(_contextAccessor.HttpContext.Session.GetString("_UserName"));

                if (cur == null)
                {
                    return RedirectToAction("Login", "Access");
                }
                return View(cur);
            }
        }


        public string HashPassword(string password)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            md5.Clear();
            return sb.ToString().ToUpper();

        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string uName, string fullName, string email, string pass, string repass)
        {
            if (_userRepos.GetByUserName(uName) != null)
            {
                ViewBag.Error = "Tên tài khoản đã tồn tại";

            }

            if (_userRepos.GetByEmail(email) != null)
            {
                ViewBag.Error = "Email đã tồn tại";

            }
            else
            {
                if (HashPassword(pass) != HashPassword(repass))
                {
                    ViewBag.Error = "Mật khẩu không khớp";
                    return View();
                }
                Org_DAL.Models.User usr = new User();
                usr.UserName = uName;
                usr.Email = email;
                usr.Password = HashPassword(pass);
                usr.FullName = fullName;
                _userRepos.AddCustomer(usr);

                return RedirectToAction("Login");
            }

            return View();
        }


        public IActionResult Logout()
        {
            _contextAccessor.HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
