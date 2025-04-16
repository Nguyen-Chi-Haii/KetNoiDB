using AutoMapper;
using KetNoiDB.Models.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KetNoiDB.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(IMapper mapper, UserManager<User> userManager
            , SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegistrationVM userModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userModel);
            }
            var user = _mapper.Map<User>(userModel);
            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View(userModel);
            }
            await _userManager.AddToRoleAsync(user, "User");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        // GET: Account/Login
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginVM userModel,string? ReturnUrl = null)
        {
           if(!ModelState.IsValid)
            {
                return View(userModel);
            }
            var user = await _userManager.FindByEmailAsync(userModel.Email);

            if (user != null &&
                await _userManager.CheckPasswordAsync(user, userModel.Password))

            {
                var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);

                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));

                await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme,

                    new ClaimsPrincipal(identity));

                return RedirectToLocal(ReturnUrl);

            }
            else
            {
                ModelState.AddModelError("", "Invalid UserName or Password.");
                return View();
            }
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if(Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

    }
}
