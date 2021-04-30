using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WithoutIdentity.Models;
using WithoutIdentity.Models.ManageViewModels;

namespace WithoutIdentity.Controllers
{
    public class ManageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ManageController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await GetUserAsync();

            UserValidation(user);

            var model = new IndexViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                IsEmailConfirmed = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber,
                StatusMessage = StatusMessage
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await GetUserAsync();
            UserValidation(user);

            if(user.Email != model.Email)
            {
                await UpdateEmailAsync(user,model.Email);
            }

            if(user.PhoneNumber != model.PhoneNumber)
            {
                await UpdatePhoneNumberAsync(user,model.PhoneNumber);
            }

            StatusMessage = "Seu perfil foi atualizado";

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);

            if(user == null)
                throw  new ApplicationException($"Não foi possivel carregar os dados do usuário  com id {_userManager.GetUserId(User)}");

            var model = new ChangePasswordViewModel {StatusMessage = StatusMessage};

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await GetUserAsync();
            
            UserValidation(user);

            var changePasswordResult = await _userManager.ChangePasswordAsync(user,model.OldPassword, model.NewPassword);

            if(!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty,error.Description);
                }
                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);

            StatusMessage = "Sua senha foi alterada com sucesso";

            return RedirectToAction(nameof(ChangePassword));

        }

        private async Task<ApplicationUser> GetUserAsync()
        {
            return await _userManager.GetUserAsync(User);
        }

        private void UserValidation(ApplicationUser user)
        {
            if(user == null)
                throw new ApplicationException($"Não foi possível carregar o usuário com ID '{_userManager.GetUserId(User)}'");
        }

        private async Task UpdateEmailAsync(ApplicationUser user, string email)
        {
            var setEmailResult = await _userManager.SetEmailAsync(user,email);
            
            if(!setEmailResult.Succeeded)
            {
                throw new ApplicationException($"Erro inseperado ao atribuir um email para o usuário com ID: {_userManager.GetUserId(User)}");
            }
        }

        private async Task UpdatePhoneNumberAsync(ApplicationUser user, string phoneNumber)
        {
            var setPhoneResult = await _userManager.SetPhoneNumberAsync(user,phoneNumber);

            if(!setPhoneResult.Succeeded)
            {
                throw new ApplicationException($"Erro inseperado ao atribuir um celular para o usuário com ID: {_userManager.GetUserId(User)}");
            }
        }

    }
}