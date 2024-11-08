using Proje_web.Areas.Admin.Models.VMs;
using Proje_web.Areas.Member.Models.DTOs;
using Proje_Dal.Context;
using Proje_Dal.Repositories.Abstract;
using Proje_Dal.Repositories.Interfaces.Abstract;
using Proje_Dal.Repositories.Interfaces.Concrete;
using Proje_model.Models.Concrete;
using Proje_model.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Proje_web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminUserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserRepo _userRepo;
        private readonly ProjectContext _project;

        public AdminUserController(UserManager<AppUser> userManager, IAppUserRepo userRepo, ProjectContext project)
        {
            _userManager = userManager;
            _userRepo = userRepo;
            _project = project;
        }

       
        public IActionResult List()
        {
            // Tüm kullanıcıları al
            List<AppUser> users = _userManager.Users.ToList();

            return Json(users);
        }


        public IActionResult Update(string id)    // getirme işlemini yapyıro  ıd yakalayarak posta düşüyor 
        {
            AppUser appUser = _userManager.FindByIdAsync(id).Result;

            
            return Json(appUser);

        }


        [HttpPost]
        public async Task<IActionResult> Update([FromBody] AppUser app)
        {
            if (ModelState.IsValid)
            {
                //AppUser appUser = _userManager.FindByIdAsync(app.Id).Result;
                AppUser appUser = await _userManager.FindByIdAsync(app.Id);

                if (appUser != null)
                {
                    // Kullanıcı bilgilerini güncelleme
                    appUser.FirstName = app.FirstName;
                    appUser.LastName = app.LastName;
                    //appUser.Password = app.Password;
                    //appUser.PasswordHash = app.g.;
                    if (!string.IsNullOrEmpty(app.Password))
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(appUser);
                        var resetPasswordResult = await _userManager.ResetPasswordAsync(appUser, token, app.Password);
                        appUser.Password = app.Password;
                    }


                    // Kullanıcıyı güncelleme işlemi
                    //var result = _userManager.UpdateAsync(appUser).Result;
                    var updateResult = await _userManager.UpdateAsync(appUser);

                    if (updateResult.Succeeded)
                    {

                        return Json(new { success = true, redirectUrl = Url.Action("List") });
                    }
                    
                }
                else
                {

                    return Json(new { success = true, redirectUrl = Url.Action("List") });
                }

            }
            return Json(new { success = true });

        }

        public async Task< IActionResult> Delete(string id)
        {
            AppUser appUser = _userManager.FindByIdAsync(id).Result;

            await _userRepo.Delete(appUser);

            var users = await _userManager.Users.ToListAsync();
            return Json(new { success = true, data = users });

        }
        public async Task< IActionResult> Active(string id)
        {
            AppUser appUser = _userManager.FindByIdAsync(id).Result;

            await _userRepo.Active(appUser);

            var users = await _userManager.Users.ToListAsync();
            return Json(new { success = true, data = users });

        }

    }
}
