using AuthAspRazorPages.Application;
using AuthAspRazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthAspRazorPages.Pages
{
    public class LoginModel : PageModel
    {

        private readonly IUserApplication _userApp;

        public LoginModel(IUserApplication userApp)
        {
            _userApp = userApp;
        }



        public void OnGet()
        {

        }
        [BindProperty]
        public Login login { get; set; }
        public IActionResult OnPostLogin()
        {
            var res = _userApp.Login(login);

            if (res == true)
                return RedirectToPage("/Index");


            return RedirectToPage("/Login", res);
        }
        public IActionResult OnGetLogout()
        {
            _userApp.Logout();
            return RedirectToPage("/Index");
        }
    }
}
