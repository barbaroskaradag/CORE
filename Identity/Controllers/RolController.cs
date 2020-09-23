using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Context;
using Identity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Authorize]
    public class RolController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _usermanager;
        public RolController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _usermanager = userManager;
        }

        public IActionResult Index()
        {
            return View(_roleManager.Roles.ToList());
        }

        public IActionResult AddRole()
        {
            return View(new RoleViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewModel model)
        {
            if(ModelState.IsValid)
            {
                AppRole role = new AppRole() { Name = model.Name };
                var result = await _roleManager.CreateAsync(role);

                return isCreateControl(model, result, "Index");
            }
            return View(model);
        }

        public IActionResult UpdateRole(int id)
        {
            var roleFind = _roleManager.Roles.FirstOrDefault(x => x.Id == id); //IQueryable sayesinde yazılan koşullar sorgu yerine geçmektedir.
            RoleViewModel role = new RoleViewModel()
            {
                Id = roleFind.Id,
                Name = roleFind.Name,
            };
            return View(role);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateRole(RoleViewModel model)
        {
            var roleFind = _roleManager.Roles.FirstOrDefault(x => x.Id == model.Id); //IQueryable sayesinde yazılan koşullar sorgu yerine geçmektedir.
            roleFind.Name = model.Name;

            var result = await _roleManager.UpdateAsync(roleFind);
            return isCreateControl(model, result, "Index");
        }


        public async Task<IActionResult> DeleteRole(int id)
        {
            var toBeDeleteRole = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            var result = await _roleManager.DeleteAsync(toBeDeleteRole);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            TempData["Errors"] = result.Errors;
            return RedirectToAction("Index");
        }


        public IActionResult UserList()
        {
            return View(_usermanager.Users.ToList());
        }


        public async Task<IActionResult> AssignRole(int id)
        {
            var user =  _usermanager.Users.FirstOrDefault(x => x.Id == id); //IQueryable sayesinde yazılan koşullar sorgu yerine geçmektedir.

            TempData["UserId"] = user.Id;

            var roles = _roleManager.Roles.ToList();
            var userRolezs = await _usermanager.GetRolesAsync(user);

            List<RoleAssignViewModel> models = new List<RoleAssignViewModel>();

            foreach (var item in roles)
            {
                RoleAssignViewModel model = new RoleAssignViewModel();
                model.RoleId = item.Id;
                model.Name = item.Name;
                model.Exists = userRolezs.Contains(item.Name);
                models.Add(model);
            }

            return View(models);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(List<RoleAssignViewModel> models)
        {
            var userId = (int)TempData["UserId"];

            var user = _usermanager.Users.FirstOrDefault(x => x.Id == userId);

            foreach (var item in models)
            {
                if(item.Exists)
                {
                    await _usermanager.AddToRoleAsync(user, item.Name);
                }
                else
                {
                    await _usermanager.RemoveFromRoleAsync(user, item.Name);
                }
            }

            return RedirectToAction("UserList");
        }


        #region private metots

        private IActionResult isCreateControl<T>(T model,IdentityResult identityResult, string redirectAction)
        {
            if(identityResult.Succeeded)
            {
                return RedirectToAction(redirectAction);
            }

            foreach (var item in identityResult.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return View(model);
        }

        #endregion
    }
}
