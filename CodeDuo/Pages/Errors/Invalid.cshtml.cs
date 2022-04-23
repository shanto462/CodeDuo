using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeDuo.Pages.Errors
{
    public class InvalidModel : PageModel
    {
        public string ErrorType { get; set; }
        public void OnGet()
        {
            ErrorType = Request.RouteValues["ErrorType"]?.ToString();
        }
    }
}
