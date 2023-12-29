using Domain.Entities;
using MainWebProject.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MainWebProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        public AccountController(UserManager<User> userManager , SignInManager<User> signInManager, IConfiguration configuration)
        {
                _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        public IActionResult Login(string ReturnUrl = null)
        {
            var data = new LogInVM { ReturnUrl = ReturnUrl };
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LogInVM logInVM)
        {
            IdentityOptions _options = new IdentityOptions();
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(logInVM.UserName);
                if (user == null)
                {
                    return View(logInVM);
                }

                var result = await _signInManager.PasswordSignInAsync(logInVM.UserName, logInVM.Password, logInVM.RememberMe, false);
                if (result.Succeeded)
                {
                    var role = await _userManager.GetRolesAsync(user);
                    var claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id)),
                        new Claim(ClaimTypes.Name, user.FullName),
                        new Claim(ClaimTypes.Role, role.FirstOrDefault())
                        //new Claim("FavoriteDrink", "Tea")
                };   
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);



                //    var tokenDescriptor = new SecurityTokenDescriptor
                //    {
                //        Subject = new ClaimsIdentity(new Claim[]
                //{
                //        new Claim(_options.ClaimsIdentity.RoleClaimType,"Admin")
                //}),
                //        Expires = DateTime.UtcNow.AddDays(1),
                //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["ApplicationSettings:JWT_Secret"])), SecurityAlgorithms.HmacSha256Signature),

                //    };
                    //var tokenHandler = new JwtSecurityTokenHandler();
                    //var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    //var token = tokenHandler.WriteToken(securityToken);


                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                    {
                        IsPersistent = logInVM.RememberMe
                    });
                    //HttpContext.Session.SetString("JWTToken", token);
                    if (!string.IsNullOrEmpty(logInVM.ReturnUrl) && Url.IsLocalUrl(logInVM.ReturnUrl))
                    {
                        return Redirect(logInVM.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ViewBag.ErrorMessage = "Please enter a valid username or password";
            return View(logInVM);
        }
    }
}
