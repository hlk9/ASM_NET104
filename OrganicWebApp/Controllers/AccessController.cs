using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org_DAL.Models;
using Org_DAL.Repository;
using OrganicWebApp.Models;
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
                if (listRole.Count>0)
                {
                    foreach(var item in  listRole)
					{
						role += item.RoleId;
					}
                }
				_contextAccessor.HttpContext.Session.SetString("_Roles", role);
                _contextAccessor.HttpContext.Session.SetString("_Name", usr.FullName);
				_contextAccessor.HttpContext.Session.SetString("_UserName", usr.UserName);
				

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

				if(cur == null)
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

		public IActionResult Logout()
		{
			_contextAccessor.HttpContext.Session.Clear();
			return RedirectToAction("Index", "Home");
		}
	}
}
