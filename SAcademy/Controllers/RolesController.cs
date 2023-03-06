﻿using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAcademy.Data;
using SAcademy.Data.Migrations;
using SAcademy.Models;
using SAcademy.ViewModel;
using System.Drawing;

namespace SAcademy.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            ViewBag.Roles = roles;
            var users = await _userManager.Users.ToListAsync();
            var userRolesViewModel = new List<ProfileViewModel>();
            foreach (User user in users)
            {
                var thisViewModel = new ProfileViewModel();
                thisViewModel.UserId = user.Id;
                thisViewModel.Email = user.Email;
                thisViewModel.FullName = user.FullName;
                thisViewModel.Roles = await GetUserRoles(user);
                userRolesViewModel.Add(thisViewModel);
            }
            return View(userRolesViewModel);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            IdentityRole role = new IdentityRole();
            role.Name = name;
            var isExist = await _roleManager.RoleExistsAsync(role.Name);
            if (isExist)
            {
                ViewBag.msg = "this role is already exist";
                ViewBag.name = name;
                return View();
            }
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }



        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            ViewBag.id = role.Id;
            ViewBag.name = role.Name;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Edit(string id, string name)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            role.Name = name;
            var isExist = await _roleManager.RoleExistsAsync(role.Name);
            if (isExist)
            {
                ViewBag.msg = "this role is already exist";
                ViewBag.name = name;
                return View();
            }
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }


        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            ViewBag.id = role.Id;
            ViewBag.name = role.Name;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, string name)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            role.Name = name;
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        private async Task<List<string>> GetUserRoles(User user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }



        public async Task<IActionResult> Manage(string userId)
        {
                var model = new List<ProfileViewModel>();
            var f = await _roleManager.Roles.ToListAsync();
            try
            {
                ViewBag.userId = userId;
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                    return View("NotFound");
                }
                ViewBag.UserName = user.UserName;
                foreach (var role in f)
                {
                    var userRolesViewModel = new ProfileViewModel
                    {
                        RoleId = role.Id,
                        RoleName = role.Name
                    };
                    var ro = await _userManager.IsInRoleAsync(user, role.Name);
                    if (ro)
                    {
                        userRolesViewModel.Selected = true;
                    }
                    else
                    {
                        userRolesViewModel.Selected = false;
                    }
                    model.Add(userRolesViewModel);
                }
            }
            catch(Exception ex)
            {
                
            }
            //return View();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Manage(List<ProfileViewModel> model, string userId, ProfileViewModel x)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View();
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }
            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var aspNetUser = await _context.Users
                .FirstOrDefaultAsync(a => a.Id == id);

            _context.Remove(aspNetUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        public IActionResult Excel()
        {

            var usersroles = _userManager.GetUsersInRoleAsync("User").Result;

            var idsWithroles = usersroles.Select(u => u.Id);

            var users = _context.Users.Where(x => x.Email != "a@a.a" && idsWithroles.Contains(x.Id)).ToList();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Users");
            var currentRow = 1;
            var id = 0;

            worksheet.Row(currentRow).Height = 25.0;
            worksheet.Row(currentRow).Style.Font.Bold = true;
            worksheet.Row(currentRow).Style.Fill.BackgroundColor = XLColor.LightGray;
            worksheet.Row(currentRow).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            worksheet.Cell(currentRow, 1).Value = "Id";

            worksheet.Cell(currentRow, 2).Value = "Username";
            worksheet.Cell(currentRow, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell(currentRow, 3).Value = "Email";
            worksheet.Cell(currentRow, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


            foreach (var user in users)
            {
                currentRow++;
                id++;

                worksheet.Row(currentRow).Height = 20.0;
                worksheet.Row(currentRow).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                worksheet.Cell(currentRow, 1).Value = id;
                worksheet.Cell(currentRow, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                
                worksheet.Cell(currentRow, 2).Value = user.FullName;
                worksheet.Cell(currentRow, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell(currentRow, 3).Value = user.Email;
                worksheet.Cell(currentRow, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


                worksheet.Columns().AdjustToContents();
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "UsersSimplonAcademy.xlsx");
        }
    }
}

